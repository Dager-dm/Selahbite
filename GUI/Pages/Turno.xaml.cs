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
using GUI.Windows;

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
        //ServicioPedido servicopedido = new ServicioPedido();
        private ListCollectionView View { get; set; }
        private ENTITY.Turno turnopadre;

        public Turno(ENTITY.Turno turnoAbierto)
        {
            InitializeComponent();
            cboCajeros.ItemsSource = servicioEmpleado.GetCajeros();
            View = new ListCollectionView(servicioTurno.GetTurnos());
            lstturnos.ItemsSource = View;
            IsTurnoOpen(turnoAbierto);


        }

        private void IsTurnoOpen(ENTITY.Turno t)
        {
            if (servicioTurno.GetOpenTurno() != null)
            {
                SelectOpenTurno(servicioTurno.GetOpenTurno());
            }
            else if (t != null)
            {
                servicioCaja.AsignarSaldoBase(t.SaldoPrevisto);
                SelectOpenTurno(t);
                servicioTurno.SetOpenTurno(t);

            }
        }

        private void SelectOpenTurno(ENTITY.Turno turno)
        {

            ShowSelectedTurno(turno);
            HideThingsinicioTurno();
            PathGeometry iconofinish = (PathGeometry)Application.Current.Resources["finish"];
            TurnoButton.Content = "Terminar Turno";
            TurnoButton.Tag = iconofinish;
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

                if (cboCajeros.SelectedItem != null)
                {
                    if (txtSaldobase.Text != null)
                    {
                        HideThingsinicioTurno();
                        TakeInfoTurnoInicio();
                        TurnoButton.Content = "Terminar Turno";
                        TurnoButton.Tag = iconofinish;

                    }
                    else
                    {
                        MiMessageBox messageBox = new MiMessageBox(NegativeMessage.N, "El campo Saldo Base es obligatorio"); messageBox.ShowDialog();
                    }

                }
                else
                {
                    MiMessageBox messageBox = new MiMessageBox(NegativeMessage.N, "El campo Cajero es obligatorio"); messageBox.ShowDialog();
                }



            }
            else
            {
                if (!string.IsNullOrEmpty(txtSaldoReal.Text))
                {
                    
                    TakeInfoTurnoTerminar();
                    ShowThingsterminarTurno();
                    TurnoButton.Content = "Iniciar Turno";
                    TurnoButton.Tag = iconostart;
                    RefreshListTurnos();
                }
                else
                {
                    MiMessageBox messageBox = new MiMessageBox(NegativeMessage.N, "El campo Saldo Real es obligatorio"); messageBox.ShowDialog();
                }

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
            terminarBorder2.Visibility = Visibility.Hidden;
            ExpanderIngresos.IsExpanded = false;
            lblCajero.Content = null;
            lblHorario.Content = null;
            lblHorario.Content = null;
            lblSaldoBase.Content = null;


        }

        private void TakeInfoTurnoInicio()
        {

            ENTITY.Turno turnonuevo = new ENTITY.Turno();
            turnonuevo.Fecha = DateTime.Now;
            turnonuevo.Cajero = (Empleado)cboCajeros.SelectedItem;
            turnonuevo.Ingreso = 0; turnonuevo.Egreso = 0; turnonuevo.Diferencia = 0;
            if (HorarioDia.IsChecked == true)
            {
                turnonuevo.Horario = "Dia";
            }
            else
            {
                turnonuevo.Horario = "Noche";
            }

            turnonuevo.SaldoInicial = float.Parse(txtSaldobase.Text);
            servicioCaja.AsignarSaldoBase(turnonuevo.SaldoInicial);
            turnonuevo.Estado = "A";
            ShowSelectedTurno(turnonuevo);
            servicioTurno.CreateTurno(turnonuevo);



        }

        private void TakeInfoTurnoTerminar()
        {
            ENTITY.Turno turnotoclose = servicioTurno.GetOpenTurno();
            turnotoclose.SaldoPrevisto = servicioCaja.GetSaldoSistema();
            turnotoclose.SaldoReal = float.Parse(txtSaldoReal.Text);
            turnotoclose.SetDiferencia();
            turnotoclose.LoadEgresos();
            turnotoclose.LoadIngresos();
            if (string.IsNullOrEmpty(txtObservacion.Text))
            { turnotoclose.Observacion = " ";} else { turnotoclose.Observacion = txtObservacion.Text.ToString(); }
            turnotoclose.CerrarTurno();
            var c = servicioTurno.EditTurno(turnotoclose);
            if (c)
            {
                MiMessageBox messageBox = new MiMessageBox(AfirmativeMessage.A, "Turno Cerrado Correctamente"); messageBox.ShowDialog();
            }
            else
            {
                MiMessageBox messageBox = new MiMessageBox(NegativeMessage.N, "Error Cerrando Turno"); messageBox.ShowDialog();
            }
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
                bdsaldoreal.Visibility = Visibility.Hidden;
                bdsaldorealselected.Visibility = Visibility.Visible;
                lblCajero.Content = turnotoshow.Cajero.Nombre.ToString();
                lblHorario.Content = turnotoshow.Horario.ToString();
                lblSaldoBase.Content = string.Format("{0:C0}", turnotoshow.SaldoInicial);
                lblSaldoPrevisto.Content = string.Format("{0:C0}", turnotoshow.SaldoPrevisto);
                lblDiferencia.Content = string.Format("{0:C0}", turnotoshow.Diferencia);
                txtSaldoRealSelected.Text = string.Format("{0:C0}", turnotoshow.SaldoReal);
                txtEgreso.Text = "Egreso Total: " + string.Format("{0:C0}", turnotoshow.Egreso);
                ShowIngresos(turnotoshow);
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
                txtEgreso.Text = "Egreso Total: " + string.Format("{0:C0}", turnotoshow.Egreso);
                ExpanderIngresos.Header = "Ingreso Total: " + string.Format("{0:C0}", turnotoshow.Ingreso);
                ShowIngresos(turnotoshow);
                lblDiferencia.Content = string.Format("{0:C0}", turnotoshow.Diferencia);
                lblSaldoPrevisto.Content = string.Format("{0:C0}", servicioCaja.GetSaldoSistema());


            }


        }

        private void ShowIngresos(ENTITY.Turno turnotoshow)
        {
            servicioTurno.DefinirIngreso(turnotoshow);
            txtIBanco.Content = "Ingreso Bancolombia: " + string.Format("{0:C0}", servicioTurno.IBanco);
            txtICredito.Content = "Ingreso Credito: " + string.Format("{0:C0}", servicioTurno.ICredito);
            txtIDaviplata.Content = "Ingreso Daviplata: " + string.Format("{0:C0}", servicioTurno.IDaviplata);
            txtINequi.Content = "Ingreso Nequi: " + string.Format("{0:C0}", servicioTurno.Inequi);
            txtIEfectivo.Content = "Ingreso Efectivo: " + string.Format("{0:C0}", servicioTurno.IEfectivo);
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
                case "ShowPedidosOT":
                    Header.PopupText.Text = "Ver Pedidos";
                    break;
                case "EgresosButtonOT":
                    Header.PopupText.Text = "Ver Egresos";
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
            var turno = servicioTurno.GetOpenTurno();
            if (turno == null)
            {
                ShowThingsterminarTurno();
            }
            else
            {
                ShowSelectedTurno(turno);
            }
            bdsaldorealselected.Visibility = Visibility.Hidden;
            bdsaldoreal.Visibility = Visibility.Visible;
            TurnoButton.Visibility = Visibility.Visible;
            btnCloseDetails.Visibility = Visibility.Hidden;
            ExpanderIngresos.IsExpanded = false;
            txtSaldoReal.IsEnabled = true;
            txtObservacion.IsEnabled = true;
        }

        private void ShowPedidoDetails(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            Pedido pedido = btn.DataContext as Pedido;
            ShowDetails detailswindows = new ShowDetails(pedido, turnopadre.Cajero);
            detailswindows.ShowDialog();
        }

        private void ShowPedidos(object sender, RoutedEventArgs e)
        {
            BorderPedidos.Visibility = Visibility.Visible;
            lblPedidos.Visibility = Visibility.Visible;
            ShowPedidosOT.Visibility = Visibility.Hidden;
            EgresosButtonOT.Visibility = Visibility.Hidden;
            HideTurnos();
            var source = sender as FrameworkElement;
            var turno = source.DataContext as ENTITY.Turno;
            PedidosListView.ItemsSource = turno.Pedidos;
            turnopadre = turno;
        }

        private void ShowEgresos(object sender, RoutedEventArgs e)
        {
            BorderEgresos.Visibility = Visibility.Visible;
            lblEgresos.Visibility = Visibility.Visible;
            ShowPedidosOT.Visibility = Visibility.Hidden;
            EgresosButtonOT.Visibility = Visibility.Hidden;
            HideTurnos();
            var source = sender as FrameworkElement;
            var turno = source.DataContext as ENTITY.Turno;
            EgresosListView.ItemsSource = turno.LstEgresos;
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
            BorderEgresos.Visibility = Visibility.Hidden;
            ShowPedidosOT.Visibility = Visibility.Visible;
            EgresosButtonOT.Visibility = Visibility.Visible;
            ShowTurnos();

        }

        private void ShowPedidosOT_Click(object sender, RoutedEventArgs e)
        {
            if (servicioTurno.GetOpenTurno() != null)
            {
                PedidosListView.ItemsSource = servicioTurno.GetOpenTurno().Pedidos;
                BorderPedidos.Visibility = Visibility.Visible;
                lblPedidos.Visibility = Visibility.Visible;
                HideTurnos();
                ShowPedidosOT.Visibility = Visibility.Hidden;
                EgresosButtonOT.Visibility = Visibility.Hidden;
                turnopadre = servicioTurno.GetOpenTurno();
            }
            else
            {
                MiMessageBox messageBox = new MiMessageBox(WarningMessage.W, "No hay turno abierto");
            }

        }

        private void EgresosButtonOT_Click(object sender, RoutedEventArgs e)
        {
            if (servicioTurno.GetOpenTurno() != null)
            {
                BorderEgresos.Visibility = Visibility.Visible;
                lblEgresos.Visibility = Visibility.Visible;
                HideTurnos();
                EgresosListView.ItemsSource = servicioTurno.GetOpenTurno().LstEgresos;
                EgresosButtonOT.Visibility = Visibility.Hidden;
                ShowPedidosOT.Visibility = Visibility.Hidden;
            }
            else
            {
                MiMessageBox messageBox = new MiMessageBox(WarningMessage.W, "No hay turno abierto");
            }
        }

        private void txtSaldoReal_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtSaldoReal.Text != "")
            {

                var diferencia = float.Parse(txtSaldoReal.Text.ToString()) - servicioCaja.GetSaldoSistema();
                lblDiferencia.Content = string.Format("{0:C0}", diferencia);
            }
        }

        private void RefreshListTurnos()
        {
            lstturnos.ItemsSource = null;
            View = new ListCollectionView(servicioTurno.GetTurnos());
            lstturnos.ItemsSource = View;
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
                    System.Media.SystemSounds.Beep.Play();
                    e.Handled = true;
                }

            }
        }

    }
}
