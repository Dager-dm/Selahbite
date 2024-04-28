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
using GUI.Themes;
//using GUI.Pages;
using System.Windows.Media.Animation;


namespace GUI
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
       
        //metodo del boton de temas
        private void Themes_Click(object sender, RoutedEventArgs e)
        {

            if (Themes.IsChecked == true)
                ThemesController.SetTheme(ThemesController.ThemeTypes.Dark);
            else
                ThemesController.SetTheme(ThemesController.ThemeTypes.Light);
  



        }







        //Botones max, min, close
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
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
            // Maximizar la ventana respetando el área de trabajo
            this.MaxWidth = SystemParameters.WorkArea.Width;
            this.MaxHeight = SystemParameters.WorkArea.Height;
            this.WindowState = WindowState.Maximized;
        }

        private void RestoreWindow()
        {
            // Restaurar la ventana al tamaño normal
            this.MaxWidth = double.PositiveInfinity;
            this.MaxHeight = double.PositiveInfinity;
            this.WindowState = WindowState.Normal;
        }


        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }







        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void rdPedidos_Click(object sender, RoutedEventArgs e)
        {
            var radioButton = (RadioButton)sender;
            ShowIndicator(radioButton);
            //frameContent.Navigate(new Home());
        }

        private void rdEgresos_Click(object sender, RoutedEventArgs e)
        {
            var radioButton = (RadioButton)sender;
            ShowIndicator(radioButton);
            //frameContent.Navigate(new Analytics());
        }

        private void rdDeudas_Click(object sender, RoutedEventArgs e)
        {
            var radioButton = (RadioButton)sender;
            ShowIndicator(radioButton);
            //frameContent.Navigate(new Messages());
        }

        private void rdCarta_Click(object sender, RoutedEventArgs e)
        {
            var radioButton = (RadioButton)sender;
            ShowIndicator(radioButton);
            //frameContent.Navigate(new Collections());
        }

        private void rdEmpleados_Click(object sender, RoutedEventArgs e)
        {
            var radioButton = (RadioButton)sender;
            ShowIndicator(radioButton);
            //frameContent.Navigate(new Users());

        }

        private void rdClientes_Click(object sender, RoutedEventArgs e)
        {
            var radioButton = (RadioButton)sender;
            ShowIndicator(radioButton);
            //frameContent.Navigate(new Clientes());

        }

        private void rdReporte_Click(object sender, RoutedEventArgs e)
        {
            var radioButton = (RadioButton)sender;
            ShowIndicator(radioButton);
            //frameContent.Navigate(new Reporte());

        }

        private void rdTurno_Click(object sender, RoutedEventArgs e)
        {
            var radioButton = (RadioButton)sender;
            ShowIndicator(radioButton);
            //frameContent.Navigate(new Turno());

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
