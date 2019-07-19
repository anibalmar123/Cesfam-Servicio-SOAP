using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using CesfamDatos;

namespace CesfamNegocio
{
    public class IngresoStock
    {
        public int IdIngreso { get; set; }
        public DateTime fecha { get; set; }
        public int IdUsuario { get; set; }

        public bool RegistrarIngreso(int idIngreso, DateTime fecha, int idUsuario, DataTable dtDetalle)
        {
            try
            {
                INGRESO_STOCK ingresoStock = new INGRESO_STOCK();
                ingresoStock.ID_INGRESO = idIngreso;
                ingresoStock.FECHA_INGRESO = fecha;
                ingresoStock.USUARIO_ID_USUARIO = idUsuario;
                Acceso.ModeloCesfam.INGRESO_STOCK.Add(ingresoStock);
                Acceso.ModeloCesfam.SaveChanges();
                foreach (DataRow row in dtDetalle.Rows)
                {
                    DetalleIngresoStock detalle = new DetalleIngresoStock();
                    Reserva bssReserva = new Reserva();
                    int idDetalleIngreso;
                    Random RndId = new Random();
                    idDetalleIngreso = RndId.Next(1, 9999999);
                    while (detalle.Read(idDetalleIngreso))
                    {
                        idDetalleIngreso = RndId.Next(1, 9999999);
                    }
                    int stockInicial = Convert.ToInt32(row[2].ToString());
                    int stockActual = Convert.ToInt32(row[2].ToString());
                    DateTime fechaProduccion = Convert.ToDateTime(row[3].ToString());
                    DateTime fechaVencimiento = Convert.ToDateTime(row[4].ToString());
                    string lote = row[5].ToString();
                    int idIngresoStock = idIngreso;
                    int idProducto = Convert.ToInt32(row[0].ToString());
                    detalle.create(idDetalleIngreso, stockInicial, stockActual, fechaProduccion, fechaVencimiento, lote, idIngresoStock, idProducto);
                    bssReserva.VerificarReservasPendientes(idProducto, idDetalleIngreso, idUsuario);
                }                
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public bool Read(int idIngreso)
        {
            try
            {
                CesfamDatos.INGRESO_STOCK ingresoStock = Acceso.ModeloCesfam.INGRESO_STOCK.First(tp => tp.ID_INGRESO == idIngreso);
                this.IdIngreso = (int)ingresoStock.ID_INGRESO;
                this.fecha = ingresoStock.FECHA_INGRESO;
                this.IdUsuario = (int)ingresoStock.USUARIO_ID_USUARIO;
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }



    }
}
