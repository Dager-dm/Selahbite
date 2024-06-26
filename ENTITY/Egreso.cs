using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY
{
    public class Egreso : Movimiento 
    {

        public Egreso() : base(DateTime.Now, 0)
        {

        }

        public Egreso( string recibidor, string descripcion, float Valor, DateTime Fecha, long id, string vuelto) : base (Fecha, Valor)
        {
            Recibidor = recibidor;
            Descripcion = descripcion;
            Id = id;
            Vuelto = vuelto;
        }
        public long Id { get; set; }
        public string Recibidor { get; set; }
        public string Descripcion { get; set; }
        public string Vuelto { get; set; }
    }
}
