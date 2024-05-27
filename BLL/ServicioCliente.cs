using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENTITY;
using DAL;

namespace BLL
{
    public class ServicioCliente
    {
        public static List<Cliente> lstClientes;
        ClientesRepository clientesRepository;

        public ServicioCliente()
        {
            lstClientes = new List<Cliente>();
            clientesRepository = new ClientesRepository();


        }


        //Crud
        
        public void AddClientes(Cliente newcliente) 
        {

            lstClientes.Add(newcliente);
            clientesRepository.insert(newcliente);
  
        }

        public List<Cliente> GetAllClientes() {
        
           return clientesRepository.GetClientes();
        
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

        public List<string> GetStringClientes()
        {
            List<string> StringList = new List<string>();
            foreach (var item in lstClientes)
            {
                StringList.Add(item.Nombre);
            }

            return StringList;
        }



    }
}
