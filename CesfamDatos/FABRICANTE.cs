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
    
    public partial class FABRICANTE
    {
        public FABRICANTE()
        {
            this.PRODUCTO = new HashSet<PRODUCTO>();
        }
    
        public decimal ID_FABRICANTE { get; set; }
        public string NOMBRE_FABRICANTE { get; set; }
        public string DESCRIPCION { get; set; }
        public string ESTADO_FABRICANTE { get; set; }
    
        public virtual ICollection<PRODUCTO> PRODUCTO { get; set; }
    }
}
