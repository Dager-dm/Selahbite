using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using ENTITY;

namespace BLL
{
    public class ServicioDetalles
    {
        DetallesRepository detallesRepository = new DetallesRepository();
        public ServicioDetalles()
        {
            
        }

        public void AddDetalle(DetallePedido detalle)
        {
            detallesRepository.Insert(detalle);
        }

        public List<DetallePedido> GetDetalles(long Idpedido)
        {
            return detallesRepository.GetDetalles(Idpedido);
        }
    }
}
