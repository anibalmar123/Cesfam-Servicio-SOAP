using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CesfamDatos;

namespace CesfamNegocio
{
    public class TipoUsuario
    {
        public int IdTipoUsuario { get; set; }
        public string NombreTipoUsuario { get; set; }

        public bool create(int tipoUsuario, string nombreUsuario)
        {
            try
            {
                TIPO_USUARIO tipUser = new TIPO_USUARIO();
                tipUser.ID_TIPO_USUARIO = tipoUsuario;
                tipUser.NOMBRE_TIPO_USUARIO = nombreUsuario;
                Acceso.ModeloCesfam.TIPO_USUARIO.Add(tipUser);
                Acceso.ModeloCesfam.SaveChanges();
                return true;

            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool Read(int idTipoUsuario)
        {
            try
            {
                CesfamDatos.TIPO_USUARIO tipoUsuario = Acceso.ModeloCesfam.TIPO_USUARIO.First(tu => tu.ID_TIPO_USUARIO == idTipoUsuario);
                this.IdTipoUsuario = (int)tipoUsuario.ID_TIPO_USUARIO;
                this.NombreTipoUsuario = tipoUsuario.NOMBRE_TIPO_USUARIO;
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
                CesfamDatos.TIPO_USUARIO tipoUsuario = Acceso.ModeloCesfam.TIPO_USUARIO.First(tu => tu.ID_TIPO_USUARIO == this.IdTipoUsuario);
                tipoUsuario.ID_TIPO_USUARIO = this.IdTipoUsuario;
                tipoUsuario.NOMBRE_TIPO_USUARIO = this.NombreTipoUsuario;
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
                CesfamDatos.TIPO_USUARIO tipoUsuario = Acceso.ModeloCesfam.TIPO_USUARIO.First(tu => tu.ID_TIPO_USUARIO == this.IdTipoUsuario);
                Acceso.ModeloCesfam.TIPO_USUARIO.Remove(tipoUsuario);
                Acceso.ModeloCesfam.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private List<TipoUsuario> GenerarListado(List<TIPO_USUARIO> Lista)
        {
            List<TipoUsuario> listado = new List<TipoUsuario>();
            foreach (var item in Lista)
            {
                TipoUsuario tipUser = new TipoUsuario();
                tipUser.IdTipoUsuario = (int)item.ID_TIPO_USUARIO;
                tipUser.NombreTipoUsuario = item.NOMBRE_TIPO_USUARIO;
                listado.Add(tipUser);
            }
            return listado;
        }
        public List<TipoUsuario> ListarTipoUsuario()
        {

            var list = Acceso.ModeloCesfam.TIPO_USUARIO;
            return GenerarListado(list.ToList());
        }
    }
}
