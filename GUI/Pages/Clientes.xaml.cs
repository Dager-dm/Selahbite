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
        /*ServicioCliente serviciocliente = new ServicioCliente()*/
        //ObservableCollection<Cliente> clientes = new ObservableCollection<Cliente>();
        private ServicioCliente serviciocliente;

        public Clientes()
        {
            InitializeComponent();
            serviciocliente = new ServicioCliente();
            this.DataContext = serviciocliente;
            miListView.ItemsSource=serviciocliente.lstClientes;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            AddCliente addClienteWindow = new AddCliente();
            addClienteWindow.Owner = mainWindow; 
            addClienteWindow.Closed += AddClienteWindow_Closed; 
            addClienteWindow.ShowDialog();

        }

        private void AddClienteWindow_Closed(object sender, EventArgs e)
        {
            MessageBox.Show("efectivamente se cerro la ventana");
            foreach (var item in serviciocliente.lstClientes)
            {
                MessageBox.Show(item.Nombre);
            }
            miListView.ItemsSource = serviciocliente.lstClientes;
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

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            // Acción para el Botón 1
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            // Acción para el Botón 2
        }
    }
}
