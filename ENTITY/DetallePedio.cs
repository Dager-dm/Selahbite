using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY
{
    public class DetallePedio
    {
        public DetallePedio() { }
        public DetallePedio(string idDetalle, long valorProductoVendido) 
        {
            IdDetalle = idDetalle;
            ValorProductoVendido = valorProductoVendido;
        } 

        public string IdDetalle { get; set; }
        public long ValorProductoVendido { get; set; }

    }
}
