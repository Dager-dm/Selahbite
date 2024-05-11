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
            Producto producto1 = new Producto("02", "Sopa", 12000, "Corriente");
            Producto producto2 = new Producto("03", "Salchipapa", 12000, "Corriente");
            Producto producto3 = new Producto("04", "Desayuno", 12000, "Corriente");
            Producto producto4 = new Producto("05", "Pechuga Asada", 12000, "Corriente");
            Producto producto5 = new Producto("06", "Costilla BBQ", 12000, "Corriente");
            Producto producto6 = new Producto("07", "Chivo", 12000, "Corriente");
            Producto producto7 = new Producto("08", "Sierra", 12000, "Corriente");
            Producto producto8 = new Producto("09", "Hamburguesa", 12000, "Corriente");
            lstproducts.Add(producto); lstproducts.Add(producto1); lstproducts.Add(producto2); lstproducts.Add(producto3); lstproducts.Add(producto4);
            lstproducts.Add(producto5); lstproducts.Add(producto6); lstproducts.Add(producto7); lstproducts.Add(producto8); lstproducts.Add(producto); lstproducts.Add(producto1); lstproducts.Add(producto2); lstproducts.Add(producto3); lstproducts.Add(producto4);
            lstproducts.Add(producto5); lstproducts.Add(producto6); lstproducts.Add(producto7); lstproducts.Add(producto8);
        }
    }
}
