﻿using ENTITY;
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
            oracleCommand.Parameters.Add("val", OracleDbType.Varchar2).Value = producto.Valor;
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


        public List<CategoriasProductos> GetCategories() 
        {
            List<CategoriasProductos> categories = new List<CategoriasProductos>();
            string oracle = "SELECT * FROM CATEGORIAS";

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

        private CategoriasProductos MapCategories(OracleDataReader reader)
        {
            CategoriasProductos category = new CategoriasProductos();
            category.Id = reader.GetChar(0);
            category.Nombre = reader.GetString(1);
            return category;
        }
    }
}
