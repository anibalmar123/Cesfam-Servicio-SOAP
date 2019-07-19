using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace CesfamWCF
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IService1" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IService1
    {
        #region Usuario
        [OperationContract]
        bool verificarUsuario(string usuario, string clave);
        [OperationContract]
        string recueprarNombre(string usuario);
        [OperationContract]
        CesfamNegocio.Usuario enviarDatosUsuario(string usuario);
        #endregion
        #region Producto
        [OperationContract]
        bool crearProducto(int idProducto, string codigoProducto, string descripcion, string componente, int cantidadMinima, int gramaje, string nombre, string estado, int idFabricante, int idCategoria, string unidadMedida);
        [OperationContract]
        bool UpdateProducto(int idProducto, string codigoProducto, string descripcion, string componente, int cantidadMinima, int gramaje, string nombre, string estado, int idFabricante, int idCategoria, string unidadMedida);
        [OperationContract]
        bool ActivarDesactivarProducto(int idProducto, string Estado);
        [OperationContract]
        bool ReadProducto(int idProducto);
        [OperationContract]
        DataTable ListarProductosData();
        [OperationContract]
        DataTable ListarProdXTipo(int idCategoria);
        [OperationContract]
        DataTable ListarProducActivos();
        [OperationContract]
        DataTable ListarProdXTipoActivo(int idCategoria);
        #endregion
        #region Fabricante
        [OperationContract]
        DataTable ListarTodosFabricantes();
        [OperationContract]
        bool ReadFabricante(int idFabricante);
        [OperationContract]
        bool CrearFabricante(int idFabricante, string nombreFabricante, string descripcion, string estadoFabricante);
        [OperationContract]
        bool UpdateFabricante(int idFabricante, string nombreFabricante, string descripcion, string estadoFabricante);
        [OperationContract]
        bool ActivarDesactivarFabricante(int idFabricante, string estado);
        [OperationContract]
        DataTable ListarFabricantesXnombre(string nombreFab);
        [OperationContract]
        DataTable ListarFabricantesActivos();
        #endregion
        #region Categoria
        [OperationContract]
        bool crearCategoria(int idCategoria, string nombreTipo, string estado);
        [OperationContract]
        bool ReadCategoria(int idCategoria);
        [OperationContract]
        bool UpdateCategoria(int idCategoria, string nombreTipo, string estado);
        [OperationContract]
        bool ActivarDesactivarCategoria(int idCategoria, string estado);
        [OperationContract]
        DataTable ListarTodosCategoria();        
        [OperationContract]
        DataTable ListarCategoriaXnombre(string nombreCat);
        [OperationContract]
        DataTable ListarCategoriasActivas();
        #endregion
        #region Ingresos
        [OperationContract]
        bool ReadIngreso(int idIngreso);
        [OperationContract]
        bool RegistrarIngreso(int idIngreso, DateTime fecha, int idUsuario, DataTable dtDetalle);
        #endregion
        #region Salidas
        [OperationContract]
        bool ReadSalidaStock(int idSalida);
        [OperationContract]
        bool RegistrarSalida(int idSalidaStock, DateTime fecha, int idUsuario, int idTipoSalida, DataTable dtDetalle);
        [OperationContract]
        bool EntregaMedicamentos(int idSalidaStock, DateTime fecha, int idUsuario, int idTipoSalida, DataTable dtDetalle, int idReceta, bool RetiraTutor, int idPaciente, int idTipoReceta, bool conReserva);
        #endregion
        #region Listas de Stock
        [OperationContract]
        DataTable ListarStockTotal();
        [OperationContract]
        DataTable ListarStock();
        [OperationContract]
        DataTable ListarStockXCategoria(int idCategoria);
        [OperationContract]
        DataTable ListarStockXLote(string NomLote);
        [OperationContract]
        DataTable ListarStockPorVencer();
        [OperationContract]
        DataTable ListarStockProducto(int idProducto);
        [OperationContract]
        DataTable ListarStockTotalPorNombre(string Nombre);
        #endregion
        #region Receta
        [OperationContract]
        DataTable ListarRecetas();
        [OperationContract]
        DataTable ListarDetalleReceta(int idReceta);
        [OperationContract]
        List<CesfamNegocio.TipoReceta> ListarTipoReceta();
        [OperationContract]
        DataTable ListarRecetasXTipo(int idTipoReceta);
        [OperationContract]
        DataTable ListarReceXCodigoPac(string codigoPaciente);
        [OperationContract]
        bool ActualizarRecetaEntregada(int idReceta, bool ConReserva);
        [OperationContract]
        bool AnularSalida(int idSalida);
        [OperationContract]
        bool ActualizarQuienRetiraRel(int idSalida, int idPaciente);
        [OperationContract]
        bool ActualizarEstado(int idDetalleReceta, string estado);
        [OperationContract]
        bool revisarPrescripciones();
        #endregion
        #region Reservas
        [OperationContract]
        bool CrearReserva(int idReserva, string estadoReserva, int idPaciente, int idProducto, int cantidadReserva, int idUsuario, DateTime fechaReserva);
        [OperationContract]
        bool ReadReserva(int idReserva);
        [OperationContract]
        bool ActualizarReserva(int idReserva, string estadoReserva, DateTime Fecha);
        [OperationContract]
        bool EliminarReserva(int idReserva);
        [OperationContract]
        DataTable ListarReservas();
        [OperationContract]
        DataTable ListarDetalleReserva(int idReserva);
        #endregion
        #region Reportes

        #endregion
        #region Usuarios
        [OperationContract]
        List<CesfamNegocio.TipoUsuario> ListarTipoUsuario();
        [OperationContract]
        bool Read(int idUsuario);
        [OperationContract]
        bool create(int idUsuario, string nombre, string apellidoPaterno, string apellidoMaterno, string nombreUsuario, string clave, DateTime fecha, int IdTipoUsuario);
        [OperationContract]
        bool verificarNombreUsuario(string usuario);
        #endregion
        
        


        


        

        
        
    }
}
