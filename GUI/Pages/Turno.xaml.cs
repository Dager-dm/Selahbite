using BLL;
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
    /// Lógica de interacción para Turno.xaml
    /// </summary>
    public partial class Turno : Page
    {

        ServicioEmpleado servicioEmpleado = new ServicioEmpleado();
        public Turno()
        {
            InitializeComponent();
            cboCajeros.ItemsSource=servicioEmpleado.GetStringCajeros();
            
        }


        private void cb_GotFocus(object sender, RoutedEventArgs e)
        {
            var comboBox = sender as ComboBox;
            if (comboBox != null)
            {
                comboBox.IsDropDownOpen = true;
            }
        }

        private void TurnoButton_Click(object sender, RoutedEventArgs e)
        {
            PathGeometry iconostart = (PathGeometry)Application.Current.Resources["start"];
            PathGeometry iconofinish = (PathGeometry)Application.Current.Resources["finish"];
            if (TurnoButton.Content.ToString() == "Iniciar Turno")
            {
                inicioTurno();
                TurnoButton.Content = "Terminar Turno";
                TurnoButton.Tag = iconofinish;   
            }
            else
            {
                terminarTurno();
                TurnoButton.Content = "Iniciar Turno";
                TurnoButton.Tag = iconostart;  
            }
        }


        private void inicioTurno()
        {
            ListviewBorder.Visibility = Visibility.Visible;

        }

        private void terminarTurno()
        {

        }

    }
}
