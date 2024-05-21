using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY
{
    public class Movimiento
    {
        public Movimiento(DateTime fecha, string horario, long valor) 
        {
            Fecha = fecha;
            Horario = horario;
            Valor = valor;
        }
        public DateTime Fecha { get; set; }
        public string Horario { get; set;}  
        public long Valor { get; set;}

    }
}
