using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY
{
     public class Turno
     {

        public Turno() { }
        public Turno(string horario, DateTime fecha, long saldoInicial, long egresos, long ingresos, long saldoSistema, long saldoUsuario, long diferencia, string observacion ) 
        {
            Horario = horario;
            Fecha = fecha;
            SaldoInicial = saldoInicial;
            Egresos = egresos;
            Ingresos = ingresos;
            SaldoSistema = saldoSistema;
            Diferencia = diferencia;
            SaldoUsuario = saldoUsuario;
            Observacion = observacion;
        }
        public string Horario { get; set; }
        public DateTime Fecha { get; set; }
        public long SaldoInicial { get; set; }
        public long Egresos { get; set; }
        public long Ingresos { get; set; }
        public long SaldoSistema { get; set; }
        public long SaldoUsuario { get; set; }
        public float Diferencia { get; set; }
        public string Observacion { get; set; }
     }
}
