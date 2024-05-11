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



namespace GUI.Pages
{
    /// <summary>
    /// Lógica de interacción para Facturar.xaml
    /// </summary>
    
    public partial class Facturar : Page
    {


        private List<String> metodos;
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
            cboEmpleados.ItemsSource = GetStringliMeserosst();
            cboClientes.ItemsSource = GetStringlist();
            cboMetodos.ItemsSource = metodos;
            items.ItemsSource = servicioproducto.GetAllProducts();
            

        }

       







        private void SumarCantidad_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null)
            {
                Producto producto = btn.DataContext as Producto;
                if (producto != null)
                {
                    //producto.Cantidad++;
                }
            }
        }

        private void RestarCantidad_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null)
            {
                Producto producto = btn.DataContext as Producto;
                //if (producto != null && producto.Cantidad > 0)
                //{
                //    producto.Cantidad--;
                //}
            }
        }

        private void DeleteProduct(object sender, RoutedEventArgs e)
        {

        }


        private List<string> GetStringlist() { 
           List<string> StringList = new List<string>();
            foreach (var item in servicioCliente.GetAllClientes())
            {
                StringList.Add(item.Nombre);
            }
         
          return StringList;
        }

        private List<string> GetStringliMeserosst()
        {
            List<string> StringList = new List<string>();
            foreach (var item in servicioEmpleado.GetAllEmpleados())
            {
                if (item.Cargo == "Mesero")
                {
                    StringList.Add(item.Nombre);
                }
                
            }

            return StringList;
        }





        //Añadir Cliente
        private void btnAddClient_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            AddCliente addClienteWindow = new AddCliente();
            addClienteWindow.Owner = mainWindow;
            addClienteWindow.ShowDialog();
            servicioCliente.AddClientes(addClienteWindow.clientepr);
            cboClientes.ItemsSource = GetStringlist();

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





        //Logica de vista de items
        private void BorderMeal_MouseEnter(object sender, MouseEventArgs e)
        {
            Border borderm = sender as Border;
            SolidColorBrush Color = (SolidColorBrush)Application.Current.Resources["SecundaryBackgroundColor"];
            SolidColorBrush shadow = (SolidColorBrush)Application.Current.Resources["Shadow"];
            PathGeometry icono = (PathGeometry)Application.Current.Resources["mas"];
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
            SolidColorBrush Color = (SolidColorBrush)Application.Current.Resources["TertiaryBackgroundColor"];
            PathGeometry icono = (PathGeometry)Application.Current.Resources["meal"];
            SolidColorBrush color = (SolidColorBrush)Application.Current.Resources["SecundaryTextColor"];

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


