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

        ClientesRepository clientesRepository;

        public ServicioCliente()
        {
            clientesRepository = new ClientesRepository();


        }


        //Crud
        
        public void AddClientes(Cliente newcliente) 
        {

          clientesRepository.insert(newcliente);
  
        }

        public List<Cliente> GetAllClientes() {
        
           return clientesRepository.GetClientess();
        
        }

        public void EditCliente(Cliente clienteOld, Cliente clienteModified)
        {
            clienteOld.Nombre = clienteModified.Nombre;
            clienteOld.Telefono = clienteModified.Telefono;
            clienteOld.Cedula = clienteModified.Cedula;
            clientesRepository.Edit(clienteOld);
        }

        public void DeleteCliente(Cliente clienteToDeelete)
        {
            clientesRepository.Delete(clienteToDeelete);
           
        }


    }
}
