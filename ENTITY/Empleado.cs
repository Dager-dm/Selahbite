using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY
{
    public class Empleado : Persona
    {
        public Empleado() { }
        public Empleado(string Cedula, string nombre, string id, string telefono, CargosEmpleados cargo) :base(Cedula, nombre,id,telefono)
        {
            Cargo = cargo;
        }
        public CargosEmpleados Cargo { get; set; }

    }
}
