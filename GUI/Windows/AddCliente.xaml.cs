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

namespace GUI.Pages
{
    /// <summary>
    /// Lógica de interacción para AddCliente.xaml
    /// </summary>
    public partial class AddCliente : Window
    {
        ServicioCliente serviciocliente = new ServicioCliente();
        //public event Action ClienteGuardado;
        public bool guardarPresionado = false;
        public Cliente clientepr { get; set; }
        public Cliente clienteModified { get; set; }



        private int accion = 0;
        //private Clientes _page;

        //public AddCliente()
        //{
        //    InitializeComponent();
        //    ValidationAnimation();
        //    //_page = page;


        //}
        public AddCliente()
        {
            InitializeComponent();
            ValidationAnimation();
        }

        public AddCliente(Cliente oldCliente)
        {
            InitializeComponent();
            lblTitulo.Content = "Editar Cliente";
            txtboxNombre.Text = oldCliente.Nombre;
            txtboxId.Text = oldCliente.Cedula;
            txtboxTelefono.Text = oldCliente.Telefono;
            accion = 1;
            clienteModified = new Cliente();
            clientepr = oldCliente;


        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private List<Cliente> LoadClientes()
        {
            return serviciocliente.GetAllClientes();
        }

        private void AddButton(object sender, RoutedEventArgs e)
        {

            
            if (accion == 0)
            {
                if (ValidarCedula())
                {
                    clientepr = new Cliente();
                    clientepr.Nombre = txtboxNombre.Text.ToString();
                    clientepr.Cedula = txtboxId.Text.ToString();
                    clientepr.Telefono = txtboxTelefono.Text.ToString();
                    clientepr.Saldo = 0;
                    guardarPresionado = true;
                }
                else
                {
                    MiMessageBox messageBox = new MiMessageBox(NegativeMessage.N, "Ya hay un cliente registrado con esta cedula"); messageBox.ShowDialog();
                }

            }
            else
            {
                clienteModified.Nombre = txtboxNombre.Text.ToString();
                clienteModified.Cedula = txtboxId.Text.ToString();
                clienteModified.Telefono = txtboxTelefono.Text.ToString();
                guardarPresionado = true;

            }

            Close();

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

        private void ValidationAnimation()
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

        private bool ValidarCedula()
        {
            int bandera = 0;
            foreach (var item in LoadClientes())
            {
                if (txtboxId.Text.ToString() == item.Cedula)
                {
                    bandera = 1;
                }
            }
            if (bandera == 0)
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
