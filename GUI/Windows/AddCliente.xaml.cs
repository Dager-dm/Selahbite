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
        //ServicioCliente serviciocliente = new ServicioCliente();
        //public event Action ClienteGuardado;
        public Cliente clientepr { get; set; }
        public Cliente clienteModified { get; set; }

        private int accion=0;
        //private Clientes _page;
        
        public AddCliente()
        {
            InitializeComponent();
            //_page = page;
           

        }

        public AddCliente(Cliente oldCliente)
        {
            InitializeComponent();
            lblTitulo.Content = "Editar Cliente";
            txtboxNombre.Text = oldCliente.Nombre;
            txtboxId.Text = oldCliente.Cedula;
            txtboxTelefono.Text = oldCliente.Telefono;
            accion = 1;
            clienteModified = new Cliente();
            clientepr = oldCliente;

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

           //Cliente cliente = new Cliente(txtboxNombre.Text.ToString(), txtboxId.Text.ToString(), txtboxTelefono.Text.ToString(), 0);
            //ClienteGuardado?.Invoke();
            if (accion==0) {
                clientepr = new Cliente();
                clientepr.Nombre = txtboxNombre.Text.ToString();
                clientepr.Cedula = txtboxId.Text.ToString();
                clientepr.Telefono = txtboxTelefono.Text.ToString();
                clientepr.Saldo = 0;

            }
            else
            {
                clienteModified.Nombre = txtboxNombre.Text.ToString();
                clienteModified.Cedula = txtboxId.Text.ToString();
                clienteModified.Telefono = txtboxTelefono.Text.ToString();
                
            }

            Close();
            
        }
    }
}
