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
    
    public partial class DETALLE_SALIDA_STOCK
    {
        public decimal ID_DETALLE_SALIDA { get; set; }
        public decimal CANTIDAD { get; set; }
        public string DESCRIPCION { get; set; }
        public decimal ID_SALIDA_STOCK { get; set; }
        public decimal ID_DETALLE_INGRESO { get; set; }
    
        public virtual DETALLE_INGRESO_STOCK DETALLE_INGRESO_STOCK { get; set; }
        public virtual SALIDA_STOCK SALIDA_STOCK { get; set; }
    }
}
