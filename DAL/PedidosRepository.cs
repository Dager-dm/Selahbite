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
    public class PedidosRepository: SelahbiteDB
    {

        EmpleadosRepository empleadosRepository = new EmpleadosRepository();
        DetallesRepository detallesRepository= new DetallesRepository();
        //TurnosRepository turnosRepository= new TurnosRepository();
        private OracleCommand oracleCommand;
        public PedidosRepository()
        {
            
        }
        public Pedido Insert(Pedido pedido, long idTurno)
        {
            oracleCommand = new OracleCommand();
            oracleCommand.Connection = Conexion();
            AbrirConexion();
            oracleCommand.CommandText = @"
                      DECLARE
                       idd NUMBER;
                       BEGIN
                        idd := fn_InsertPedido(:id_pago, :valor, :id_tur, :id_client, :id_empl, :es, :modalidad);
                       :return_value := idd;
                       END;";
            oracleCommand.CommandType = CommandType.Text;
            oracleCommand.Parameters.Add("id_pago", OracleDbType.Varchar2).Value = pedido.MetodoPago.Id;
            oracleCommand.Parameters.Add("valor", OracleDbType.Int32).Value = pedido.Valor;
            oracleCommand.Parameters.Add("id_tur", OracleDbType.Int32).Value = idTurno;
            oracleCommand.Parameters.Add("id_client", OracleDbType.Int32).Value = pedido.Cliente.Id;
            oracleCommand.Parameters.Add("id_empl", OracleDbType.Int32).Value = pedido.Mesero.Id;
            oracleCommand.Parameters.Add("es", OracleDbType.Varchar2).Value = pedido.Estado;
            oracleCommand.Parameters.Add("modalidad", OracleDbType.Varchar2).Value = pedido.ModalidadDePago.ToString(); ;

            OracleParameter returnParam = new OracleParameter("return_value", OracleDbType.Int32);
            returnParam.Direction = ParameterDirection.Output;
            oracleCommand.Parameters.Add(returnParam);

            oracleCommand.ExecuteNonQuery();

            if (returnParam.Value != DBNull.Value)
            {
                Oracle.ManagedDataAccess.Types.OracleDecimal oracleDecimal = (Oracle.ManagedDataAccess.Types.OracleDecimal)returnParam.Value;
                pedido.Id = oracleDecimal.ToInt32();
            }

            CerrarConexion();
            return pedido;
        }

        //public  List<Pedido> GetPedidos(Turno turno)
        //{
        //    List<Pedido> lstPedidos = new List<Pedido>();
        //    oracleCommand = new OracleCommand();
        //    oracleCommand.Connection = Conexion();
        //    AbrirConexion();
        //    oracleCommand.CommandText = "BEGIN :cursor := fn_obtener_pedidos(:idturno); END;";
        //    oracleCommand.CommandType = System.Data.CommandType.Text;
        //    OracleParameter cursor = new OracleParameter();
        //    cursor.ParameterName = "cursor";
        //    cursor.OracleDbType = OracleDbType.RefCursor;
        //    cursor.Direction = System.Data.ParameterDirection.Output;

        //    oracleCommand.Parameters.Add(cursor);
        //    oracleCommand.Parameters.Add("idturno", OracleDbType.Int32).Value = turno.Id; 


        //    oracleCommand.ExecuteNonQuery();

        //    using (OracleDataReader reader = ((OracleRefCursor)cursor.Value).GetDataReader())
        //    {
        //        while (reader.Read())
        //        {
        //            var p=MapPedido(reader);
        //            //p.Turno= turno;
        //            lstPedidos.Add(p);
        //        }
        //    }
        //    CerrarConexion();
        //    return lstPedidos;
        //}


        public List<Pedido> GetPedidos(long idTurno)
        {
            List<Pedido> lstPedidos = new List<Pedido>();
            oracleCommand = new OracleCommand();
            oracleCommand.Connection = Conexion();
            AbrirConexion();
            oracleCommand.CommandText = "BEGIN :cursor := fn_obtener_pedidos(:idturno); END;";
            oracleCommand.CommandType = System.Data.CommandType.Text;
            OracleParameter cursor = new OracleParameter();
            cursor.ParameterName = "cursor";
            cursor.OracleDbType = OracleDbType.RefCursor;
            cursor.Direction = System.Data.ParameterDirection.Output;

            oracleCommand.Parameters.Add(cursor);
            oracleCommand.Parameters.Add("idturno", OracleDbType.Int32).Value = idTurno; // Asegúrate de reemplazar "pedido.Id" con el valor correcto


            oracleCommand.ExecuteNonQuery();

            using (OracleDataReader reader = ((OracleRefCursor)cursor.Value).GetDataReader())
            {
                while (reader.Read())
                {
                    lstPedidos.Add(MapPedido(reader));
                }
            }
            CerrarConexion();
            return lstPedidos;
        }
        private  Pedido MapPedido(OracleDataReader reader)
        {
            Pedido pedido = new Pedido();
            pedido.Id = reader.GetInt64(8);
            pedido.ModalidadDePago = reader.GetString(7) == "Contado" ? ModalidadDePago.Contado : ModalidadDePago.Credito; //operador ternario
            pedido.MetodoPago = LoadMetodo(reader.GetString(0));
            pedido.Valor = reader.GetInt64(1);
            //pedido.Turno = LoadTurno(reader.GetInt64(3));
            pedido.Cliente=LoadCliente(reader.GetInt64(3));
            pedido.Mesero=LoadMesero(reader.GetInt64(4));
            pedido.Estado=reader.GetString(5);
            pedido.Fecha=reader.GetDateTime(6);
            pedido.Detalles = detallesRepository.GetDetalles(pedido.Id);
            return pedido;
        }

        private Empleado LoadMesero(long idEmpleado)
        {
            oracleCommand = new OracleCommand();
            string oracle = "SELECT * FROM EMPLEADOS WHERE id_empleado = :idEmpleado";
            oracleCommand.CommandText = oracle;
            oracleCommand.Parameters.Add(new OracleParameter("idEmpleado", idEmpleado));
            oracleCommand.Connection = Conexion();
            AbrirConexion();
            using (var reader = oracleCommand.ExecuteReader())
            {
                if (reader.Read())
                {
                    return empleadosRepository.MapEmpleado(reader);

                }
            }
            
            CerrarConexion();
            return null;
        }

        private Cliente LoadCliente(long idCliente)
        {
            oracleCommand = new OracleCommand();
            string oracle = "SELECT * FROM CLIENTES WHERE id_cliente = :idCliente";
            oracleCommand.CommandText = oracle;
            oracleCommand.Parameters.Add(new OracleParameter("idCliente", idCliente));
            oracleCommand.Connection = Conexion();
            AbrirConexion();
            using (var reader = oracleCommand.ExecuteReader())
            {
                if (reader.Read())
                {
                    return ClientesRepository.MapCliente(reader);

                }
            }
            CerrarConexion();
            return null;
        }

        private MetodosPago LoadMetodo(string idMetodo)
        {
            oracleCommand = new OracleCommand();
            string oracle = "SELECT * FROM FORMAPAGO WHERE id_formapago = :idMetodo";
            oracleCommand.CommandText = oracle;
            oracleCommand.Parameters.Add(new OracleParameter("idMetodo", idMetodo));
            oracleCommand.Connection = Conexion();
            AbrirConexion();
            using (var reader = oracleCommand.ExecuteReader())
            {
                if (reader.Read())
                {
                    return MapMetodo(reader);

                }
            }
            CerrarConexion();
            return null;
        }

        public List<MetodosPago> GetMetodos()
        {
            List<MetodosPago> lstMetodos = new List<MetodosPago>();
            oracleCommand = new OracleCommand();
            oracleCommand.Connection = Conexion();
            AbrirConexion();

            oracleCommand.CommandText = "BEGIN :cursor := fn_obtener_formapago; END;";
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
                    lstMetodos.Add(MapMetodo(reader));
                }
            }
            CerrarConexion();
            return lstMetodos;
        }

        private MetodosPago MapMetodo(OracleDataReader reader)
        {
            MetodosPago metodo = new MetodosPago();
            metodo.Id = reader.GetString(0);
            metodo.Nombre = reader.GetString(1);
            return metodo;
        }


        public void PagarDeuda(long idPedido, string idMetodo)
        {
            oracleCommand = new OracleCommand("pr_PagarPedido");
            oracleCommand.CommandType = CommandType.StoredProcedure;
            oracleCommand.Connection = Conexion();
            AbrirConexion();

            oracleCommand.Parameters.Add("metodo", OracleDbType.Varchar2).Value = idMetodo;
            oracleCommand.Parameters.Add("idped", OracleDbType.Long).Value = idPedido;

            oracleCommand.ExecuteNonQuery();

            CerrarConexion();

        }
    }
}
