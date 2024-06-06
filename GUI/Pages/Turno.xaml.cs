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
using System.Globalization;

namespace GUI.Pages
{
    /// <summary>
    /// Lógica de interacción para Turno.xaml
    /// </summary>
    public partial class Turno : Page
    {
       
        ServicioCaja servicioCaja = new ServicioCaja();
        ServicioEmpleado servicioEmpleado = new ServicioEmpleado();
        ServicioTurno servicioTurno = new ServicioTurno();
        ServicioPedido servicopedido = new ServicioPedido();
        private ListCollectionView View { get; set; }

        public Turno()
        {
            InitializeComponent();
            cboCajeros.ItemsSource=servicioEmpleado.GetCajeros();
            IsTurnoOpen();
            View = new ListCollectionView(servicioTurno.GetTurnos());
            lstturnos.ItemsSource = View;



        }

        private void IsTurnoOpen()
        {
            if (servicioTurno.GetOpenTurno()!=null)
            {
                MessageBox.Show("hay un turno abierto compae");
                PathGeometry iconofinish = (PathGeometry)Application.Current.Resources["finish"];
                HideThingsinicioTurno();
                ShowSelectedTurno(servicioTurno.GetOpenTurno());
                TurnoButton.Content = "Terminar Turno";
                TurnoButton.Tag = iconofinish;
            }
        }






        //evento para mostrar los cajeros
        private void cb_GotFocus(object sender, RoutedEventArgs e)
        {
            var comboBox = sender as ComboBox;
            if (comboBox != null)
            {
                comboBox.IsDropDownOpen = true;
            }
        }







        //evento y funciones Turno
        private void TurnoButton_Click(object sender, RoutedEventArgs e)
        {
            PathGeometry iconostart = (PathGeometry)Application.Current.Resources["start"];
            PathGeometry iconofinish = (PathGeometry)Application.Current.Resources["finish"];
            if (TurnoButton.Content.ToString() == "Iniciar Turno")
            {
                HideThingsinicioTurno();
                TakeInfoTurnoInicio();
                TurnoButton.Content = "Terminar Turno";
                TurnoButton.Tag = iconofinish;   
            }
            else
            {
                TakeInfoTurnoTerminar();
                ShowThingsterminarTurno();
                TurnoButton.Content = "Iniciar Turno";
                TurnoButton.Tag = iconostart;
                RefreshListTurnos();
            }
        }

        private void HideThingsinicioTurno()
        {
            InfoTurnoBorder.Visibility = Visibility.Visible;
            GridIniciar.Visibility = Visibility.Hidden;
            terminarBorder.Visibility = Visibility.Visible;
            terminarBorder2.Visibility = Visibility.Visible;


        }

        private void ShowThingsterminarTurno()
        {
            InfoTurnoBorder.Visibility = Visibility.Hidden;
            GridIniciar.Visibility = Visibility.Visible;
            terminarBorder.Visibility = Visibility.Hidden;
            terminarBorder2.Visibility= Visibility.Hidden;
            Expanderegresos.IsExpanded = false;
            ExpanderIngresos.IsExpanded = false;
            lblCajero.Content = null;
            lblHorario.Content = null;
            lblHorario.Content = null;
            lblSaldoBase.Content = null;


        }

        private void TakeInfoTurnoInicio()
        {
            
            ENTITY.Turno turnonuevo = new ENTITY.Turno();
            turnonuevo.Fecha=DateTime.Now;
            turnonuevo.Cajero = (Empleado)cboCajeros.SelectedItem;
            turnonuevo.Ingreso = 0; turnonuevo.Egreso=0; turnonuevo.Diferencia=0;
            if (HorarioDia.IsChecked == true)
            {
                turnonuevo.Horario = "Dia";
            }else
            {
                turnonuevo.Horario = "Noche";
            }
            
            turnonuevo.SaldoInicial= float.Parse(txtSaldobase.Text);
            servicioCaja.AsignarSaldoBase(turnonuevo.SaldoInicial);
            turnonuevo.Estado = "A";
            ShowSelectedTurno(turnonuevo);
            servicioTurno.CreateTurno(turnonuevo);
            

        
        }

        private void TakeInfoTurnoTerminar()
        {
            ENTITY.Turno turnotoclose = servicioTurno.GetOpenTurno();
            turnotoclose.SaldoPrevisto = servicioCaja.GetSaldoSistema();
            turnotoclose.SaldoReal=float.Parse(txtSaldoReal.Text);
            turnotoclose.SetDiferencia();
            turnotoclose.SetEgresos();
            turnotoclose.SetIngresos();
            turnotoclose.Observacion = txtObservacion.Text.ToString();
            turnotoclose.CerrarTurno();
            var ms=servicioTurno.EditTurno(turnotoclose);
            MessageBox.Show(ms);
        }
       
        private void HideTurnos()
        {
            BorderTurnos.Visibility = Visibility.Hidden;
            GoBackButton.Visibility = Visibility.Visible;
            DataPickerBorder.Visibility = Visibility.Hidden;


        }

