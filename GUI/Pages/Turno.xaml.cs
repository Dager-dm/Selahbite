using BLL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;
using ENTITY;

namespace GUI.Pages
{
    /// <summary>
    /// Lógica de interacción para Turno.xaml
    /// </summary>
    public partial class Turno : Page
    {
       

        ServicioEmpleado servicioEmpleado = new ServicioEmpleado();
        ServicioTurno servicioTurno = new ServicioTurno();
        public ListCollectionView View { get; set; }

        public Turno()
        {
            InitializeComponent();
            List<string> lstingresos = new List<string> { "200.000", "150.000", "80.000", "70.000", "60.000", "500.000" };
            List<string> lstegresos = new List<string> { "200.000", "150.000"};
            cboCajeros.ItemsSource=servicioEmpleado.GetStringCajeros();
            lsbxegresos.ItemsSource = lstegresos;
            lsbxIngresos.ItemsSource = lstingresos;
            //lstturnos.ItemsSource = servicioTurno.GetTurnos();
             View = new ListCollectionView(servicioTurno.GetTurnos());
            lstturnos.ItemsSource = View;

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
                TakeInfo(null);
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
            InfoTurnoBorder.Visibility = Visibility.Visible;
            GridIniciar.Visibility = Visibility.Hidden;
            terminarBorder.Visibility = Visibility.Visible;
            terminarBorder2.Visibility = Visibility.Visible;


        }

        private void terminarTurno()
        {
            InfoTurnoBorder.Visibility = Visibility.Hidden;
            GridIniciar.Visibility = Visibility.Visible;
            terminarBorder.Visibility = Visibility.Hidden;
            terminarBorder2.Visibility= Visibility.Hidden;
            Expanderegresos.IsExpanded = false;
            ExpanderIngresos.IsExpanded = false;
        
                
        }

        private void TakeInfo(ENTITY.Turno turno)
        {
            if (turno == null)
            {
                lblCajero.Content = cboCajeros.SelectedItem.ToString();
                if (HorarioDia.IsChecked == true)
                {
                    lblHorario.Content = lblIniciarDia.Content.ToString();
                }
                else
                {
                    lblHorario.Content = lblIniciarNoche.Content.ToString();
                }
                lblSaldoBase.Content = txtSaldobase.Text.ToString();

            }else
            {
                lblCajero.Content = turno.empleado.Nombre.ToString();
                lblHorario.Content = turno.Horario.ToString();
                lblSaldoBase.Content = string.Format("{0:C0}", turno.SaldoInicial);
                lblSaldoPrevisto.Content = string.Format("{0:C0}", turno.SaldoSistema);
                lblDiferencia.Content = string.Format("{0:C0}", turno.Diferencia);
                txtSaldoReal.Text = string.Format("{0:C0}", turno.SaldoUsuario);
                Expanderegresos.Header = "Egreso Total: "+string.Format("{0:C0}",turno.Egresos);
                ExpanderIngresos.Header = "Ingreso Total: " + string.Format("{0:C0}", turno.Ingresos);
                txtSaldoReal.IsEnabled = false;
                txtObservacion.IsEnabled = false;
                if (turno.Observacion != null)
                {
                    txtObservacion.Text = turno.Observacion.ToString();
                }
                

            }
        }

        private void MouseEnterbtnDetails(object sender, MouseEventArgs e)
        {
            Button btn = sender as Button;
            Popup.PlacementTarget = btn;
            Popup.Placement = PlacementMode.Bottom;
            Popup.IsOpen = true;
            switch (btn.Name)
            {
                case "DetailsButton":
                    Header.PopupText.Text = "Ver Detalles";
                    break;
                case "PedidosButton":
                    Header.PopupText.Text = "Ver Pedidos";
                    break;
                case "EgresosButton":
                    Header.PopupText.Text = "Ver Egresos";
                    break;
            }
            
        }

        private void MoueseLeavebtnDetails(object sender, MouseEventArgs e)
        {
            Popup.Visibility = Visibility.Collapsed;
            Popup.IsOpen = false;
        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            var datePicker = sender as DatePicker;

            if (datePicker.SelectedDate.HasValue)
            {
                // Filtra la lista de objetos por la fecha seleccionada
                View.Filter = obj =>
                {
                    var myObj = obj as ENTITY.Turno;
                    return myObj.Fecha.Date == datePicker.SelectedDate.Value.Date;
                };
            }
            else
            {
                // Si no hay fecha seleccionada, muestra todos los objetos
                View.Filter = null;
            }
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

        private void DetailsButton_Click(object sender, RoutedEventArgs e)
        {

            Button details = sender as Button;
            ENTITY.Turno turno = details.DataContext as ENTITY.Turno;
            inicioTurno();
            TakeInfo(turno);
            TurnoButton.Visibility = Visibility.Hidden;
            btnCloseDetails.Visibility = Visibility.Visible;

        }

        private void CloseDetailsButtonClick(object sender, RoutedEventArgs e)
        {
            terminarTurno();
            TurnoButton.Visibility= Visibility.Visible;
            btnCloseDetails.Visibility= Visibility.Hidden;
            txtSaldoReal.IsEnabled = true;
            txtObservacion.IsEnabled = true;
        }
    }
}
