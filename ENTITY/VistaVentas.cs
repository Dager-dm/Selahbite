using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY
{
    public  class VistaVentas
    {
        public VistaVentas()
        {
            
        }

        public VistaVentas(float ventaTotal, DateTime fecha)
        {
            VentaTotal = ventaTotal;
            Fecha = fecha;
        }

        public float VentaTotal { get; set; }
        public DateTime Fecha { get; set; }
        public string Mes {  get; set; }
    }
}
