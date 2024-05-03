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
            lstbox.ItemsSource = clientes;
            txtbox.TextChanged += Tb_TextChanged;
            txtbox.AddHandler(TextBox.MouseLeftButtonDownEvent, new MouseButtonEventHandler(TextBoxclick), true);

        }



        private void Tb_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox != null)
            {
                var filteredList = clientes.Where(item => item.StartsWith(textBox.Text)).ToList();
                lstbox.ItemsSource = filteredList;
            }
        }

        private void lb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var listBox = sender as ListBox;
            if (listBox != null)
            {
                txtbox.Text = listBox.SelectedItem.ToString();
            }
            lstbox.Visibility = Visibility.Hidden;
            ControlTemplate template = txtbox.Template;
            Border border = (Border)template.FindName("borderbox", txtbox);
            border.CornerRadius = new CornerRadius(5);
        }


        private void TextBoxclick(object sender, RoutedEventArgs e)
        {
            lstbox.Visibility = Visibility.Visible;
            ControlTemplate template = txtbox.Template;
            Border border = (Border)template.FindName("borderbox", txtbox);
            border.CornerRadius = new CornerRadius(5,5,0,0);
           

        }

        //private void lstboxclick(object sender, RoutedEventArgs e)
        //{


        //    txtbox.Text= lstbox.SelectedItem.ToString();
        //    MessageBox.Show(lstbox.SelectedItem.ToString());
        //    lstbox.Visibility = Visibility.Hidden;

        //}

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


