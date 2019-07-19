//------------------------------------------------------------------------------
// <auto-generated>
//    Este código se generó a partir de una plantilla.
//
//    Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//    Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CesfamDatos
{
    using System;
    using System.Collections.Generic;
    
    public partial class PRODUCTO
    {
        public PRODUCTO()
        {
            this.DETALLE_INGRESO_STOCK = new HashSet<DETALLE_INGRESO_STOCK>();
            this.DETALLE_RECETA = new HashSet<DETALLE_RECETA>();
            this.RESERVA = new HashSet<RESERVA>();
        }
    
        public decimal ID_PRODUCTO { get; set; }
        public string CODIGO_PRODUCTO { get; set; }
        public string DESCRIPCION { get; set; }
        public string COMPONENTE { get; set; }
        public decimal CANTIDAD_MINIMA_STOCK { get; set; }
        public decimal GRAMAJE { get; set; }
        public string NOMBRE_PRODUCTO { get; set; }
        public string ESTADO_PRODUCTO { get; set; }
        public decimal CATEGORIA_ID_CATEGORIA { get; set; }
        public decimal FABRICANTE_ID_FABRICANTE { get; set; }
        public string UNIDAD_MEDIDAD { get; set; }
    
        public virtual CATEGORIA CATEGORIA { get; set; }
        public virtual ICollection<DETALLE_INGRESO_STOCK> DETALLE_INGRESO_STOCK { get; set; }
        public virtual ICollection<DETALLE_RECETA> DETALLE_RECETA { get; set; }
        public virtual FABRICANTE FABRICANTE { get; set; }
        public virtual ICollection<RESERVA> RESERVA { get; set; }
    }
}