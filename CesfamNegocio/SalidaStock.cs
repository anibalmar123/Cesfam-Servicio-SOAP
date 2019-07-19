using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using CesfamDatos;

namespace CesfamNegocio
{
    public class SalidaStock
    {
        public int IdSalidaStock { get; set; }
        public DateTime Fecha { get; set; }
        public int IdUsuario { get; set; }
        public int IdTipoSalida { get; set; }
        public string Estado { get; set; }
        public SalidaStock() { }
        public bool RegistrarSalida(int idSalidaStock, DateTime fecha, int idUsuario, int idTipoSalida, DataTable dtDetalle)
        {
            try
            {
                SALIDA_STOCK salidaStock = new SALIDA_STOCK();
                salidaStock.ID_SALIDA_STOCK = idSalidaStock;
                salidaStock.FECHA = fecha;
                salidaStock.USUARIO_ID_USUARIO = idUsuario;
                salidaStock.TIPO_SALIDA_ID_TIPO_SALIDA = idTipoSalida;
                salidaStock.ESTADO = "ACTIVO";
                foreach (DataRow row in dtDetalle.Rows)
                {
                    DetalleSalidaStock detalle = new DetalleSalidaStock();
                    DetalleIngresoStock detalleIngreso = new DetalleIngresoStock();
                    Random RndId = new Random();
                    int idDetalleSalida = RndId.Next(1, 9999999);
                    while (detalle.Read(idDetalleSalida))
                    {
                        idDetalleSalida = RndId.Next(1, 9999999);
                    }
                    int cantidadRebajada = Convert.ToInt32(row[2].ToString());
                    string descripcion = row[4].ToString();
                    int idSalStock = idSalidaStock;
                    int idDetalleIngreso = Convert.ToInt32(row[0].ToString());
                    detalle.RegistrarDetalle(idDetalleSalida, cantidadRebajada, descripcion, idSalStock, idDetalleIngreso);
                    detalleIngreso.ActualizarStockActual(idDetalleIngreso, cantidadRebajada);
                }
                Acceso.ModeloCesfam.SALIDA_STOCK.Add(salidaStock);
                Acceso.ModeloCesfam.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool EntregaMedicamentos(int idSalidaStock, DateTime fecha, int idUsuario, int idTipoSalida, DataTable dtDetalle, int idReceta, bool RetiraTutor,int idPaciente, int idTipoReceta, bool conReserva)
        {
            try
            {                
                CesfamNegocio.Receta bssReceta = new Receta();
                int idRetira;
                if (RetiraTutor)
                {
                    var tutor = Acceso.ModeloCesfam.REL_PACIENTE.First(ret => ret.PACIENTE_ID_PACIENTE == idPaciente);
                    idRetira = (int)tutor.TUTOR_ID_TUTOR;
                }
                else 
                {
                    idRetira = idPaciente;
                }
                CesfamDatos.REL_RECETA dataRelReceta = new REL_RECETA();
                if (this.RegistrarSalida(idSalidaStock, fecha, idUsuario, idTipoSalida, dtDetalle))
                {
                    
                    Random RndId = new Random();
                    int idRelReceta = RndId.Next(1, 9999999);
                    while (this.ReadRelReceta(idRelReceta))
                    {
                        idRelReceta = RndId.Next(1, 9999999);
                    }
                    dataRelReceta.ID_REL_RECETA = idRelReceta;
                    dataRelReceta.ID_RETIRA = idRetira;
                    dataRelReceta.RECETA_ID_RECETA = idReceta;
                    dataRelReceta.SALIDA_STOCK_ID_SALIDA_STOCK = idSalidaStock;
                }                                           
                Acceso.ModeloCesfam.REL_RECETA.Add(dataRelReceta);
                Acceso.ModeloCesfam.SaveChanges();
                bssReceta.ActualizarRecetaEntregada(idReceta, conReserva);   
                return true;
            }
            catch (Exception)
            {
                return false;
            }       
        }
        public bool EntregaReserva(int idSalidaStock, DateTime fecha, int idUsuario, int idTipoSalida, int idReceta, int idPaciente,int idReserva)
        {
            try
            {
                CesfamNegocio.Receta bssReceta = new Receta();
                CesfamNegocio.DetalleReceta bssDetalleRece = new DetalleReceta();
                SALIDA_STOCK salidaStock = new SALIDA_STOCK();
                salidaStock.ID_SALIDA_STOCK = idSalidaStock;
                salidaStock.FECHA = fecha;
                salidaStock.USUARIO_ID_USUARIO = idUsuario;
                salidaStock.TIPO_SALIDA_ID_TIPO_SALIDA = idTipoSalida;
                salidaStock.ESTADO = "ACTIVO";
                Acceso.ModeloCesfam.SALIDA_STOCK.Add(salidaStock);
                Acceso.ModeloCesfam.SaveChanges();

                CesfamDatos.REL_RECETA dataRelReceta = new REL_RECETA();
                Random RndId = new Random();
                int idRelReceta = RndId.Next(1, 9999999);
                while (this.ReadRelReceta(idRelReceta))
                {
                    idRelReceta = RndId.Next(1, 9999999);
                }
                dataRelReceta.ID_REL_RECETA = idRelReceta;
                dataRelReceta.ID_RETIRA = idPaciente;
                dataRelReceta.RECETA_ID_RECETA = idReceta;
                dataRelReceta.SALIDA_STOCK_ID_SALIDA_STOCK = idSalidaStock;

                Acceso.ModeloCesfam.REL_RECETA.Add(dataRelReceta);
                Acceso.ModeloCesfam.SaveChanges();
                CesfamDatos.REL_RESERVA dataRelReserva = new REL_RESERVA();
                Random RndId2 = new Random();
                int idRelReserva = RndId2.Next(1, 9999999);
                while (this.ReadRelReServa(idRelReserva))
                {
                    idRelReserva = RndId.Next(1, 9999999);
                }
                dataRelReserva.ID_REL_RESERVA = idRelReceta;
                dataRelReserva.RESERVA_ID_RESERVA = idReserva;
                dataRelReserva.SALIDA_STOCK_ID_SALIDA_STOCK = idSalidaStock;
                Acceso.ModeloCesfam.REL_RESERVA.Add(dataRelReserva);
                Acceso.ModeloCesfam.SaveChanges();

                if(bssDetalleRece.ReservasPorReceta(idReceta) < 2)
                {
                    bssReceta.ActualizarReservaEntregada(idReceta);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool Read(int idSalida)
        {
            try
            {
                CesfamDatos.SALIDA_STOCK salidaStock = Acceso.ModeloCesfam.SALIDA_STOCK.First(tp => tp.ID_SALIDA_STOCK == idSalida);
                this.IdSalidaStock = (int)salidaStock.ID_SALIDA_STOCK;
                this.Fecha = salidaStock.FECHA;
                this.IdUsuario = (int)salidaStock.USUARIO_ID_USUARIO;
                this.IdTipoSalida = (int)salidaStock.TIPO_SALIDA_ID_TIPO_SALIDA;
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public bool ReadRelReceta(int idRelReceta) 
        {
            try
            {
                CesfamDatos.REL_RECETA dataRelReceta = Acceso.ModeloCesfam.REL_RECETA.First(da => da.ID_REL_RECETA == idRelReceta);
                return true;
            }
            catch (Exception)
            {
                
                return false;
            } 
        }
        public bool ReadRelReServa(int idRelReserva)
        {
            try
            {
                CesfamDatos.REL_RESERVA dataRelReceta = Acceso.ModeloCesfam.REL_RESERVA.First(da => da.ID_REL_RESERVA == idRelReserva);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }        
        public bool ActualizarQuienRetiraRel(int idSalida, int idPaciente)
        {
            try
            {
                CesfamDatos.REL_RECETA dataRelReceta = Acceso.ModeloCesfam.REL_RECETA.First(tp => tp.SALIDA_STOCK_ID_SALIDA_STOCK == idSalida);
                var tutor = Acceso.ModeloCesfam.REL_PACIENTE.First(ret => ret.PACIENTE_ID_PACIENTE == idPaciente);
                dataRelReceta.ID_RETIRA = (int)tutor.TUTOR_ID_TUTOR;
                Acceso.ModeloCesfam.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public bool AnularSalida(int idSalida)
        {
            try
            {
                CesfamDatos.SALIDA_STOCK salidaStock = Acceso.ModeloCesfam.SALIDA_STOCK.First(tp => tp.ID_SALIDA_STOCK == idSalida);
                CesfamDatos.DETALLE_SALIDA_STOCK detalleSalidaStock = Acceso.ModeloCesfam.DETALLE_SALIDA_STOCK.First(tc => tc.ID_SALIDA_STOCK == idSalida);
                DetalleIngresoStock detalleIngreso = new DetalleIngresoStock();
                salidaStock.ESTADO = "ANULADO";
                Acceso.ModeloCesfam.SaveChanges();
                detalleIngreso.DevolverStock((int)detalleSalidaStock.ID_DETALLE_INGRESO, (int)detalleSalidaStock.CANTIDAD);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public List<SalidaStock> GenerarListado(List<SALIDA_STOCK> list)
        {

            List<SalidaStock> listado = new List<SalidaStock>();
            foreach (var item in list)
            {
                SalidaStock salidaStock = new SalidaStock();
                salidaStock.IdSalidaStock = (int)item.ID_SALIDA_STOCK;
                salidaStock.Fecha = item.FECHA;
                salidaStock.IdUsuario = (int)item.USUARIO_ID_USUARIO;
                salidaStock.IdTipoSalida = (int)item.TIPO_SALIDA_ID_TIPO_SALIDA;
                listado.Add(salidaStock);
            }
            return listado;
        }
        public List<SalidaStock> ListarSalidaStock()
        {

            var list = Acceso.ModeloCesfam.SALIDA_STOCK;
            return GenerarListado(list.ToList());
        }
    }
}