        private void ShowTurnos()
        {
            BorderTurnos.Visibility = Visibility.Visible;
            GoBackButton.Visibility = Visibility.Hidden;
            DataPickerBorder.Visibility = Visibility.Visible;
            lblPedidos.Visibility = Visibility.Hidden;
            lblEgresos.Visibility = Visibility.Hidden;

        }

        private void ShowSelectedTurno(ENTITY.Turno turnotoshow)
        {
            if (turnotoshow.Estado == "C")
            {
                lblCajero.Content = turnotoshow.Cajero.Nombre.ToString();
                lblHorario.Content = turnotoshow.Horario.ToString();
                lblSaldoBase.Content = string.Format("{0:C0}", turnotoshow.SaldoInicial);
                lblSaldoPrevisto.Content = string.Format("{0:C0}", turnotoshow.SaldoPrevisto);
                lblDiferencia.Content = string.Format("{0:C0}", turnotoshow.Diferencia);
                txtSaldoReal.Text = string.Format("{0:C0}", turnotoshow.SaldoReal);
                Expanderegresos.Header = "Egreso Total: " + string.Format("{0:C0}", turnotoshow.Egreso);
                ExpanderIngresos.Header = "Ingreso Total: " + string.Format("{0:C0}", turnotoshow.Ingreso);
                txtSaldoReal.IsEnabled = false;
                txtObservacion.IsEnabled = false;
                if (turnotoshow.Observacion != null)
                {
                    txtObservacion.Text = turnotoshow.Observacion.ToString();
                }
            }
            else
            {
                lblCajero.Content = turnotoshow.Cajero.Nombre;
                lblHorario.Content = turnotoshow.Horario;                  
                lblSaldoBase.Content = string.Format("{0:C0}", turnotoshow.SaldoInicial);
                Expanderegresos.Header = "Egreso Total: " + string.Format("{0:C0}", turnotoshow.Egreso);
                ExpanderIngresos.Header = "Ingreso Total: " + string.Format("{0:C0}", turnotoshow.Ingreso);
                lblDiferencia.Content = string.Format("{0:C0}", turnotoshow.Diferencia);
                lblSaldoPrevisto.Content = string.Format("{0:C0}", servicioCaja.GetSaldoSistema());


            }


        }




        //eventos de los botones de control(ver detalles, movimientos etc)

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
                case "PedidoDetailsButton":
                    Header.PopupText.Text = "Ver Detalles";
                    break;
            }
            
        }

        private void MoueseLeavebtnDetails(object sender, MouseEventArgs e)
        {
            Popup.Visibility = Visibility.Collapsed;
            Popup.IsOpen = false;
        }

        private void DetailsButton_Click(object sender, RoutedEventArgs e)
        {

            Button details = sender as Button;
            ENTITY.Turno turno = details.DataContext as ENTITY.Turno;
            HideThingsinicioTurno();
            ShowSelectedTurno(turno);
            TurnoButton.Visibility = Visibility.Hidden;
            btnCloseDetails.Visibility = Visibility.Visible;

        }

        private void CloseDetailsButtonClick(object sender, RoutedEventArgs e)
        {
            var turno=servicioTurno.GetOpenTurno();
            if (turno == null)
            {
                ShowThingsterminarTurno();
            }
            else
            {
                ShowSelectedTurno(turno);
            }

            TurnoButton.Visibility= Visibility.Visible;
            btnCloseDetails.Visibility= Visibility.Hidden;
            txtSaldoReal.IsEnabled = true;
            txtObservacion.IsEnabled = true;
        }

        private void ShowPedidoDetails(object sender, RoutedEventArgs e)
        {

        }

        private void ShowPedidos(object sender, RoutedEventArgs e) 
        {
          BorderPedidos.Visibility = Visibility.Visible;
          lblPedidos.Visibility = Visibility.Visible;
          HideTurnos();
          var source = sender as FrameworkElement;
          var turno = source.DataContext as ENTITY.Turno;
          PedidosListView.ItemsSource= servicopedido.GetPedidos(turno);
        }
       
        private void ShowEgresos(object sender, RoutedEventArgs e)
        {

        }
       
        
        
        
        
        
        
        
        
        //evento selector de fechas

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







        //boton hacía atras
        private void GoBack(object sender, RoutedEventArgs e)
        {

            BorderPedidos.Visibility = Visibility.Hidden;
            ShowTurnos();

        }



        private void txtSaldoReal_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtSaldoReal.Text!="")
            {

                var diferencia = float.Parse(txtSaldoReal.Text.ToString())- servicioCaja.GetSaldoSistema();
                lblDiferencia.Content = string.Format("{0:C0}",diferencia);
            }
        }




        private void RefreshListTurnos()
        {
            lstturnos.ItemsSource = null;
            View = new ListCollectionView(servicioTurno.GetTurnos());
            lstturnos.ItemsSource = View;
        }

      
       

      
    }
}
