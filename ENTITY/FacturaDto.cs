using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY
{
    public class FacturaDto
    {
        public FacturaDto()
        {
            Detalles = new List<DetallePedido>();
        }

        public long IdPedido { get; set; }
        public DateTime Fecha { get; set; }
        public string NombreCajero { get; set; }
        public string NombreCliente { get; set; }
        public string CedulaCliente { get; set; }
        public string ValorTotal { get; set; }
        public string Efectivo { get; set; }
        public string Cambio { get; set;}
        public List<DetallePedido> Detalles { get; set; }
    }
}
