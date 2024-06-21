using BLL;
using ENTITY;
using GUI.Styles;
using GUI.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using static System.Net.Mime.MediaTypeNames;



namespace GUI.Pages
{
    /// <summary>
    /// Lógica de interacción para Facturar.xaml
    /// </summary>

    public partial class Facturar : Page
    {


        ServicioProducto servicioproducto = new ServicioProducto();
        ServicioCliente servicioCliente = new ServicioCliente();
        ServicioEmpleado servicioEmpleado = new ServicioEmpleado();
        ServicioPedido servicioPedido = new ServicioPedido();
        ServicioDetalles servicioDetalles = new ServicioDetalles();

        private List<Producto> filteredItems;
        private List<DetallePedido> Detalles = new List<DetallePedido>();


        public Facturar()
        {
            InitializeComponent();
            cboEmpleados.ItemsSource = servicioEmpleado.GetMeseros();
            cboClientes.ItemsSource = servicioCliente.GetAllClientes();
            items.ItemsSource = servicioproducto.GetAllProducts();


        }

        private void SumarCantidad_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            DetallePedido detalle = btn.DataContext as DetallePedido;
            detalle.Cantidad++;
            detalle.CalculoValor();
            RefreshDetallesList();


        }

        private void RestarCantidad_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            DetallePedido detalle = btn.DataContext as DetallePedido;
            if (detalle.Cantidad == 1)
            {
                Detalles.Remove(detalle);
                RefreshDetallesList();
            }
            else
            {
                detalle.Cantidad--;
                detalle.CalculoValor();
                RefreshDetallesList();
            }

        }

        private void QuitProduct(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            DetallePedido detalle = btn.DataContext as DetallePedido;
            Detalles.Remove(detalle);
            RefreshDetallesList();
        }

        private void btnAddClient_Click(object sender, RoutedEventArgs e)
        {
            AddCliente addClienteWindow = new AddCliente();
            addClienteWindow.ShowDialog();
            if (addClienteWindow.guardarPresionado)
            {
                servicioCliente.AddClientes(addClienteWindow.clientepr);
                cboClientes.ItemsSource = servicioCliente.GetAllClientes();
            }

        }

        private void MouseEnterbtnAddClient(object sender, MouseEventArgs e)
        {
            Button btn = sender as Button;
            Popup.PlacementTarget = btn;
            Popup.Placement = PlacementMode.Bottom;
            Popup.IsOpen = true;
            switch (btn.Name)
            {
                case "ShowPedidos":
                    Header.PopupText.Text = "Ver Pedidos";
                    break;
                case "btnAddClient":
                    Header.PopupText.Text = "Añadir Cliente";
                    break;
                case "PedidoDetailsButton":
                    Header.PopupText.Text = "Ver Detalles";
                    break;
            }

        }

        private void MoueseLeavebtnAddClient(object sender, MouseEventArgs e)
        {
            Popup.Visibility = Visibility.Collapsed;
            Popup.IsOpen = false;
        }





        //Logica de vista de Productos
        private void BorderMeal_MouseEnter(object sender, MouseEventArgs e)
        {
            Border borderm = sender as Border;
            SolidColorBrush Color = (SolidColorBrush)System.Windows.Application.Current.Resources["SecundaryBackgroundColor"];
            SolidColorBrush shadow = (SolidColorBrush)System.Windows.Application.Current.Resources["Shadow"];
            PathGeometry icono = (PathGeometry)System.Windows.Application.Current.Resources["mas"];
            SolidColorBrush miColor = new SolidColorBrush(Colors.Gray);
            var txtNombre = (TextBlock)borderm.FindName("txtNombre");
            var lblValor = (TextBlock)borderm.FindName("lblValor");
            var btn = (Button)borderm.FindName("btn");
            txtNombre.Foreground = miColor;
            lblValor.Foreground = miColor;
            borderm.Background = shadow;
            btn.Tag = icono;

        }

        private void BorderMeal_MouseLeave(object sender, MouseEventArgs e)
        {
            Border borderm = sender as Border;
            SolidColorBrush Color = (SolidColorBrush)System.Windows.Application.Current.Resources["TertiaryBackgroundColor"];
            PathGeometry icono = (PathGeometry)System.Windows.Application.Current.Resources["Meal2"];
            SolidColorBrush color = (SolidColorBrush)System.Windows.Application.Current.Resources["SecundaryTextColor"];

            var txtNombre = (TextBlock)borderm.FindName("txtNombre");
            var lblValor = (TextBlock)borderm.FindName("lblValor");
            var btn = (Button)borderm.FindName("btn");
            txtNombre.Foreground = color;
            lblValor.Foreground = color;
            borderm.Background = Color;
            btn.Tag = icono;

        }

        private void cb_GotFocus(object sender, RoutedEventArgs e)
        {
            var comboBox = sender as ComboBox;
            if (comboBox != null)
            {
                comboBox.IsDropDownOpen = true;
            }
        }

        private void TxtBusqueda_TextChanged(object sender, TextChangedEventArgs e)
        {

            string searchText = txbBusqueda.Text.ToLower();
            filteredItems = new List<Producto>(servicioproducto.GetAllProducts().Where(item => item.Nombre.ToLower().Contains(searchText)));
            items.ItemsSource = filteredItems;

        }

        private void BorderMealClick(object sender, RoutedEventArgs e)
        {

            var source = sender as FrameworkElement;
            var producto = source.DataContext as Producto;
            DetallePedido detalle = new DetallePedido();
            detalle.Producto = producto;
            detalle.Cantidad = 1;
            detalle.CalculoValor();
            var detalleFound = ValidarDetalle(detalle);
            if (detalleFound != null)
            {
                detalleFound.Cantidad++;
                detalleFound.CalculoValor();
                RefreshDetallesList();
            }
            else
            {
                Detalles.Add(detalle);
                RefreshDetallesList();
            }

        }

        private void RefreshDetallesList()
        {
            listviewProductos.ItemsSource = null;
            listviewProductos.ItemsSource = Detalles;
        }

        private DetallePedido ValidarDetalle(DetallePedido detalle)
        {
            foreach (var item in Detalles)
            {
                if (detalle.Producto == item.Producto)
                {
                    return item;
                }
            }

            return null;
        }

        private void ConfirmarPedido_Click(object sender, RoutedEventArgs e)
        {
            if (validateCLiente())
            {
                if (validateMesero())
                {
                    if (Detalles.Count > 0)
                    {
                        ShowDetails ShowDetailsWindow = new ShowDetails(Detalles, (Cliente)cboClientes.SelectedItem, (Empleado)cboEmpleados.SelectedItem);
                        ShowDetailsWindow.ShowDialog();
                        if (ShowDetailsWindow.Confirmar)
                        {
                            try
                            {
                                var pedido = servicioPedido.AddPedido(ShowDetailsWindow.NPedido);
                                if (ShowDetailsWindow.print)
                                {
                                    servicioPedido.GenerateFactura(pedido, ShowDetailsWindow.Cambio, ShowDetailsWindow.Efectivo);

                                }

                                foreach (var item in Detalles)
                                {
                                    item.Pedido.Id = pedido.Id;
                                    item.CalcularValorUnitario();
                                    servicioDetalles.AddDetalle(item);
                                }
                            }
                            catch (Exception ex)
                            {
                                MiMessageBox messageBox = new MiMessageBox(ExcepcionMessage.E, "Ha ocurrido un error\n" + ex.Message); messageBox.ShowDialog();
                            }
                        }
                    }
                    else
                    {
                        MiMessageBox messageBox = new MiMessageBox(WarningMessage.W, "Tiene que agregar productos al pedido para confirmarlo"); messageBox.ShowDialog();
                    }

                }
                else
                {
                    MiMessageBox messageBox = new MiMessageBox(WarningMessage.W, "El campo mesero es obligatorio"); messageBox.ShowDialog();
                }

            }
            else
            {
                MiMessageBox messageBox = new MiMessageBox(WarningMessage.W, "El campo cliente es obligatorio"); messageBox.ShowDialog();
            }

        }

        private void GoBack(object sender, RoutedEventArgs e)
        {

            BorderPedidos.Visibility = Visibility.Hidden;
            BorderProductos.Visibility = Visibility.Visible;
            ShowPedidos.Visibility = Visibility.Visible;
            GoBackButton.Visibility = Visibility.Hidden;
            stackBuscar.Visibility = Visibility.Visible;
            lblPedidos.Visibility = Visibility.Hidden;

        }

        private void ShowPedidos_Click(object sender, RoutedEventArgs e)
        {
            BorderProductos.Visibility = Visibility.Hidden;
            BorderPedidos.Visibility = Visibility.Visible;
            ShowPedidos.Visibility = Visibility.Hidden;
            GoBackButton.Visibility = Visibility.Visible;
            stackBuscar.Visibility = Visibility.Hidden;
            lblPedidos.Visibility = Visibility.Visible;
            if (ServicioTurno.turnoAbierto != null)
            {
                try
                {
                    PedidosListView.ItemsSource = servicioPedido.GetPedidos(ServicioTurno.turnoAbierto);
                }
                catch (Exception ex)
                {
                    MiMessageBox messageBox = new MiMessageBox(ExcepcionMessage.E, "Ha ocurrido un error\n" + ex.Message); messageBox.ShowDialog();
                }

            }

        }

        private void PedidoDetailsButton_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            Pedido pedido = btn.DataContext as Pedido;
            ServicioTurno servicioTurno = new ServicioTurno();
            ShowDetails detailswindows = new ShowDetails(pedido, servicioTurno.GetOpenTurno().Cajero);
            detailswindows.ShowDialog();
        }

        private bool validateCLiente()
        {
            if (cboClientes.SelectedItem == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool validateMesero()
        {
            if (cboEmpleados.SelectedItem == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }



    }

}


