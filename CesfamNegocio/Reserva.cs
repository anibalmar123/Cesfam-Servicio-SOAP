using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CesfamDatos;

namespace CesfamNegocio
{
    public class Reserva
    {
        #region Atributos
        public int IdReserva { get; set; }
        public string EstadoReserva { get; set; }
        public int IdReceta { get; set; }
        public int IdProducto { get; set; }
        public int CantidadReserva { get; set; }
        #endregion
        public bool CrearReserva(int idReserva, string estadoReserva, int idReceta, int idProducto, int cantidadReserva, int idUsuario, DateTime FechaReserva)
        {
            try
            {
                RESERVA reserva = new RESERVA();
                reserva.ID_RESERVA = idReserva;
                reserva.ESTADO_RESERVA = estadoReserva;
                reserva.RECETA_ID_RECETA = idReceta;
                reserva.PRODUCTO_ID_PRODUCTO = idProducto;
                reserva.CANTIDAD_RESERVA = cantidadReserva;
                reserva.USUARIO_ID_USUARIO = idUsuario;
                reserva.FECHA_RESERVA = FechaReserva;
                Acceso.ModeloCesfam.RESERVA.Add(reserva);
                Acceso.ModeloCesfam.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public bool ReadReserva(int idReserva)
        {
            try
            {
                CesfamDatos.RESERVA reserva = Acceso.ModeloCesfam.RESERVA.First(tp => tp.ID_RESERVA == idReserva);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool ActualizarReserva(int idReserva, string estadoReserva,DateTime fechaReserva)
        {
            try
            {
                CesfamDatos.RESERVA reserva = Acceso.ModeloCesfam.RESERVA.First(tp => tp.ID_RESERVA == idReserva);
                reserva.ESTADO_RESERVA = estadoReserva;
                reserva.FECHA_RESERVA = fechaReserva;
                Acceso.ModeloCesfam.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool EliminarReserva(int idReserva)
        {
            try
            {
                CesfamDatos.RESERVA reserva = Acceso.ModeloCesfam.RESERVA.First(tp => tp.ID_RESERVA == idReserva);
                Acceso.ModeloCesfam.RESERVA.Remove(reserva);
                Acceso.ModeloCesfam.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool VerificarReservasPendientes(int idProducto, int idDetalleIngreso, int idUsuario)
        {
            try
            {
                var list = (from Res in Acceso.ModeloCesfam.RESERVA.ToList()
                            where Res.PRODUCTO_ID_PRODUCTO == idProducto && Res.ESTADO_RESERVA.Equals("Pendiente")
                            orderby Res.FECHA_RESERVA ascending
                            select new
                            {
                                IdReserva = Res.ID_RESERVA,
                                IdProducto = Res.PRODUCTO_ID_PRODUCTO,
                                nombreProducto = Res.PRODUCTO.NOMBRE_PRODUCTO,
                                Cantidad = Res.CANTIDAD_RESERVA,
                                IdReceta = Res.RECETA_ID_RECETA,
                                idPaciente = Res.RECETA.PACIENTE_ID_PACIENTE,
                                nombrePaciente = Res.RECETA.PACIENTE.NOMBRE + " " + Res.RECETA.PACIENTE.APELLIDO_PATERNO,
                                codigoPaciente = Res.RECETA.PACIENTE.CODIGO_LIBRETA,
                                correoPaciente = Res.RECETA.PACIENTE.CORREO,
                                NombreMedico = Res.RECETA.USUARIO.NOMBRE + " " + Res.RECETA.USUARIO.APELLIDO_PATERNO,
                            });
                CesfamDatos.DETALLE_INGRESO_STOCK dataDetalle = Acceso.ModeloCesfam.DETALLE_INGRESO_STOCK.First(det => det.ID_DETALLE_INGRESO == idDetalleIngreso);
                foreach (var item in list.ToList())
                {
                    if (item.Cantidad < dataDetalle.STOCK_ACTUAL)
                    {
                        CesfamNegocio.DetalleIngresoStock bssDetalleIngreso = new DetalleIngresoStock();
                        CesfamNegocio.SalidaStock bssSalida = new SalidaStock();
                        CesfamNegocio.DetalleSalidaStock bssDetalleSalida = new DetalleSalidaStock();
                        CesfamNegocio.DetalleReceta bssDetReceta = new DetalleReceta();
                        CesfamNegocio.CorreosAvisos bssCcorreo = new CorreosAvisos();
                        Random RndId2 = new Random();
                        int idSalidaStock = RndId2.Next(1, 9999999);
                        while (bssSalida.Read(idSalidaStock))
                        {
                            idSalidaStock = RndId2.Next(1, 9999999);
                        }
                        bssSalida.EntregaReserva(idSalidaStock, DateTime.Now, idUsuario, 4, (int)item.IdReceta, (int)item.idPaciente, (int)item.IdReserva);
                        this.ActualizarReserva((int)item.IdReserva,"Entregar", DateTime.Now);
                        Random RndId = new Random();
                        int idDetSalidaStock = RndId.Next(1, 9999999);
                        while (bssDetalleSalida.Read(idDetSalidaStock))
                        {
                            idDetSalidaStock = RndId.Next(1, 9999999);
                        }
                        bssDetalleSalida.RegistrarDetalle(idDetSalidaStock, (int)item.Cantidad, "Reserva", idSalidaStock, idDetalleIngreso);
                        bssDetalleIngreso.ActualizarStockActual(idDetalleIngreso, (int)item.Cantidad);
                        bssDetReceta.ActualizarEstado(bssDetReceta.idDetalleEntregado((int)item.IdReceta, (int)item.IdProducto), "Entregado");
                        
                        string asunto = string.Format("Producto reservado disponible");
                        string mensaje = string.Format("Estimada " + item.nombrePaciente + ": \nJunto con saludar, le informo que el producto " + item.nombreProducto + " que fue reservado ya se encuentra disponible para retiro. \n\nProducto: " + item.nombreProducto + "\nCantidad: " + item.Cantidad + " \n\nSaludos Cordiales. \n\nSe despide Cesfam Farmacia.");
                        bssCcorreo.CorreoElectronico(item.correoPaciente, asunto, mensaje);
                    }
                }
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public DataTable ListarReservas()
        {
            var list = (from Res in Acceso.ModeloCesfam.RESERVA.ToList()
                        where Res.ESTADO_RESERVA.Equals("Entregar")
                        orderby Res.FECHA_RESERVA ascending
                        select new
                        {
                            IdReserva = Res.ID_RESERVA,
                            IdProducto = Res.PRODUCTO_ID_PRODUCTO,
                            Producto = Res.PRODUCTO.NOMBRE_PRODUCTO,
                            Cantidad = Res.CANTIDAD_RESERVA,
                            IdReceta = Res.RECETA_ID_RECETA,
                            idPaciente = Res.RECETA.PACIENTE_ID_PACIENTE,
                            nombrePaciente = Res.RECETA.PACIENTE.NOMBRE+" "+Res.RECETA.PACIENTE.APELLIDO_PATERNO,
                            codigoPaciente = Res.RECETA.PACIENTE.CODIGO_LIBRETA,
                            NombreMedico = Res.RECETA.USUARIO.NOMBRE+" "+Res.RECETA.USUARIO.APELLIDO_PATERNO,
                            estadoReserva = Res.ESTADO_RESERVA,
                            fechaReserva = Res.FECHA_RESERVA
                        });
            DataTable dts = new DataTable("Lista Stock");
            dts.Columns.Add("IdReserva");
            dts.Columns.Add("IdProducto");
            dts.Columns.Add("Nombre Producto");
            dts.Columns.Add("Cantidad Reservada");
            dts.Columns.Add("Estado Reserva");
            dts.Columns.Add("IdReceta");
            dts.Columns.Add("IdPaciente");
            dts.Columns.Add("Nombre Paciente");
            dts.Columns.Add("Codigo");
            dts.Columns.Add("Nombre Medico");
            dts.Columns.Add("Fecha Reserva");
            foreach (var data in list)
            {
                dts.Rows.Add(data.IdReserva, data.IdProducto,data.Producto,data.Cantidad,data.estadoReserva,data.IdReceta,data.idPaciente,data.nombrePaciente,data.codigoPaciente,data.NombreMedico,data.fechaReserva);
            }
            return dts;
        }       
    }
}
