using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ServicioCaja
    {
        private static float Saldo; 
        public ServicioCaja() { }

        public void AsignarSaldoBase(float saldo)
        {
            Saldo = saldo;
        }

        public void RestarEgreso(float egreso) 
        {
            Saldo = Saldo-egreso;
        }

        public void SumarIngreso(float ingreso)
        {
            Saldo=Saldo+ingreso;
        }

        public float GetSaldoSistema() 
        {
            return Saldo;
        }


    }
}
