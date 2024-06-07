using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY
{
    public class Pedido : Movimiento
    {
        public Pedido(): base(DateTime.Now, 0)
        {
            
        }
        public Pedido(long id, List<DetallePedido> detalles, MetodosPago metodopago, Empleado mesero, Cliente cliente, DateTime Fecha, long ValorTotal, string esatdo): base (Fecha, ValorTotal)
        {
            Id = id;
            Detalles = detalles;
            MetodoPago = metodopago;
            Mesero = mesero;
            Cliente = cliente;
            Estado = esatdo;

            //Turno = turno;
        }


        public string Estado { get; set; }
        public long Id { get; set;}
        public List<DetallePedido> Detalles { get; set;}
        public MetodosPago MetodoPago { get; set; }
        public Empleado Mesero { get; set; }
        public Cliente Cliente { get; set; }
        public FormaDePago FormaDePago { get; set; }
        //public Turno Turno { get; set; }

        public void CalculoValor()
        {
            foreach (var item in Detalles)
            {
                Valor=Valor+item.ValorProductoVendido;
            }
        }

        
    }
}
