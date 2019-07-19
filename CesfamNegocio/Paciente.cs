using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CesfamDatos;

namespace CesfamNegocio
{
    public class Paciente
    {
        public int IdPaciente { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Correo { get; set; }
        public string CodigoLibreta { get; set; }
        public string Rut { get; set; }
        public string Direccion { get; set; }
        public bool create(int idPaciente, DateTime fechaNacimiento, string nombre, string apPaterno, string apMaterno, string correo, string codigoLibreta, string rut, string direccion)
        {
            try
            {
                PACIENTE Pac = new PACIENTE();
                Pac.ID_PACIENTE = idPaciente;
                Pac.APELLIDO_MATERNO = apMaterno;
                Pac.APELLIDO_PATERNO = apPaterno;
                Pac.CODIGO_LIBRETA = codigoLibreta;
                Pac.CORREO = correo;
                Pac.FECHA_NACIMIENTO = fechaNacimiento;
                Pac.NOMBRE = nombre;
                Pac.RUT = rut;
                Pac.DIRECCION = direccion;
                Acceso.ModeloCesfam.PACIENTE.Add(Pac);
                Acceso.ModeloCesfam.SaveChanges();
                return true;

            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool crearPaciente(int idPaciente, DateTime fechaNacimiento, string nombre, string apPaterno, string apMaterno, string correo, string codigoLibreta, string rut, string direccion)
        {
            try
            {
                PACIENTE Pac = new PACIENTE();
                Pac.ID_PACIENTE = idPaciente;
                Pac.APELLIDO_MATERNO = apMaterno;
                Pac.APELLIDO_PATERNO = apPaterno;
                Pac.CODIGO_LIBRETA = codigoLibreta;
                Pac.CORREO = correo;
                Pac.FECHA_NACIMIENTO = fechaNacimiento;
                Pac.NOMBRE = nombre;
                Pac.RUT = rut;
                Pac.DIRECCION = direccion;
                Acceso.ModeloCesfam.PACIENTE.Add(Pac);
                Acceso.ModeloCesfam.SaveChanges();
                return true;

            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool Read(int idPaciente)
        {
            try
            {
                CesfamDatos.PACIENTE Pac = Acceso.ModeloCesfam.PACIENTE.First(pac => pac.ID_PACIENTE == idPaciente);
                this.IdPaciente = (int)Pac.ID_PACIENTE;
                this.ApellidoMaterno = Pac.APELLIDO_MATERNO;
                this.ApellidoPaterno = Pac.APELLIDO_PATERNO;
                this.CodigoLibreta = Pac.CODIGO_LIBRETA;
                this.Correo = Pac.CORREO;
                this.FechaNacimiento = Pac.FECHA_NACIMIENTO;
                this.Nombre = Pac.NOMBRE;
                this.Rut = Pac.RUT;
                this.Direccion = Pac.DIRECCION;
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public bool Update()
        {
            try
            {
                CesfamDatos.PACIENTE Pac = Acceso.ModeloCesfam.PACIENTE.First(pac => pac.ID_PACIENTE == this.IdPaciente);
                Pac.ID_PACIENTE = this.IdPaciente;
                Pac.APELLIDO_MATERNO = this.ApellidoMaterno;
                Pac.APELLIDO_PATERNO = this.ApellidoPaterno;
                Pac.CODIGO_LIBRETA = this.CodigoLibreta;
                Pac.CORREO = this.Correo;
                Pac.FECHA_NACIMIENTO = this.FechaNacimiento;
                Pac.NOMBRE = this.Nombre;
                Pac.RUT = this.Rut;
                Pac.DIRECCION = this.Direccion;
                Acceso.ModeloCesfam.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public bool Update(int idPaciente, string apMaterno, string apPaterno, string codigoLibreta, string correo, string fechaNac, string nombre, string rut, string direccion)
        {
            try
            {
                CesfamDatos.PACIENTE Pac = Acceso.ModeloCesfam.PACIENTE.First(pac => pac.ID_PACIENTE == idPaciente);
                Pac.APELLIDO_MATERNO = apMaterno;
                Pac.APELLIDO_PATERNO = apPaterno;
                Pac.CODIGO_LIBRETA = codigoLibreta;
                Pac.CORREO = correo;
                Pac.FECHA_NACIMIENTO = DateTime.Parse(fechaNac);
                Pac.NOMBRE = nombre;
                Pac.RUT = rut;
                Pac.DIRECCION = direccion;
                Acceso.ModeloCesfam.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public bool Delete()
        {
            try
            {
                CesfamDatos.PACIENTE Pac = Acceso.ModeloCesfam.PACIENTE.First(pac => pac.ID_PACIENTE == this.IdPaciente);
                Acceso.ModeloCesfam.PACIENTE.Remove(Pac);
                Acceso.ModeloCesfam.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private List<Paciente> GenerarListado(List<PACIENTE> Lista)
        {
            List<Paciente> listado = new List<Paciente>();
            foreach (var item in Lista)
            {
                Paciente Pac = new Paciente();
                Pac.IdPaciente = (int)item.ID_PACIENTE;
                Pac.ApellidoMaterno = item.APELLIDO_MATERNO;
                Pac.ApellidoPaterno = item.APELLIDO_PATERNO;
                Pac.CodigoLibreta = item.CODIGO_LIBRETA;
                Pac.Correo = item.CORREO;
                Pac.FechaNacimiento = item.FECHA_NACIMIENTO;
                Pac.Nombre = item.NOMBRE;
                Pac.Rut = item.RUT;
                Pac.Direccion = item.DIRECCION;
                listado.Add(Pac);
            }
            return listado;
        }
        public List<Paciente> ListarPacientes()
        {

            var list = Acceso.ModeloCesfam.PACIENTE;
            return GenerarListado(list.ToList());
        }

        public List<Paciente> listarPacientes()
        {

            var list = Acceso.ModeloCesfam.PACIENTE;
            return GenerarListado(list.ToList());
        }

        public List<Paciente> listarPacientesPorId(int idPaciente)
        {
            var list = (from pac in Acceso.ModeloCesfam.PACIENTE
                        where pac.ID_PACIENTE == idPaciente
                        select pac);
            return GenerarListado(list.ToList());
        }

        public class PacienteUltimaReceta
        {
            public int idPaciente { get; set; }
            public string codigoLibreta { get; set; }
            public string nombre { get; set; }
            public string apellidoPaterno { get; set; }
            public string apellidoMaterno { get; set; }
            public string rut { get; set; }
            public string direccion { get; set; }
            public string correo { get; set; }
            public DateTime fechaUltimaReceta { get; set; }
            public string valor { get; set; }
            public bool ver { get; set; }
            public DateTime fecha { get; set; }


            public List<PacienteUltimaReceta> listarPacienteUltimaReceta()
            {


                return (from pac in Acceso.ModeloCesfam.PACIENTE
                        join rec in Acceso.ModeloCesfam.RECETA on pac.ID_PACIENTE equals rec.PACIENTE_ID_PACIENTE


                        select new PacienteUltimaReceta
                        {
                            idPaciente = (int)pac.ID_PACIENTE,
                            codigoLibreta = pac.CODIGO_LIBRETA,
                            nombre = pac.NOMBRE,
                            apellidoPaterno = pac.APELLIDO_PATERNO,
                            apellidoMaterno = pac.APELLIDO_MATERNO,
                            rut = pac.RUT,
                            direccion = pac.DIRECCION,
                            correo = pac.CORREO,
                            fechaUltimaReceta = rec.FECHA_RECETA

                        }).ToList();

            }

        }
    }
}
