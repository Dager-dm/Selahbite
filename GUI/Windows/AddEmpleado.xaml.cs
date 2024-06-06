
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
using System.Windows.Shapes;

namespace GUI.Pages
{
    /// <summary>
    /// Lógica de interacción para AddEmpleado.xaml
    /// </summary>
    public partial class AddEmpleado : Window
    {
        public bool guardarPresionado = false;
        public Empleado EmpleadoPropiety {  get; set; }
        public Empleado EmpleadoModified { get; set; }

        private int accion = 0;



        //ServicioEmpleado servicioEmpleado = new ServicioEmpleado();
        //public event Action EmpleadoGuardado;
        public AddEmpleado(List<CargosEmpleados> cargos)
        {
            InitializeComponent();
            cboCargo.ItemsSource =cargos;
        }

        public AddEmpleado(Empleado OldEmpleado, List<CargosEmpleados> cargos)
        {
            InitializeComponent();
            cboCargo.ItemsSource= cargos;
            lblTitulo.Content = "Editar Empleado";
            txtboxNombre.Text = OldEmpleado.Nombre;
            txtboxId.Text = OldEmpleado.Cedula;
            txtboxTelefono.Text = OldEmpleado.Telefono;
            cboCargo.SelectedItem = OldEmpleado.Cargo;
            accion = 1;
            EmpleadoModified = new Empleado();
            EmpleadoPropiety = OldEmpleado;
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

     
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }


        private void AddEmpleadoButton_Click_1(object sender, RoutedEventArgs e)
        {
            guardarPresionado = true;
            if (accion == 0)
            {
                EmpleadoPropiety=new Empleado();
                EmpleadoPropiety.Nombre = txtboxNombre.Text.ToString();
                EmpleadoPropiety.Telefono = txtboxTelefono.Text.ToString();
                EmpleadoPropiety.Cedula = txtboxId.Text.ToString();
                EmpleadoPropiety.Cargo= (CargosEmpleados)cboCargo.SelectedItem;

            }
            else
            {

                EmpleadoModified.Nombre= txtboxNombre.Text.ToString();
                EmpleadoModified.Cedula= txtboxId.Text.ToString();
                EmpleadoModified.Telefono= txtboxTelefono.Text.ToString();
                EmpleadoModified.Cargo= (CargosEmpleados)cboCargo.SelectedItem;

            }

            Close();
        }
    }
}
