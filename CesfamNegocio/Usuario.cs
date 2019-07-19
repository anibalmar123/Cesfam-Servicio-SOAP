using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CesfamDatos;
using System.Data;

namespace CesfamNegocio
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string NombreUsuario { get; set; }
        public string Clave { get; set; }
        public DateTime FechaRegistro { get; set; }
        public int IdTipoUsuario { get; set; }

        public bool create(int idUsuario, string nombre, string apellidoPaterno, string apellidoMaterno, string nombreUsuario, string clave, DateTime fecha, int IdTipoUsuario)
        {
            try
            {
                USUARIO usuario = new USUARIO();
                usuario.ID_USUARIO = idUsuario;
                usuario.NOMBRE = nombre;
                usuario.APELLIDO_PATERNO = apellidoPaterno;
                usuario.APELLIDO_MATERNO = apellidoMaterno;
                usuario.USUARIO1 = nombreUsuario;
                usuario.CLAVE = clave;
                usuario.FECHA_REGISTRO = fecha;
                usuario.TIPO_USUARIO_ID_TIPO_USUARIO = IdTipoUsuario;
                Acceso.ModeloCesfam.USUARIO.Add(usuario);
                Acceso.ModeloCesfam.SaveChanges();
                return true;

            }
            catch (Exception)
            {

                return false;
            }
        }
        public bool read(int idUsuario)
        {
            try
            {
                CesfamDatos.USUARIO usuario = Acceso.ModeloCesfam.USUARIO.First(tp => tp.ID_USUARIO == idUsuario);
                this.IdUsuario = (int)usuario.ID_USUARIO;
                this.Nombre = usuario.NOMBRE;
                this.ApellidoPaterno = usuario.APELLIDO_PATERNO;
                this.ApellidoMaterno = usuario.APELLIDO_MATERNO;
                this.NombreUsuario = usuario.USUARIO1;
                this.Clave = usuario.CLAVE;
                this.FechaRegistro = usuario.FECHA_REGISTRO;
                this.IdTipoUsuario = (int)usuario.TIPO_USUARIO_ID_TIPO_USUARIO;
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public bool update()
        {
            try
            {
                CesfamDatos.USUARIO usuario = Acceso.ModeloCesfam.USUARIO.First(pr => pr.ID_USUARIO == this.IdUsuario);
                usuario.ID_USUARIO = this.IdUsuario;
                usuario.NOMBRE = this.Nombre;
                usuario.APELLIDO_PATERNO = this.ApellidoPaterno;
                usuario.APELLIDO_MATERNO = this.ApellidoMaterno;
                usuario.USUARIO1 = this.NombreUsuario;
                usuario.CLAVE = this.Clave;
                usuario.FECHA_REGISTRO = this.FechaRegistro;
                usuario.TIPO_USUARIO_ID_TIPO_USUARIO = this.IdTipoUsuario;
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
                CesfamDatos.USUARIO User = Acceso.ModeloCesfam.USUARIO.First(usu => usu.ID_USUARIO == this.IdUsuario);
                Acceso.ModeloCesfam.USUARIO.Remove(User);
                Acceso.ModeloCesfam.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private List<Usuario> GenerarListado(List<USUARIO> Lista)
        {
            List<Usuario> listado = new List<Usuario>();
            foreach (var item in Lista)
            {
                Usuario User = new Usuario();
                User.IdUsuario = (int)item.ID_USUARIO;
                User.Nombre = item.NOMBRE;
                User.ApellidoMaterno = item.APELLIDO_MATERNO;
                User.ApellidoPaterno = item.APELLIDO_PATERNO;
                User.NombreUsuario = item.USUARIO1;
                User.Clave = item.CLAVE;
                User.FechaRegistro = item.FECHA_REGISTRO;
                User.IdTipoUsuario = (int)item.TIPO_USUARIO_ID_TIPO_USUARIO;
                listado.Add(User);
            }
            return listado;
        }
        public List<Usuario> ListarUsuarios()
        {

            var list = Acceso.ModeloCesfam.USUARIO;
            return GenerarListado(list.ToList());
        }
        public bool verificarUsuario(string usuario, string clave)
        {
            try
            {
                var user = Acceso.ModeloCesfam.USUARIO.First(u => u.USUARIO1.Equals(usuario) && u.CLAVE.Equals(clave));
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        
        }
        public bool verificarNombreUsuario(string usuario)
        {
            try
            {
                var user = Acceso.ModeloCesfam.USUARIO.First(u => u.USUARIO1.Equals(usuario));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public string recueprarNombre(string usuario) 
        {
            try
            {
                var user = Acceso.ModeloCesfam.USUARIO.First(u => u.USUARIO1.Equals(usuario));
                return string.Format(user.NOMBRE+" "+user.APELLIDO_PATERNO+" "+user.APELLIDO_MATERNO);
            }
            catch (Exception)
            {
                return "";
            }
        }
        public Usuario enviarDatosUsuario(string usuario)
        {
            try
            {
                var user = Acceso.ModeloCesfam.USUARIO.First(u => u.USUARIO1.Equals(usuario));
                Usuario Nuser = new Usuario();
                Nuser.read((int)user.ID_USUARIO);
                return Nuser;
            }
            catch (Exception)
            {
                return null;
            }
        }
        
    }
}
