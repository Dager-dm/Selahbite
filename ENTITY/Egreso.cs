using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY
{
    public class Egreso : Movimiento 
    {

        public Egreso( string recibidor, string descripcion, float Valor, DateTime Fecha) : base (Fecha, Valor)
        {
            Recibidor = recibidor;
            Descripcion = descripcion;
 
        }
        public string Recibidor { get; set; }
        public string Descripcion { get; set; }
        
    }
}
