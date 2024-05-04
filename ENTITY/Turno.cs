using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY
{
     public class Turno
    {
        public Turno(string horario, DateTime fecha, float saldoInicial, float egresos, float ingresos, float saldoSistema, float saldoUsuario, float diferencia, string observacion ) 
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
            Pedidos = new List<string>();
        }
        public string Horario { get; set; }
        public DateTime Fecha { get; set; }
        public float SaldoInicial { get; set; }
        public float Egresos { get; set; }
        public float Ingresos { get; set; }
        public float SaldoSistema { get; set; }
        public float SaldoUsuario { get; set; }
        public float Diferencia { get; set; }
        public string Observacion { get; set; }
        public List<string> Pedidos { get; set; }
     }
}
