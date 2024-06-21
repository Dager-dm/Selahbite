﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENTITY;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;

namespace DAL
{
    public class VistasRepository : SelahbiteDB
    {
        private OracleCommand oracleCommand;
        public VistasRepository()
        {

        }

        public List<VistaDeuda> GetCreditos()
        {
            try
            {

                List<VistaDeuda> lstVista = new List<VistaDeuda>();
                oracleCommand = new OracleCommand();
                oracleCommand.Connection = Conexion();
                AbrirConexion();

                oracleCommand.CommandText = "BEGIN :cursor := fn_obtener_creditos; END;";
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
                        lstVista.Add(MapVista(reader));
                    }
                }
                CerrarConexion();
                return lstVista;
            }
            catch (Exception e)
            {

                ExcepcionesTxtManager.SaveExcepctionTxt(e.Message);
                return null;
            }
        }

        private VistaDeuda MapVista(OracleDataReader reader)
        {
            VistaDeuda vista = new VistaDeuda();
            vista.Id_pedido = reader.GetInt64(0);
            vista.Fecha = reader.GetDateTime(1);
            vista.Id_Cliente = reader.GetInt32(2);
            vista.CedulaCliente = reader.GetString(3);
            vista.NombreCliente = reader.GetString(4);
            vista.Id_Turno = reader.GetInt64(5);
            vista.Horario = reader.GetString(6);
            vista.Id_Cajero = reader.GetInt64(7);
            vista.NombreCajero = reader.GetString(8);
            vista.Valor = reader.GetInt32(9);
            vista.Estado = reader.GetString(10);
            vista.Modalidad = reader.GetString(11) == "Contado" ? ModalidadDePago.Contado : ModalidadDePago.Credito;
            return vista;
        }

        public List<VistaVentas> GetVentasMensuales()
        {
            try
            {
                List<VistaVentas> lstVista = new List<VistaVentas>();
                oracleCommand = new OracleCommand();
                oracleCommand.Connection = Conexion();
                AbrirConexion();

                oracleCommand.CommandText = "BEGIN :cursor := fn_obtener_ventasmensuales; END;";
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
                        lstVista.Add(MapVistaVentas(reader, 1));
                    }
                }
                CerrarConexion();
                return lstVista;
            }
            catch (Exception e)
            {

                ExcepcionesTxtManager.SaveExcepctionTxt(e.Message);
                return null;
            }
        }

        public List<VistaVentas> GetVentasSemanales()
        {
            try
            {
                List<VistaVentas> lstVista = new List<VistaVentas>();
                oracleCommand = new OracleCommand();
                oracleCommand.Connection = Conexion();
                AbrirConexion();

                oracleCommand.CommandText = "BEGIN :cursor := fn_obtener_ventassemanales; END;";
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
                        lstVista.Add(MapVistaVentas(reader, 2));
                    }
                }
                CerrarConexion();
                return lstVista;
            }
            catch (Exception e)
            {

                ExcepcionesTxtManager.SaveExcepctionTxt(e.Message);
                return null;
            }
        }

        private VistaVentas MapVistaVentas(OracleDataReader reader, int i)
        {

            VistaVentas ventas = new VistaVentas();
            if (i == 2)
            {
                ventas.Fecha = reader.GetDateTime(0);
                ventas.VentaTotal = reader.GetInt64(1);
            }
            else
            {
                ventas.Mes = reader.GetString(0);
                ventas.VentaTotal = reader.GetInt64(1);
            }

            return ventas;
        
        }
    }
}
