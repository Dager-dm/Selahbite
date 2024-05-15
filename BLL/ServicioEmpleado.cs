using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENTITY;

namespace BLL
{

    public class ServicioEmpleado
    {
        private static List<Empleado> lstEmpleados;

        public ServicioEmpleado()
        {
            lstEmpleados = new List<Empleado>();
            empleadopr();

        }
    


        //Crud
        public void AddEmpleado(Empleado newempleado)
        {
            lstEmpleados.Add(newempleado);
            
        }

        public List<Empleado> GetAllEmpleados()
        {

            return lstEmpleados;

        }



        public void EditEmpleado(Empleado empleadoOld, Empleado empleadoModified)
        {
            empleadoOld.Nombre = empleadoModified.Nombre;
            empleadoOld.Telefono = empleadoModified.Telefono;
            empleadoOld.Id = empleadoModified.Id;
            empleadoOld.Cargo = empleadoModified.Cargo;
        }

        public void DeleteEmpleado(Empleado empleadoToDelete)
        {

            lstEmpleados.Remove(empleadoToDelete);
        }





        public List<string> GetStringMeseros()
        {
            List<string> StringList = new List<string>();
            foreach (var item in lstEmpleados)
            {
                if (item.Cargo == "Mesero")
                {
                    StringList.Add(item.Nombre);
                }

            }

            return StringList;
        }


        public List<string> GetStringCajeros()
        {
            List<string> StringList = new List<string>();
            foreach (var item in lstEmpleados)
            {
                if (item.Cargo == "Cajero")
                {
                    StringList.Add(item.Nombre);
                }

            }

            return StringList;
        }



        private void empleadopr()
        {
            Empleado empleado = new Empleado("Juan", "11", "301", "Mesero", 0);
            Empleado empleado1 = new Empleado("Camilo", "12", "301", "Mesero", 0);
            Empleado empleado2 = new Empleado("Andres", "13", "301", "Mesero", 0);
            Empleado empleado3 = new Empleado("Maria", "14", "301", "Mesero", 0);
            Empleado empleado4 = new Empleado("Carolina", "15", "301", "Cajero", 0);
            Empleado empleado5 = new Empleado("Sofia", "16", "301", "Cajero", 0);
            Empleado empleado6 = new Empleado("Andrea", "17", "301", "Cajero", 0);
            Empleado empleado7 = new Empleado("Camila", "18", "301", "Cajero", 0);
            lstEmpleados.Add(empleado); lstEmpleados.Add(empleado1); lstEmpleados.Add(empleado2); lstEmpleados.Add(empleado3);
            lstEmpleados.Add(empleado4); lstEmpleados.Add(empleado5); lstEmpleados.Add(empleado6); lstEmpleados.Add(empleado7);

        }
    }
}
