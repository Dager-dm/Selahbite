using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY
{
    public class Egreso : Movimiento
    {
        public Egreso(DateTime fecha, string horario, long valor, string recibidor, string descripcion) : base (fecha, horario, valor)
        {
            Recibidor = recibidor;
            Descripcion = descripcion;
        }
        public string Recibidor { get; set; }
        public string Descripcion { get; set; }
    }
}
