using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY
{
    public class Pedido : Movimiento
    {
        public Pedido(DateTime fecha, Empleado cajero, string horario, float valor, string id, int numeroFactura, string formaPago)
               :base ( fecha, cajero, horario, valor)
        {
            Id = id;
            NumeroFactura = numeroFactura;
            FormaPago = formaPago;
            Platos = new List<Producto>();

        }

        public string Id { get; set; }
        public int NumeroFactura { get; set;}
        public List<Producto> Platos { get; set;}
        public string FormaPago { get; set; }
    }
}
