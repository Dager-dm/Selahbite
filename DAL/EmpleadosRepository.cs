using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENTITY;
using Oracle.ManagedDataAccess.Client;

namespace DAL
{
    public class EmpleadosRepository : SelahbiteDB
    {
        private OracleCommand oracleCommand;
        public EmpleadosRepository()
        {

        }

        public bool insert(Empleado empleado)
        {
            oracleCommand = new OracleCommand("pr_InsertEmpleado");
            oracleCommand.CommandType = CommandType.StoredProcedure;
            oracleCommand.Connection = Conexion();
            AbrirConexion();

            oracleCommand.Parameters.Add("nomb", OracleDbType.Varchar2).Value = empleado.Nombre;
            oracleCommand.Parameters.Add("ced", OracleDbType.Varchar2).Value = empleado.Cedula;
            oracleCommand.Parameters.Add("tel", OracleDbType.Varchar2).Value = empleado.Telefono;
            oracleCommand.Parameters.Add("cargo", OracleDbType.Varchar2).Value = empleado.Cargo.Id;
            //pr_InsertEmpleado(nomb EMPLEADOS.nombre%type, ced EMPLEADOS.cedula%type, tel EMPLEADOS.telefono%type, cargo EMPLEADOS.id_cargo%TYPE)
            var i = oracleCommand.ExecuteNonQuery();
            if (i > 0)
            {
                return true;
            }
            CerrarConexion();
            return false;
        }

        public List<Empleado> GetEmpleados()
        {
            oracleCommand = new OracleCommand();
            List<Empleado> lstEmpleados = new List<Empleado>();
            string oracle = "SELECT * FROM EMPLEADOS";
            oracleCommand.CommandText = oracle;
            oracleCommand.Connection = Conexion();
            AbrirConexion();
            var reader = oracleCommand.ExecuteReader(); 
            while (reader.Read())
            {
                lstEmpleados.Add(MapEmpleado(reader));
            }
            CerrarConexion();
            return lstEmpleados;
        }

        public bool Edit(Empleado empleado)
        {
            oracleCommand = new OracleCommand("pr_EditEmpleado");
            oracleCommand.CommandType = CommandType.StoredProcedure;
            oracleCommand.Connection = Conexion();
            AbrirConexion();

            oracleCommand.Parameters.Add("nomb", OracleDbType.Varchar2).Value = empleado.Nombre;
            oracleCommand.Parameters.Add("ced", OracleDbType.Varchar2).Value = empleado.Cedula;
            oracleCommand.Parameters.Add("tel", OracleDbType.Varchar2).Value = empleado.Telefono;
            oracleCommand.Parameters.Add("cargo", OracleDbType.Varchar2).Value = empleado.Cargo.Id;
            oracleCommand.Parameters.Add("idemp", OracleDbType.Varchar2).Value = empleado.Id;
            // pr_EditEmpleado (nomb EMPLEADOS.nombre%type, ced EMPLEADOS.cedula%type, tel EMPLEADOS.telefono%type, cargo EMPLEADOS.id_cargo%TYPE, idemp EMPLEADOS.id_empleado%type)
            var i = oracleCommand.ExecuteNonQuery();
            if (i > 0)
            {
                return true;
            }
            CerrarConexion();

            return false;
        }





        public List<CargosEmpleados> GetCargos()
        {
            oracleCommand = new OracleCommand();
            List<CargosEmpleados> cargos = new List<CargosEmpleados>();
            string oracle = "SELECT * FROM CARGOS";
            oracleCommand.CommandText = oracle;
            oracleCommand.Connection = Conexion();
            AbrirConexion();
            var reader = oracleCommand.ExecuteReader(); //select
            while (reader.Read())
            {
                cargos.Add(MapCargo(reader));
            }
            CerrarConexion();
            return cargos;

        }

        public List<Empleado> GetCajeros()
        {
            oracleCommand = new OracleCommand();
            List<Empleado> cajeros = new List<Empleado>();
            string oracle = "SELECT * FROM EMPLEADOS WHERE id_cargo = '1'";
            oracleCommand.CommandText = oracle;
            oracleCommand.Connection = Conexion();
            AbrirConexion();
            var reader = oracleCommand.ExecuteReader();
            while (reader.Read())
            {
                cajeros.Add(MapEmpleado(reader));
            }
            CerrarConexion();
            return cajeros;

        }

        public List<Empleado> GetMeseros()
        {
            oracleCommand = new OracleCommand();
            List<Empleado> meseros = new List<Empleado>();
            string oracle = "SELECT * FROM EMPLEADOS WHERE id_cargo = '2'";
            oracleCommand.CommandText = oracle;
            oracleCommand.Connection = Conexion();
            AbrirConexion();
            var reader = oracleCommand.ExecuteReader();
            while (reader.Read())
            {
                meseros.Add(MapEmpleado(reader));
            }
            CerrarConexion();
            return meseros;

        }


        private CargosEmpleados LoadCargo(string idCargo)
        {
            oracleCommand = new OracleCommand();
            string oracle = "SELECT * FROM CARGOS WHERE id_cargo = :idCargo";
            oracleCommand.CommandText = oracle;
            oracleCommand.Parameters.Add(new OracleParameter("idCategoria", idCargo));
            oracleCommand.Connection = Conexion();
            AbrirConexion();
            var reader = oracleCommand.ExecuteReader(); // select
            if (reader.Read())
            {
                return MapCargo(reader);

            }
            CerrarConexion();

            return null;
        }

        public Empleado MapEmpleado(OracleDataReader reader)
        {
            Empleado empleado = new Empleado();
            empleado.Id = reader.GetInt64(0);
            empleado.Cedula = reader.GetString(2);
            empleado.Nombre = reader.GetString(1);
            empleado.Telefono = reader.GetString(3);
            empleado.Cargo=LoadCargo(reader.GetString(4));
            
            return empleado;
        }

        private CargosEmpleados MapCargo(OracleDataReader reader)
        {
            CargosEmpleados cargo = new CargosEmpleados();
            cargo.Id = reader.GetString(0);
            cargo.Nombre = reader.GetString(1);
            return cargo;
        }

        
    }
}
