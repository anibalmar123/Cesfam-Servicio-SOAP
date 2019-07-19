using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CesfamDatos;

namespace CesfamNegocio
{
    public class TipoSalida
    {
        public int IdTipoSalida { get; set; }
        public string NombreTipoSalida { get; set; }

        public bool create(int idTipoSalida, string nombreTipoSalida)
        {
            try
            {
                TIPO_SALIDA tipoSalida = new TIPO_SALIDA();
                tipoSalida.ID_TIPO_SALIDA = idTipoSalida;
                tipoSalida.NOMBRE_TIPO_SALIDA = nombreTipoSalida;
                Acceso.ModeloCesfam.TIPO_SALIDA.Add(tipoSalida);
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
                CesfamDatos.TIPO_SALIDA tipoSalida = Acceso.ModeloCesfam.TIPO_SALIDA.First(tp => tp.ID_TIPO_SALIDA == this.IdTipoSalida);
                this.IdTipoSalida = (int)tipoSalida.ID_TIPO_SALIDA;
                this.NombreTipoSalida = tipoSalida.NOMBRE_TIPO_SALIDA;
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

                CesfamDatos.TIPO_SALIDA tipoSalida = Acceso.ModeloCesfam.TIPO_SALIDA.First(tp => tp.ID_TIPO_SALIDA == this.IdTipoSalida);
                tipoSalida.ID_TIPO_SALIDA = this.IdTipoSalida;
                tipoSalida.NOMBRE_TIPO_SALIDA = this.NombreTipoSalida;
                Acceso.ModeloCesfam.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public List<TipoSalida> GenerarListado(List<TIPO_SALIDA> list)
        {

            List<TipoSalida> listado = new List<TipoSalida>();
            foreach (var item in list)
            {
                TipoSalida tipoSalida = new TipoSalida();
                tipoSalida.IdTipoSalida = (int)item.ID_TIPO_SALIDA;
                tipoSalida.NombreTipoSalida = item.NOMBRE_TIPO_SALIDA;
                listado.Add(tipoSalida);
            }
            return listado;
        }
        public List<TipoSalida> ListarTipoSalida()
        {

            var list = Acceso.ModeloCesfam.TIPO_SALIDA;
            return GenerarListado(list.ToList());
        }
    }
}
