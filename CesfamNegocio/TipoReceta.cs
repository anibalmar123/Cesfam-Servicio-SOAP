using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CesfamDatos;

namespace CesfamNegocio
{
    public class TipoReceta
    {
        public int IdTipoReceta { get; set; }
        public string NombreTipoReceta { get; set; }

        public bool create(int idTipoReceta, string nombreTipo)
        {
            try
            {
                TIPO_RECETA tipoReceta = new TIPO_RECETA();
                tipoReceta.ID_TIPO_RECETA = idTipoReceta;
                tipoReceta.NOMBRE_TIPO = nombreTipo;
                Acceso.ModeloCesfam.TIPO_RECETA.Add(tipoReceta);
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
                CesfamDatos.TIPO_RECETA tipoReceta = Acceso.ModeloCesfam.TIPO_RECETA.First(tp => tp.ID_TIPO_RECETA == this.IdTipoReceta);
                this.IdTipoReceta = (int)tipoReceta.ID_TIPO_RECETA;
                this.NombreTipoReceta = tipoReceta.NOMBRE_TIPO;
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

                CesfamDatos.TIPO_RECETA tipoReceta = Acceso.ModeloCesfam.TIPO_RECETA.First(tp => tp.ID_TIPO_RECETA == this.IdTipoReceta);
                tipoReceta.ID_TIPO_RECETA = this.IdTipoReceta;
                tipoReceta.NOMBRE_TIPO = this.NombreTipoReceta;
                Acceso.ModeloCesfam.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public List<TipoReceta> GenerarListado(List<TIPO_RECETA> list)
        {

            List<TipoReceta> listado = new List<TipoReceta>();
            foreach (var item in list)
            {
                TipoReceta tipoReceta = new TipoReceta();
                tipoReceta.IdTipoReceta = (int)item.ID_TIPO_RECETA;
                tipoReceta.NombreTipoReceta = item.NOMBRE_TIPO;
                listado.Add(tipoReceta);
            }
            return listado;
        }
        public List<TipoReceta> ListarTipoReceta()
        {

            var list = Acceso.ModeloCesfam.TIPO_RECETA;
            return GenerarListado(list.ToList());
        }


    }
}
