using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace CesfamWCF
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IService2" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IService2
    {
        #region Usuario
        [OperationContract]
        bool verificarUsuario(string usuario, string clave);
        [OperationContract]
        string recueprarNombre(string usuario);
        [OperationContract]
        CesfamNegocio.Usuario enviarDatosUsuario(string usuario);
        #endregion

        #region tutor
        [OperationContract]
        bool crearTutor(int idTutor, string fechaNacimiento, string nombre, string apPaterno, string apMaterno, string correo, string parentesco);

        [OperationContract]
        List<CesfamNegocio.Tutor> listarTutor();

        [OperationContract]
        bool actualizarTutor(int idTutor, string fechaNacimiento, string nombre, string apPaterno, string apMaterno, string correo, string parentesco);
        #endregion

        #region categoria
        [OperationContract]
        List<CesfamNegocio.Categoria> listarCategoriaAct();
        #endregion

        #region paciente
        [OperationContract]
        List<CesfamNegocio.Paciente> listarPacientes();

        [OperationContract]
        List<CesfamNegocio.Paciente.PacienteUltimaReceta> listarPacienteUltimaReceta();

        [OperationContract]
        List<CesfamNegocio.Paciente> listarPacientesPorId(int idPaciente);

        [OperationContract]
        bool crearPaciente(int idPaciente, string fechaNacimiento, string nombre, string apPaterno, string apMaterno, string correo, string codigoLibreta, string rut, string direccion);

        [OperationContract]
        bool actualizarPaciente(int idPaciente, string apMaterno, string apPaterno, string codigoLibreta, string correo, string fechaNac, string nombre, string rut, string direccion);
        #endregion

        #region producto
        [OperationContract]
        List<CesfamNegocio.Producto.ProductoPorCategoriaActiva> listarProductosPorCategoriaActiva(int idCategoriaJava);
        [OperationContract]
        int obtenerStockActual(int idProducto);
        #endregion

        #region receta
        [OperationContract]
        bool IngresarReceta(int idReceta, string estadoReceta, string fechaReceta, int idTipoReceta, int idPaciente, string diagnostico, int cantidadTiempo, int idUsuario, String[] DetalleReceta);
        #endregion

        #region relacionPacienteTutor
        [OperationContract]
        bool crearRelacionPacienteTutor(int idRelacion, int idPaciente, int idTutor);
        [OperationContract]
        List<CesfamNegocio.RelPaciente.PacienteTutor> listarPacienteTutor();
        #endregion

        
        
       


    }
}
