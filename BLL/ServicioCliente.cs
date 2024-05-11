using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENTITY;

namespace BLL
{
    public class ServicioCliente
    {
        public static List<Cliente> lstClientes;
   

        public ServicioCliente()
        {
            lstClientes = new List<Cliente>();

            clienteprueba();

        }

        
        public void AddClientes(Cliente newcliente) 
        {

            lstClientes.Add(newcliente);
  
        }

        public List<Cliente> GetAllClientes() {
        
        return lstClientes;
        
        }

        private void clienteprueba()
        {
            Cliente cliente = new Cliente("Jose", "11", "301",0);
            lstClientes.Add(cliente);
            Cliente cliente1 = new Cliente("Juanito", "11", "301", 0);
            lstClientes.Add(cliente1);
            Cliente cliente2 = new Cliente("Mario", "11", "301", 0);
            lstClientes.Add(cliente2);
            Cliente cliente3 = new Cliente("Carlos", "11", "301", 0);
            lstClientes.Add(cliente3);
            Cliente cliente4 = new Cliente("Mariano", "11", "301", 0);
            lstClientes.Add(cliente4);
         

        }

        public void EditCliente(Cliente clienteOld, Cliente clienteModified)
        {
            clienteOld.Nombre = clienteModified.Nombre;
            clienteOld.Telefono = clienteModified.Telefono;
            clienteOld.Id = clienteModified.Id;
        }

        public void DeleteCliente(Cliente clienteToDeelete)
        {

            lstClientes.Remove(clienteToDeelete);
        }
        
    }
}
