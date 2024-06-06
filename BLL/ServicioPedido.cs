using DAL;
using ENTITY;
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
        //ServicioTurno servicioTurno = new ServicioTurno();

        public ServicioPedido()
        {
            
        }


        //public Pedido AddPedido(Pedido pedido)
        //{
        //    //pedido.Turno = servicioTurno.GetOpenTurno();
        //    //var pedid=PedidosRepository.Insert(pedido);
        //    //var TurnoA=servicioTurno.GetOpenTurno();
        //    //TurnoA.Pedidos.Add(pedido);
        //    //TurnoA.SetIngresos();
        //    //if (pedido.MetodoPago.Nombre == "Efectivo") { servicioCaja.SumarIngreso(pedido.Valor); }
        //    return pedid;
        //}

        public List<Pedido> GetPedidos(Turno turno) 
        {
            //return PedidosRepository.GetPedidos(turno);
            return null;
        }

        public List<MetodosPago> GetMetodos()
        {

            //return PedidosRepository.GetMetodos();
            return null;
        }

        public void GenerateFactura(Pedido pedido, string cambio, string efectivo)
        {
            //Turno turno = servicioTurno.GetOpenTurno();
            //Empleado cajero = new Empleado();
            //cajero.Nombre = "Jose Dolores Herazo Peñaranda";
            //cajero.Cedula = "11230402";
            //ServicioFactura.CreateFactura(turno.Cajero, pedido, cambio, efectivo);
            ServicioFactura.OpenCash();
            //ServicioFactura.ConvertImg();
            //ServicioFactura.printImg();
            Serviciofactura.test();
            
            
        }

        public List<Pedido> GetCreditos() 
        {
            

            //return PedidosRepository.GetCreditos();
            return null ;

        }


    }

}
