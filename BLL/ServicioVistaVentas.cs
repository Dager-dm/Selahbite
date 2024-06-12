using DAL;
using ENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ServicioVistaVentas
    {
        VistasRepository VistasRepository=new VistasRepository();
        public ServicioVistaVentas()
        {
            
        }

        public List<VistaVentas> GetVistaVentasMensuales()
        {

            return VistasRepository.GetVentasMensuales();
        }

        public List<VistaVentas> GetVentasSemanales()
        {
            return VistasRepository.GetVentasSemanales();
        }
    }
}
