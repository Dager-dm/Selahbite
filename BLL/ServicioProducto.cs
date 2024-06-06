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
      

        ProductosRepository productsRepository = new ProductosRepository();

        public ServicioProducto() 
        {     
          

            

        }

        public void AddProductos(Producto producto)
        {
            productsRepository.insert(producto);
        }

        public List<Producto> GetAllProducts() { 
        
         return productsRepository.GetProductos();
        }

        public void EditProducto(Producto OldProducto, Producto ModifiedProduct)
        {
            OldProducto.Nombre = ModifiedProduct.Nombre;
            OldProducto.Valor = ModifiedProduct.Valor;
            OldProducto.Categoria = ModifiedProduct.Categoria;
            productsRepository.Edit(OldProducto);
          
        }

        public void DeleteProducto(Producto Producto)
        {
            
        }

        public List<CategoriasProductos> GetCategoriasProductos()
        {
            return productsRepository.GetCategories();
        }

    }
}
