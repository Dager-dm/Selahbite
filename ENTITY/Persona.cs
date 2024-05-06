﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY
{
    public class Persona
    {
        public Persona(string nombre, string id, string telefono, float sueldo) 
        {
            Nombre = nombre;
            Id = id;
            Telefono = telefono;
            Sueldo = sueldo;
        }

        public string Nombre { get; set;}
        public string Id { get; set; }
        public string Telefono { get; set; }
        public float Sueldo { get; set;}
    }
}