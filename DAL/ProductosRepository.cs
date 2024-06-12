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
            string oracle = "SELECT * FROM PRODUCTOS";
            oracleCommand.CommandText = oracle;
            oracleCommand.Connection = Conexion();
            AbrirConexion();
            var reader = oracleCommand.ExecuteReader(); 
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

            var i = oracleCommand.ExecuteNonQuery();
            if (i > 0)
            {
                return true;
            }
            CerrarConexion();

            return false;
        }

        public bool Delete(Producto producto)
        {
            oracleCommand = new OracleCommand("pr_DeleteProducto");
            oracleCommand.CommandType = CommandType.StoredProcedure;
            oracleCommand.Connection = Conexion();
            AbrirConexion();

            oracleCommand.Parameters.Add("idproduct", OracleDbType.Varchar2).Value = producto.Id;

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
            producto.Id = reader.GetInt64(3);
            producto.Nombre = reader.GetString(0);
            producto.Valor = reader.GetFloat(1);
            producto.Categoria=LoadCategoria(reader.GetString(2));
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
            var reader = oracleCommand.ExecuteReader(); 
            if (reader.Read())
            {
                return MapCategories(reader);

            }
            CerrarConexion();
         
            return null;
        }

       
    }
}
