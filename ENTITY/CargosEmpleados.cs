﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY
{
    public  class CargosEmpleados
    {
        public CargosEmpleados()
        {
            
        }
        public CargosEmpleados(string nombre, string id)
        {
            Nombre = nombre;
            Id = id;
        }

        public string Nombre { get; set; }

        public string Id { get; set; }
    }
}