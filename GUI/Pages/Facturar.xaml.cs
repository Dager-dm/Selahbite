using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;



namespace GUI.Pages
{
    /// <summary>
    /// Lógica de interacción para Facturar.xaml
    /// </summary>
    /// ESTE CODIGO NO HUELE BIEN PERO SE LIMPIARÁ EN UN FUTURO
    public partial class Facturar : Page
    {
        private List<string> clientes;
        private List<string> meseros;
        public ObservableCollection<Producto> Productos { get; set; }

        public Facturar()
        {
            InitializeComponent();
            clientes = new List<string> { "Cliente 1", "Cliente 2", "Cliente 3", "juan0", "julian", "carlos", "camilo" };
            meseros = new List<string> { "Mesero 1", "Mesero 2", "Mesero 3", "juanda", "juana", "carolina", "carla" };
            Productos = new ObservableCollection<Producto>();
            lstbox.ItemsSource = clientes;
            lstboxMeseros.ItemsSource = meseros;
            txtbox.TextChanged += Tb_TextChanged;
            txtboxMeseros.TextChanged += TbMeseros_TextChanged;
            txtbox.AddHandler(TextBox.MouseLeftButtonDownEvent, new MouseButtonEventHandler(TextBoxclick), true);
            txtboxMeseros.AddHandler(TextBox.MouseLeftButtonDownEvent, new MouseButtonEventHandler(txtboxMeserosclick), true);

            lstProductos.ItemsSource = Productos;

        }

       
        //LISTBOX COMO COMBOBOX!!!


        //CLIENTE
        private void Tb_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox != null)
            {
                var filteredList = clientes.Where(item => item.StartsWith(textBox.Text)).ToList();
                lstbox.ItemsSource = filteredList;
            }
        }

        private void lb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var listBox = sender as ListBox;
            if (listBox != null)
            {
                txtbox.Text = listBox.SelectedItem.ToString();
            }
            lstbox.Visibility = Visibility.Hidden;
            ControlTemplate template = txtbox.Template;
            Border border = (Border)template.FindName("borderbox", txtbox);
            border.CornerRadius = new CornerRadius(5);
        }


        private void TextBoxclick(object sender, RoutedEventArgs e)
        {
            txtbox.Text = "";
            lstbox.Visibility = Visibility.Visible;
            ControlTemplate template = txtbox.Template;
            Border border = (Border)template.FindName("borderbox", txtbox);
            border.CornerRadius = new CornerRadius(5,5,0,0);
        }


        //MESEROS

        private void txtboxMeserosclick(object sender, MouseButtonEventArgs e)
        {
            txtboxMeseros.Text = "";
            lstboxMeseros.Visibility = Visibility.Visible;
            ControlTemplate template = txtboxMeseros.Template;
            Border border = (Border)template.FindName("borderbox", txtboxMeseros);
            border.CornerRadius = new CornerRadius(5, 5, 0, 0);
        }

        private void lstboxMeseros_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var listBox = sender as ListBox;
            if (listBox != null)
            {
                txtboxMeseros.Text = listBox.SelectedItem.ToString();
            }
            lstboxMeseros.Visibility = Visibility.Hidden;
            ControlTemplate template = txtboxMeseros.Template;
            Border border = (Border)template.FindName("borderbox", txtboxMeseros);
            border.CornerRadius = new CornerRadius(5);

        }

        private void TbMeseros_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox != null)
            {
                var filteredList = meseros.Where(item => item.StartsWith(textBox.Text)).ToList();
                lstboxMeseros.ItemsSource = filteredList;
            }
        }












        //EVENTOS DEL LISTVIEW!!!!


        private void SumarCantidad_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null)
            {
                Producto producto = btn.DataContext as Producto;
                if (producto != null)
                {
                    producto.Cantidad++;
                }
            }
        }


        private void RestarCantidad_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null)
            {
                Producto producto = btn.DataContext as Producto;
                if (producto != null && producto.Cantidad > 0)
                {
                    producto.Cantidad--;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Producto producto = new Producto("Almuerzo", 12000, 3);
            Productos.Add(producto);
            lstProductos.ItemsSource = Productos;
        }

        private void DeleteProduct(object sender, RoutedEventArgs e)
        {

        }

















        //private void cb_GotFocus(object sender, RoutedEventArgs e)
        //{
        //    var comboBox = sender as ComboBox;
        //    if (comboBox != null)
        //    {
        //        comboBox.IsDropDownOpen = true;
        //    }
        //}


    }


    //ESTO EFECTIVAMENTE NO VA AQUÍ PERO ESTÁ PARA TESTEAR TEMPORALY
    public class Producto : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _nombre;
        private decimal _precio;
        private int _cantidad;

        public Producto(string nombre, decimal precio, int cantidad)
        {
            Nombre = nombre;
            Precio = precio;
            Cantidad = cantidad;
        }

        public string Nombre
        {
            get { return _nombre; }
            set
            {
                _nombre = value;
                OnPropertyChanged("Nombre");
            }
        }

        public decimal Precio
        {
            get { return _precio; }
            set
            {
                _precio = value;
                OnPropertyChanged("Precio");
            }
        }

        public int Cantidad
        {
            get { return _cantidad; }
            set
            {
                _cantidad = value;
                OnPropertyChanged("Cantidad");
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}


