using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace PruebasUnitarias
{
    class Program
    {
        static void Main(string[] args)
        {
            CesfamNegocio.Paciente bssPaciente = new CesfamNegocio.Paciente();
            string CodigoLibreta = string.Format("AA123");
            string Nombre = string.Format("Cristian");
            string ApellidoPaterno = string.Format("Zamora");
            string ApellidoMaterno = string.Format("Quiñones");
            string Correo = string.Format("Cristian@Sabroson.cl");
            string Direccion = string.Format("Avenida Siempre Viva");
            DateTime FechaNacimiento = Convert.ToDateTime(string.Format("09/11/1988"));
            int IdPaciente = 1;
            string Rut = string.Format("16912591-0");


            if (bssPaciente.create(IdPaciente, FechaNacimiento, Nombre, ApellidoPaterno, ApellidoMaterno, Correo, CodigoLibreta, Rut, Direccion))
            {
                Console.WriteLine("Paciente Ingresado");
            }
            else 
            {
                Console.WriteLine("Error");
            }

            bssPaciente.Read(IdPaciente);

            Console.WriteLine("Paciente: " + bssPaciente.Nombre + " " + bssPaciente.ApellidoPaterno + " " +
                              bssPaciente.ApellidoMaterno + " Correo:  "+bssPaciente.Correo+" Codigo Paciente: "+
                              bssPaciente.CodigoLibreta);
            Console.ReadKey();

        }
    }
}
