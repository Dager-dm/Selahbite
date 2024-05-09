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
using System.Windows.Shapes;

namespace GUI.Pages
{
    /// <summary>
    /// Lógica de interacción para AddEmpleado.xaml
    /// </summary>
    public partial class AddEmpleado : Window
    {
        private List<string> cargos;

        ServicioEmpleado servicioEmpleado = new ServicioEmpleado();
        public event Action EmpleadoGuardado;
        public AddEmpleado()
        {
            InitializeComponent();
            cargos = new List<string> { "Mesero", "Cajero", "Cocinero", "Bodeguero", "Oficios Varios"};
            cboCargo.ItemsSource = cargos;
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
            Empleado empleado = new Empleado(txtboxNombre.Text.ToString(), txtboxId.Text.ToString(), txtboxTelefono.Text.ToString(),cboCargo.SelectedItem.ToString(), 0);
            var cont = servicioEmpleado.AddClientes(empleado);
            MessageBox.Show(cont.ToString());
            EmpleadoGuardado?.Invoke();
            Close();
        }
    }
}
