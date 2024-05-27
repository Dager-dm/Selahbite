using ENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ServicioPedido
    {

        public ServicioPedido()
        {
            
        }


        public void AddPedido(Pedido pedido)
        {
          //enviar el pedido al repositorio
        }

        public List<Pedido> GetAllPedidos() 
        {
            List<Pedido> pedidos = new List<Pedido>();

            return pedidos;
        }

    }

}
