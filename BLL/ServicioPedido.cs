using DAL;
using ENTITY;
using Org.BouncyCastle.Asn1.Tsp;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ServicioPedido
    {

        ServicioCaja servicioCaja = new ServicioCaja();
        ServicioFactura Serviciofactura = new ServicioFactura();
        PedidosRepository PedidosRepository = new PedidosRepository();
        ServicioTurno servicioTurno = new ServicioTurno();

        public ServicioPedido()
        {

        }

        public Pedido AddPedido(Pedido pedido)
        {
            var TurnoA = servicioTurno.GetOpenTurno();
            var pedid = PedidosRepository.Insert(pedido, TurnoA.Id);
            TurnoA.SetAPedido(pedido);
            TurnoA.CalcularIngreso(pedid.Valor);
            if (pedido.MetodoPago.Nombre == "Efectivo") { servicioCaja.SumarIngreso(pedido.Valor); ServicioFactura.OpenCash(); }
            return pedid;
        }

        public List<Pedido> GetPedidos(Turno turno)
        {
            return PedidosRepository.GetPedidos(turno.Id);

        }

        public List<MetodosPago> GetMetodos()
        {
            try
            {
                return PedidosRepository.GetMetodos();
            }
            catch (Exception)
            {

                throw;
            }


        }

        public void GenerateFactura(Pedido pedido, float cambio, string efectivo)
        {
            Turno turno = servicioTurno.GetOpenTurno();
            var dto = Serviciofactura.MapFacturaDto(turno.Cajero.Nombre, pedido, cambio, efectivo);
            ServicioFactura.CreateFactura(dto);
            ServicioFactura.PdfToImg();
            ServicioFactura.printImg();
            // return Serviciofactura.ToString(); revisar

        }

        public string PagarDeuda(long idPedido, string idMetodo)
        {
            string message;
            try
            {
                PedidosRepository.PagarDeuda(idPedido, idMetodo);
                message = "Pagado correctamente";
            }
            catch (Exception ex)
            {
                message = "Ha ocurrido algun error\n" + ex.Message;
            }

            return message;
        }

    }

}
