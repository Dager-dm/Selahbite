using DAL;
using ENTITY;
using Org.BouncyCastle.Asn1.Tsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ServicioPedido
    {

        ServicioCaja servicioCaja = new ServicioCaja();
        ServicioFactura Serviciofactura=new ServicioFactura();
        PedidosRepository PedidosRepository = new PedidosRepository();
        ServicioTurno servicioTurno = new ServicioTurno();

        public ServicioPedido()
        {
            
        }


        public Pedido AddPedido(Pedido pedido)
        {
            var turno = servicioTurno.GetOpenTurno();
            var pedid = PedidosRepository.Insert(pedido, turno.Id);
            var TurnoA = servicioTurno.GetOpenTurno();
            TurnoA.Pedidos.Add(pedido);
            TurnoA.SetIngresos();
            if (pedido.MetodoPago.Nombre == "Efectivo") { servicioCaja.SumarIngreso(pedido.Valor); }
            return pedid;
        }

        public List<Pedido> GetPedidos(Turno turno) 
        {
            return PedidosRepository.GetPedidos(turno.Id);

        }

        public List<MetodosPago> GetMetodos()
        {

            return PedidosRepository.GetMetodos();
            
        }

        public string GenerateFactura(Pedido pedido, string cambio, string efectivo)
        {
            Turno turno = servicioTurno.GetOpenTurno();
            //Empleado cajero = new Empleado();
            //cajero.Nombre = "Jose Dolores Herazo Peñaranda";
            //cajero.Cedula = "11230402";
            ServicioFactura.CreateFactura(turno.Cajero, pedido, cambio, efectivo);
            ServicioFactura.OpenCash();
            ServicioFactura.PdfToImg();
            ServicioFactura.printImg();
            return Serviciofactura.ToString();// revisar
            
            
        }

        public List<Pedido> GetCreditos() 
        {
            return PedidosRepository.GetCreditos();

        }


    }

}
