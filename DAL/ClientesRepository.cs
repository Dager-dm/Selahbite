using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENTITY;
using System.Data;
using System.Runtime.ConstrainedExecution;
using Oracle.ManagedDataAccess.Types;

namespace DAL
{
    public class ClientesRepository : SelahbiteDB
    {
        private OracleCommand oracleCommand;
        public ClientesRepository()
        {
            //oracleCommand = new OracleCommand();
        }


        public bool insert(Cliente cliente)
        {
            try
            {
                oracleCommand = new OracleCommand("pr_InsertClient");
                oracleCommand.CommandType = CommandType.StoredProcedure;
                oracleCommand.Connection = Conexion();
                AbrirConexion();

                oracleCommand.Parameters.Add("nomb", OracleDbType.Varchar2).Value = cliente.Nombre;
                oracleCommand.Parameters.Add("ced", OracleDbType.Varchar2).Value = cliente.Cedula;
                oracleCommand.Parameters.Add("tel", OracleDbType.Varchar2).Value = cliente.Telefono;
                oracleCommand.Parameters.Add("sald", OracleDbType.Long).Value = cliente.Saldo;
                //pr_InsertValuesClientes(nomb CLIENTES.nombre % type, ced CLIENTES.cedula % type, tel CLIENTES.telefono % type, sald CLIENTES.saldo % type)
                // Ejecuta el procedimiento
                var i = oracleCommand.ExecuteNonQuery();
                if (i > 0)
                {
                    return true;
                }
                CerrarConexion();
                return false;
            }
            catch (Exception e)
            {
                ExcepcionesTxtManager.SaveExcepctionTxt(e.Message);
                return false;
            }
        }

        public bool Edit(Cliente cliente)
        {
            try
            {
                oracleCommand = new OracleCommand("pr_EditCliente");
                oracleCommand.CommandType = CommandType.StoredProcedure;
                oracleCommand.Connection = Conexion();
                AbrirConexion();

                oracleCommand.Parameters.Add("nomb", OracleDbType.Varchar2).Value = cliente.Nombre;
                oracleCommand.Parameters.Add("ced", OracleDbType.Varchar2).Value = cliente.Cedula;
                oracleCommand.Parameters.Add("tel", OracleDbType.Varchar2).Value = cliente.Telefono;
                oracleCommand.Parameters.Add("idclient", OracleDbType.Varchar2).Value = cliente.Id;

                // pr_EditCliente(nomb CLIENTES.nombre%type, ced CLIENTES.cedula%type, tel CLIENTES.telefono%type, idclient CLIENTES.id_cliente%type)
                var i = oracleCommand.ExecuteNonQuery();
                if (i > 0)
                {
                    return true;
                }
                CerrarConexion();

                return false;
            }
            catch (Exception e)
            {
                ExcepcionesTxtManager.SaveExcepctionTxt(e.Message);
                return false;
            }
        }

        public bool Delete(Cliente cliente)
        {
            try
            {
                oracleCommand = new OracleCommand("pr_DeleteCliente");
                oracleCommand.CommandType = CommandType.StoredProcedure;
                oracleCommand.Connection = Conexion();
                AbrirConexion();

                oracleCommand.Parameters.Add("idclient", OracleDbType.Varchar2).Value = cliente.Id;

                var i = oracleCommand.ExecuteNonQuery();
                if (i > 0)
                {
                    return true;
                }
                CerrarConexion();

                return false;
            }
            catch (Exception e)
            {
                ExcepcionesTxtManager.SaveExcepctionTxt(e.Message);
                return false;
            }
        }
  
        public static Cliente MapCliente(OracleDataReader reader)
        {
            Cliente client = new Cliente();
            client.Id = reader.GetInt64(4);
            client.Cedula = reader.GetString(0);
            client.Nombre = reader.GetString(1);
            client.Telefono = reader.GetString(2);
            client.Saldo = reader.GetFloat(3);
            return client;
        }

        public List<Cliente> GetClientess()
        {
            try
            {
                List<Cliente> lstClientes = new List<Cliente>();
                oracleCommand = new OracleCommand();
                oracleCommand.Connection = Conexion();
                AbrirConexion();

                oracleCommand.CommandText = "BEGIN :cursor := fn_obtener_clientes; END;";
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
                        lstClientes.Add(MapCliente(reader));
                    }
                }
                CerrarConexion();
                return lstClientes;
            }
            catch (Exception e)
            {
                ExcepcionesTxtManager.SaveExcepctionTxt(e.Message);
                return null;
            }
        }

        
    }
}
