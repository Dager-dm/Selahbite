using BLL;
using ENTITY;
using GUI.Windows;
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

namespace GUI.Pages
{
    /// <summary>
    /// Lógica de interacción para Deudas.xaml
    /// </summary>
    public partial class Deudas : Page
    {
        //ServicioPedido servicioPedido = new ServicioPedido();
        public Deudas()
        {
            InitializeComponent();
            //miListView.ItemsSource = servicioPedido.GetCreditos();

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
            //miListView.ItemsSource = servicioPedido.GetCreditos();
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            //Button btn = sender as Button;
            //Pedido pedido = btn.DataContext as Pedido;
            //MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            //NewEgreso newEgreso= new NewEgreso(pedido);
            //newEgreso.Owner = mainWindow;
            //newEgreso.ShowDialog();
            //servicioempleado.EditEmpleado(addEmpleadoWindow.EmpleadoPropiety, addEmpleadoWindow.EmpleadoModified);
            //Refreshlistview();

             
        }





        private void TxtBusqueda_TextChanged(object sender, TextChangedEventArgs e)
        {

            //string filtro = txbBusqueda.Text.ToLower();
            //List<Pedido> creditos = servicioPedido.GetCreditos();
            //List<Pedido> deudasFiltrados = creditos.Where(c => c.Cliente.Nombre.ToLower().Contains(filtro)).ToList();
            //miListView.ItemsSource = deudasFiltrados;
        }

        private void btnAbonar_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
