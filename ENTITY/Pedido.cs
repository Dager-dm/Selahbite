using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY
{
    public class Pedido : Movimiento
    {
        public Pedido(long numeroFactura, List<DetallePedido> detalles, string formaPago, Empleado mesero, Cliente cliente, DateTime Fecha, long ValorTotal): base (Fecha, ValorTotal)
        {
            NumeroFactura = numeroFactura;
            Detalles = detalles;
            FormaPago = formaPago;
            Mesero = mesero;
            Cliente = cliente;
        }

 
        public long NumeroFactura { get; set;}
        public List<DetallePedido> Detalles { get; set;}
        public string FormaPago { get; set; }
        public Empleado Mesero { get; set; }
        public Cliente Cliente { get; set; }
    }
}
