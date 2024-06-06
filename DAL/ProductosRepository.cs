using ENTITY;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ProductosRepository : SelahbiteDB
    {
        private OracleCommand oracleCommand;
        public ProductosRepository()
        {
            
        }

        public bool insert(Producto producto)
        {
            oracleCommand = new OracleCommand("pr_InsertProducto");
            oracleCommand.CommandType = CommandType.StoredProcedure;
            oracleCommand.Connection = Conexion();
            AbrirConexion();

            oracleCommand.Parameters.Add("nomb", OracleDbType.Varchar2).Value = producto.Nombre;
            oracleCommand.Parameters.Add("val", OracleDbType.Long).Value = producto.Valor;
            oracleCommand.Parameters.Add("id_cat", OracleDbType.Varchar2).Value = producto.Categoria.Id;
            // pr_InsertProducto(nomb PRODUCTOS.nombre%type, val PRODUCTOS.valor%type, id_cat PRODUCTOS.id_categoria%type)
            // Ejecuta el procedimiento
            var i = oracleCommand.ExecuteNonQuery();
            if (i > 0)
            {
                return true;
            }
            CerrarConexion();
            return false;
        }

        public List<Producto> GetProductos() 
        {
            oracleCommand = new OracleCommand();
            List<Producto> lstProductos = new List<Producto>();
            string oracle = "SELECT id_producto, nombre, valor, id_categoria FROM PRODUCTOS";
            oracleCommand.CommandText = oracle;
            oracleCommand.Connection = Conexion();
            AbrirConexion();
            var reader = oracleCommand.ExecuteReader(); //select
            while (reader.Read())
            {
                lstProductos.Add(MapProducto(reader));
            }
            CerrarConexion();
            return lstProductos;


        }

        public bool Edit(Producto producto)
        {
            oracleCommand = new OracleCommand("pr_EditProducto");
            oracleCommand.CommandType = CommandType.StoredProcedure;
            oracleCommand.Connection = Conexion();
            AbrirConexion();

            oracleCommand.Parameters.Add("nomb", OracleDbType.Varchar2).Value = producto.Nombre;
            oracleCommand.Parameters.Add("val", OracleDbType.Varchar2).Value = producto.Valor;
            oracleCommand.Parameters.Add("id_cat", OracleDbType.Varchar2).Value = producto.Categoria.Id;
            oracleCommand.Parameters.Add("idproduct", OracleDbType.Varchar2).Value = producto.Id;

            // pr_EditProducto(nomb PRODUCTOS.nombre%type, val PRODUCTOS.valor%type, id_cat PRODUCTOS.id_categoria%type, idproduct PRODUCTOS.id_producto%type)
            var i = oracleCommand.ExecuteNonQuery();
            if (i > 0)
            {
                return true;
            }
            CerrarConexion();

            return false;
        }






        public Producto MapProducto(OracleDataReader reader)
        {
            Producto producto = new Producto();
            producto.Id = reader.GetInt64(0);
            producto.Nombre = reader.GetString(1);
            producto.Valor = reader.GetFloat(2);
            producto.Categoria=LoadCategoria(reader.GetString(3));
            return producto;
        }

        public List<CategoriasProductos> GetCategories() 
        {
            List<CategoriasProductos> categories = new List<CategoriasProductos>();
            string oracle = "SELECT * FROM CATEGORIAS";
            oracleCommand = new OracleCommand();
            oracleCommand.CommandText = oracle;
            oracleCommand.Connection = Conexion();
            AbrirConexion();
            var reader = oracleCommand.ExecuteReader(); //select
            while (reader.Read())
            {
                categories.Add(MapCategories(reader));
            }
            CerrarConexion();
            return categories;
        }

        private  CategoriasProductos MapCategories(OracleDataReader reader)
        {
            CategoriasProductos category = new CategoriasProductos();
            category.Id = reader.GetString(0);
            category.Nombre = reader.GetString(1);
            return category;
        }

        private  CategoriasProductos LoadCategoria(string idCategoria)
        {
            oracleCommand = new OracleCommand();
            string oracle = "SELECT * FROM CATEGORIAS WHERE id_categoria = :idCategoria";
            oracleCommand.CommandText = oracle;
            oracleCommand.Parameters.Add(new OracleParameter("idCategoria", idCategoria));
            oracleCommand.Connection = Conexion();
            AbrirConexion();
            var reader = oracleCommand.ExecuteReader(); // select
            if (reader.Read())
            {
                return MapCategories(reader);

            }
            CerrarConexion();
         
            return null;
        }

       
    }
}
