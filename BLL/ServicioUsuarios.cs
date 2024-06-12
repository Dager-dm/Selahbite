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
        public bool ValidadoLog { get; set; }
        public bool ValidadoRep { get; set; }
        
        public ServicioUsuarios()
        {
            ValidadoLog=false;
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

        public string ValidarLogeo(Usuarios usuario)
        {
            LoadUsers();
            int bandera = 0; string ms = "";
            foreach (var item in LstUsuarios)
            {
                if (usuario.Username == item.Username)
                {
                    if (usuario.Password == item.Password)
                    {
                        ValidadoLog = true;
                        bandera = 1;
                        break;
                    }
                    else
                    {
                        bandera = 2;
                        break;
                    }
                }
                else
                {
                    bandera = 3;
                }
            }

            switch (bandera)
            {
                case 0:
                    ms = "Error Iniciando Sesión";
                    break;
                case 1:
                    ms = "Sesión Iniciada Correctamente";
                    break;
                case 2:
                    ms = "Contraseña Incorrecta";
                    break;
                case 3:
                    ms = "Usuario no registrado";
                    break;
            }
            return ms;
        }

        public string Validarrep(string username)
        {
            int c = 0;
            foreach (var item in LstUsuarios)
            {
                if (item.Username==username)
                {
                    c=1;
                    break;
                }
                
            }

            if (c==1)
            {
                ValidadoRep = false;
                return "Este usuario ya está registrado";
            }
            else
            {
                ValidadoRep = true;
                return "";
            }

        }
    }
}
