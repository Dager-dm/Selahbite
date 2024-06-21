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
    
    public class DetallesRepository:SelahbiteDB
    {

        private OracleCommand oracleCommand;
        ProductosRepository productosrepository=new ProductosRepository();


        public DetallesRepository()
        {
            
        }

        public bool Insert(DetallePedido detalle)
        {
            try
            {
                oracleCommand = new OracleCommand("pr_InsertDetallePedido");
                oracleCommand.CommandType = CommandType.StoredProcedure;
                oracleCommand.Connection = Conexion();
                AbrirConexion();

                oracleCommand.Parameters.Add("id_Product", OracleDbType.Int32).Value = detalle.Producto.Id;
                oracleCommand.Parameters.Add("cant", OracleDbType.Int16).Value = detalle.Cantidad;
                oracleCommand.Parameters.Add("valor", OracleDbType.Int32).Value = detalle.ValorProductoVendido;
                oracleCommand.Parameters.Add("id_ped", OracleDbType.Int32).Value = detalle.Pedido.Id;
                oracleCommand.Parameters.Add("valoru", OracleDbType.Int32).Value = detalle.valorUnitario;
                // pr_InsertDetallePedido(id_Product DETALLEPEDIDO.id_producto%type, cant DETALLEPEDIDO.cantidad%type, valor DETALLEPEDIDO.valor_productovendido%type, id_ped DETALLEPEDIDO.id_pedido%TYPE)
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


        public List<DetallePedido> GetDetalles(long Idpedido) 
        {

            try
            {
                List<DetallePedido> lstDetalles = new List<DetallePedido>();
                oracleCommand = new OracleCommand();
                oracleCommand.Connection = Conexion();
                AbrirConexion();
                oracleCommand.CommandText = "BEGIN :cursor := fn_obtener_detallepedido(:id_ped); END;";
                oracleCommand.CommandType = System.Data.CommandType.Text;
                OracleParameter cursor = new OracleParameter();
                cursor.ParameterName = "cursor";
                cursor.OracleDbType = OracleDbType.RefCursor;
                cursor.Direction = System.Data.ParameterDirection.Output;
                oracleCommand.Parameters.Add(cursor);
                oracleCommand.Parameters.Add("id_ped", OracleDbType.Int32).Value = Idpedido;


                oracleCommand.ExecuteNonQuery();

                using (OracleDataReader reader = ((OracleRefCursor)cursor.Value).GetDataReader())
                {
                    while (reader.Read())
                    {
                        lstDetalles.Add(MapDetalle(reader));
                    }
                }
                CerrarConexion();
                return lstDetalles;
            }
            catch (Exception e)
            {
                ExcepcionesTxtManager.SaveExcepctionTxt(e.Message);
                return null;
            }

        }

        private DetallePedido MapDetalle(OracleDataReader reader)
        {
            DetallePedido detalle = new DetallePedido();
            detalle.Producto = LoadProducto(reader.GetInt64(0));
            detalle.Cantidad = reader.GetInt16(1);
            detalle.ValorProductoVendido = reader.GetInt32(2);
            detalle.Id = reader.GetInt64(4);
            detalle.valorUnitario=reader.GetInt64(5);
            return detalle;

        }

        private Producto LoadProducto(long idproducto)
        {
            try
            {
                oracleCommand = new OracleCommand();
                string oracle = "SELECT * FROM PRODUCTOS WHERE id_producto = :id";
                oracleCommand.CommandText = oracle;
                oracleCommand.Parameters.Add(new OracleParameter("id", idproducto));
                oracleCommand.Connection = Conexion();
                AbrirConexion();
                using (var reader = oracleCommand.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return productosrepository.MapProducto(reader);

                    }
                }
                CerrarConexion();
                return null;
            }
            catch (Exception e)
            {
                ExcepcionesTxtManager.SaveExcepctionTxt(e.Message);
                return null;
            }
        }


    }
}
