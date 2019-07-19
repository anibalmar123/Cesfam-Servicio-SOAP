using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CesfamDatos;
using System.Data;

namespace CesfamNegocio
{
    public class DetalleIngresoStock
    {
        public int IdDetalleIngreso { get; set; }
        public int StockInicial { get; set; }
        public int StockActual { get; set; }
        public DateTime FechaProduccion { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public string Lote { get; set; }
        public int IdIngresoStock { get; set; }
        public int IdProducto { get; set; }
        public DataTable ListaStock { get; set; }

        public bool create(int idDetalleIngreso, int stockInicial, int stockActual, DateTime fechaProduccion, DateTime fechaVencimiento, string lote, int idIngreso, int idProducto)
        {
            try
            {
                DETALLE_INGRESO_STOCK detalleIngreso = new DETALLE_INGRESO_STOCK();
                detalleIngreso.ID_DETALLE_INGRESO = idDetalleIngreso;
                detalleIngreso.STOCK_INICIAL = stockInicial;
                detalleIngreso.STOCK_ACTUAL = stockActual;
                detalleIngreso.FECHA_PRODUCCION = fechaProduccion;
                detalleIngreso.FECHA_VENCIMIENTO = fechaVencimiento;
                detalleIngreso.LOTE = lote;
                detalleIngreso.INGRESO_STOCK_ID_INGRESO = idIngreso;
                detalleIngreso.PRODUCTO_ID_PRODUCTO = idProducto;
                Acceso.ModeloCesfam.DETALLE_INGRESO_STOCK.Add(detalleIngreso);
                Acceso.ModeloCesfam.SaveChanges();
                return true;

            }
            catch (Exception)
            {

                return false;
            }
        }
        public bool Read(int idDetalleIngreso)
        {
            try
            {
                CesfamDatos.DETALLE_INGRESO_STOCK detalleIngreso = Acceso.ModeloCesfam.DETALLE_INGRESO_STOCK.First(tp => tp.ID_DETALLE_INGRESO == idDetalleIngreso);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public bool ActualizarStockActual(int idDetalle, int CantARebajar)
        {
            try
            {
                CesfamDatos.DETALLE_INGRESO_STOCK detalleIngreso = Acceso.ModeloCesfam.DETALLE_INGRESO_STOCK.First(tp => tp.ID_DETALLE_INGRESO == idDetalle);
                decimal newStokActual = detalleIngreso.STOCK_ACTUAL - CantARebajar;
                detalleIngreso.STOCK_ACTUAL = newStokActual;
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public bool DevolverStock(int idDetalleIngreso, int cantidad)
        {
            try
            {
                CesfamDatos.DETALLE_INGRESO_STOCK detalleIngreso = Acceso.ModeloCesfam.DETALLE_INGRESO_STOCK.First(tp => tp.ID_DETALLE_INGRESO == idDetalleIngreso);
                detalleIngreso.STOCK_ACTUAL = detalleIngreso.STOCK_ACTUAL + cantidad;
                Acceso.ModeloCesfam.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public DataTable ListarStockTotal()
        {
            var list = (from det in Acceso.ModeloCesfam.DETALLE_INGRESO_STOCK.ToList()
                        group det by new { det.PRODUCTO.NOMBRE_PRODUCTO, det.PRODUCTO_ID_PRODUCTO } into g
                        where g.Sum(x => x.STOCK_ACTUAL)>0
                        select new
                        {
                            g.Key.PRODUCTO_ID_PRODUCTO,
                            g.Key.NOMBRE_PRODUCTO,
                            Cantidad = g.Sum(x => x.STOCK_ACTUAL)
                        });
            DataTable dts = new DataTable("Lista");
            dts.Columns.Add("Id");
            dts.Columns.Add("Nombre Producto");
            dts.Columns.Add("Cantidad Total");
            foreach (var data in list)
            {
                dts.Rows.Add(data.PRODUCTO_ID_PRODUCTO ,data.NOMBRE_PRODUCTO, data.Cantidad);
            }
            return dts;
        }
        public DataTable ListarStockTotalPorNombre(string Nombre)
        {
            var list = (from det in Acceso.ModeloCesfam.DETALLE_INGRESO_STOCK.ToList()
                        group det by new { det.PRODUCTO.NOMBRE_PRODUCTO, det.PRODUCTO_ID_PRODUCTO } into g
                        where g.Sum(x => x.STOCK_ACTUAL) > 0 && g.Key.NOMBRE_PRODUCTO == Nombre
                        select new
                        {
                            g.Key.PRODUCTO_ID_PRODUCTO,
                            g.Key.NOMBRE_PRODUCTO,
                            Cantidad = g.Sum(x => x.STOCK_ACTUAL)
                        });
            DataTable dts = new DataTable("Lista");
            dts.Columns.Add("Id");
            dts.Columns.Add("Nombre Producto");
            dts.Columns.Add("Cantidad Total");
            foreach (var data in list)
            {
                dts.Rows.Add(data.PRODUCTO_ID_PRODUCTO, data.NOMBRE_PRODUCTO, data.Cantidad);
            }
            return dts;
        }
        public DataTable ListarStock()
        {
            var list = (from det in Acceso.ModeloCesfam.DETALLE_INGRESO_STOCK.ToList()
                        where det.STOCK_ACTUAL>0
                        orderby det.FECHA_VENCIMIENTO ascending
                        select new {IdDetalle = det.ID_DETALLE_INGRESO , IdProducto = det.PRODUCTO_ID_PRODUCTO, 
                            Producto = det.PRODUCTO.NOMBRE_PRODUCTO, Cantidad = det.STOCK_ACTUAL, 
                            FechaVencimiento = det.FECHA_VENCIMIENTO, Lote = det.LOTE});
            DataTable dts = new DataTable("Lista Stock");
            dts.Columns.Add("IdDetalle");
            dts.Columns.Add("IdProducto");
            dts.Columns.Add("Nombre Producto");
            dts.Columns.Add("Lote");
            dts.Columns.Add("Cantidad Total");
            dts.Columns.Add("Fecha Vencimiento");
            foreach (var data in list)
            {
                dts.Rows.Add(data.IdDetalle, data.IdProducto , data.Producto, data.Lote, data.Cantidad, data.FechaVencimiento);
            }
            return dts;
        }
        public DataTable ListarStockXCategoria(int idCategoria)
        {
            var list = (from det in Acceso.ModeloCesfam.DETALLE_INGRESO_STOCK.ToList() 
                        where det.PRODUCTO.CATEGORIA_ID_CATEGORIA == idCategoria && det.STOCK_ACTUAL>0
                        orderby det.FECHA_VENCIMIENTO ascending
                        select new
                        {
                            IdDetalle = det.ID_DETALLE_INGRESO,
                            IdProducto = det.PRODUCTO_ID_PRODUCTO,
                            Producto = det.PRODUCTO.NOMBRE_PRODUCTO,
                            Cantidad = det.STOCK_ACTUAL,
                            FechaVencimiento = det.FECHA_VENCIMIENTO,
                            Lote = det.LOTE
                        });
            DataTable dts = new DataTable("Lista Stock por Categoria");
            dts.Columns.Add("IdDetalle");
            dts.Columns.Add("IdProducto");
            dts.Columns.Add("Nombre Producto");
            dts.Columns.Add("Lote");
            dts.Columns.Add("Cantidad Total");
            dts.Columns.Add("Fecha Vencimiento");
            foreach (var data in list)
            {
                dts.Rows.Add(data.IdDetalle, data.IdProducto, data.Producto, data.Lote, data.Cantidad, data.FechaVencimiento);
            }
            return dts;
        }
        public DataTable ListarStockXLote(string NomLote)
        {
            var list = (from det in Acceso.ModeloCesfam.DETALLE_INGRESO_STOCK.ToList()
                        where det.LOTE == NomLote && det.STOCK_ACTUAL > 0
                        orderby det.FECHA_VENCIMIENTO ascending
                        select new
                        {
                            IdDetalle = det.ID_DETALLE_INGRESO,
                            IdProducto = det.PRODUCTO_ID_PRODUCTO,
                            Producto = det.PRODUCTO.NOMBRE_PRODUCTO,
                            Cantidad = det.STOCK_ACTUAL,
                            FechaVencimiento = det.FECHA_VENCIMIENTO,
                            Lote = det.LOTE
                        });
            DataTable dts = new DataTable("Lista Stock por Categoria");
            dts.Columns.Add("IdDetalle");
            dts.Columns.Add("IdProducto");
            dts.Columns.Add("Nombre Producto");
            dts.Columns.Add("Lote");
            dts.Columns.Add("Cantidad Total");
            dts.Columns.Add("Fecha Vencimiento");
            foreach (var data in list)
            {
                dts.Rows.Add(data.IdDetalle, data.IdProducto, data.Producto, data.Lote, data.Cantidad, data.FechaVencimiento);
            }
            return dts;
        }
        public DataTable ListarStockProducto(int idProducto)
        {
            var list = (from det in Acceso.ModeloCesfam.DETALLE_INGRESO_STOCK.ToList()
                        where det.PRODUCTO_ID_PRODUCTO == idProducto && det.STOCK_ACTUAL > 0
                        orderby det.FECHA_VENCIMIENTO ascending
                        select new
                        {
                            IdDetalle = det.ID_DETALLE_INGRESO,
                            IdProducto = det.PRODUCTO_ID_PRODUCTO,
                            Producto = det.PRODUCTO.NOMBRE_PRODUCTO,
                            Cantidad = det.STOCK_ACTUAL,
                            FechaVencimiento = det.FECHA_VENCIMIENTO,
                            Lote = det.LOTE
                        });
            DataTable dts = new DataTable("Lista Stock por Categoria");
            dts.Columns.Add("IdDetalle");
            dts.Columns.Add("IdProducto");
            dts.Columns.Add("Nombre Producto");
            dts.Columns.Add("Lote");
            dts.Columns.Add("Cantidad Total");
            dts.Columns.Add("Fecha Vencimiento");
            foreach (var data in list)
            {
                dts.Rows.Add(data.IdDetalle, data.IdProducto, data.Producto, data.Lote, data.Cantidad, data.FechaVencimiento);
            }
            return dts;
        }
        public DataTable ListarStockPorVencer()
        {
            var list = (from det in Acceso.ModeloCesfam.DETALLE_INGRESO_STOCK.ToList()
                        where det.FECHA_VENCIMIENTO < DateTime.Now.AddDays(90) && det.STOCK_ACTUAL > 0
                        orderby det.FECHA_VENCIMIENTO ascending
                        select new
                        {
                            IdDetalle = det.ID_DETALLE_INGRESO,
                            IdProducto = det.PRODUCTO_ID_PRODUCTO,
                            Producto = det.PRODUCTO.NOMBRE_PRODUCTO,
                            Cantidad = det.STOCK_ACTUAL,
                            FechaVencimiento = det.FECHA_VENCIMIENTO,
                            Lote = det.LOTE
                        });
            DataTable dts = new DataTable("Lista Stock por Categoria");
            dts.Columns.Add("IdDetalle");
            dts.Columns.Add("IdProducto");
            dts.Columns.Add("Nombre Producto");
            dts.Columns.Add("Lote");
            dts.Columns.Add("Cantidad Total");
            dts.Columns.Add("Fecha Vencimiento");
            foreach (var data in list)
            {
                dts.Rows.Add(data.IdDetalle, data.IdProducto, data.Producto, data.Lote, data.Cantidad, data.FechaVencimiento);
            }
            return dts;
        }
    }
}
