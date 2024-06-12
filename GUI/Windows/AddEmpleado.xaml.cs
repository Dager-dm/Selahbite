
using ENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace GUI.Pages
{
    /// <summary>
    /// Lógica de interacción para AddEmpleado.xaml
    /// </summary>
    public partial class AddEmpleado : Window
    {
        public bool guardarPresionado = false;
        public Empleado EmpleadoPropiety { get; set; }
        public Empleado EmpleadoModified { get; set; }
        private List<Empleado> lstEmpleados;

        private int accion = 0;



        //ServicioEmpleado servicioEmpleado = new ServicioEmpleado();
        //public event Action EmpleadoGuardado;
        public AddEmpleado(List<CargosEmpleados> cargos, List<Empleado> Empleados)
        {
            InitializeComponent();
            cboCargo.ItemsSource = cargos;
            ValidationAnimation();
            lstEmpleados = Empleados;
        }

        public AddEmpleado(Empleado OldEmpleado, List<CargosEmpleados> cargos, List<Empleado> Empleados)
        {
            InitializeComponent();
            cboCargo.ItemsSource = cargos;
            lblTitulo.Content = "Editar Empleado";
            txtboxNombre.Text = OldEmpleado.Nombre;
            txtboxId.Text = OldEmpleado.Cedula;
            txtboxTelefono.Text = OldEmpleado.Telefono;
            cboCargo.SelectedItem = OldEmpleado.Cargo;
            accion = 1;
            EmpleadoModified = new Empleado();
            EmpleadoPropiety = OldEmpleado;
            lstEmpleados = Empleados;
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

            if (accion == 0)
            {
                if (ValidarCedula())
                {
                    if (ValidarNull())
                    {
                        EmpleadoPropiety = new Empleado();
                        EmpleadoPropiety.Nombre = txtboxNombre.Text.ToString();
                        EmpleadoPropiety.Telefono = txtboxTelefono.Text.ToString();
                        EmpleadoPropiety.Cedula = txtboxId.Text.ToString();
                        EmpleadoPropiety.Cargo = (CargosEmpleados)cboCargo.SelectedItem;
                        guardarPresionado = true;
                    }
                    else
                    {
                        MiMessageBox messageBox = new MiMessageBox(NegativeMessage.N, "El campo Cargo no puede estar vacío"); messageBox.ShowDialog();
                    }

                }
                else
                {
                    MiMessageBox messageBox = new MiMessageBox(NegativeMessage.N, "Ya hay un empleado registrado con esta cedula"); messageBox.ShowDialog();
                }

            }
            else
            {
                if (ValidarNull())
                {
                    EmpleadoModified.Nombre = txtboxNombre.Text.ToString();
                    EmpleadoModified.Cedula = txtboxId.Text.ToString();
                    EmpleadoModified.Telefono = txtboxTelefono.Text.ToString();
                    EmpleadoModified.Cargo = (CargosEmpleados)cboCargo.SelectedItem;
                    guardarPresionado = true;
                }
                else
                {
                    MiMessageBox messageBox = new MiMessageBox(NegativeMessage.N, "El campo Cargo no puede estar vacío"); messageBox.ShowDialog();
                }

            }
            Close();


        }

        private void Alphabetic_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back || e.Key == Key.Space)
            {
                return;
            }

            if (e.Key < Key.A || e.Key > Key.Z)
            {
                TextBox txt = sender as TextBox;
                Popup.PlacementTarget = txt;
                Popup.Placement = PlacementMode.Right;
                Popup.IsOpen = true;
                Header.PopupText.Text = "No se pueden digitar numeros";
                ValidationAnimation2();
                System.Media.SystemSounds.Beep.Play();
                e.Handled = true;
            }
        }

        private void Numeric_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back)
            {
                return;
            }

            if (e.Key < Key.D0 || e.Key > Key.D9)
            {
                if (e.Key < Key.NumPad0 || e.Key > Key.NumPad9)
                {
                    TextBox txt = sender as TextBox;
                    Popup.PlacementTarget = txt;
                    Popup.Placement = PlacementMode.Right;
                    Popup.IsOpen = true;
                    Header.PopupText.Text = "No se pueden digitar caracteres alfabeticos";
                    ValidationAnimation2();
                    System.Media.SystemSounds.Beep.Play();
                    e.Handled = true;
                }

            }
        }

        private void txtbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textbox = (TextBox)sender;
            if (textbox.Text.Length >= textbox.MaxLength)
            {
                TextBox txt = sender as TextBox;
                Popup.PlacementTarget = txt;
                Popup.Placement = PlacementMode.Right;
                Popup.IsOpen = true;
                Header.PopupText.Text = "Cantidad de caracteres maxima alcanzada";
                ValidationAnimation2();
                System.Media.SystemSounds.Beep.Play();
                e.Handled = true;
            }
        }

        private void ValidationAnimation()
        {

            DispatcherTimer timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(5) };
            timer.Tick += (sender, args) =>
            {
                timer.Stop();
                Storyboard storyboard = Header.FindResource("AutoFadeOutStoryboard") as Storyboard;
                if (storyboard != null)
                {
                    storyboard.Begin(Header);
                }
            };
            timer.Start();

        }

        private void ValidationAnimation2()
        {
            Storyboard fadeInStoryboard = Header.FindResource("FadeInStoryboard") as Storyboard;
            if (fadeInStoryboard != null)
            {
                fadeInStoryboard.Begin(Header);
            }

            // Inicia el temporizador para la animación de salida
            DispatcherTimer timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(5) };
            timer.Tick += (sender, args) =>
            {
                timer.Stop();
                Storyboard fadeOutStoryboard = Header.FindResource("FadeOutStoryboard") as Storyboard;
                if (fadeOutStoryboard != null)
                {
                    fadeOutStoryboard.Begin(Header);
                }
            };
            timer.Start();
        }

        private bool ValidarCedula()
        {
            int bandera = 0;
            foreach (var item in lstEmpleados)
            {
                if (txtboxId.Text.ToString() == item.Cedula)
                {
                    bandera = 1;
                }
            }
            if (bandera == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool ValidarNull()
        {
            if (cboCargo.SelectedItem == null)
            {
                return false;
            }

            return true;
        }
    }
}
