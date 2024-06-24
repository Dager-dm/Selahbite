using DAL;
using ENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ServicioEgresos
    {
        EgresosRepository egresorepository = new EgresosRepository();
        ServicioTurno servicioturno = new ServicioTurno();
        ServicioCaja serviciocaja = new ServicioCaja();
        public ServicioEgresos()
        {
            
        }

        public void Insertar(Egreso egreso) 
        {
            var t=servicioturno.GetOpenTurno();
            egresorepository.insert(egreso, t.Id);
            t.SetAEgreso(egreso);
            t.CalcularEgreso(egreso.Valor);
            serviciocaja.RestarEgreso(egreso.Valor);
        }

        public float GetSaldo()
        {
           return serviciocaja.GetSaldoSistema();
        }

        public List<Egreso> GetEgresos() 
        {
            var t = servicioturno.GetOpenTurno();
            if (t!=null)
            {
                return egresorepository.GetEgresos(t.Id);
            }
            return null;
        }

        public void OpenCash()
        {
            ServicioFactura.OpenCash();
        }
    }
}
