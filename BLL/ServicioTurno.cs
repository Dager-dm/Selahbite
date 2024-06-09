using DAL;
using ENTITY;
using Org.BouncyCastle.Asn1.Mozilla;
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

        public float Inequi=0;
        public float IBanco=0;
        public float IEfectivo = 0;
        public float ICredito = 0;
        public float IDaviplata = 0;
        public ServicioTurno()
        {

        }

        public void CreateTurno(Turno turno)
        {
            turnoAbierto = TurnosRepository.AbrirTurno(turno);


        }

        public bool EditTurno(Turno turno)
        {
           if(TurnosRepository.CerrarTurno(turno)==true)
           {
                turnoAbierto = null;
                return true;
           }
            else
            {
                return false;
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

        public Turno IsAnyTurnoOpen()
        {
            return TurnosRepository.IsAnyTurnoOpen();
        }

        public void SetOpenTurno(Turno turno)
        {
            turnoAbierto = turno;
        }

        public void DefinirIngreso(Turno turno)
        {
            foreach (var item in turno.Pedidos)
            {
                switch (item.MetodoPago.Id)
                {
                    case "1": IEfectivo = IEfectivo + item.Valor;
                        break;

                    case "2":
                        Inequi = Inequi + item.Valor;
                        break;
                    case "3":
                        IBanco = IBanco + item.Valor;
                        break;
                    case "4":
                        IDaviplata = IDaviplata + item.Valor;
                        break;
                    case "5":
                        ICredito = ICredito + item.Valor;
                        break;
                }
            }

        }
    }
}