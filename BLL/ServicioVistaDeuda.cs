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
        VistasRepository vistarepository = new VistasRepository();
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

        
        public void PagarDeuda(long idPedido, MetodosPago metodo, float Valor)
        {
            serviciopedido.PagarDeuda(idPedido, metodo.Id);
            if (metodo.Nombre == "Efectivo") { servicioCaja.SumarIngreso(Valor); ServicioFactura.OpenCash(); }
        }

        public List<DetallePedido> LoadDetalles(long idPedido)
        {
            return detallesRepository.GetDetalles(idPedido);
        }
        public FacturaDto GetfacturaDto(VistaDeuda vista, float cambio, string efectivo)
        {
            return servicioFactura.MapFacturaDto(vista.NombreCajero, vista, cambio, efectivo);
        }

        public void PrintTrue(VistaDeuda vista, float cambio, string efectivo)
        {
            var dto = GetfacturaDto(vista, cambio, efectivo);
            ServicioFactura.CreateFactura(dto);
            ServicioFactura.PdfToImg();
            ServicioFactura.printImg();
        }
    }
}
