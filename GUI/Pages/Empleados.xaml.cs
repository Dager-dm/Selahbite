using BLL;
using ENTITY;
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
    /// Lógica de interacción para Empleados.xaml
    /// </summary>
    public partial class Empleados : Page
    {
        private ServicioEmpleado servicioempleado;
        public Empleados()
        {
            InitializeComponent();
            servicioempleado = new ServicioEmpleado();
            miListView.ItemsSource = servicioempleado.GetAllClientes();
            miListView.SetValue(ScrollViewer.HorizontalScrollBarVisibilityProperty, ScrollBarVisibility.Disabled);
        }

        private void NewEmployee(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            AddEmpleado addEmpleadoWindow = new AddEmpleado();
            addEmpleadoWindow.Owner = mainWindow;
            addEmpleadoWindow.EmpleadoGuardado += Refreshlistview;
            addEmpleadoWindow.ShowDialog();
          


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
            miListView.ItemsSource = servicioempleado.GetAllClientes();
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
