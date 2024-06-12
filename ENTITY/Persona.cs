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

        public Persona(string cedula, string nombre, long id, string telefono)
        {
            Cedula = cedula;
            Nombre = nombre;
            Id = id;
            Telefono = telefono;
        }

        public string Cedula { get; set; }
        public string Nombre { get; set;}
        public long Id { get; set; }
        public string Telefono { get; set; }
        
    }
}
