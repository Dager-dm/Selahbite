using System;
using System.Collections.Generic;
using System.Globalization;
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
using BLL;
using ENTITY;
using LiveCharts;
using LiveCharts.Wpf;

namespace GUI.Pages
{
    /// <summary>
    /// Lógica de interacción para Reporte.xaml
    /// </summary>
    public partial class Reporte : Page
    {
        private List<VistaVentas> ventasMensuales;
        private List<VistaVentas> ventasSemanales;
        ServicioVistaVentas serviciovistaventas = new ServicioVistaVentas();

        public Reporte()
        {
            InitializeComponent();
            ventasMensuales = new List<VistaVentas>(); ventasMensuales = serviciovistaventas.GetVistaVentasMensuales();
            ventasSemanales = new List<VistaVentas>(); ventasSemanales = serviciovistaventas.GetVentasSemanales();
            // Datos de ejemplo
            //ventasMensuales = new List<double> { 1000, 1500, 50000, 1200, 700, 1300, 1400, 1600, 1800, 2000, 2100, 2200, 2300 };
            //ventasSemanales = new List<double> { 250, 300, 280, 320, 270, 290, 310, 340, 350, 380, 400, 420, 430, 450, 460, 470, 480, 490, 500, 510, 520, 530, 540, 550, 560, 570, 580, 590, 600, 610, 620, 630, 640, 650, 660, 670, 680, 690, 700, 710, 720, 730, 740, 750, 760, 770, 780, 790, 800, 810, 820, 830, 840, 850 };

            // Configurar etiquetas y formato
            //LabelsMensuales = new[] { "Ene", "Feb", "Mar", "Abr", "May", "Jun", "Jul", "Ago", "Sep", "Oct", "Nov", "Dic" };
            //LabelsSemanales = new[] { "Lunes", "Martes", "Miercoles", "Jueves", "Viernes", "Sabado", "Domingo" };
            Formatter = value => value.ToString("C0");

            // Inicializar gráfico con ventas mensuales
            MostrarVentasMensuales();

            DataContext = this; // Establece el contexto de datos
        }

        //public string[] LabelsMensuales { get; set; }
        //public string[] LabelsSemanales { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }

        private void MostrarVentasMensuales()
        {
            Labels = new string[ventasMensuales.Count];
            for (int i = 0; i < ventasMensuales.Count; i++) { Labels[i] = ventasMensuales[i].Mes; }
            //Labels = LabelsMensuales;
            cartesianChart.Series = new SeriesCollection
            {
                new ColumnSeries
                {
                    Fill= (SolidColorBrush)System.Windows.Application.Current.Resources["TertiaryGreenColor"],
                    Title = "Ventas Mensuales",
                    Values = new ChartValues<double>(ventasMensuales.Select(v => (double)v.VentaTotal).ToList())
                }
            };
            cartesianChart.AxisX.First().Labels = Labels;
        }

        private void MostrarVentasSemanales()
        {
            Labels = new string[ventasSemanales.Count];
            for (int i = 0; i < ventasSemanales.Count; i++)
            {
                string dia = ventasSemanales[i].Fecha.ToString("dddd", new CultureInfo("es-ES"));
                Labels[i] = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(dia);
            }
            //Labels = LabelsSemanales;
            cartesianChart.Series = new SeriesCollection
            {
                new ColumnSeries
                {
                    Fill= (SolidColorBrush)System.Windows.Application.Current.Resources["TertiaryGreenColor"],
                    Title = "Ventas Semanales",
                    Values = new ChartValues<double>(ventasSemanales.Take(7).Select(v => (double)v.VentaTotal).ToList())
                }
            };
            cartesianChart.AxisX.First().Labels = Labels;
        }

        private void BtnMensuales_Click(object sender, RoutedEventArgs e)
        {
            MostrarVentasMensuales();
        }

        private void BtnSemanales_Click(object sender, RoutedEventArgs e)
        {
            MostrarVentasSemanales();
        }

    }
}
