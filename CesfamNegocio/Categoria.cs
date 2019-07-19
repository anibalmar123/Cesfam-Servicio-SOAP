using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using CesfamDatos;

namespace CesfamNegocio
{
    public class Categoria
    {
        public int IdCategoria { get; set; }
        public string NombreCategoria { get; set; }
        public string EstadoCategoria { get; set; }
        public bool create(int idCategoria, string nombreTipo, string estado)
        {
            try
            {
                CATEGORIA categoria = new CATEGORIA();
                categoria.ID_CATEGORIA = idCategoria;
                categoria.NOMBRE_CATEGORIA = nombreTipo;
                categoria.ESTADO_CATEGORIA = estado;
                Acceso.ModeloCesfam.CATEGORIA.Add(categoria);
                Acceso.ModeloCesfam.SaveChanges();
                return true;

            }
            catch (Exception)
            {

                return false;
            }
        }
        public bool Read(int idCategoria)
        {
            try
            {
                CesfamDatos.CATEGORIA categoria = Acceso.ModeloCesfam.CATEGORIA.First(tp => tp.ID_CATEGORIA == idCategoria);
                this.IdCategoria = (int)categoria.ID_CATEGORIA;
                this.NombreCategoria = categoria.NOMBRE_CATEGORIA;
                this.EstadoCategoria = categoria.ESTADO_CATEGORIA;
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public bool Update(int idCategoria, string nombreTipo, string estado)
        {
            try
            {
                CesfamDatos.CATEGORIA categoria = Acceso.ModeloCesfam.CATEGORIA.First(tp => tp.ID_CATEGORIA == idCategoria);
                categoria.NOMBRE_CATEGORIA = nombreTipo;
                categoria.ESTADO_CATEGORIA = estado;
                Acceso.ModeloCesfam.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public bool ActivarDesactivarCategoria(int idCategoria, string estado)
        {
            try
            {
                CesfamDatos.CATEGORIA categoria = Acceso.ModeloCesfam.CATEGORIA.First(tp => tp.ID_CATEGORIA == idCategoria);
                categoria.ESTADO_CATEGORIA = estado;
                Acceso.ModeloCesfam.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public DataTable ListarTodosCategoria()
        {
            var dt = (from cat in Acceso.ModeloCesfam.CATEGORIA.ToList()
                      select cat).ToList();
            DataTable dts = new DataTable("Lista");
            dts.Columns.Add("Id Categoria");
            dts.Columns.Add("Nombre Categoria");
            dts.Columns.Add("Estado");
            foreach (var data in dt)
            {
                dts.Rows.Add(data.ID_CATEGORIA, data.NOMBRE_CATEGORIA, data.ESTADO_CATEGORIA);
            }
            return dts;
        }
        public DataTable ListarCategoriasActivas()
        {
            var dt = (from cat in Acceso.ModeloCesfam.CATEGORIA.ToList()
                      where cat.ESTADO_CATEGORIA.Equals("ACTIVO")
                      select cat).ToList();
            DataTable dts = new DataTable("Lista");
            dts.Columns.Add("Id Categoria");
            dts.Columns.Add("Nombre Categoria");
            dts.Columns.Add("Estado");
            foreach (var data in dt)
            {
                dts.Rows.Add(data.ID_CATEGORIA, data.NOMBRE_CATEGORIA, data.ESTADO_CATEGORIA);
            }
            return dts;
        }
        public DataTable ListarCategoriaXnombre(string nombreCat)
        {
            var dt = (from cat in Acceso.ModeloCesfam.CATEGORIA.ToList()
                      where cat.NOMBRE_CATEGORIA == nombreCat
                      select cat).ToList();
            DataTable dts = new DataTable("Lista");
            dts.Columns.Add("Id Categoria");
            dts.Columns.Add("Nombre Categoria");
            dts.Columns.Add("Estado");
            foreach (var data in dt)
            {
                dts.Rows.Add(data.ID_CATEGORIA, data.NOMBRE_CATEGORIA, data.ESTADO_CATEGORIA);
            }
            return dts;
        }
        public List<Categoria> listarCategoriaAct()
        {
            return (from cat in Acceso.ModeloCesfam.CATEGORIA
                    where cat.ESTADO_CATEGORIA.Equals("ACTIVO")

                    select new Categoria
                    {
                        IdCategoria = (int)cat.ID_CATEGORIA,
                        NombreCategoria = cat.NOMBRE_CATEGORIA,
                        EstadoCategoria = cat.ESTADO_CATEGORIA

                    }).ToList();
        }

    }
}
