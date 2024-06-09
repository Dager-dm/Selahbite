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
        ServicioVistaDeuda servicioVistaDeuda =  new ServicioVistaDeuda();
       
        
        public Deudas()
        {
            InitializeComponent();
            miListView.ItemsSource = servicioVistaDeuda.GetCreditos();

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
            miListView.ItemsSource = servicioVistaDeuda.GetCreditos();
        }


        private void TxtBusqueda_TextChanged(object sender, TextChangedEventArgs e)
        {

            string filtro = txbBusqueda.Text.ToLower();
            List<VistaDeuda> creditos = servicioVistaDeuda.GetCreditos();
            List<VistaDeuda> deudasFiltrados = creditos.Where(c => c.NombreCliente.ToLower().Contains(filtro)).ToList();
            miListView.ItemsSource = deudasFiltrados;
        }

        private void btnPagar_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            VistaDeuda vista = btn.DataContext as VistaDeuda;
            if (vista.Estado=="Pendiente")
            {
                ShowPayWindow(vista);
            }
            else
            {
                MessageBox.Show("Este pedido ya se pagó");
            }
            

        }

        private void ShowPayWindow(VistaDeuda vista)
        {
            vista.Detalles = servicioVistaDeuda.LoadDetalles(vista.Id_pedido);
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            ShowDetails showDetails = new ShowDetails(vista);
            showDetails.Owner = mainWindow;
            showDetails.ShowDialog();
            if (showDetails.Confirmar)
            {
                servicioVistaDeuda.PagarPedido(vista.Id_pedido, (MetodosPago)showDetails.cboMetodos.SelectedItem, vista.Valor);
                if (showDetails.print)
                {
                    servicioVistaDeuda.PrintTrue(vista);
                }

            }
            Refreshlistview();
        }
    }
}
