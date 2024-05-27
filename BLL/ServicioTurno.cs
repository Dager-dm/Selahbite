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

        }

        public void CreateTurno(Turno turno)
        {

           
        }

        public void EditTurno(Turno turno)
        {

        }

        public List<Turno> GetTurnos()
        {
            return turnos;
        }

        public Turno GetOpenTurno()
        {
            Turno turno = null;

            return turno;
        }
    }
}