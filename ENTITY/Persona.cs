using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY
{
    public class Persona
    {
        public Persona() { }

        public Persona(string cedula, string nombre, string id, string telefono)
        {
            Cedula = cedula;
            Nombre = nombre;
            Id = id;
            Telefono = telefono;
        }

        public string Cedula { get; set; }
        public string Nombre { get; set;}
        public string Id { get; set; }
        public string Telefono { get; set; }
        
    }
}
