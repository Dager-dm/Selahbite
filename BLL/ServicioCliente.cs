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
        public  List<Cliente> lstClientes = new List<Cliente>();

        public ServicioCliente()
        {
            lstClientes = new List<Cliente>();
            clientepr();

        }

        
        public void AddClientes(Cliente cliente) {

         lstClientes.Add(cliente);
        
        }

        public List<Cliente> GetAll() {
        
        return lstClientes;
        
        }

        private void clientepr()
        {
            Cliente cliente = new Cliente("Juan", "11", "301",0);
            lstClientes.Add(cliente);

        }
    }
}
