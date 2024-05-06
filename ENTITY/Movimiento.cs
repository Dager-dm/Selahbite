﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY
{
    public class Movimiento
    {
        public Movimiento(DateTime fecha, string cajero, string horario, float valor) 
        {
            Fecha = fecha;
            Cajero = cajero;
            Horario = horario;
            Valor = valor;
        }
        public DateTime Fecha { get; set; }
        public string Cajero { get; set;}
        public string Horario { get; set;}  
        public float Valor { get; set;}

    }
}