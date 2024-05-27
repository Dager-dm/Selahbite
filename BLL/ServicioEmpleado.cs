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
        private static List<Empleado> lstEmpleados;

        EmpleadosRepository empleadosRepository = new EmpleadosRepository();

        public ServicioEmpleado()
        {
            lstEmpleados = new List<Empleado>();


        }
    


        //Crud
        public void AddEmpleado(Empleado newempleado)
        {
            lstEmpleados.Add(newempleado);
            
        }

        public List<Empleado> GetAllEmpleados()
        {

            return lstEmpleados;

        }

        public List<Empleado> GetCajeros() 
        {
            List<Empleado> lstCajeros;




            return lstEmpleados; 
        }

        public void EditEmpleado(Empleado empleadoOld, Empleado empleadoModified)
        {
            empleadoOld.Nombre = empleadoModified.Nombre;
            empleadoOld.Telefono = empleadoModified.Telefono;
            empleadoOld.Id = empleadoModified.Id;
            empleadoOld.Cargo = empleadoModified.Cargo;
        }

        public void DeleteEmpleado(Empleado empleadoToDelete)
        {

            lstEmpleados.Remove(empleadoToDelete);
        }

        public List<Empleado> GetMeseros()
        {

            return lstEmpleados;
        }



        public List<CargosEmpleados> GetCargos()
        {
            return empleadosRepository.GetCargos();
        }







    }
}
