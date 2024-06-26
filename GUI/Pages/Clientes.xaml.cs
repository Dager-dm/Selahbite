using ENTITY;
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
using BLL;



namespace GUI.Pages
{
    /// <summary>
    /// Lógica de interacción para Clientes.xaml
    /// </summary>
    public partial class Clientes : Page
    {

        private ServicioCliente serviciocliente;
        

        public Clientes()
        {
            InitializeComponent();
            serviciocliente = new ServicioCliente();
            miListView.ItemsSource = serviciocliente.GetAllClientes();

        }

        private void NewClient(object sender, RoutedEventArgs e)
        {
            AddCliente addClienteWindow = new AddCliente();
            addClienteWindow.ShowDialog();
            if (addClienteWindow.guardarPresionado)
            {
                serviciocliente.AddClientes(addClienteWindow.clientepr);
                Refreshlistview();
            }

        }

        public void Refreshlistview()
        { 
            miListView.ItemsSource = null;
            miListView.ItemsSource = serviciocliente.GetAllClientes();
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            
            Button btnEditar = sender as Button;
            Cliente cliente = btnEditar.DataContext as Cliente;
            AddCliente addClienteWindow = new AddCliente(cliente);
            addClienteWindow.ShowDialog();
            if (addClienteWindow.guardarPresionado)
            {
                serviciocliente.EditCliente(addClienteWindow.clientepr, addClienteWindow.clienteModified);
                Refreshlistview();
            }
        }

        private void btnBorrar_Click(object sender, RoutedEventArgs e)
        {
            Button btnBorrar = sender as Button;
            Cliente cliente = btnBorrar.DataContext as Cliente;
            MiMessageBox messageBox = new MiMessageBox("¿Está seguro de borrar\n"+" el cliente "+cliente.Nombre+"?" + " Esta acción no se puede revertir");
            bool? resultado = messageBox.ShowDialog();
            if (resultado == true)
            {
              serviciocliente.DeleteCliente(cliente);
              Refreshlistview();
            }
                    
        }

        public void test()
        {
            MiMessageBox messageBox = new MiMessageBox(WarningMessage.W, "perdio el año valentina"); messageBox.ShowDialog();
        }
        private void TxtBusqueda_TextChanged(object sender, TextChangedEventArgs e)
        {
            
            string filtro = txbBusqueda.Text.ToLower();
            List<Cliente>clientes = serviciocliente.GetAllClientes();


            List<Cliente> clientesFiltrados = clientes.Where(c => c.Nombre.ToLower().Contains(filtro)).ToList();

           
            miListView.ItemsSource = clientesFiltrados;
        }

    }
}
