using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CesfamDatos;

namespace CesfamNegocio
{
    public class Fabricante
    {
        public int IdFabricante { get; set; }
        public string NombreFabricante { get; set; }
        public string Descripcion { get; set; }
        public string EstadoFabricante { get; set; }
        public bool create(int idFabricante, string nombreFabricante, string descripcion, string estadoFabricante)
        {
            try
            {
                FABRICANTE fabricante = new FABRICANTE();
                fabricante.ID_FABRICANTE = idFabricante;
                fabricante.NOMBRE_FABRICANTE = nombreFabricante;
                fabricante.DESCRIPCION = descripcion;
                fabricante.ESTADO_FABRICANTE = estadoFabricante;
                Acceso.ModeloCesfam.FABRICANTE.Add(fabricante);
                Acceso.ModeloCesfam.SaveChanges();
                return true;

            }
            catch (Exception)
            {

                return false;
            }
        }
        public bool Read(int idFabricante)
        {
            try
            {
                CesfamDatos.FABRICANTE fabricante = Acceso.ModeloCesfam.FABRICANTE.First(pr => pr.ID_FABRICANTE == idFabricante);
                this.IdFabricante = Convert.ToInt32(fabricante.ID_FABRICANTE);
                this.NombreFabricante = fabricante.NOMBRE_FABRICANTE;
                this.Descripcion = fabricante.DESCRIPCION;
                this.EstadoFabricante = fabricante.ESTADO_FABRICANTE;
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public bool Update(int idFabricante, string nombreFabricante, string descripcion, string estadoFabricante)
        {
            try
            {
                CesfamDatos.FABRICANTE fabricante = Acceso.ModeloCesfam.FABRICANTE.First(pr => pr.ID_FABRICANTE == idFabricante);
                fabricante.NOMBRE_FABRICANTE = nombreFabricante;
                fabricante.DESCRIPCION = descripcion;
                fabricante.ESTADO_FABRICANTE = estadoFabricante;
                Acceso.ModeloCesfam.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public bool ActivarDesactivar(int idFabricante, string estado)
        {
            try
            {
                CesfamDatos.FABRICANTE fabricante = Acceso.ModeloCesfam.FABRICANTE.First(pr => pr.ID_FABRICANTE == idFabricante);
                fabricante.ESTADO_FABRICANTE = estado;
                Acceso.ModeloCesfam.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public DataTable ListarTodosFabricantes()
        {
            var dt = (from fabr in Acceso.ModeloCesfam.FABRICANTE.ToList() 
                      select fabr).ToList();
            DataTable dts = new DataTable("Lista");
            dts.Columns.Add("Id Fabricante");
            dts.Columns.Add("Nombre Fabricante");
            dts.Columns.Add("Descripcion");
            dts.Columns.Add("Estado");
            foreach (var data in dt)
            {
                dts.Rows.Add(data.ID_FABRICANTE, data.NOMBRE_FABRICANTE, data.DESCRIPCION,data.ESTADO_FABRICANTE);
            }
            return dts;
        }
        public DataTable ListarFabricantesActivos()
        {
            var dt = (from fabr in Acceso.ModeloCesfam.FABRICANTE.ToList()
                      where fabr.ESTADO_FABRICANTE.Equals("ACTIVO")
                      select fabr).ToList();
            DataTable dts = new DataTable("Lista");
            dts.Columns.Add("Id Fabricante");
            dts.Columns.Add("Nombre Fabricante");
            dts.Columns.Add("Descripcion");
            dts.Columns.Add("Estado");
            foreach (var data in dt)
            {
                dts.Rows.Add(data.ID_FABRICANTE, data.NOMBRE_FABRICANTE, data.DESCRIPCION, data.ESTADO_FABRICANTE);
            }
            return dts;
        }
        public DataTable ListarFabricantesXnombre(string nombreFab)
        {
            var dt = (from fabr in Acceso.ModeloCesfam.FABRICANTE.ToList()
                      where fabr.NOMBRE_FABRICANTE == nombreFab
                      select fabr).ToList();
            DataTable dts = new DataTable("Lista");
            dts.Columns.Add("Id Fabricante");
            dts.Columns.Add("Nombre Fabricante");
            dts.Columns.Add("Descripcion");
            dts.Columns.Add("Estado");
            foreach (var data in dt)
            {
                dts.Rows.Add(data.ID_FABRICANTE, data.NOMBRE_FABRICANTE, data.DESCRIPCION, data.ESTADO_FABRICANTE);
            }
            return dts;
        }

    }
}
