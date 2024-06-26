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
    /// Lógica de interacción para Egresos.xaml
    /// </summary>
    public partial class Egresos : Page
    {
        ServicioEgresos servicioegresos = new ServicioEgresos();
        public Egresos()
        {
            InitializeComponent();
            miListView.ItemsSource = servicioegresos.GetEgresos();

        }

        private void AddEgreso(object sender, RoutedEventArgs e)
        {
            AddEgresos addEgresoWindow = new AddEgresos(servicioegresos.GetSaldo());
            addEgresoWindow.ShowDialog();
            if (addEgresoWindow.guardarPresionado)
            {
                servicioegresos.OpenCash();
                servicioegresos.Insertar(addEgresoWindow.egreso);
                Refreshlistview();
            }
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
            miListView.ItemsSource = servicioegresos.GetEgresos();
        }




        private void TxtBusqueda_TextChanged(object sender, TextChangedEventArgs e)
        {

            string filtro = txbBusqueda.Text.ToLower();
            List<Egreso> egresos = servicioegresos.GetEgresos();
            List<Egreso> egresosFiltrados = egresos.Where(c => c.Recibidor.ToLower().Contains(filtro)).ToList();
            miListView.ItemsSource = egresosFiltrados;
        }

        private void btnVuelto_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            Egreso egreso= btn.DataContext as Egreso;
            if (egreso.Vuelto=="N")
            {
                AddEgresos addEgresoWindow = new AddEgresos(egreso);
                addEgresoWindow.ShowDialog();
                if (addEgresoWindow.guardarPresionado)
                {
                    servicioegresos.OpenCash();
                    egreso.Valor =egreso.Valor - addEgresoWindow.vueltos;
                    servicioegresos.SetVueltos(egreso, addEgresoWindow.vueltos);
                    Refreshlistview();
                }
                Refreshlistview();
            }
            else
            {
                MiMessageBox messageBox = new MiMessageBox(WarningMessage.W, "Ya se registraron los vueltos de este egreso"); messageBox.ShowDialog();
            }

        }
    }
}
