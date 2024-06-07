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
    public class TurnosRepository: SelahbiteDB
    {
        EmpleadosRepository EmpleadosRepository = new EmpleadosRepository();
        PedidosRepository PedidosRepository = new PedidosRepository();
        EgresosRepository EgresosRepository = new EgresosRepository();
        private OracleCommand oracleCommand;
        public TurnosRepository()
        {
            
        }

        public Turno  AbrirTurno(Turno turno)
        {
            oracleCommand = new OracleCommand();
            oracleCommand.Connection = Conexion();
            AbrirConexion();
            oracleCommand.CommandText= @"
                      DECLARE
                       idd NUMBER;
                       BEGIN
                        idd := fn_AbrirTurno(:hor, :cajero, :saldo, :es);
                       :return_value := idd;
                       END;";
            oracleCommand.CommandType = CommandType.Text;
            oracleCommand.Parameters.Add("hor", OracleDbType.Varchar2).Value = turno.Horario;
            oracleCommand.Parameters.Add("cajero", OracleDbType.Int32).Value = turno.Cajero.Id;
            oracleCommand.Parameters.Add("saldo", OracleDbType.Int32).Value = turno.SaldoInicial;
            oracleCommand.Parameters.Add("es", OracleDbType.Varchar2).Value = turno.Estado;

            OracleParameter returnParam = new OracleParameter("return_value", OracleDbType.Int32);
            returnParam.Direction = ParameterDirection.Output;
            oracleCommand.Parameters.Add(returnParam);

            oracleCommand.ExecuteNonQuery();

            if (returnParam.Value != DBNull.Value)
            {
                Oracle.ManagedDataAccess.Types.OracleDecimal oracleDecimal = (Oracle.ManagedDataAccess.Types.OracleDecimal)returnParam.Value;
                turno.Id = oracleDecimal.ToInt32();  
            }

            CerrarConexion();
            return turno ;
        }

        public bool CerrarTurno(Turno turno)
        {
            oracleCommand = new OracleCommand("pr_CerrarTurno");
            oracleCommand.CommandType = CommandType.StoredProcedure;
            oracleCommand.Connection = Conexion();
            AbrirConexion();

            oracleCommand.Parameters.Add("idtur", OracleDbType.Long).Value = turno.Id;
            oracleCommand.Parameters.Add("ingreso", OracleDbType.Long).Value = turno.Ingreso;
            oracleCommand.Parameters.Add("egreso", OracleDbType.Long).Value = turno.Egreso;
            oracleCommand.Parameters.Add("saldoU", OracleDbType.Long).Value = turno.SaldoReal;
            oracleCommand.Parameters.Add("saldoS", OracleDbType.Long).Value = turno.SaldoPrevisto;
            oracleCommand.Parameters.Add("dif", OracleDbType.Long).Value = turno.Diferencia;
            oracleCommand.Parameters.Add("obs", OracleDbType.Varchar2).Value = turno.Observacion;
            OracleParameter returnParam = new OracleParameter("p_filas_afectadas", OracleDbType.Int32);
            returnParam.Direction = ParameterDirection.Output;
            oracleCommand.Parameters.Add(returnParam);
            //pr_CerrarTurno(idtur TURNOS.id_turno%type, ingreso TURNOS.ingreso_total%type, egreso TURNOS.egreso_total%type, saldoU TURNOS.saldo_usuario%type, saldoS TURNOS.saldo_sistema%type, dif TURNOS.diferencia%type, obs TURNOS.observacion%type )
            oracleCommand.ExecuteNonQuery();

            if (returnParam.Value != DBNull.Value)
            {
                Oracle.ManagedDataAccess.Types.OracleDecimal oracleDecimal = (Oracle.ManagedDataAccess.Types.OracleDecimal)returnParam.Value;
                var filas = oracleDecimal.ToInt32();
                if (filas>0)
                {
                    return true;
                }
            }

            CerrarConexion();
            return false;
        }


        public List<Turno> GetTurnos()
        {
            List<Turno> lstTurnos = new List<Turno>();
            oracleCommand = new OracleCommand();
            oracleCommand.Connection = Conexion();
            AbrirConexion();

            oracleCommand.CommandText = "BEGIN :cursor := fn_obtener_turnos; END;";
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
                    lstTurnos.Add(MapTurno(reader));
                }
            }
            CerrarConexion();
            return lstTurnos;
        }

        public Turno MapTurno(OracleDataReader reader)
        {
            Turno turno = new Turno();
            turno.Id = reader.GetInt64(0);
            turno.Horario = reader.GetString(1);
            turno.Fecha = reader.GetDateTime(2);
            turno.SaldoInicial = reader.GetInt64(3);
            turno.Ingreso= reader.GetInt64(4);
            turno.Egreso= reader.GetInt64(5);
            turno.SaldoReal = reader.GetInt64(6);
            turno.SaldoPrevisto = reader.GetInt64(7);
            turno.Diferencia = reader.GetInt64(8);
            turno.Observacion = reader.GetString(9); 
            turno.Cajero=LoadCajero(reader.GetString(10));
            turno.Estado = reader.GetString(11);
            turno.LstEgresos = EgresosRepository.GetEgresos(turno.Id);
            turno.Pedidos = PedidosRepository.GetPedidos(turno.Id);
            
            return turno;
        }

        private Empleado LoadCajero(string idEmpleado)
        {
            oracleCommand = new OracleCommand();
            string oracle = "SELECT * FROM EMPLEADOS WHERE id_empleado = :idEmpleado";
            oracleCommand.CommandText = oracle;
            oracleCommand.Parameters.Add(new OracleParameter("idEmpleado", idEmpleado));
            oracleCommand.Connection = Conexion();
            AbrirConexion();
            var reader = oracleCommand.ExecuteReader(); // select
            if (reader.Read())
            {
                return EmpleadosRepository.MapEmpleado(reader);

            }
            CerrarConexion();

            return null;
        }

        


    }
}
