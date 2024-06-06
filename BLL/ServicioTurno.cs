using DAL;
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
        TurnosRepository TurnosRepository = new TurnosRepository();
        
        public static Turno turnoAbierto;
        public ServicioTurno()
        {

        }

        public void CreateTurno(Turno turno)
        {
            turnoAbierto = TurnosRepository.AbrirTurno(turno);


        }

        public string EditTurno(Turno turno)
        {
           if(TurnosRepository.CerrarTurno(turno)==true)
           {
                turnoAbierto = null;
                return "Turno cerrado correctamente";
           }
            else
            {
                return "Error al cerrar el turno";
            }
        }

        public List<Turno> GetTurnos()
        {
            return TurnosRepository.GetTurnos();
        }

        public Turno GetOpenTurno()
        {
            return turnoAbierto;
        }
    }
}