using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENTITY;
using DAL;

namespace BLL
{

    public class ServicioEmpleado
    {
       

        EmpleadosRepository empleadosRepository = new EmpleadosRepository();

        public ServicioEmpleado()
        {
            
        }
    


        //Crud
        public void AddEmpleado(Empleado newempleado)
        {
            empleadosRepository.insert(newempleado);
            
        }

        public List<Empleado> GetAllEmpleados()
        {

            return empleadosRepository.GetEmpleados();

        }

        public void EditEmpleado(Empleado empleadoOld, Empleado empleadoModified)
        {
            empleadoOld.Nombre = empleadoModified.Nombre;
            empleadoOld.Telefono = empleadoModified.Telefono;
            empleadoOld.Cedula = empleadoModified.Cedula;
            empleadoOld.Cargo = empleadoModified.Cargo;
            empleadosRepository.Edit(empleadoOld);
        }

        public void DeleteEmpleado(Empleado empleadoToDelete)
        {
            empleadosRepository.Delete(empleadoToDelete);
            
        }

        public List<Empleado> GetMeseros()
        {

            return empleadosRepository.GetMeseros();
        }

        public List<Empleado> GetCajeros() 
        {
            return empleadosRepository.GetCajeros(); 
        }

        public List<CargosEmpleados> GetCargos()
        {
            return empleadosRepository.GetCargos();
        }







    }
}
