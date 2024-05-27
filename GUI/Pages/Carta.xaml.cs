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
            miListView.SetValue(ScrollViewer.HorizontalScrollBarVisibilityProperty, ScrollBarVisibility.Disabled);
        }

        private void NewProduct(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            AddProducto addProductoWindow = new AddProducto(servicioProducto.GetCategoriasProductos());
            addProductoWindow.Owner = mainWindow;
            addProductoWindow.ShowDialog();
            servicioProducto.AddProductos(addProductoWindow.ProductoPropiety);
            Refreshlistview();
        }

        private void ListView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var listView = sender as ListView;
            var gridView = listView.View as GridView;
            var width = listView.ActualWidth / gridView.Columns.Count;
            foreach (var column in gridView.Columns)
            {
                column.Width = width;
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
            if (btnEditar != null)
            {
                ListViewItem listViewItem = FindAncestor<ListViewItem>(btnEditar);
                if (listViewItem != null)
                {

                    Producto item = listViewItem.DataContext as ENTITY.Producto;
                    if (item != null)
                    {

                        MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
                        AddProducto addProductoWindow = new AddProducto(item, servicioProducto.GetCategoriasProductos());
                        addProductoWindow.Owner = mainWindow;
                        addProductoWindow.ShowDialog();
                        servicioProducto.EditProducto(addProductoWindow.ProductoPropiety, addProductoWindow.ProductoModified);
                        Refreshlistview();

                    }
                }
            }
        }

        private void btnBorrar_Click(object sender, RoutedEventArgs e)
        {
            Button btnBorrar = sender as Button;
            if (btnBorrar != null)
            {
                ListViewItem listViewItem = FindAncestor<ListViewItem>(btnBorrar);
                if (listViewItem != null)
                {

                    ENTITY.Producto item = listViewItem.DataContext as ENTITY.Producto;
                    if (item != null)
                    {


                        MiMessageBox messageBox = new MiMessageBox("¿Está seguro de borrar\n" + " el Plato " + item.Nombre + "?");
                        bool? resultado = messageBox.ShowDialog();

                        if (resultado == true)
                        {
                            servicioProducto.DeleteProducto(item);
                            Refreshlistview();
                        }
                    }
                }
            }
        }

        private T FindAncestor<T>(DependencyObject current) where T : DependencyObject
        {
            do
            {
                if (current is T)
                {
                    return (T)current;
                }
                current = VisualTreeHelper.GetParent(current);
            }
            while (current != null);
            return null;
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
