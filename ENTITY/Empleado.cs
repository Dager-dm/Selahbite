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
        public Empleado(string nombre, string id, string telefono, float sueldo, string cargo) :base(nombre,id,telefono, sueldo)
        {
            Cargo = cargo;
        }
        public string Cargo { get; set; }
    }
}
