using System;
using System.Collections.Generic;
using Oracle.ManagedDataAccess.Client;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using ENTITY;

namespace BLL
{
    public class ServicioMapeo
    {
        EgresosRepository egresosrepository = new EgresosRepository();

        PedidosRepository pedidosrepository = new PedidosRepository();

        EmpleadosRepository empleadorepository = new EmpleadosRepository();

        public ServicioMapeo()
        {
            
        }

        //public Turno MapTurno(OracleDataReader reader)
        //{
        //    Turno turno = new Turno();
        //    turno.Id = reader.GetInt64(0);
        //    turno.Horario = reader.GetString(1);
        //    turno.Fecha = reader.GetDateTime(2);
        //    turno.SaldoInicial = reader.GetInt64(3);
        //    turno.Ingreso = reader.GetInt64(4);
        //    turno.Egreso = reader.GetInt64(5);
        //    turno.SaldoReal = reader.GetInt64(6);
        //    turno.SaldoPrevisto = reader.GetInt64(7);
        //    turno.Diferencia = reader.GetInt64(8);
        //    turno.Observacion = reader.GetString(9);
        //    turno.Cajero = empleadorepository.LoadCajero(reader.GetString(10));
        //    turno.Estado = reader.GetString(11);
        //    turno.LstEgresos = egresosrepository.GetEgresos(turno);
        //    turno.Pedidos = pedidosrepository.GetPedidos(turno);
        //    return turno;
        //}


        //private Pedido MapPedido(OracleDataReader reader)
        //{
        //    Pedido pedido = new Pedido();
        //    pedido.Id = reader.GetInt64(0);
        //    pedido.MetodoPago = LoadMetodo(reader.GetString(1));
        //    pedido.Valor = reader.GetInt64(2);
        //    pedido.Turno = LoadTurno(reader.GetInt64(3));
        //    pedido.Cliente = LoadCliente(reader.GetInt64(4));
        //    pedido.Mesero = LoadMesero(reader.GetInt64(5));
        //    pedido.Estado = reader.GetString(6);
        //    pedido.Fecha = reader.GetDateTime(7);
        //    pedido.Detalles = detallesRepository.GetDetalles(pedido);
        //    return pedido;
        //}

        //private Turno LoadTurno(float idturno)
        //{
        //    oracleCommand = new OracleCommand();
        //    string oracle = "SELECT * FROM TURNOS WHERE id_turno = :idturno";
        //    oracleCommand.CommandText = oracle;
        //    oracleCommand.Parameters.Add(new OracleParameter("idturno", idturno));
        //    oracleCommand.Connection = Conexion();
        //    AbrirConexion();
        //    var reader = oracleCommand.ExecuteReader(); // select
        //    if (reader.Read())
        //    {
        //        return turnosRepository.MapTurno(reader);

        //    }
        //    CerrarConexion();

        //    return null;
        //}




        private MetodosPago MapMetodo(OracleDataReader reader)
        {
            MetodosPago metodo = new MetodosPago();
            metodo.Id = reader.GetString(0);
            metodo.Nombre = reader.GetString(1);
            return metodo;
        }
    }
}
