using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENTITY;

namespace BLL
{
    
    public class ServicioEmpleado
    {
        private static List<Empleado> lstEmpleados;

        public ServicioEmpleado()
        {
            lstEmpleados = new List<Empleado>();
            empleadopr();

        }

        public void AddEmpleado(Empleado newempleado)
        {
            lstEmpleados.Add(newempleado);
            
        }

        public List<Empleado> GetAllEmpleados()
        {

            return lstEmpleados;

        }

        private void empleadopr()
        {
            Empleado cliente = new Empleado("Juan", "11", "301", "Mesero", 0);
            lstEmpleados.Add(cliente);

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

    }
}
