using BLL;
using ENTITY;
using GUI.Pages;
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


namespace GUI.Windows
{
    /// <summary>
    /// Lógica de interacción para ShowDetails.xaml
    /// </summary>
    public partial class ShowDetails : Window
    {
        ServicioPedido serviciopedido = new ServicioPedido();
        public Pedido NPedido { get; set; }

        private Empleado Cajero;

        public float Cambio;

        public string Efectivo;

        public bool Confirmar = false;

        public bool print = false;

        private Cliente cliente1;

        private int Action = 0;

        public ShowDetails(List<DetallePedido> detalles, Cliente cliente, Empleado Mesero)
        {
            InitializeComponent();
            listviewProductos.ItemsSource = detalles;
            cboMetodos.ItemsSource = serviciopedido.GetMetodos();
            CreatePedido(detalles, cliente, Mesero);
            lblTotalPedido.Content = string.Format("{0:C0}", NPedido.Valor);
            rbnContado.IsChecked = true;
            cliente1 = cliente;
            ValidationAnimation();
        }

        public ShowDetails(VistaDeuda vista)
        {
            InitializeComponent();
            listviewProductos.ItemsSource = vista.Detalles;
            cboMetodos.ItemsSource = serviciopedido.GetMetodos();
            lblTotalPedido.Content = string.Format("{0:C0}", vista.Valor);
            modalidad.Visibility = Visibility.Hidden;
            NPedido = new Pedido();
            NPedido.Valor = vista.Valor;
            rbnCredito.IsChecked = false;
            ValidationAnimation();

        }

        public ShowDetails(Pedido pedido, Empleado cajero)
        {
            InitializeComponent();
            listviewProductos.ItemsSource = pedido.Detalles;
            lblTotalPedido.Content = string.Format("{0:C0}", pedido.Valor);
            modalidad.Visibility = Visibility.Hidden;
            cboMetodos.Visibility = Visibility.Hidden;
            Checkprint.Visibility = Visibility.Hidden;
            lblmetodos.Visibility = Visibility.Hidden;
            bdmetodos.Visibility = Visibility.Hidden;
            btnImprimirF.Visibility = Visibility.Visible;
            btnConfirmarpago.Content = "Aceptar";
            PathGeometry icono = (PathGeometry)System.Windows.Application.Current.Resources["Save"];
            btnConfirmarpago.Tag = icono;
            Grid.SetRow(btnConfirmarpago, 3);
            Grid.SetRow(btnImprimirF, 2);
            btnImprimirF.VerticalAlignment = VerticalAlignment.Bottom;
            Action = 1;
            this.Height = 500;
            Cajero = cajero;
            NPedido = pedido;

        }



        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void cboMetodos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MetodosPago metodo = (MetodosPago)cboMetodos.SelectedItem;
            if (metodo.Nombre == "Efectivo")
            {
                lblEectivo1.Visibility = Visibility.Visible;
                brdEfectivo1.Visibility = Visibility.Visible;
                lblCambio1.Visibility = Visibility.Visible;
                lblCambio2.Visibility = Visibility.Visible;
            }
            else
            {
                lblEectivo1.Visibility = Visibility.Hidden;
                brdEfectivo1.Visibility = Visibility.Hidden;
                lblCambio1.Visibility = Visibility.Hidden;
                lblCambio2.Visibility = Visibility.Hidden;

            }
        }

        private void CreatePedido(List<DetallePedido> detalles, Cliente cliente, Empleado mesero)
        {
            Pedido pedido = new Pedido();
            pedido.Detalles = detalles;
            pedido.CalculoValor();
            pedido.Mesero = mesero;
            pedido.Cliente = cliente;
            pedido.Estado = "Pagado";
            pedido.Fecha = DateTime.Now;
            pedido.ModalidadDePago = ModalidadDePago.Contado;
            NPedido = pedido;

            foreach (var item in detalles)
            {
                item.Pedido = pedido;
            }


        }

        private void cb_GotFocus(object sender, RoutedEventArgs e)
        {
            var comboBox = sender as ComboBox;
            if (comboBox != null)
            {
                comboBox.IsDropDownOpen = true;
            }
        }

