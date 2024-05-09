using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENTITY;


namespace BLL
{
    public class ServicioProducto
    {
        public static List<Producto> lstproducts;

        public ServicioProducto() 
        {     
        lstproducts = new List<Producto>();
            productoprueba();

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

        private void productoprueba()
        {
            Producto producto = new Producto("01","Almuerzo",12000, "Corriente");
            lstproducts.Add(producto);
        }
    }
}
