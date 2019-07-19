using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CesfamDatos;

namespace CesfamNegocio
{
    public class DetalleSalidaStock
    {
        public int IdDetalleSalida { get; set; }
        public int Cantidad { get; set; }
        public string Descripcion { get; set; }
        public int IdSalidaStock { get; set; }
        public int IdDetalleIngreso { get; set; }
        public bool RegistrarDetalle(int idDetalleSalida, int cantidad, string descripcion, int idSalidaStock, int idDetalleIngreso)
        {
            try
            {
                DETALLE_SALIDA_STOCK detalleSalida = new DETALLE_SALIDA_STOCK();
                detalleSalida.ID_DETALLE_SALIDA = idDetalleSalida;
                detalleSalida.CANTIDAD = cantidad;
                detalleSalida.DESCRIPCION = descripcion;
                detalleSalida.ID_SALIDA_STOCK = idSalidaStock;
                detalleSalida.ID_DETALLE_INGRESO = idDetalleIngreso;
                Acceso.ModeloCesfam.DETALLE_SALIDA_STOCK.Add(detalleSalida);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public bool Read(int idDetSalida)
        {
            try
            {
                CesfamDatos.DETALLE_SALIDA_STOCK detalleSalida = Acceso.ModeloCesfam.DETALLE_SALIDA_STOCK.First(tp => tp.ID_DETALLE_SALIDA == idDetSalida);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public DataTable ListarDetalleReserva(int idReserva) 
        {
            try
            {
                CesfamDatos.REL_RESERVA reserva = Acceso.ModeloCesfam.REL_RESERVA.First(res => res.RESERVA_ID_RESERVA == idReserva);
                int idSalida = (int)reserva.SALIDA_STOCK_ID_SALIDA_STOCK;
                CesfamDatos.DETALLE_SALIDA_STOCK detalle = Acceso.ModeloCesfam.DETALLE_SALIDA_STOCK.First(det => det.ID_SALIDA_STOCK == idSalida);
                string lote = detalle.DETALLE_INGRESO_STOCK.LOTE;
                int cantidad = (int)detalle.CANTIDAD;
                int idDetalleSalida = (int)detalle.ID_DETALLE_SALIDA;
                int idDetalleIngreso = (int)detalle.ID_DETALLE_INGRESO;
                DataTable dts = new DataTable("Lista Stock por Categoria");
                dts.Columns.Add("IdSalida");
                dts.Columns.Add("Cantidad");
                dts.Columns.Add("Lote");
                dts.Columns.Add("idDetalleSalida");
                dts.Columns.Add("IdDetalleIngreso");
                dts.Rows.Add(idSalida, cantidad, lote, idDetalleSalida, idDetalleIngreso);
                return dts;
            }
            catch (Exception)
            {

                return null;
            }
            
        }
    }
}
