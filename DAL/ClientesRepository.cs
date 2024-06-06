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


        public bool Edit(Cliente cliente)
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

        public bool Delete(Cliente cliente)
        {


            return false;
        }
  
        

        public bool EditSaldo(Cliente cliente)
        {
            oracleCommand = new OracleCommand("pr_EditSaldoCliente");
            oracleCommand.CommandType = CommandType.StoredProcedure;
            oracleCommand.Connection = Conexion();
            AbrirConexion();

            oracleCommand.Parameters.Add("idclient", OracleDbType.Varchar2).Value = cliente.Id;
            oracleCommand.Parameters.Add("sald", OracleDbType.Decimal).Value = cliente.Saldo;

            // pr_EditSaldoCliente( idclient CLIENTES.id_cliente%type, sald CLIENTES.saldo%type)
            var i = oracleCommand.ExecuteNonQuery();
            if (i > 0)
            {
                return true;
            }
            CerrarConexion();

            return false;
        }

        public static Cliente MapCliente(OracleDataReader reader)
        {
            Cliente client = new Cliente();
            client.Id = reader.GetInt64(0);
            client.Cedula = reader.GetString(1);
            client.Nombre = reader.GetString(2);
            client.Telefono = reader.GetString(3);
            client.Saldo = reader.GetFloat(4);
            return client;
        }

        public List<Cliente> GetClientess()
        {
            List<Cliente> lstClientes = new List<Cliente>();
            oracleCommand = new OracleCommand();
            oracleCommand.Connection= Conexion();
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

        


    }
}
