using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CesfamDatos;

namespace CesfamNegocio
{
    public class Acceso
    {
        private static CesfamEntities _modeloCesfam;

        public static CesfamEntities ModeloCesfam
        {
            get
            {
                if (_modeloCesfam == null)
                {
                    _modeloCesfam = new CesfamEntities();
                }
                return _modeloCesfam;
            }
        }
        public Acceso() { }
    }
}
