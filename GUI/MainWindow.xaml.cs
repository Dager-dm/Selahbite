﻿using System;
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
using GUI.Themes;
using GUI.Pages;
using System.Windows.Media.Animation;
using ENTITY;
using BLL;
using System.Diagnostics;
using GUI.Windows;
using MaterialDesignThemes.Wpf;
using System.ComponentModel;


namespace GUI
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    /// 


    public partial class MainWindow : Window
    {

        ServicioTurno servicioTurno = new ServicioTurno();
        private readonly PaletteHelper paletteHelper = new PaletteHelper();
        public MainWindow()
        {
            InitializeComponent();
            MaximizeWindow();
            IsTurnoOpen();
            Closing += OnWindowClosing;

        }

        private  void OnWindowClosing(object sender, CancelEventArgs e)
        {
            var t = servicioTurno.GetOpenTurno();
            if (t == null)
            {
                Close();
            }
            else
            {
                MiMessageBox messageBox = new MiMessageBox(WarningMessage.W, "No puede salir del programa sin cerrar turno"); messageBox.ShowDialog();
                e.Cancel = true;
            }
        }

        private void IsTurnoOpen()
        {
            var t = servicioTurno.IsAnyTurnoOpen();
            if (t!=null)
            {

                frameContent.Navigate(new Pages.Turno(t));
                MiMessageBox messageBox = new MiMessageBox(WarningMessage.W, "Se cerró el programa inesperadamente"); messageBox.ShowDialog();
            }
            else
            {
                frameContent.Navigate(new Pages.Turno(null));
            }
        }

        //metodo del boton de temas
        private void Themes_Click(object sender, RoutedEventArgs e)
        {
            ITheme theme = paletteHelper.GetTheme();
            if (Themes.IsChecked == true)
            {
                ThemesController.SetTheme(ThemesController.ThemeTypes.Dark);
                theme.SetBaseTheme(Theme.Dark);
            }
            else
            {
                ThemesController.SetTheme(ThemesController.ThemeTypes.Light);
                theme.SetBaseTheme(Theme.Light);
            }
            paletteHelper.SetTheme(theme);
        }







        //Botones max, min, close
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            var t = servicioTurno.GetOpenTurno();
            if (t==null)
            {
                Close();
            }
            else
            {
                MiMessageBox messageBox = new MiMessageBox(WarningMessage.W,"No puede salir del programa sin cerrar turno"); messageBox.ShowDialog();
            }
            
        }

        private void btnRestore_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState != WindowState.Maximized)
            {
                MaximizeWindow();
            }
            else
            {
                RestoreWindow();
            }
        }

        private void MaximizeWindow()
        {
            // Maximizar la ventana 
            this.MaxWidth = SystemParameters.WorkArea.Width;
            this.MaxHeight = SystemParameters.WorkArea.Height;
            this.WindowState = WindowState.Maximized;
            bordeInferior.CornerRadius = new CornerRadius(10, 10, 0, 0);
        }

        private void RestoreWindow()
        {
            // Restaurar la ventana al tamaño normal
            this.MaxWidth = double.PositiveInfinity;
            this.MaxHeight = double.PositiveInfinity;
            this.WindowState = WindowState.Normal;
            bordeInferior.CornerRadius = new CornerRadius(10);
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }






        //Botones de navegacion

        private void rdFacturar_Click(object sender, RoutedEventArgs e)
        {
            if (servicioTurno.GetOpenTurno()!=null)
            {
                var radioButton = (RadioButton)sender;
                ShowIndicator(radioButton);
                frameContent.Navigate(new Facturar());
            }
            else
            {
                RadioButton btn = sender as RadioButton;
                btn.IsChecked = false;
                MiMessageBox messageBox = new MiMessageBox(NegativeMessage.N, "No se pueden realizar pedidos sin abrir turno"); messageBox.ShowDialog();

            }

        }

        private void rdEgresos_Click(object sender, RoutedEventArgs e)
        {
            if (servicioTurno.GetOpenTurno() != null)
            {
                var radioButton = (RadioButton)sender;
                ShowIndicator(radioButton);
                frameContent.Navigate(new Egresos());
            }
            else
            {
                RadioButton btn = sender as RadioButton;
                btn.IsChecked = false;
                MiMessageBox messageBox = new MiMessageBox(NegativeMessage.N, "No se pueden realizar egresos sin abrir turno"); messageBox.ShowDialog();
            }
        }

        private void rdDeudas_Click(object sender, RoutedEventArgs e)
        {
            var radioButton = (RadioButton)sender;
            ShowIndicator(radioButton);
            frameContent.Navigate(new Deudas());
        }

        private void rdCarta_Click(object sender, RoutedEventArgs e)
        {
            var radioButton = (RadioButton)sender;
            ShowIndicator(radioButton);
            frameContent.Navigate(new Carta());
        }

        private void rdEmpleados_Click(object sender, RoutedEventArgs e)
        {
            var radioButton = (RadioButton)sender;
            ShowIndicator(radioButton);
            frameContent.Navigate(new Empleados());

        }

        private void rdClientes_Click(object sender, RoutedEventArgs e)
        {
            var radioButton = (RadioButton)sender;
            ShowIndicator(radioButton);
            frameContent.Navigate(new Clientes());

        }

        private void rdReporte_Click(object sender, RoutedEventArgs e)
        {
            var radioButton = (RadioButton)sender;
            ShowIndicator(radioButton);
            frameContent.Navigate(new Reporte());

        }

        private void rdTurno_Click(object sender, RoutedEventArgs e)
        {
            var radioButton = (RadioButton)sender;
            ShowIndicator(radioButton);
            frameContent.Navigate(new Pages.Turno(null));

        }

        private void ShowIndicator(RadioButton button)
        {

            Indicator.Opacity = 1;
            TranslateTransform transform = Indicator.RenderTransform as TranslateTransform;
            double currentPosition = transform != null ? transform.X : 0;
            int column = Grid.GetColumn(button);
            double columnWidthPercentage = 100.0 / gridMenu.ColumnDefinitions.Count; // Calcular el ancho porcentual de cada columna
            double newPosition = column * (columnWidthPercentage / 100.0 * gridMenu.ActualWidth);

            //  animación
            DoubleAnimation da = new DoubleAnimation();
            da.From = currentPosition;
            da.To = newPosition;
            da.Duration = new Duration(TimeSpan.FromSeconds(0.2));

            // Crear el Storyboard
            Storyboard sb = new Storyboard();
            Storyboard.SetTarget(da, Indicator);
            Storyboard.SetTargetProperty(da, new PropertyPath("RenderTransform.(TranslateTransform.X)"));
            sb.Children.Add(da);

            // Iniciar la animación
            sb.Begin();

        }





    }
}
