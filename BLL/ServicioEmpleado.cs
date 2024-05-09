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
            //empleadopr();

        }

        public int AddClientes(Empleado newempleado)
        {
            lstEmpleados.Add(newempleado);
            return lstEmpleados.Count;

        }

        public List<Empleado> GetAllClientes()
        {

            return lstEmpleados;

        }

        private void empleadopr()
        {
            Empleado cliente = new Empleado("Juan", "11", "301", "Mesero", 0);
            lstEmpleados.Add(cliente);

        }



    }
}
