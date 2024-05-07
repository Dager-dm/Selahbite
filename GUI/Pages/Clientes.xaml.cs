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
            miListView.ItemsSource=serviciocliente.GetAllClientes();
            miListView.SetValue(ScrollViewer.HorizontalScrollBarVisibilityProperty, ScrollBarVisibility.Disabled);

        }

        private void NewClient(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            AddCliente addClienteWindow = new AddCliente();
            addClienteWindow.Owner = mainWindow;
            addClienteWindow.ClienteGuardado += Refreshlistview;
            addClienteWindow.ShowDialog();

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



        public void Refreshlistview()
        {
          miListView.ItemsSource=serviciocliente.GetAllClientes();


        }
    }
}
