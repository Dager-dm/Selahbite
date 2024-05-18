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
            miListView.ItemsSource = servicioempleado.GetAllEmpleados();
            miListView.SetValue(ScrollViewer.HorizontalScrollBarVisibilityProperty, ScrollBarVisibility.Disabled);
        }

        private void NewEmployee(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            AddEmpleado addEmpleadoWindow = new AddEmpleado();
            addEmpleadoWindow.Owner = mainWindow;
            addEmpleadoWindow.ShowDialog();
            servicioempleado.AddEmpleado(addEmpleadoWindow.EmpleadoPropiety);
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
            miListView.ItemsSource = servicioempleado.GetAllEmpleados();
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
           Button btnEditar = sender as Button;
           Empleado empleado = btnEditar.DataContext as Empleado;
           MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
           AddEmpleado addEmpleadoWindow = new AddEmpleado(empleado);
           addEmpleadoWindow.Owner = mainWindow;
           addEmpleadoWindow.ShowDialog();
           servicioempleado.EditEmpleado(addEmpleadoWindow.EmpleadoPropiety, addEmpleadoWindow.EmpleadoModified);
           Refreshlistview();

        }

        private void btnBorrar_Click(object sender, RoutedEventArgs e)
        {
            Button btnBorrar = sender as Button;
            Empleado empleado = btnBorrar.DataContext as Empleado;


            MiMessageBox messageBox = new MiMessageBox("¿Está seguro de borrar\n" + " el Empleado " + empleado.Nombre + "?");
            bool? resultado = messageBox.ShowDialog();

            if (resultado == true)
            {
                servicioempleado.DeleteEmpleado(empleado);
                Refreshlistview();
            }

        }

        private void TxtBusqueda_TextChanged(object sender, TextChangedEventArgs e)
        {

            string filtro = txbBusqueda.Text.ToLower();
            List<Empleado> empleados = servicioempleado.GetAllEmpleados();


            List<Empleado> empleadosFiltrados = empleados.Where(c => c.Nombre.ToLower().Contains(filtro)).ToList();


            miListView.ItemsSource = empleadosFiltrados;
        }
    }
}
