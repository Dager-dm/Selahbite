using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY
{
    public class Salidas : Movimiento
    {
        public Salidas(DateTime fecha, string cajero, string horario, float valor, string recibidor, string descripcion) : base (fecha, cajero, horario, valor)
        {
            Recibidor = recibidor;
            Descripcion = descripcion;
        }
        public string Recibidor { get; set; }
        public string Descripcion { get; set; }
    }
}
