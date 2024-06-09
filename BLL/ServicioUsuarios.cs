using DAL;
using ENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ServicioUsuarios
    {
        UsuarioRepository usuarioRepository = new UsuarioRepository();

        private static List<Usuarios> LstUsuarios;
        public bool Validado { get; set; }
        
        public ServicioUsuarios()
        {
            Validado=false;
            LstUsuarios = new List<Usuarios>();
            LoadUsers();
        }

        public void Insert(Usuarios usuario) 
        {
          usuarioRepository.insert(usuario);
        }

        public void UpdatePassword(Usuarios usuario) 
        {
          usuarioRepository.Changepwd(usuario);
        }

        public string LoadUsers()
        {
            LstUsuarios = usuarioRepository.GetUsuarios();
            return "";
        }

        public List<Usuarios> GetUsuarios()
        {
           
            return LstUsuarios;
        }

        public string Validar(Usuarios usuario)
        {
            int bandera = 0; string ms="";
            foreach (var item in LstUsuarios)
            {
                if (usuario.Username==item.Username)
                {
                    if (usuario.Password==item.Password)
                    {
                        Validado = true;
                        bandera = 1;
                        
                    } else { bandera = 2; }
                }
                else 
                { 
                    bandera = 3;
                }
            }
            
            switch (bandera)
            {
                case 0: ms = "Error Iniciando Sesión";
                    break;
                case 1: ms = "Sesión Iniciada Correctamente";
                    break;
                case 2: ms = "Contraseña Incorrecta";
                    break;
                case 3: ms = "Usuario no registrado";
                    break;
            }
            return ms ;
        }
    }
}
