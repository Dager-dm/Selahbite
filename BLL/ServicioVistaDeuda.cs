using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENTITY;

namespace BLL
{
    public class ServicioVistaDeuda
    {
        VistaDeudasRepository vistarepository = new VistaDeudasRepository();
        DetallesRepository detallesRepository= new DetallesRepository();
        ServicioPedido serviciopedido = new ServicioPedido();
        ServicioCaja servicioCaja = new ServicioCaja();
        ServicioFactura servicioFactura = new ServicioFactura();
        public ServicioVistaDeuda()
        {
            
        }

        public List<VistaDeuda> GetCreditos()
        {
            return vistarepository.GetCreditos();
        }

        
        public void PagarPedido(long idPedido, MetodosPago metodo, float Valor)
        {
            serviciopedido.PagarPedido(idPedido, metodo.Id);
            if (metodo.Nombre == "Efectivo") { servicioCaja.SumarIngreso(Valor); }
        }

        public List<DetallePedido> LoadDetalles(long idPedido)
        {
            return detallesRepository.GetDetalles(idPedido);
        }
        public FacturaDto GetfacturaDto(VistaDeuda vista)
        {
            return servicioFactura.MapFacturaDto(vista.NombreCajero, vista, "0", "0");
        }
        //public void GenerateFactura(FacturaDto factura)
        //{
        //    ServicioFactura.CreateFactura(factura);
        //}

        public void PrintTrue(VistaDeuda vista)
        {
            var dto = GetfacturaDto(vista);
            ServicioFactura.OpenCash();
            ServicioFactura.CreateFactura(dto);
            ServicioFactura.PdfToImg();
            ServicioFactura.printImg();
        }
    }
}
