using ENTITY;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ServicioTurno
    {
        private static List<Turno> turnos = new List<Turno>();

        public ServicioTurno()
        {
            turnosprueba();

        }

        private void turnosprueba()
        {
            Empleado empleado = new Empleado();
            empleado.Nombre = ("Jose Dolores Herazo Peñaranda");
            Turno turno = new Turno("Dia",new DateTime(2020, 12, 24), 4000, 5000, 3000, 4899,17212, 89322,"asa", empleado );
            Turno turno1 = new Turno("Nohce", new DateTime(2021, 12, 24), 4000, 5000, 3000, 4899, 17212, 89322, "aja", empleado);
            Turno turno2 = new Turno("Noche", new DateTime(2022, 12, 24), 4000, 5000, 3000, 4899, 17212, 89322, "asa", empleado);
            Turno turno3 = new Turno("Dia", new DateTime(2023, 12, 24), 4000, 5000, 3000, 4899, 17212, 89322, "aaskja", empleado);
            Turno turno4 = new Turno("Dia", DateTime.Now, 4000, 5000, 3000, 4899, 17212, 89322, "asa", empleado);
            turnos.Add( turno ); turnos.Add(turno1); turnos.Add(turno2); turnos.Add(turno3); turnos.Add(turno4);


        }

        public List<Turno> GetTurnos() { return turnos; }
    }
}
