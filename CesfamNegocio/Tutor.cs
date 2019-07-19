using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CesfamDatos;

namespace CesfamNegocio
{
    public class Tutor
    {
        public int IdTutor { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Rut { get; set; }
        public string Correo { get; set; }
        public string Parentesco { get; set; }

        public bool crearTutor(int idTutor, DateTime fechaNacimiento, string nombre, string apPaterno, string apMaterno, string correo, string parentesco)//cambio de nombre
        {
            try
            {
                TUTOR tutor = new TUTOR();
                tutor.ID_TUTOR = idTutor;
                tutor.APELLIDO_MATERNO = apPaterno;
                tutor.APELLIDO_PATERNO = apMaterno;
                tutor.CORREO = correo;
                tutor.FECHA_NACIMIENTO = fechaNacimiento;
                tutor.NOMBRE = nombre;
                tutor.PARENTESCO = parentesco;
                Acceso.ModeloCesfam.TUTOR.Add(tutor);
                Acceso.ModeloCesfam.SaveChanges();
                return true;

            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool Read()
        {
            try
            {
                CesfamDatos.TUTOR tutor = Acceso.ModeloCesfam.TUTOR.First(pac => pac.ID_TUTOR == this.IdTutor);
                this.IdTutor = (int)tutor.ID_TUTOR;
                this.ApellidoMaterno = tutor.APELLIDO_MATERNO;
                this.ApellidoPaterno = tutor.APELLIDO_PATERNO;
                this.Correo = tutor.CORREO;
                this.FechaNacimiento = tutor.FECHA_NACIMIENTO;
                this.Nombre = tutor.NOMBRE;
                this.Parentesco = tutor.PARENTESCO;
             
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }


        public bool Update(int idTutor, string fechaNacimiento, string nombre, string apPaterno, string apMaterno, string correo, string parentesco)
        {
            try
            {

                CesfamDatos.TUTOR tutor = Acceso.ModeloCesfam.TUTOR.First(tp => tp.ID_TUTOR == this.IdTutor);
                tutor.FECHA_NACIMIENTO = DateTime.Parse(fechaNacimiento);
                tutor.NOMBRE = nombre;
                tutor.APELLIDO_MATERNO = apMaterno;
                tutor.APELLIDO_PATERNO = apPaterno;
                tutor.CORREO = correo;
                tutor.PARENTESCO = parentesco;
                Acceso.ModeloCesfam.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        private List<Tutor> GenerarListado(List<TUTOR> Lista)
        {
            List<Tutor> listado = new List<Tutor>();
            foreach (var item in Lista)
            {
                Tutor tutor = new Tutor();
                tutor.IdTutor = (int)item.ID_TUTOR;
                tutor.ApellidoMaterno = item.APELLIDO_MATERNO;
                tutor.ApellidoPaterno = item.APELLIDO_PATERNO;
                tutor.Correo = item.CORREO;
                tutor.FechaNacimiento = item.FECHA_NACIMIENTO;
                tutor.Nombre = item.NOMBRE;
                tutor.Parentesco = item.PARENTESCO;
                listado.Add(tutor);
            }
            return listado;
        }
        public List<Tutor> ListarTutor()
        {

            var list = Acceso.ModeloCesfam.TUTOR;
            return GenerarListado(list.ToList());
        }

    }
}
