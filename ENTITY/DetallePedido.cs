using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY
{
    public class DetallePedido
    {
        public DetallePedido()
        {
            
        }

        public DetallePedido(long id, Pedido pedido, Producto producto, long valorProductoVendido, int cantidad)
        {
            Id = id;
            Pedido = pedido;
            Producto = producto;
            ValorProductoVendido = valorProductoVendido;
            Cantidad = cantidad;
        }

        public long Id { get; set; }
        public Pedido Pedido { get; set; }
        public Producto Producto { get; set; }
        public long ValorProductoVendido { get; set; }
        public int Cantidad { get; set; }
        public float valorUnitario { get; set; }

        public void CalcularValorUnitario()
        {
            valorUnitario = ValorProductoVendido / Cantidad;
        }
        public void CalculoValor()
        {
            ValorProductoVendido = (long)(Producto.Valor * Cantidad);
        }


    }
}