        private void btnConfirmarpago_Click(object sender, RoutedEventArgs e)
        {

            if (Action==1)
            {
                Close();
            }
            else
            {
                if (btnConfirmarpago.Content.ToString() != "Aceptar")
                {
                    if (rbnCredito.IsChecked == true)
                    {
                        NPedido.Estado = "Pendiente";
                        NPedido.ModalidadDePago = ModalidadDePago.Credito;
                        MetodosPago m = new MetodosPago("Credito", "5");
                        NPedido.MetodoPago = m;
                        Confirmar = true;
                        Close();
                    }
                    else
                    {
                        if (ValidateCboMetodos())
                        {
                            NPedido.MetodoPago = (MetodosPago)cboMetodos.SelectedItem;
                            if (NPedido.MetodoPago.Nombre == "Efectivo")
                            {
                                if (ValidateTxtEfectivo())
                                {
                                    Efectivo = txtEfectivo.Text.ToString();
                                    Confirmar = true;
                                    Close();
                                }
                                else
                                {
                                    MiMessageBox messageBox = new MiMessageBox(NegativeMessage.N, "El campo 'Efectivo' es obligatorio"); messageBox.ShowDialog();
                                }

                            }
                            else
                            {
                                Cambio = 0;
                                Efectivo = "0";
                                Confirmar = true;
                                Close();
                            }
                        }
                        else
                        {
                            MiMessageBox messageBox = new MiMessageBox(NegativeMessage.N, "El campo 'Metodo de Pago' es obligatorio"); messageBox.ShowDialog();
                        }

                    }

                    if (Checkprint.IsChecked == true) { print = true; }
                }
            }


        }

        private void txtEfectivo_TextChanged(object sender, TextChangedEventArgs e)
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
            else
            {

                if (txtEfectivo.Text != "")
                {
                    Cambio = (float.Parse(txtEfectivo.Text) - NPedido.Valor);
                    lblCambio2.Content = string.Format("{0:C0}", float.Parse(txtEfectivo.Text) - NPedido.Valor);
                }


            }



        }

        private void rbnCredito_Checked(object sender, RoutedEventArgs e)
        {
            if (cliente1.Nombre != "Generico")
            {
                cboMetodos.Visibility = Visibility.Hidden;
                Checkprint.Visibility = Visibility.Hidden;
                lblmetodos.Visibility = Visibility.Hidden;
                bdmetodos.Visibility = Visibility.Hidden;
                lblEectivo1.Visibility = Visibility.Hidden;
                brdEfectivo1.Visibility = Visibility.Hidden;
                lblCambio1.Visibility = Visibility.Hidden;
                lblCambio2.Visibility = Visibility.Hidden;
                btnConfirmarpago.Content = "Confirmar";
            }
            else
            {
                rbnCredito.IsChecked = false;
                rbnContado.IsChecked = true;
                MiMessageBox messageBox = new MiMessageBox(NegativeMessage.N, "El Cliente Generico no puede hacer pedidos a credito \nSe requiere registrar el cliente"); messageBox.ShowDialog();
            }

        }

        private void rbnContado_Checked(object sender, RoutedEventArgs e)
        {
            cboMetodos.Visibility = Visibility.Visible;
            Checkprint.Visibility = Visibility.Visible;
            btnConfirmarpago.Content = "Confirmar Pago";
            lblmetodos.Visibility = Visibility.Visible;
            bdmetodos.Visibility = Visibility.Visible;
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

        private bool ValidateCboMetodos()
        {
            if (cboMetodos.SelectedItem == null)
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        private bool ValidateTxtEfectivo()
        {
            if (string.IsNullOrEmpty(txtEfectivo.Text))
            {

                return false;
            }
            else
            {

                return true;
            }
        }

        private void btnImprimirF_Click(object sender, RoutedEventArgs e)
        {
            ServicioFactura servicioFactura = new ServicioFactura();
            var dto = servicioFactura.MapFacturaDto(Cajero.Nombre, NPedido, 0, "0");
            ServicioFactura.CreateFactura(dto);
            ServicioFactura.PdfToImg();
            ServicioFactura.printImg();
        }
    }
}
