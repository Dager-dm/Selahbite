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
using BLL;
using ENTITY;
using GUI;

namespace GUI.Pages
{
    /// <summary>
    /// Lógica de interacción para AddCliente.xaml
    /// </summary>
    public partial class AddCliente : Window
    {
        ServicioCliente serviciocliente = new ServicioCliente();
        public event Action ClienteGuardado;
        public AddCliente()
        {
            InitializeComponent();
            
        }



        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void AddButton(object sender, RoutedEventArgs e)
        {
            Cliente cliente = new Cliente(txtboxNombre.Text.ToString(), txtboxId.Text.ToString(), txtboxTelefono.Text.ToString(), 0);
            serviciocliente.AddClientes(cliente);
            ClienteGuardado?.Invoke();
            Close();
            
        }
    }
}
