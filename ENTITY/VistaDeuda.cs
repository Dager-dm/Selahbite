using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY
{
    public class VistaDeuda
    {
        public VistaDeuda()
        {
            
        }

        public VistaDeuda(long id_pedido, long id_Turno, long id_Cliente, string cedulaCliente, long id_Cajero, string nombreCajero, string horario, float valor, string estado, ModalidadDePago modalidad, DateTime fecha)
        {
            Id_pedido = id_pedido;
            Id_Turno = id_Turno;
            Id_Cliente = id_Cliente;
            CedulaCliente = cedulaCliente;
            Id_Cajero = id_Cajero;
            NombreCajero = nombreCajero;
            Horario = horario;
            Valor = valor;
            Estado = estado;
            Modalidad = modalidad;
            Fecha = fecha;

        }

        public long Id_pedido {  get; set; }
        public long Id_Turno { get; set; }
        public long Id_Cliente { get; set; }
        public string CedulaCliente { get; set; }
        public string NombreCliente { get; set; }
        public long Id_Cajero { get; set; }
        public string NombreCajero { get; set; }
        public string Horario {  get; set; }
        public float Valor {  get; set; }
        public string Estado { get; set; }
        public ModalidadDePago Modalidad { get; set; }
        public DateTime Fecha { get; set; }
        public List<DetallePedido> Detalles { get; set;}
    }
}
