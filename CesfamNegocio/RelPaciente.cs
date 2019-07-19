using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CesfamDatos;

namespace CesfamNegocio
{
    public class RelPaciente
    {
        public int IdRelPaciente { get; set; }
        public int IdPaciente { get; set; }
        public int IdTutor { get; set; }

        public bool crearRelacionPacienteTutor(int idRelacion, int idPaciente, int idTutor)
        {

            try
            {
                REL_PACIENTE rel = new REL_PACIENTE();
                rel.ID_REL_PACIENTE = idRelacion;
                rel.PACIENTE_ID_PACIENTE = idPaciente;
                rel.TUTOR_ID_TUTOR = idTutor;
                Acceso.ModeloCesfam.REL_PACIENTE.Add(rel);
                Acceso.ModeloCesfam.SaveChanges();
                return true;
            }
            catch(Exception)
            {
                return false;
            }
            
        }

        public class PacienteTutor
        {
            public string nombreTutor {get; set;}
            public string nombrePaciente { get; set; }
            public string rutPaciente { get; set; }


            public List<PacienteTutor> listarPacienteTutor()
            {


                return (from pac in Acceso.ModeloCesfam.PACIENTE
                        join rel in Acceso.ModeloCesfam.REL_PACIENTE on pac.ID_PACIENTE equals rel.PACIENTE_ID_PACIENTE
                        join tut in Acceso.ModeloCesfam.TUTOR on rel.TUTOR_ID_TUTOR equals tut.ID_TUTOR


                        select new PacienteTutor
                        {
                            nombreTutor = tut.NOMBRE,
                            nombrePaciente = pac.NOMBRE,
                            rutPaciente = pac.RUT

                        }).ToList();

            }

        }

    }
}
