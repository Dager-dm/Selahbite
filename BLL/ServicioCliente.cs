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
        private static List<Cliente> lstClientes;

        public ServicioCliente()
        {
            lstClientes = new List<Cliente>();
            clientepr();

        }

        
        public void AddClientes(Cliente newcliente) {

         lstClientes.Add(newcliente);
          
        }

        public List<Cliente> GetAllClientes() {
        
        return lstClientes;
        
        }

        private void clientepr()
        {
            Cliente cliente = new Cliente("Juan", "11", "301",0);
            lstClientes.Add(cliente);

        }

        
    }
}
