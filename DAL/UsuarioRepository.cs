using ENTITY;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class UsuarioRepository:SelahbiteDB
    {
        private OracleCommand oracleCommand;
        public UsuarioRepository()
        {
            
        }
        public bool insert(Usuarios usuario)
        {
            oracleCommand = new OracleCommand("pr_InsertUsuario");
            oracleCommand.CommandType = CommandType.StoredProcedure;
            oracleCommand.Connection = Conexion();
            AbrirConexion();

            oracleCommand.Parameters.Add("useer", OracleDbType.Varchar2).Value = usuario.Username;
            oracleCommand.Parameters.Add("pwd", OracleDbType.Varchar2).Value = usuario.Password;


            var i = oracleCommand.ExecuteNonQuery();
            if (i > 0)
            {
                return true;
            }
            CerrarConexion();
            return false;
        }

        public bool Changepwd(Usuarios usuario)
        {
            oracleCommand = new OracleCommand("pr_ChangePwd");
            oracleCommand.CommandType = CommandType.StoredProcedure;
            oracleCommand.Connection = Conexion();
            AbrirConexion();

            oracleCommand.Parameters.Add("iduser", OracleDbType.Varchar2).Value = usuario.Id;
            oracleCommand.Parameters.Add("newpwd", OracleDbType.Varchar2).Value = usuario.Password;


            var i = oracleCommand.ExecuteNonQuery();
            if (i > 0)
            {
                return true;
            }
            CerrarConexion();
            return false;
        }

        public List<Usuarios> GetUsuarios()
        {
            oracleCommand = new OracleCommand();
            List<Usuarios> lstUsuarios = new List<Usuarios>();
            oracleCommand.Connection = Conexion();
            AbrirConexion();

            oracleCommand.CommandText = "BEGIN :cursor := fn_obtener_usuarios; END;";
            oracleCommand.CommandType = System.Data.CommandType.Text;

            OracleParameter cursor = new OracleParameter();
            cursor.ParameterName = "cursor";
            cursor.OracleDbType = OracleDbType.RefCursor;
            cursor.Direction = System.Data.ParameterDirection.Output;

            oracleCommand.Parameters.Add(cursor);

            oracleCommand.ExecuteNonQuery();

            using (OracleDataReader reader = ((OracleRefCursor)cursor.Value).GetDataReader())
            {
                while (reader.Read())
                {
                    lstUsuarios.Add(MapUsuario(reader));
                }
            }

            CerrarConexion();
            return lstUsuarios;
        }

        private Usuarios MapUsuario(OracleDataReader reader)
        {
            Usuarios usuario = new Usuarios();
            usuario.Id = reader.GetInt16(0);
            usuario.Username= reader.GetString(1);
            usuario.Password= reader.GetString(2);
            return usuario;
        }
    }
}
