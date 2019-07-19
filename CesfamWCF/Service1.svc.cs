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
    public class Service1 : IService1
    { 
        #region Usuario
        public bool verificarUsuario(string usuario, string clave)
        {
            CesfamNegocio.Usuario bssUsuario = new CesfamNegocio.Usuario();
            return bssUsuario.verificarUsuario(usuario, clave);
        }
        public string recueprarNombre(string usuario)
        {
            CesfamNegocio.Usuario bssUsuario = new CesfamNegocio.Usuario();
            return bssUsuario.recueprarNombre(usuario);
        }
        CesfamNegocio.Usuario IService1.enviarDatosUsuario(string usuario)
        {     
            CesfamNegocio.Usuario bssUsuario = new CesfamNegocio.Usuario();
            return bssUsuario.enviarDatosUsuario(usuario);            
        }
        #endregion
        #region Producto
        public bool crearProducto(int idProducto, string codigoProducto, string descripcion, string componente, int cantidadMinima, int gramaje, string nombre, string estado, int idFabricante, int idCategoria, string unidadMedida)
        {     
            CesfamNegocio.Producto bssProducto = new CesfamNegocio.Producto();
            return bssProducto.crearProducto(idProducto, codigoProducto, descripcion, componente, cantidadMinima, gramaje, nombre, estado, idFabricante, idCategoria, unidadMedida);
        }
        public bool UpdateProducto(int idProducto, string codigoProducto, string descripcion, string componente, int cantidadMinima, int gramaje, string nombre, string estado, int idFabricante, int idCategoria, string unidadMedida)
        {
            CesfamNegocio.Producto bssProducto = new CesfamNegocio.Producto();
            return bssProducto.UpdateProducto(idProducto, codigoProducto, descripcion, componente, cantidadMinima, gramaje, nombre, estado, idFabricante, idCategoria, unidadMedida);
        }
        public bool ReadProducto(int idProducto)
        {
            CesfamNegocio.Producto bssProducto = new CesfamNegocio.Producto();
            return bssProducto.Read(idProducto);
        }
        public bool ActivarDesactivarProducto(int idProducto, string Estado)
        {
            CesfamNegocio.Producto bssProducto = new CesfamNegocio.Producto();
            return bssProducto.ActivarDesactivar(idProducto, Estado);
        }
        public DataTable ListarProductosData()
        {
            CesfamNegocio.Producto bssProducto = new CesfamNegocio.Producto();
            return bssProducto.ListarProductosData();
        }
        public DataTable ListarProdXTipo(int idCategoria)
        {
            CesfamNegocio.Producto bssProducto = new CesfamNegocio.Producto();
            return bssProducto.ListarProdXTipo(idCategoria);
        }
        public DataTable ListarProducActivos()
        {
            CesfamNegocio.Producto bssProducto = new CesfamNegocio.Producto();
            return bssProducto.ListarProducActivos();
        }
        public DataTable ListarProdXTipoActivo(int idCategoria)
        {
            CesfamNegocio.Producto bssProducto = new CesfamNegocio.Producto();
            return bssProducto.ListarProdXTipoActivo(idCategoria);
        }
        #endregion
        #region Fabricante
        public bool ReadFabricante(int idFabricante)
        {
            CesfamNegocio.Fabricante bssFabricante = new CesfamNegocio.Fabricante();
            return bssFabricante.Read(idFabricante);
        }
        public bool CrearFabricante(int idFabricante, string nombreFabricante, string descripcion, string estadoFabricante)
        {
            CesfamNegocio.Fabricante bssFabricante = new CesfamNegocio.Fabricante();
            return bssFabricante.create(idFabricante, nombreFabricante, descripcion, estadoFabricante);
        }
        public bool UpdateFabricante(int idFabricante, string nombreFabricante, string descripcion, string estadoFabricante)
        {
            CesfamNegocio.Fabricante bssFabricante = new CesfamNegocio.Fabricante();
            return bssFabricante.Update(idFabricante, nombreFabricante, descripcion, estadoFabricante);
        }
        public bool ActivarDesactivarFabricante(int idFabricante, string estado)
        {
            CesfamNegocio.Fabricante bssFabricante = new CesfamNegocio.Fabricante();
            return bssFabricante.ActivarDesactivar(idFabricante, estado);
        }
        public DataTable ListarTodosFabricantes()
        {
            CesfamNegocio.Fabricante bssFabricante = new CesfamNegocio.Fabricante();
            return bssFabricante.ListarTodosFabricantes();
        }
        public DataTable ListarFabricantesActivos()
        {
            CesfamNegocio.Fabricante bssFabricante = new CesfamNegocio.Fabricante();
            return bssFabricante.ListarFabricantesActivos();
        }
        public DataTable ListarFabricantesXnombre(string nombreFab)
        {
            CesfamNegocio.Fabricante bssFabricante = new CesfamNegocio.Fabricante();
            return bssFabricante.ListarFabricantesXnombre(nombreFab);
        }
        #endregion
        #region Categorias        
        public bool crearCategoria(int idCategoria, string nombreTipo, string estado)
        {
            CesfamNegocio.Categoria bssCategoria = new CesfamNegocio.Categoria();
            return bssCategoria.create(idCategoria, nombreTipo, estado);
        }
        public bool ReadCategoria(int idCategoria)
        {
            CesfamNegocio.Categoria bssCategoria = new CesfamNegocio.Categoria();
            return bssCategoria.Read(idCategoria);
        }
        public bool UpdateCategoria(int idCategoria, string nombreTipo, string estado)
        {
            CesfamNegocio.Categoria bssCategoria = new CesfamNegocio.Categoria();
            return bssCategoria.Update(idCategoria, nombreTipo, estado);
        }
        public bool ActivarDesactivarCategoria(int idCategoria, string estado)
        {
            CesfamNegocio.Categoria bssCategoria = new CesfamNegocio.Categoria();
            return bssCategoria.ActivarDesactivarCategoria(idCategoria, estado);
        }
        public DataTable ListarTodosCategoria()
        {
            CesfamNegocio.Categoria bssCategoria = new CesfamNegocio.Categoria();
            return bssCategoria.ListarTodosCategoria();
        }
        public DataTable ListarCategoriaXnombre(string nombreCat)
        {
            CesfamNegocio.Categoria bssCategoria = new CesfamNegocio.Categoria();
            return bssCategoria.ListarCategoriaXnombre(nombreCat);
        }
        public DataTable ListarCategoriasActivas()
        {
            CesfamNegocio.Categoria bssCategoria = new CesfamNegocio.Categoria();
            return bssCategoria.ListarCategoriasActivas();
        }
        #endregion
        #region ManejoStock
        public DataTable ListarStockTotalPorNombre(string Nombre)
        {
            CesfamNegocio.DetalleIngresoStock bssDetalleIngreso = new CesfamNegocio.DetalleIngresoStock();
            return bssDetalleIngreso.ListarStockTotalPorNombre(Nombre);
        }
        public DataTable ListarStockTotal()
        {
            CesfamNegocio.DetalleIngresoStock bssDetalleIngreso = new CesfamNegocio.DetalleIngresoStock();
            return bssDetalleIngreso.ListarStockTotal();
        }
        public DataTable ListarStock()
        {
            CesfamNegocio.DetalleIngresoStock bssDetalleIngreso = new CesfamNegocio.DetalleIngresoStock();
            return bssDetalleIngreso.ListarStock();
        }
        public DataTable ListarStockXCategoria(int idCategoria)
        {
            CesfamNegocio.DetalleIngresoStock bssDetalleIngreso = new CesfamNegocio.DetalleIngresoStock();
            return bssDetalleIngreso.ListarStockXCategoria(idCategoria);
        }
        public DataTable ListarStockXLote(string NomLote)
        {
            CesfamNegocio.DetalleIngresoStock bssDetalleIngreso = new CesfamNegocio.DetalleIngresoStock();
            return bssDetalleIngreso.ListarStockXLote(NomLote);
        }
        public DataTable ListarStockProducto(int idProducto)
        {
            CesfamNegocio.DetalleIngresoStock bssDetalleIngreso = new CesfamNegocio.DetalleIngresoStock();
            return bssDetalleIngreso.ListarStockProducto(idProducto);
        }
        public bool ReadSalidaStock(int idSalida)
        {
            CesfamNegocio.SalidaStock bssSalidaStock = new CesfamNegocio.SalidaStock();
            return bssSalidaStock.Read(idSalida);
        }
        public bool RegistrarSalida(int idSalidaStock, DateTime fecha, int idUsuario, int idTipoSalida, DataTable dtDetalle)
        {
            CesfamNegocio.SalidaStock bssSalidaStock = new CesfamNegocio.SalidaStock();
            return bssSalidaStock.RegistrarSalida(idSalidaStock, fecha, idUsuario, idTipoSalida, dtDetalle);
        }
        public bool EntregaMedicamentos(int idSalidaStock, DateTime fecha, int idUsuario, int idTipoSalida, DataTable dtDetalle, int idReceta, bool RetiraTutor, int idPaciente, int idTipoReceta, bool conReserva)
        {
            CesfamNegocio.SalidaStock bssSalidaStock = new CesfamNegocio.SalidaStock();
            return bssSalidaStock.EntregaMedicamentos(idSalidaStock, fecha, idUsuario, idTipoSalida, dtDetalle, idReceta, RetiraTutor, idPaciente, idTipoReceta, conReserva);
        }
        public bool AnularSalida(int idSalida)
        {
            CesfamNegocio.SalidaStock bssSalidaStock = new CesfamNegocio.SalidaStock();
            return bssSalidaStock.AnularSalida(idSalida);
        }
        #endregion
        #region Receta
        public DataTable ListarRecetas()
        {
            CesfamNegocio.Receta bssReceta = new CesfamNegocio.Receta();
            return bssReceta.ListarRecetas();
        }
        public DataTable ListarDetalleReceta(int idReceta)
        {
            CesfamNegocio.DetalleReceta bssDetalleReceta = new CesfamNegocio.DetalleReceta();
            return bssDetalleReceta.ListarDetalleReceta(idReceta);
        }
        public DataTable ListarRecetasXTipo(int idTipoReceta)
        {
            CesfamNegocio.Receta bssReceta = new CesfamNegocio.Receta();
            return bssReceta.ListarRecetasXTipo(idTipoReceta);
        }
        public DataTable ListarReceXCodigoPac(string codigoPaciente)
        {
            CesfamNegocio.Receta bssReceta = new CesfamNegocio.Receta();
            return bssReceta.ListarReceXCodigoPac(codigoPaciente);
        }
        public bool ActualizarQuienRetiraRel(int idSalida, int idPaciente)
        {
            CesfamNegocio.SalidaStock bssSalidaStock = new CesfamNegocio.SalidaStock();
            return bssSalidaStock.ActualizarQuienRetiraRel(idSalida, idPaciente);
        }
        public bool revisarPrescripciones()
        {
            CesfamNegocio.Receta bssReceta = new CesfamNegocio.Receta();
            return bssReceta.revisarPrescripciones();
        }
        #endregion
        #region Ingreso
        public bool ReadIngreso(int idIngreso)
        {
            CesfamNegocio.IngresoStock bssIngresoStock = new CesfamNegocio.IngresoStock();
            return bssIngresoStock.Read(idIngreso);

        }
        public bool RegistrarIngreso(int idIngreso, DateTime fecha, int idUsuario, DataTable dtDetalle)
        {
            CesfamNegocio.IngresoStock bssIngresoStock = new CesfamNegocio.IngresoStock();
            return bssIngresoStock.RegistrarIngreso(idIngreso, fecha, idUsuario, dtDetalle);
        }
        #endregion
        #region TipoReceta
        public List<CesfamNegocio.TipoReceta> ListarTipoReceta()
        {
            CesfamNegocio.TipoReceta bssTipoReceta = new CesfamNegocio.TipoReceta();
            return bssTipoReceta.ListarTipoReceta();
        }
        #endregion
        #region Reserva 
        public bool CrearReserva(int idReserva, string estadoReserva, int idPaciente, int idProducto, int cantidadReserva, int idUsuario, DateTime fechaReserva)
        {
            CesfamNegocio.Reserva bssReserva = new CesfamNegocio.Reserva();
            return bssReserva.CrearReserva(idReserva, estadoReserva, idPaciente, idProducto, cantidadReserva, idUsuario, fechaReserva);
        }
        public bool ReadReserva(int idReserva)
        {
            CesfamNegocio.Reserva bssReserva = new CesfamNegocio.Reserva();
            return bssReserva.ReadReserva(idReserva);
        }
        public bool ActualizarReserva(int idReserva, string estadoReserva,DateTime Fecha)
        {
            CesfamNegocio.Reserva bssReserva = new CesfamNegocio.Reserva();
            return bssReserva.ActualizarReserva(idReserva, estadoReserva, Fecha);
        }
        public bool EliminarReserva(int idReserva)
        {
            CesfamNegocio.Reserva bssReserva = new CesfamNegocio.Reserva();
            return bssReserva.EliminarReserva(idReserva);
        }
        public DataTable ListarDetalleReserva(int idReserva)
        {
            CesfamNegocio.DetalleSalidaStock bssDetalleSalida = new CesfamNegocio.DetalleSalidaStock();
            return bssDetalleSalida.ListarDetalleReserva(idReserva);
        }
        #endregion
        #region Receta
        public bool ActualizarEstado(int idDetalleReceta, string estado)
        {
            CesfamNegocio.DetalleReceta bssDetalleReceta = new CesfamNegocio.DetalleReceta();
            return bssDetalleReceta.ActualizarEstado(idDetalleReceta, estado);
        }
        public bool ActualizarRecetaEntregada(int idReceta, bool ConReserva)
        {
            CesfamNegocio.Receta bssReceta = new CesfamNegocio.Receta();
            return bssReceta.ActualizarRecetaEntregada(idReceta, ConReserva);
        }
        public DataTable ListarReservas()
        {
            CesfamNegocio.Reserva bssReserva = new CesfamNegocio.Reserva();
            return bssReserva.ListarReservas();
        }
        #endregion       
        #region Alertas
        public DataTable ListarStockPorVencer()
        {
            CesfamNegocio.DetalleIngresoStock bssDetalleIngreso = new CesfamNegocio.DetalleIngresoStock();
            return bssDetalleIngreso.ListarStockPorVencer();
        }
        #endregion
        #region TipoDocumento
        public List<CesfamNegocio.TipoUsuario> ListarTipoUsuario()
        {
            CesfamNegocio.TipoUsuario bssTipoUsuario = new CesfamNegocio.TipoUsuario();
            return bssTipoUsuario.ListarTipoUsuario();

        }
        #endregion












        public bool Read(int idUsuario)
        {
            CesfamNegocio.Usuario bssUsuario = new CesfamNegocio.Usuario();
            return bssUsuario.read(idUsuario);
        }

        public bool create(int idUsuario, string nombre, string apellidoPaterno, string apellidoMaterno, string nombreUsuario, string clave, DateTime fecha, int IdTipoUsuario)
        {
            CesfamNegocio.Usuario bssUsuario = new CesfamNegocio.Usuario();
            return bssUsuario.create(idUsuario, nombre, apellidoPaterno, apellidoMaterno, nombreUsuario, clave,fecha, IdTipoUsuario);
        }


        public bool verificarNombreUsuario(string usuario)
        {
            CesfamNegocio.Usuario bssUsuario = new CesfamNegocio.Usuario();
            return bssUsuario.verificarNombreUsuario(usuario);
        }
    }
}
