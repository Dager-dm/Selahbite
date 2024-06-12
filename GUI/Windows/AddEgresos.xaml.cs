using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using BLL;
using ENTITY;
using GUI.Pages;

namespace GUI.Windows
{
    /// <summary>
    /// Lógica de interacción para AddEgresos.xaml
    /// </summary>
    public partial class AddEgresos : Window
    {
        public bool guardarPresionado = false;
        public Egreso egreso { get; set; }

        float saldocaja;
        public AddEgresos(float saldo)
        {
            InitializeComponent();
            Animation();
            saldocaja = saldo;
        }

        private void Animation()
        {
            DispatcherTimer timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(5) };
            timer.Tick += (sender, args) =>
            {
                timer.Stop();
                Storyboard storyboard = Header.FindResource("AutoFadeOutStoryboard") as Storyboard;
                if (storyboard != null)
                {
                    storyboard.Begin(Header);
                }
            };
            timer.Start();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }


        private void CreateEgreso()
        {
            egreso = new Egreso();
            egreso.Descripcion = txtboxDescripcion.Text;
            egreso.Fecha = DateTime.Now;
            egreso.Recibidor = txtboxRecibidor.Text;
            egreso.Valor = float.Parse(txtboxValor.Text);
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidarNull())
            {
                if (float.Parse(txtboxValor.Text) <= saldocaja)
                {
                    guardarPresionado = true;
                    CreateEgreso();
                    Close();
                }
                else
                {
                    MiMessageBox messageBox = new MiMessageBox(NegativeMessage.N, "Saldo en la caja insuficiente \nSaldo en Caja:" + saldocaja.ToString("C0")); messageBox.ShowDialog();
                }

            }
            else
            {
                MiMessageBox messageBox = new MiMessageBox(NegativeMessage.N, "No se pueden realizar egresos con campos vacíos"); messageBox.ShowDialog();
            }

        }

        private void Alphabetic_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back || e.Key == Key.Space)
            {
                return;
            }

            if (e.Key < Key.A || e.Key > Key.Z)
            {
                TextBox txt = sender as TextBox;
                Popup.PlacementTarget = txt;
                Popup.Placement = PlacementMode.Right;
                Popup.IsOpen = true;
                Header.PopupText.Text = "No se pueden digitar numeros";
                ValidationAnimation2();
                System.Media.SystemSounds.Beep.Play();
                e.Handled = true;
            }
        }

        private void Numeric_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back)
            {
                return;
            }

            if (e.Key < Key.D0 || e.Key > Key.D9)
            {
                if (e.Key < Key.NumPad0 || e.Key > Key.NumPad9)
                {
                    TextBox txt = sender as TextBox;
                    Popup.PlacementTarget = txt;
                    Popup.Placement = PlacementMode.Right;
                    Popup.IsOpen = true;
                    Header.PopupText.Text = "No se pueden digitar caracteres alfabeticos";
                    ValidationAnimation2();
                    System.Media.SystemSounds.Beep.Play();
                    e.Handled = true;
                }

            }
        }

        private void txtbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textbox = (TextBox)sender;
            if (textbox.Text.Length >= textbox.MaxLength)
            {
                TextBox txt = sender as TextBox;
                Popup.PlacementTarget = txt;
                Popup.Placement = PlacementMode.Right;
                Popup.IsOpen = true;
                Header.PopupText.Text = "Cantidad de caracteres maxima alcanzada";
                ValidationAnimation2();
                System.Media.SystemSounds.Beep.Play();
                e.Handled = true;
            }
        }

        private void ValidationAnimation2()
        {
            Storyboard fadeInStoryboard = Header.FindResource("FadeInStoryboard") as Storyboard;
            if (fadeInStoryboard != null)
            {
                fadeInStoryboard.Begin(Header);
            }

            // Inicia el temporizador para la animación de salida
            DispatcherTimer timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(5) };
            timer.Tick += (sender, args) =>
            {
                timer.Stop();
                Storyboard fadeOutStoryboard = Header.FindResource("FadeOutStoryboard") as Storyboard;
                if (fadeOutStoryboard != null)
                {
                    fadeOutStoryboard.Begin(Header);
                }
            };
            timer.Start();
        }

        private bool ValidarNull()
        {
            if (string.IsNullOrEmpty(txtboxValor.Text))
            {
                return true;
            }
            else if (string.IsNullOrEmpty(txtboxDescripcion.Text))
            {
                return true;
            }
            else if (string.IsNullOrEmpty(txtboxRecibidor.Text))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
