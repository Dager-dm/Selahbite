using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY
{
     public class Turno
    {
        public Turno()
        {
            
        }

        public Turno(string horario, DateTime fecha, float saldoInicial, Empleado cajero, float egresos, float ingresos, float saldoPrevisto, float saldoReal, float diferencia, string observacion, List<Pedido> pedidos, List<Egreso> lstEgresos, string estado)
        {
            Horario = horario;
            Fecha = fecha;
            SaldoInicial = saldoInicial;
            Cajero = cajero;
            Egreso = egresos;
            Ingreso = ingresos;
            SaldoPrevisto = saldoPrevisto;
            SaldoReal = saldoReal;
            Diferencia = diferencia;
            Observacion = observacion;
            Pedidos = pedidos;
            LstEgresos = lstEgresos;
            Estado = estado;
        }

        public string Horario { get; set; }
        public DateTime Fecha { get; set; }
        public float SaldoInicial { get; set; }
        public Empleado Cajero { get; set; }
        public float Egreso { get; set; }
        public float Ingreso { get; set; }
        public float SaldoPrevisto { get; set; }
        public float SaldoReal { get; set; }
        public float Diferencia { get; set; }
        public string Observacion { get; set; }
        public List<Pedido> Pedidos { get; set; }
        public List<Egreso> LstEgresos { get; set; }
        public string Estado {  get; set; }
        
        


        public void CerrarTurno()
        {
            Estado = "Cerrado";
        }

        public void SetAPedido(Pedido pedido)
        {
          Pedidos.Add(pedido);
        
        }

        public void SetAEgreso(Egreso egreso)
        {
            LstEgresos.Add(egreso);

        }
        public void SetDiferencia()
        {
            Diferencia = SaldoPrevisto - SaldoReal;
        }

        public void SetEgresos()
        {
            foreach(var item in LstEgresos)
            {
                Egreso =Egreso+ item.Valor;

            }

        }

        public void SetIngresos()
        {
            foreach (var item in Pedidos)
            {
                Ingreso = Ingreso + item.Valor;

            }

        }
    }
}
