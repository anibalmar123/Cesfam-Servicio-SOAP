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
    
    public partial class INGRESO_STOCK
    {
        public INGRESO_STOCK()
        {
            this.DETALLE_INGRESO_STOCK = new HashSet<DETALLE_INGRESO_STOCK>();
        }
    
        public decimal ID_INGRESO { get; set; }
        public System.DateTime FECHA_INGRESO { get; set; }
        public decimal USUARIO_ID_USUARIO { get; set; }
    
        public virtual ICollection<DETALLE_INGRESO_STOCK> DETALLE_INGRESO_STOCK { get; set; }
        public virtual USUARIO USUARIO { get; set; }
    }
}