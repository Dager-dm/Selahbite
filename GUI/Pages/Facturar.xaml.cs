using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Lógica de interacción para Facturar.xaml
    /// </summary>
    public partial class Facturar : Page
    {
        private List<string> clientes;



        public Facturar()
        {
            InitializeComponent();
            clientes = new List<string> { "Cliente 1", "Cliente 2", "Cliente 3", "juan0", "julian", "carlos0", "camilo" };
            cb.ItemsSource = clientes;

        }


        private void cb_GotFocus(object sender, RoutedEventArgs e)
        {
            var comboBox = sender as ComboBox;
            if (comboBox != null)
            {
                comboBox.IsDropDownOpen = true;
            }
        }
    }
    }


