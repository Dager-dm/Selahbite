using BLL;
using ENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace GUI.Windows
{
    /// <summary>
    /// Lógica de interacción para ShowDetails.xaml
    /// </summary>
    public partial class ShowDetails : Window
    {
       ServicioPedido serviciopedido = new ServicioPedido();
        public Pedido NPedido { get; set; }

        public string Cambio;

        public string Efectivo;

        public bool Confirmar= false;

        public bool print=false;
        public ShowDetails( List<DetallePedido> detalles, Cliente cliente, Empleado Mesero)
        {
            InitializeComponent();
            listviewProductos.ItemsSource = detalles;
            cboMetodos.ItemsSource = serviciopedido.GetMetodos();
            CreatePedido(detalles, cliente, Mesero);
            lblTotalPedido.Content = string.Format("{0:C0}", NPedido.Valor);
            rbnContado.IsChecked = true;

        }

        public ShowDetails(VistaDeuda vista)
        {
            InitializeComponent();
            listviewProductos.ItemsSource = vista.Detalles;
            cboMetodos.ItemsSource = serviciopedido.GetMetodos();
            lblTotalPedido.Content = string.Format("{0:C0}", vista.Valor);
            modalidad.Visibility = Visibility.Hidden;
            NPedido= new Pedido();
            rbnCredito.IsChecked = false;

        }

        public ShowDetails(Pedido pedido) 
        {
            InitializeComponent();
            listviewProductos.ItemsSource = pedido.Detalles;
            lblTotalPedido.Content = string.Format("{0:C0}", pedido.Valor);
            modalidad.Visibility = Visibility.Hidden;
            cboMetodos.Visibility = Visibility.Hidden;
            Checkprint.Visibility = Visibility.Hidden;
            lblmetodos.Visibility = Visibility.Hidden;
            bdmetodos.Visibility = Visibility.Hidden;
            btnConfirmarpago.Content = "Aceptar";
            PathGeometry icono = (PathGeometry)System.Windows.Application.Current.Resources["Save"];
            btnConfirmarpago.Tag= icono;
            Grid.SetRow(btnConfirmarpago, 2);
            this.Height = 500;

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
            MetodosPago metodo=(MetodosPago)cboMetodos.SelectedItem;
            if (metodo.Nombre=="Efectivo")
            {
              lblEectivo1.Visibility = Visibility.Visible;
              brdEfectivo1.Visibility=Visibility.Visible;
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
            pedido.Mesero= mesero;
            pedido.Cliente= cliente;
            pedido.Estado="Pagado";
            pedido.Fecha=DateTime.Now;
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
            Confirmar = true;

            if (btnConfirmarpago.Content.ToString() != "Aceptar")
            {
                if (rbnCredito.IsChecked == true)
                {
                    NPedido.Estado = "Pendiente";
                    NPedido.ModalidadDePago = ModalidadDePago.Credito;
                    MetodosPago m = new MetodosPago("Credito", "5");
                    NPedido.MetodoPago = m;
                }
                else
                {
                    NPedido.MetodoPago = (MetodosPago)cboMetodos.SelectedItem;
                    if (NPedido.MetodoPago.Nombre == "Efectivo")
                    {
                        Cambio = lblCambio2.Content.ToString();
                        Efectivo = txt.Text.ToString();
                    }
                    else
                    {
                        Cambio = "0";
                        Efectivo = "0";
                    }

                }

                if (Checkprint.IsChecked == true) { print = true; }
            }


            Close();
        }

        private void txtEfectivo_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txt.Text!="")
            {
                lblCambio2.Content = string.Format("{0:C0}", float.Parse(txt.Text) - NPedido.Valor);
            }

            
        }

        private void rbnCredito_Checked(object sender, RoutedEventArgs e)
        {
            cboMetodos.Visibility= Visibility.Hidden;
            Checkprint.Visibility= Visibility.Hidden;
            lblmetodos.Visibility=Visibility.Hidden;
            bdmetodos.Visibility= Visibility.Hidden;
            lblEectivo1.Visibility = Visibility.Hidden;
            brdEfectivo1.Visibility = Visibility.Hidden;
            lblCambio1.Visibility = Visibility.Hidden;
            lblCambio2.Visibility = Visibility.Hidden;
            btnConfirmarpago.Content = "Confirmar";
        }

        private void rbnContado_Checked(object sender, RoutedEventArgs e)
        {
            cboMetodos.Visibility = Visibility.Visible;
            Checkprint.Visibility= Visibility.Visible;
            btnConfirmarpago.Content = "Confirmar Pago";
            lblmetodos.Visibility = Visibility.Hidden;
            bdmetodos.Visibility = Visibility.Hidden;
        }
    }
}
