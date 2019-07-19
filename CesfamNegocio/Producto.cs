using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CesfamDatos;

namespace CesfamNegocio
{
    public class Producto
    {
        public int IdProducto { get; set; }
        public string CodigoProducto { get; set; }
        public string Descripcion { get; set; }
        public string Componente { get; set; }
        public int CantidadMinimaStock { get; set; }
        public int Gramaje { get; set; }
        public string NombreProducto { get; set; }
        public string EstadoProducto { get; set; }
        public int IdFabricante { get; set; }
        public int IdCategoria { get; set; }
        public string UnidadMedida { get; set; }
        public bool crearProducto(int idProducto, string codigoProducto, string descripcion, string componente, int cantidadMinima, int gramaje, string nombre, string estado, int idFabricante, int idCategoria, string unidadMedida)
        {
            try
            {
                //Acceso.ModeloCesfam.SP_CREARPRODUCTO(this.IdProducto, this.CodigoProducto, this.Descripcion, this.Fabricante, this.Componente, this.CantidadMinimaStock, this.Gramaje, this.IdTipoProducto, this.NombreProducto);
                PRODUCTO Pro = new PRODUCTO();
                Pro.ID_PRODUCTO = idProducto;
                Pro.CODIGO_PRODUCTO = codigoProducto;
                Pro.DESCRIPCION = descripcion;
                Pro.COMPONENTE = componente;
                Pro.CANTIDAD_MINIMA_STOCK = cantidadMinima;
                Pro.GRAMAJE = gramaje;
                Pro.NOMBRE_PRODUCTO = nombre;
                Pro.ESTADO_PRODUCTO = estado;
                Pro.FABRICANTE_ID_FABRICANTE = idFabricante;
                Pro.CATEGORIA_ID_CATEGORIA = idCategoria;
                Pro.UNIDAD_MEDIDAD = unidadMedida;
                Acceso.ModeloCesfam.PRODUCTO.Add(Pro);
                Acceso.ModeloCesfam.SaveChanges();
                return true;

            }
            catch (Exception)
            {

                return false;
            }
        }
        public bool Read(int idProducto)
        {
            try
            {
                CesfamDatos.PRODUCTO Pro = Acceso.ModeloCesfam.PRODUCTO.First(pr => pr.ID_PRODUCTO == idProducto);
                this.IdProducto = (int)Pro.ID_PRODUCTO;
                this.CodigoProducto = Pro.CODIGO_PRODUCTO;
                this.Descripcion = Pro.DESCRIPCION;
                this.Componente = Pro.COMPONENTE;
                this.CantidadMinimaStock = (int)Pro.CANTIDAD_MINIMA_STOCK;
                this.Gramaje = (int)Pro.GRAMAJE;
                this.NombreProducto = Pro.NOMBRE_PRODUCTO;
                this.EstadoProducto = Pro.ESTADO_PRODUCTO;
                this.IdFabricante = (int)(Pro.FABRICANTE_ID_FABRICANTE);
                this.IdCategoria = (int)Pro.CATEGORIA_ID_CATEGORIA;
                this.UnidadMedida = Pro.UNIDAD_MEDIDAD;
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public bool UpdateProducto(int idProducto, string codigoProducto, string descripcion, string componente, int cantidadMinima, int gramaje, string nombre, string estado, int idFabricante, int idCategoria, string unidadMedida)
        {
            try
            {
                CesfamDatos.PRODUCTO Pro = Acceso.ModeloCesfam.PRODUCTO.First(pr => pr.ID_PRODUCTO == idProducto);
                Pro.CODIGO_PRODUCTO = codigoProducto;
                Pro.DESCRIPCION = descripcion;
                Pro.COMPONENTE = componente;
                Pro.CANTIDAD_MINIMA_STOCK = cantidadMinima;
                Pro.GRAMAJE = gramaje;
                Pro.NOMBRE_PRODUCTO = nombre;
                Pro.ESTADO_PRODUCTO = estado;
                Pro.FABRICANTE_ID_FABRICANTE = idFabricante;
                Pro.CATEGORIA_ID_CATEGORIA = idCategoria;
                Pro.UNIDAD_MEDIDAD = unidadMedida;
                Acceso.ModeloCesfam.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public bool ActivarDesactivar(int idProducto, string Estado)
        {
            try
            {
                CesfamDatos.PRODUCTO Pro = Acceso.ModeloCesfam.PRODUCTO.First(pr => pr.ID_PRODUCTO == idProducto);
                Pro.ESTADO_PRODUCTO = Estado;
                Acceso.ModeloCesfam.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public DataTable ListarProductosData()
        {
            var dt = (from Prod in Acceso.ModeloCesfam.PRODUCTO
                      select Prod).ToList();
            DataTable dts = new DataTable("Lista");
            dts.Columns.Add("IdProducto");
            dts.Columns.Add("Codigo Producto");
            dts.Columns.Add("NombreProducto");
            dts.Columns.Add("Componente");
            dts.Columns.Add("Gramaje");
            dts.Columns.Add("Unidad Medida");
            dts.Columns.Add("EstadoProducto");
            dts.Columns.Add("Descripcion");
            dts.Columns.Add("CantidadMinimaStock");
            dts.Columns.Add("IdFabricante");
            dts.Columns.Add("Nombre Fabricante");
            dts.Columns.Add("IdCategoria ");
            dts.Columns.Add("Nombre Categoria");
            foreach (var data in dt)
            {
                dts.Rows.Add(data.ID_PRODUCTO, data.CODIGO_PRODUCTO, data.NOMBRE_PRODUCTO, data.COMPONENTE, data.GRAMAJE, data.UNIDAD_MEDIDAD, data.ESTADO_PRODUCTO, data.DESCRIPCION, data.CANTIDAD_MINIMA_STOCK, data.FABRICANTE.ID_FABRICANTE, data.FABRICANTE.NOMBRE_FABRICANTE, data.CATEGORIA.ID_CATEGORIA, data.CATEGORIA.NOMBRE_CATEGORIA);
            }
            return dts;
        }
        public DataTable ListarProducActivos()
        {
            var dt = (from Prod in Acceso.ModeloCesfam.PRODUCTO
                      where Prod.ESTADO_PRODUCTO.Equals("ACTIVO")
                      select Prod).ToList();
            DataTable dts = new DataTable("Lista");
            dts.Columns.Add("IdProducto");
            dts.Columns.Add("Codigo Producto");
            dts.Columns.Add("NombreProducto");
            dts.Columns.Add("Componente");
            dts.Columns.Add("Gramaje");
            dts.Columns.Add("Unidad Medida");
            dts.Columns.Add("EstadoProducto");
            dts.Columns.Add("Descripcion");
            dts.Columns.Add("CantidadMinimaStock");
            dts.Columns.Add("IdFabricante");
            dts.Columns.Add("Nombre Fabricante");
            dts.Columns.Add("IdCategoria ");
            dts.Columns.Add("Nombre Categoria");
            foreach (var data in dt)
            {
                dts.Rows.Add(data.ID_PRODUCTO, data.CODIGO_PRODUCTO, data.NOMBRE_PRODUCTO, data.COMPONENTE, data.GRAMAJE, data.UNIDAD_MEDIDAD, data.ESTADO_PRODUCTO, data.DESCRIPCION, data.CANTIDAD_MINIMA_STOCK, data.FABRICANTE.ID_FABRICANTE, data.FABRICANTE.NOMBRE_FABRICANTE, data.CATEGORIA.ID_CATEGORIA, data.CATEGORIA.NOMBRE_CATEGORIA);
            }
            return dts;
        }
        public DataTable ListarProdXTipo(int idCategoria)
        {
            var dt = (from Prod in Acceso.ModeloCesfam.PRODUCTO
                      where Prod.CATEGORIA_ID_CATEGORIA == idCategoria
                      select Prod).ToList();
            DataTable dts = new DataTable("Lista");
            dts.Columns.Add("IdProducto");
            dts.Columns.Add("Codigo Producto");
            dts.Columns.Add("NombreProducto");
            dts.Columns.Add("Componente");
            dts.Columns.Add("Gramaje");
            dts.Columns.Add("Unidad Medida");
            dts.Columns.Add("EstadoProducto");
            dts.Columns.Add("Descripcion");
            dts.Columns.Add("CantidadMinimaStock");
            dts.Columns.Add("IdFabricante");
            dts.Columns.Add("Nombre Fabricante");
            dts.Columns.Add("IdCategoria ");
            dts.Columns.Add("Nombre Categoria");
            foreach (var data in dt)
            {
                dts.Rows.Add(data.ID_PRODUCTO, data.CODIGO_PRODUCTO, data.NOMBRE_PRODUCTO, data.COMPONENTE, data.GRAMAJE, data.UNIDAD_MEDIDAD, data.ESTADO_PRODUCTO, data.DESCRIPCION, data.CANTIDAD_MINIMA_STOCK, data.FABRICANTE.ID_FABRICANTE, data.FABRICANTE.NOMBRE_FABRICANTE, data.CATEGORIA.ID_CATEGORIA, data.CATEGORIA.NOMBRE_CATEGORIA);
            }
            return dts;
        }
        public DataTable ListarProdXTipoActivo(int idCategoria)
        {
            var dt = (from Prod in Acceso.ModeloCesfam.PRODUCTO
                      where Prod.CATEGORIA_ID_CATEGORIA == idCategoria && Prod.ESTADO_PRODUCTO.Equals("ACTIVO")
                      select Prod).ToList();
            DataTable dts = new DataTable("Lista");
            dts.Columns.Add("IdProducto");
            dts.Columns.Add("Codigo Producto");
            dts.Columns.Add("NombreProducto");
            dts.Columns.Add("Componente");
            dts.Columns.Add("Gramaje");
            dts.Columns.Add("Unidad Medida");
            dts.Columns.Add("EstadoProducto");
            dts.Columns.Add("Descripcion");
            dts.Columns.Add("CantidadMinimaStock");
            dts.Columns.Add("IdFabricante");
            dts.Columns.Add("Nombre Fabricante");
            dts.Columns.Add("IdCategoria ");
            dts.Columns.Add("Nombre Categoria");
            foreach (var data in dt)
            {
                dts.Rows.Add(data.ID_PRODUCTO, data.CODIGO_PRODUCTO, data.NOMBRE_PRODUCTO, data.COMPONENTE, data.GRAMAJE, data.UNIDAD_MEDIDAD, data.ESTADO_PRODUCTO, data.DESCRIPCION, data.CANTIDAD_MINIMA_STOCK, data.FABRICANTE.ID_FABRICANTE, data.FABRICANTE.NOMBRE_FABRICANTE, data.CATEGORIA.ID_CATEGORIA, data.CATEGORIA.NOMBRE_CATEGORIA);
            }
            return dts;
        }

        public int obtenerStockActual(int idProducto)
        {
            decimal query = (from x in Acceso.ModeloCesfam.DETALLE_INGRESO_STOCK
                             where x.PRODUCTO_ID_PRODUCTO == idProducto
                             select x.STOCK_ACTUAL).Sum();

            int cantidad = Convert.ToInt32(query);

            return cantidad;
        }


        public class ProductoPorCategoriaActiva
        {

            public int idProducto { get; set; }
            public string nombreProducto { get; set; }
            public int gramaje { get; set; }
            public string unidadMedida { get; set; }//nombre/ gramaje unidadMedida



            public List<ProductoPorCategoriaActiva> productoPorCategoriaActiva(int idCategoriaJava)
            {


                return (from pro in Acceso.ModeloCesfam.PRODUCTO
                        join cat in Acceso.ModeloCesfam.CATEGORIA on pro.CATEGORIA_ID_CATEGORIA equals cat.ID_CATEGORIA

                        //let informacion = pro.NOMBRE_PRODUCTO + " " + cat.NOMBRE_CATEGORIA = alias de una tabla
                        where pro.CATEGORIA_ID_CATEGORIA.Equals(idCategoriaJava)
                        where pro.ESTADO_PRODUCTO.Equals("ACTIVO")
                        where cat.ESTADO_CATEGORIA.Equals("ACTIVO")

                        select new ProductoPorCategoriaActiva
                        {
                            idProducto = (int)pro.ID_PRODUCTO,
                            nombreProducto = pro.NOMBRE_PRODUCTO,
                            gramaje = (int)pro.GRAMAJE,
                            unidadMedida = pro.UNIDAD_MEDIDAD


                            //nombre = informacion

                        }).ToList();

            }
        }
    }
}
