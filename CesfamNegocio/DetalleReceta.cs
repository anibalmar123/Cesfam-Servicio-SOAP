using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CesfamDatos;

namespace CesfamNegocio
{
    public class DetalleReceta
    {
        public int IdProducto { get; set; }
        public int IdReceta { get; set; }
        public int Cantidad { get; set; }
        public int IdDetalleReceta { get; set;}
        public string Estado { get; set; }

        public bool CrearDetalleReceta(int idProducto, int idReceta,int cantidadTotal, int idDetalleReceta, string estado)
        {
            try
            {
                DETALLE_RECETA detalleReceta = new DETALLE_RECETA();
                detalleReceta.PRODUCTO_ID_PRODUCTO = idProducto;
                detalleReceta.RECETA_ID_RECETA = idReceta;
                detalleReceta.CANTIDAD = Cantidad;
                detalleReceta.ID_DETALLE_RECETA = idDetalleReceta;
                detalleReceta.ESTADO = estado;
                Acceso.ModeloCesfam.DETALLE_RECETA.Add(detalleReceta);
                Acceso.ModeloCesfam.SaveChanges();
                return true;

            }
            catch (Exception)
            {

                return false;
            }
        }
        public bool Read(int idDetalleReceta)
        {
            try
            {
                CesfamDatos.DETALLE_RECETA detalleReceta = Acceso.ModeloCesfam.DETALLE_RECETA.First(tp => tp.ID_DETALLE_RECETA == idDetalleReceta);
                this.IdProducto = (int)detalleReceta.PRODUCTO_ID_PRODUCTO;
                this.IdReceta = (int)detalleReceta.RECETA_ID_RECETA;
                this.Cantidad = (int)detalleReceta.CANTIDAD;
                this.IdDetalleReceta = (int)detalleReceta.ID_DETALLE_RECETA;
                this.Estado = detalleReceta.ESTADO;
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public bool ActualizarEstado(int idDetalleReceta, string estado)
        {
            try
            {
                CesfamDatos.DETALLE_RECETA detalleReceta = Acceso.ModeloCesfam.DETALLE_RECETA.First(tp => tp.ID_DETALLE_RECETA == idDetalleReceta);
                detalleReceta.ESTADO = estado;
                Acceso.ModeloCesfam.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public bool Delete(int idDetalleReceta)
        {
            try
            {
                CesfamDatos.DETALLE_RECETA detalleReceta = Acceso.ModeloCesfam.DETALLE_RECETA.First(tp => tp.ID_DETALLE_RECETA == idDetalleReceta);
                Acceso.ModeloCesfam.DETALLE_RECETA.Remove(detalleReceta);
                Acceso.ModeloCesfam.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public List<DetalleReceta> GenerarListado(List<DETALLE_RECETA> list)
        {

            List<DetalleReceta> listado = new List<DetalleReceta>();
            foreach (var item in list)
            {
                DetalleReceta detalleReceta = new DetalleReceta();
                detalleReceta.IdProducto = (int)item.PRODUCTO_ID_PRODUCTO;
                detalleReceta.IdReceta = (int)item.RECETA_ID_RECETA;
                detalleReceta.Cantidad = (int)item.CANTIDAD;
                detalleReceta.IdDetalleReceta = (int)item.ID_DETALLE_RECETA;
                detalleReceta.Estado = item.ESTADO;
                listado.Add(detalleReceta);
            }
            return listado;
        }
        public List<DetalleReceta> ListarDetalleReceta()
        {

            var list = Acceso.ModeloCesfam.DETALLE_RECETA;
            return GenerarListado(list.ToList());
        }
        public DataTable ListarDetalleReceta(int idReceta)
        {
            var dt = (from rec in Acceso.ModeloCesfam.DETALLE_RECETA.ToList()
                      where rec.RECETA_ID_RECETA == idReceta
                      select rec).ToList();
            DataTable dts = new DataTable("Lista");
            dts.Columns.Add("IdDetalleReceta");
            dts.Columns.Add("IdProducto");
            dts.Columns.Add("NombreProducto");
            dts.Columns.Add("Cantidad");
            dts.Columns.Add("EstadoDetalle");
            foreach (var data in dt)
            {
                dts.Rows.Add(data.ID_DETALLE_RECETA, data.PRODUCTO_ID_PRODUCTO, data.PRODUCTO.NOMBRE_PRODUCTO, data.CANTIDAD, data.ESTADO);
            }
            return dts;
        }

        public int ReservasPorReceta(int IdReceta) 
        {            
            var Reservadas = Acceso.ModeloCesfam.DETALLE_RECETA.Where(res => res.RECETA_ID_RECETA == IdReceta && res.ESTADO.Equals("Reservado")).ToList();
            return Reservadas.Count(); 
        }
        public int idDetalleEntregado(int idReceta, int idProducto) 
        {
            CesfamDatos.DETALLE_RECETA det = Acceso.ModeloCesfam.DETALLE_RECETA.First(detalle => detalle.RECETA_ID_RECETA == idReceta && detalle.PRODUCTO_ID_PRODUCTO == idProducto);
            return (int)det.ID_DETALLE_RECETA;
        }
    }
}
