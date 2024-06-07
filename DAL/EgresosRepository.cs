﻿using ENTITY;
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
    public class EgresosRepository:SelahbiteDB
    {
        private OracleCommand oracleCommand;
        public EgresosRepository()
        {
            
        }

        public bool insert(Egreso egreso, Turno turno)
        {
            oracleCommand = new OracleCommand("pr_InsertEgreso");
            oracleCommand.CommandType = CommandType.StoredProcedure;
            oracleCommand.Connection = Conexion();
            AbrirConexion();

            oracleCommand.Parameters.Add("rec", OracleDbType.Varchar2).Value = egreso.Recibidor;
            oracleCommand.Parameters.Add("descrip", OracleDbType.Varchar2).Value = egreso.Descripcion;
            oracleCommand.Parameters.Add("id_tur", OracleDbType.Varchar2).Value = turno.Id;
            //pr_InsertEgreso(rec EGRESOS.recibidor%type, descrip EGRESOS.descripcion%type, id_tur EGRESOS.id_turno%type)
            // Ejecuta el procedimiento
            var i = oracleCommand.ExecuteNonQuery();
            if (i > 0)
            {
                return true;
            }
            CerrarConexion();
            return false;
        }


        public List<Egreso> GetEgresos(Turno turno)
        {
            List<Egreso> lstEgresos = new List<Egreso>();
            oracleCommand = new OracleCommand();
            oracleCommand.Connection = Conexion();
            AbrirConexion();
            oracleCommand.CommandText = "BEGIN :cursor := fn_obtener_egresos(:id_turn); END;";
            oracleCommand.CommandType = System.Data.CommandType.Text;
            OracleParameter cursor = new OracleParameter();
            cursor.ParameterName = "cursor";
            cursor.OracleDbType = OracleDbType.RefCursor;
            cursor.Direction = System.Data.ParameterDirection.Output;

            oracleCommand.Parameters.Add(cursor);
            oracleCommand.Parameters.Add("id_turn", OracleDbType.Int32).Value = turno.Id; // Asegúrate de reemplazar "pedido.Id" con el valor correcto


            oracleCommand.ExecuteNonQuery();

            using (OracleDataReader reader = ((OracleRefCursor)cursor.Value).GetDataReader())
            {
                while (reader.Read())
                {
                    lstEgresos.Add(MapEgresos(reader));
                }
            }
            CerrarConexion();
            return lstEgresos;

        }
        public List<Egreso> GetEgresos(long idTurno)
        {
            List<Egreso> lstEgresos = new List<Egreso>();
            oracleCommand = new OracleCommand();
            oracleCommand.Connection = Conexion();
            AbrirConexion();
            oracleCommand.CommandText = "BEGIN :cursor := fn_obtener_egresos(:id_turn); END;";
            oracleCommand.CommandType = System.Data.CommandType.Text;
            OracleParameter cursor = new OracleParameter();
            cursor.ParameterName = "cursor";
            cursor.OracleDbType = OracleDbType.RefCursor;
            cursor.Direction = System.Data.ParameterDirection.Output;

            oracleCommand.Parameters.Add(cursor);
            oracleCommand.Parameters.Add("id_turn", OracleDbType.Int32).Value = idTurno; // Asegúrate de reemplazar "pedido.Id" con el valor correcto


            oracleCommand.ExecuteNonQuery();

            using (OracleDataReader reader = ((OracleRefCursor)cursor.Value).GetDataReader())
            {
                while (reader.Read())
                {
                    lstEgresos.Add(MapEgresos(reader));
                }
            }
            CerrarConexion();
            return lstEgresos;

        }
        private static Egreso MapEgresos(OracleDataReader reader)
        {
            Egreso egreso = new Egreso();
            egreso.Id = reader.GetInt64(0);
            egreso.Recibidor = reader.GetString(1);
            egreso.Descripcion = reader.GetString(2);
            return egreso;
        }
    }
}