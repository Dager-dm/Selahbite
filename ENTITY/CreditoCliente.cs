using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY
{
    public class CreditoCliente : Debito
    {
        public CreditoCliente(DateTime fecha, string cajero, string horario, float valor, string estado) : base(fecha, cajero, horario, valor, estado)       
        {
        
        }
    }
}
