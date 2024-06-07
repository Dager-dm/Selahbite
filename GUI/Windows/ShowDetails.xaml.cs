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

        public Pedido NPedido { get; set; }

        public string Cambio;

        public string Efectivo;

        public bool Confirmar= false;

        public bool print=false;
        public ShowDetails( List<DetallePedido> detalles, List<MetodosPago> metodos, Cliente cliente, Empleado Mesero)
        {
            InitializeComponent();
            listviewProductos.ItemsSource = detalles;
            cboMetodos.ItemsSource = metodos;
            CreatePedido(detalles, cliente, Mesero);
            lblTotalPedido.Content = string.Format("{0:C0}", NPedido.Valor);
            rbnContado.IsChecked = true;

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
            pedido.FormaDePago = FormaDePago.Contado;
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
  


            if (rbnCredito.IsChecked==true)
            {
                NPedido.Estado = "Pendiente";
                NPedido.FormaDePago = FormaDePago.Credito;
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

            if (Checkprint.IsChecked==true) { print = true; }
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
        }

        private void rbnContado_Checked(object sender, RoutedEventArgs e)
        {
            cboMetodos.Visibility = Visibility.Visible;
            Checkprint.Visibility= Visibility.Visible;
        }
    }
}
