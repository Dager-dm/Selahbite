using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY
{
    public class Productos
    {
        public Productos(string id, string nombre, float valor, string categoria) 
        {
            Id = id;
            Nombre = nombre;
            Valor = valor;
            Categoria = categoria;
        }
        public string Id { get; set; }
        public string Nombre { get; set; }
        public float Valor { get; set; } 
        public string Categoria { get; set; }
    }
}
