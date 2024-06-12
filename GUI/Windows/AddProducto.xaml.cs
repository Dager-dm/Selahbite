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
    /// Lógica de interacción para AddProducto.xaml
    /// </summary>
    public partial class AddProducto : Window
    {

        public bool guardarPresionado = false;
        public Producto ProductoPropiety { get; set; }
        public Producto ProductoModified { get; set; }

        private List<Producto> LstProductos;

        private int accion = 0;
        public AddProducto(List<CategoriasProductos> categories, List<Producto> productos)
        {
            InitializeComponent();
            cboCategoria.ItemsSource = categories;
            ValidationAnimation();
            LstProductos = productos;
        }


        public AddProducto(Producto OldProducto, List<CategoriasProductos> categories, List<Producto> productos)
        {
            InitializeComponent();

            cboCategoria.ItemsSource = categories;
            lblTitulo.Content = "Editar Plato/Bebida";
            txtboxNombre.Text = OldProducto.Nombre;
            txtboxValor.Text = OldProducto.Valor.ToString();
            cboCategoria.SelectedItem = OldProducto.Categoria.Nombre;
            accion = 1;
            ProductoModified = new Producto();
            ProductoPropiety = OldProducto;
            LstProductos = productos;
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AddProductoButton_Click(object sender, RoutedEventArgs e)
        {

            guardarPresionado = true;
            if (accion == 0)
            {
                if (ValidarNombre())
                {
                    if (ValidarNull())
                    {
                        ProductoPropiety = new Producto();
                        ProductoPropiety.Nombre = txtboxNombre.Text.ToString();
                        ProductoPropiety.Valor = float.Parse(txtboxValor.Text);
                        ProductoPropiety.Categoria = (CategoriasProductos)cboCategoria.SelectedItem;
                        guardarPresionado = true;
                        Close();
                    }
                    else
                    {
                        MiMessageBox messageBox = new MiMessageBox(NegativeMessage.N, "El campo Categoria no puede estar vacío"); messageBox.ShowDialog();
                    }

                }
                else
                {
                    MiMessageBox messageBox = new MiMessageBox(NegativeMessage.N, "Ya hay un producto registrado con este nombre"); messageBox.ShowDialog();
                }

            }
            else
            {
                if (ValidarNull())
                {
                    ProductoModified.Nombre = txtboxNombre.Text.ToString();
                    ProductoModified.Valor = float.Parse(txtboxValor.Text);
                    ProductoModified.Categoria = (CategoriasProductos)cboCategoria.SelectedItem;
                    guardarPresionado = true;
                    Close();
                }
                else
                {
                    MiMessageBox messageBox = new MiMessageBox(NegativeMessage.N, "El campo Categoria no puede estar vacío"); messageBox.ShowDialog();
                }
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

        private bool ValidarNombre()
        {
            int bandera = 0;
            foreach (var item in LstProductos)
            {
                if (txtboxNombre.Text.ToString() == item.Nombre)
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

        private bool ValidarNull()
        {
            if (cboCategoria.SelectedItem == null)
            {
                return false;
            }

            return true;
        }
    }
}
