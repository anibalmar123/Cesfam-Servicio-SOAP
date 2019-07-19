using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace CesfamWCF
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "Service2" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione Service2.svc o Service2.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class Service2 : IService2
    {
        private CesfamNegocio.Producto bssProducto = new CesfamNegocio.Producto();
        private CesfamNegocio.Receta bssReceta = new CesfamNegocio.Receta();
        private CesfamNegocio.Usuario bssUsuario = new CesfamNegocio.Usuario();
        private CesfamNegocio.Categoria bssCategoria = new CesfamNegocio.Categoria();
        private CesfamNegocio.Paciente bssPaciente = new CesfamNegocio.Paciente();
        private CesfamNegocio.Paciente.PacienteUltimaReceta bssPacienteUltimaReceta = new CesfamNegocio.Paciente.PacienteUltimaReceta();
        private CesfamNegocio.Producto.ProductoPorCategoriaActiva bssProductosPorCategoriaActiva = new CesfamNegocio.Producto.ProductoPorCategoriaActiva();
        private CesfamNegocio.Tutor bssTutor = new CesfamNegocio.Tutor();
        private CesfamNegocio.RelPaciente bssRel = new CesfamNegocio.RelPaciente();
        private CesfamNegocio.RelPaciente.PacienteTutor bssPacienteTutor = new CesfamNegocio.RelPaciente.PacienteTutor();

        #region tutor
        public bool crearTutor(int idTutor, string fechaNacimiento, string nombre, string apPaterno, string apMaterno, string correo, string parentesco)
        {
            DateTime fechaNac = DateTime.Parse(fechaNacimiento);
            return bssTutor.crearTutor(idTutor, fechaNac, nombre, apPaterno, apMaterno, correo, parentesco);
        }

        public List<CesfamNegocio.Tutor> listarTutor()
        {
            return bssTutor.ListarTutor();
        }

        public bool actualizarTutor(int idTutor, string fechaNacimiento, string nombre, string apPaterno, string apMaterno, string correo, string parentesco)
        {
            return bssTutor.Update(idTutor, fechaNacimiento, nombre, apPaterno, apMaterno, correo, parentesco);
        }
        #endregion

        #region Usuario

        public bool verificarUsuario(string usuario, string clave)
        {
            return bssUsuario.verificarUsuario(usuario, clave);
        }

        public string recueprarNombre(string usuario)
        {
            return bssUsuario.recueprarNombre(usuario);
        }

        public CesfamNegocio.Usuario enviarDatosUsuario(string usuario)
        {
            return bssUsuario.enviarDatosUsuario(usuario);
        }
        #endregion

        #region categoria
        public List<CesfamNegocio.Categoria> listarCategoriaAct()
        {
            return bssCategoria.listarCategoriaAct();
        }

        #endregion

        #region paciente
        public List<CesfamNegocio.Paciente> listarPacientes()
        {
            return bssPaciente.listarPacientes();
        }

        public List<CesfamNegocio.Paciente.PacienteUltimaReceta> listarPacienteUltimaReceta()
        {
            return bssPacienteUltimaReceta.listarPacienteUltimaReceta();
        }

        public List<CesfamNegocio.Paciente> listarPacientesPorId(int idPaciente)
        {
            return bssPaciente.listarPacientesPorId(idPaciente);
        }


        public bool crearPaciente(int idPaciente, string fechaNacimiento, string nombre, string apPaterno, string apMaterno, string correo, string codigoLibreta, string rut, string direccion)
        {
            DateTime fechaNac = DateTime.Parse(fechaNacimiento);
            return bssPaciente.crearPaciente(idPaciente, fechaNac, nombre, apPaterno, apMaterno, correo, codigoLibreta, rut, direccion);
        }

        public bool actualizarPaciente(int idPaciente, string apMaterno, string apPaterno, string codigoLibreta, string correo, string fechaNac, string nombre, string rut, string direccion)
        {
            return bssPaciente.Update(idPaciente, apMaterno, apPaterno, codigoLibreta, correo, fechaNac, nombre, rut, direccion);
        }
        #endregion

        #region producto
        public List<CesfamNegocio.Producto.ProductoPorCategoriaActiva> listarProductosPorCategoriaActiva(int idCategoriaJava)
        {
            return bssProductosPorCategoriaActiva.productoPorCategoriaActiva(idCategoriaJava);
        }

        public int obtenerStockActual(int idProducto)
        {
            return bssProducto.obtenerStockActual(idProducto);
        }
        #endregion

        #region receta
        public bool IngresarReceta(int idReceta, string estadoReceta, string fechaReceta, int idTipoReceta, int idPaciente, string diagnostico, int cantidadTiempo, int idUsuario, String[] DetalleReceta)
        {
            return bssReceta.IngresarReceta(idReceta, estadoReceta, fechaReceta, idTipoReceta, idPaciente, diagnostico, cantidadTiempo, idUsuario, DetalleReceta);
        }
        #endregion

        #region relacionPacienteTutor
        public bool crearRelacionPacienteTutor(int idRelacion, int idPaciente, int idTutor)
        {
            return bssRel.crearRelacionPacienteTutor(idRelacion, idPaciente, idTutor);
        }

        public List<CesfamNegocio.RelPaciente.PacienteTutor> listarPacienteTutor()
        {
            return bssPacienteTutor.listarPacienteTutor();
        }
        #endregion


    }
}
