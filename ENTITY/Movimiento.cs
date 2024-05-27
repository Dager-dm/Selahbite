using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY
{
    public class Movimiento
    {
        public Movimiento(DateTime fecha, float valor) 
        {
            Fecha = fecha;
            Valor = valor;
        }

        public DateTime Fecha { get; set; }
        public float Valor { get; set;}

    }
}
