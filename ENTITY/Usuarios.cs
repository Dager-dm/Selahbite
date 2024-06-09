using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY
{
    public class Usuarios
    {
        public Usuarios()
        {
            
        }

        public Usuarios(string username, string password, int id)
        {
            Username = username;
            Password = password;
            Id = id;
        }

        public String Username { get; set; }

        public String Password { get; set; }

        public int Id { get; set; }
    }
}
