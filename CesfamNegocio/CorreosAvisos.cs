using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Net.Mail;

namespace CesfamNegocio
{
    public class CorreosAvisos
    {
        public bool CorreoElectronico(string correoDestinatario,string Asunto, string mensaje )
        {
            try
            {
                MailMessage correo = new MailMessage();
                SmtpClient protocolo = new SmtpClient();
                correo.To.Add(correoDestinatario);
                correo.CC.Add("cristianzamoraquinones1988@gmail.com");
                correo.From = new MailAddress("cristianzamoraquinones1988@gmail.com", "Cesfam Farmacia", System.Text.Encoding.UTF8);
                correo.Subject = Asunto;
                correo.SubjectEncoding = System.Text.Encoding.UTF8;
                correo.Body = mensaje;
                correo.BodyEncoding = System.Text.Encoding.UTF8;
                correo.IsBodyHtml = false;

                protocolo.Credentials = new System.Net.NetworkCredential("cristianzamoraquinones1988@gmail.com", "chocopop316");
                protocolo.Port = 587;
                protocolo.Host = "smtp.gmail.com";
                protocolo.EnableSsl = true;

                protocolo.Send(correo);
                return true;
            }
            catch (SmtpException)
            {

                return false;
            }
        }
    }
}
