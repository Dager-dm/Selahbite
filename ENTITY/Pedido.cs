using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY
{
    public class Pedido : Movimiento
    {
        public Pedido(DateTime fecha, string horario, long valor, string idFactura, string numeroFactura, string formaPago)
               :base ( fecha, horario, valor)
        {
            IdFactura = idFactura;
            NumeroFactura = numeroFactura;
            FormaPago = formaPago;
            

        }

        public string IdFactura { get; set; }
        public string NumeroFactura { get; set;}
        public string FormaPago { get; set; }
        public List<DetallePedio> DetallePedio { get; set; }
    }
}
