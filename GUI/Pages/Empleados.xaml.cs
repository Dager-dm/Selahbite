using BLL;
using ENTITY;
using System;
using System.Collections;
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

        }

        private void NewEmployee(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            AddEmpleado addEmpleadoWindow = new AddEmpleado(servicioempleado.GetCargos(), servicioempleado.GetAllEmpleados());
            addEmpleadoWindow.Owner = mainWindow;
            addEmpleadoWindow.ShowDialog();
            if (addEmpleadoWindow.guardarPresionado)
            {
                servicioempleado.AddEmpleado(addEmpleadoWindow.EmpleadoPropiety);
                Refreshlistview();
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
           AddEmpleado addEmpleadoWindow = new AddEmpleado(empleado, servicioempleado.GetCargos(), servicioempleado.GetAllEmpleados());
           addEmpleadoWindow.ShowDialog();
            if (addEmpleadoWindow.guardarPresionado)
            {
                servicioempleado.EditEmpleado(addEmpleadoWindow.EmpleadoPropiety, addEmpleadoWindow.EmpleadoModified);
                Refreshlistview();
            }
        }

        private void btnBorrar_Click(object sender, RoutedEventArgs e)
        {
            Button btnBorrar = sender as Button;
            Empleado empleado = btnBorrar.DataContext as Empleado;


            MiMessageBox messageBox = new MiMessageBox("¿Está seguro de borrar\n" + " el Empleado " + empleado.Nombre + "?"+ " Esta acción no se puede revertir");
            bool ? resultado = messageBox.ShowDialog();

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
