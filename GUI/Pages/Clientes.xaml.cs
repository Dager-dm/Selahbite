using ENTITY;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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



namespace GUI.Pages
{
    /// <summary>
    /// Lógica de interacción para Clientes.xaml
    /// </summary>
    public partial class Clientes : Page
    {

        private ServicioCliente serviciocliente;
        

        public Clientes()
        {
          
            InitializeComponent();
            serviciocliente = new ServicioCliente();
            miListView.ItemsSource = serviciocliente.GetAllClientes();
            miListView.SetValue(ScrollViewer.HorizontalScrollBarVisibilityProperty, ScrollBarVisibility.Disabled);

        }

        private void NewClient(object sender, RoutedEventArgs e)
        {
            
            // addClienteWindow.ClienteGuardado += Refreshlistview;
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            AddCliente addClienteWindow = new AddCliente();
            addClienteWindow.Owner = mainWindow;
            addClienteWindow.ShowDialog();
            serviciocliente.AddClientes(addClienteWindow.clientepr);
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
            miListView.ItemsSource = serviciocliente.GetAllClientes();
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            
            Button btnEditar = sender as Button;
            if (btnEditar != null)
            {
                ListViewItem listViewItem = FindAncestor<ListViewItem>(btnEditar);
                if (listViewItem != null)
                {
                   
                    Cliente item = listViewItem.DataContext as Cliente;
                    if (item != null)
                    {
                       
                        MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
                        AddCliente addClienteWindow = new AddCliente(item);
                        addClienteWindow.Owner = mainWindow;
                        addClienteWindow.ShowDialog();
                        serviciocliente.EditCliente(addClienteWindow.clientepr, addClienteWindow.clienteModified);
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
                    
                    Cliente item = listViewItem.DataContext as Cliente;
                    if (item != null)
                    {
                        
                       
                        MiMessageBox messageBox = new MiMessageBox("¿Está seguro de borrar\n"+" el cliente "+item.Nombre+"?");
                        bool? resultado = messageBox.ShowDialog();

                        if (resultado == true)
                        {
                            serviciocliente.DeleteCliente(item);
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
            List<Cliente>clientes = serviciocliente.GetAllClientes();


            List<Cliente> clientesFiltrados = clientes.Where(c => c.Nombre.ToLower().Contains(filtro)).ToList();

           
            miListView.ItemsSource = clientesFiltrados;
        }

    }
}
