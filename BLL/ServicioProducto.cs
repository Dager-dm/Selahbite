using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENTITY;
using DAL;


namespace BLL
{
    public class ServicioProducto
    {
       private static List<Producto> lstproducts;
        ProductosRepository productsRepository = new ProductosRepository();

        public ServicioProducto() 
        {     
           lstproducts = new List<Producto>();
            

        }

        public void AddProductos(Producto productos)
        {
            lstproducts.Add(productos);
        }

        public List<Producto> GetAllProducts() { 
        
         return lstproducts;
        }

        public void EditProducto(Producto OldProducto, Producto ModifiedProduct)
        {
            OldProducto.Nombre = ModifiedProduct.Nombre;
            OldProducto.Valor = ModifiedProduct.Valor;
            OldProducto.Categoria = ModifiedProduct.Categoria;
          
        }

        public void DeleteProducto(Producto Producto)
        {
            lstproducts.Remove(Producto);
        }

        public List<CategoriasProductos> GetCategoriasProductos()
        {
            return productsRepository.GetCategories();
        }

    }
}
