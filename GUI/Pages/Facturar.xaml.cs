using BLL;
using ENTITY;
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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
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
        private List<Producto> filteredItems;
        private List<Producto> ProductosDePedido = new List<Producto>();


        public Facturar()
        {
            InitializeComponent();
            List<string> metodos = new List<string> {"Efectivo","Nequi","Bancolombia","Daviplata", "Credito" };
           /* List<Producto> ProductosDePedido = new List<Producto>()*/;
            cboEmpleados.ItemsSource = servicioEmpleado.GetMeseros();
            cboClientes.ItemsSource = servicioCliente.GetAllClientes();
            items.ItemsSource = servicioproducto.GetAllProducts();

        }









        private void SumarCantidad_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            Producto producto = btn.DataContext as Producto;
            var listViewItem = FindAncestor<ListViewItem>(btn);
            var textBoxCantidad = FindVisualChild<TextBox>(listViewItem, "txtCantidad");
            var textBlockTotal = FindVisualChild<TextBlock>(listViewItem, "valortoatl");
            int cantidad = int.Parse(textBoxCantidad.Text);
            cantidad++;
            textBoxCantidad.Text = cantidad.ToString();
            textBlockTotal.Text = string.Format("{0:C0}",(cantidad * producto.Valor).ToString());

        }

        private void RestarCantidad_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            Producto producto = btn.DataContext as Producto;
            var listViewItem = FindAncestor<ListViewItem>(btn);
            var textBoxCantidad = FindVisualChild<TextBox>(listViewItem, "txtCantidad");
            var textBlockTotal = FindVisualChild<TextBlock>(listViewItem, "valortoatl");
            int cantidad = int.Parse(textBoxCantidad.Text);
            if (cantidad == 1)
            {
                ProductosDePedido.Remove(producto);
                listviewProductos.ItemsSource = null;
                listviewProductos.ItemsSource = ProductosDePedido;
            }
            else
            {
                cantidad--;
                textBoxCantidad.Text = cantidad.ToString();
                textBlockTotal.Text = string.Format("{0:C0}", (cantidad * producto.Valor).ToString());
            }

        }

        private void QuitProduct(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            Producto producto = btn.DataContext as Producto;
            ProductosDePedido.Remove(producto);
            listviewProductos.ItemsSource = null;
            listviewProductos.ItemsSource = ProductosDePedido;
        }


        private T FindAncestor<T>(DependencyObject current) where T : DependencyObject
        {
            while (current != null && !(current is T))
            {
                current = VisualTreeHelper.GetParent(current);
            }
            return current as T;
        }

  
        private T FindVisualChild<T>(DependencyObject parent, string name) where T : FrameworkElement
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                if (child is T && (child as T).Name == name)
                {
                    return child as T;
                }
                else
                {
                    var foundChild = FindVisualChild<T>(child, name);
                    if (foundChild != null)
                        return foundChild;
                }
            }
            return null;
        }




        private void btnAddClient_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = System.Windows.Application.Current.MainWindow as MainWindow;
            AddCliente addClienteWindow = new AddCliente();
            addClienteWindow.Owner = mainWindow;
            addClienteWindow.ShowDialog();
            servicioCliente.AddClientes(addClienteWindow.clientepr);
            cboClientes.ItemsSource = servicioCliente.GetStringClientes();

        }

        private void MouseEnterbtnAddClient(object sender, MouseEventArgs e)
        {
            Popup.PlacementTarget = btnAddClient;
            Popup.Placement = PlacementMode.Bottom;
            Popup.IsOpen = true;
            Header.PopupText.Text = "Agregar Cliente";
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
            borderm.Background= shadow;
            btn.Tag = icono;

        }

        private void BorderMeal_MouseLeave(object sender, MouseEventArgs e)
        {
            Border borderm = sender as Border;
            SolidColorBrush Color = (SolidColorBrush)System.Windows.Application.Current.Resources["TertiaryBackgroundColor"];
            PathGeometry icono = (PathGeometry)System.Windows.Application.Current.Resources["meal"];
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
            ProductosDePedido.Add(producto);
            listviewProductos.ItemsSource = null;
            listviewProductos.ItemsSource = ProductosDePedido;
            
        }

      
    }


   
   

}


