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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BLL;
using ENTITY;

namespace GUI.Pages
{
    /// <summary>
    /// Lógica de interacción para Carta.xaml
    /// </summary>
    public partial class Carta : Page
    {
        private ServicioProducto servicioProducto;
        public Carta()
        {
            InitializeComponent();
            servicioProducto = new ServicioProducto();
            miListView.ItemsSource= servicioProducto.GetAllProducts();
            
        }

        private void NewProduct(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            AddProducto addProductoWindow = new AddProducto(servicioProducto.GetCategoriasProductos(), servicioProducto.GetAllProducts());
            addProductoWindow.Owner = mainWindow;
            addProductoWindow.ShowDialog();
            if (addProductoWindow.guardarPresionado)
            {
                servicioProducto.AddProductos(addProductoWindow.ProductoPropiety);
                Refreshlistview();
            }
        }


        public void Refreshlistview()
        {
            miListView.ItemsSource = null;
            miListView.ItemsSource = servicioProducto.GetAllProducts();
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            Button btnEditar = sender as Button;
            Producto producto = btnEditar.DataContext as Producto;
            AddProducto addProductoWindow = new AddProducto(producto, servicioProducto.GetCategoriasProductos(), servicioProducto.GetAllProducts());
            addProductoWindow.ShowDialog();
            if (addProductoWindow.guardarPresionado)
            {
                servicioProducto.EditProducto(addProductoWindow.ProductoPropiety, addProductoWindow.ProductoModified);
                Refreshlistview();
            }
        }

        private void btnBorrar_Click(object sender, RoutedEventArgs e)
        {
            Button btnBorrar = sender as Button;
            Producto producto = btnBorrar.DataContext as Producto;
            MiMessageBox messageBox = new MiMessageBox("¿Está seguro de borrar\n" + " el Plato " + producto.Nombre + "?"+"  Esta acción no se puede revertir");
            bool? resultado = messageBox.ShowDialog();
            if (resultado == true)
            {
                servicioProducto.DeleteProducto(producto);
                Refreshlistview();
            }
        }



        private void TxtBusqueda_TextChanged(object sender, TextChangedEventArgs e)
        {

            string filtro = txbBusqueda.Text.ToLower();
            List<Producto> Productos = servicioProducto.GetAllProducts();


            List<Producto> ProductosFiltrados = Productos.Where(c => c.Nombre.ToLower().Contains(filtro)).ToList();


            miListView.ItemsSource = ProductosFiltrados;
        }
    }
}
