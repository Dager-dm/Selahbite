using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY
{
    public class MetodosPago
    {
        public MetodosPago() { }

        public MetodosPago(string nombre, string id)
        {
            Nombre = nombre;
            Id = id;
        }

        public string Nombre { get; set; }

        public string Id { get; set; }
    }
}
