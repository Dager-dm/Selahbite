using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY
{
    public class Cliente : Persona
    {
        public Cliente() { }
        public Cliente(string Cedula, string nombre, string id, string telefono, float saldo) : base(Cedula,nombre, id, telefono)
        {
         Saldo = saldo;
        }

        public float Saldo { get; set; }
    }
}
