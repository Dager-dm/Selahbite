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
    /// Lógica de interacción para MiMessageBox.xaml
    /// </summary>
    public partial class MiMessageBox : Window
    {
        public MiMessageBox()
        {
            
        }
        public MiMessageBox(string mensaje)
        {
            InitializeComponent();          
            lblQuestion.Text = mensaje; 
            btnAceptar.Visibility = Visibility.Hidden;
            btnYes.Visibility = Visibility.Visible;
            btnNo.Visibility = Visibility.Visible;
        }
       
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        public MiMessageBox(AfirmativeMessage m, string message)
        {
            InitializeComponent();
            lblQuestion.Text = message;
            btnAceptar.Visibility = Visibility.Visible;
            btnYes.Visibility = Visibility.Hidden;
            btnNo.Visibility = Visibility.Hidden;
            PathGeometry icono = (PathGeometry)System.Windows.Application.Current.Resources["Afirmative"];
            SolidColorBrush Color = (SolidColorBrush)System.Windows.Application.Current.Resources["TertiaryGreenColor"];
            icon.Data = icono;
            icon.Fill = Color;

        }

        public MiMessageBox(NegativeMessage m, string message)
        {
            InitializeComponent();
            lblQuestion.Text = message;
            btnAceptar.Visibility = Visibility.Visible;
            btnYes.Visibility = Visibility.Hidden;
            btnNo.Visibility = Visibility.Hidden;
            PathGeometry icono = (PathGeometry)System.Windows.Application.Current.Resources["Negative"];
            SolidColorBrush Color = (SolidColorBrush)System.Windows.Application.Current.Resources["TertiaryRedColor"];
            icon.Data = icono;
            icon.Fill= Color;
        }

        public MiMessageBox(WarningMessage m, string message)   
        {
            InitializeComponent();
            lblQuestion.Text = message;
            btnAceptar.Visibility = Visibility.Visible;
            btnYes.Visibility = Visibility.Hidden;
            btnNo.Visibility = Visibility.Hidden;
            PathGeometry icono = (PathGeometry)System.Windows.Application.Current.Resources["Warning"];
            SolidColorBrush Color = (SolidColorBrush)System.Windows.Application.Current.Resources["SecundaryYellowColor"];
            icon.Data = icono;
            icon.Fill = Color;
        }

    }

    public enum AfirmativeMessage
    {
        A
    }
    public enum NegativeMessage
    {
        N
    }
    public enum WarningMessage
    {
        W
    }
}
