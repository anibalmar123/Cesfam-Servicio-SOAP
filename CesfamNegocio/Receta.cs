using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using CesfamDatos;

namespace CesfamNegocio
{
    public class Receta
    {
        #region Atributos
        public int IdReceta { get; set; }
        public string EstadoReceta { get; set; }
        public DateTime FechaReceta { get; set; }
        public int IdTipoReceta { get; set; }
        public int IdPaciente { get; set; }
        public string Diagnostico { get; set; }
        public int CantidadTiempo { get; set; }//para la proxima entrega en el caso de prescripcion 
        public DateTime FechaParaEntrega { get; set; } // solo una en caso de receta, se modifica sumandole la cantidad de dias en caso de Prescripcion
        public int IdUsuario { get; set; }
        public bool CorreoEnviado { get; set; }
        #endregion
        public bool IngresarReceta(string fechaReceta, int idTipoReceta, int idPaciente, string diagnostico, int cantidadTiempo, int idUsuario, string[] DetalleReceta)
        {
            try
            {
                RECETA receta = new RECETA();
                if (idTipoReceta == 1)//Prescripcion
                {

                    Random RndId = new Random();
                    int idReceta = RndId.Next(1, 9999999);
                    while (this.Read(idReceta))
                    {
                        idReceta = RndId.Next(1, 9999999);
                    }
                    receta.ID_RECETA = idReceta;
                    receta.CANTIDAD_TIEMPO = cantidadTiempo;
                    receta.DIAGNOSTICO = diagnostico;
                    receta.ESTADO_RECETA = string.Format("Pendiente");
                    receta.FECHA_ENTREGA = DateTime.Parse(fechaReceta);
                    receta.FECHA_RECETA = DateTime.Parse(fechaReceta);
                    receta.PACIENTE_ID_PACIENTE = idPaciente;
                    receta.TIPO_RECETA_ID_TIPO_RECETA = idTipoReceta;
                    receta.USUARIO_ID_USUARIO = idUsuario;
                    receta.CORREOENVIADO = 1;
                    this.CargarDetalle(DetalleReceta, idReceta);
                }
                else if (idTipoReceta == 2) //Receta
                {
                    Random RndId = new Random();
                    int idReceta = RndId.Next(1, 9999999);
                    while (this.Read(idReceta))
                    {
                        idReceta = RndId.Next(1, 9999999);
                    }
                    receta.ID_RECETA = idReceta;
                    receta.CANTIDAD_TIEMPO = 0;
                    receta.DIAGNOSTICO = diagnostico;
                    receta.ESTADO_RECETA = string.Format("Pendiente");
                    receta.FECHA_ENTREGA = DateTime.Parse(fechaReceta);
                    receta.FECHA_RECETA = DateTime.Parse(fechaReceta);
                    receta.PACIENTE_ID_PACIENTE = idPaciente;
                    receta.TIPO_RECETA_ID_TIPO_RECETA = idTipoReceta;
                    receta.USUARIO_ID_USUARIO = idUsuario;
                    receta.CORREOENVIADO = 0;
                    this.CargarDetalle(DetalleReceta, idReceta);

                }
                Acceso.ModeloCesfam.RECETA.Add(receta);
                Acceso.ModeloCesfam.SaveChanges();
                return true;

            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool IngresarReceta(int idReceta, string estadoReceta, string fechaReceta, int idTipoReceta, int idPaciente, string diagnostico, int cantidadTiempo, int idUsuario, String[] DetalleReceta)
        {
            try
            {
                RECETA receta = new RECETA();
                receta.ID_RECETA = idReceta;
                receta.ESTADO_RECETA = estadoReceta;
                receta.FECHA_RECETA = DateTime.Parse(fechaReceta);
                receta.TIPO_RECETA_ID_TIPO_RECETA = idTipoReceta;
                receta.PACIENTE_ID_PACIENTE = idPaciente;
                receta.DIAGNOSTICO = diagnostico;
                receta.CANTIDAD_TIEMPO = cantidadTiempo;
                receta.FECHA_ENTREGA = DateTime.Now.Date;
                receta.USUARIO_ID_USUARIO = idUsuario;
                Acceso.ModeloCesfam.RECETA.Add(receta);
                Acceso.ModeloCesfam.SaveChanges();
                return true;

            }
            catch (Exception)
            {

                return false;
            }
        }

        private bool CargarDetalle(string[] detalleReceta, int idRecetaNew)
        {
            try
            {
                for (int i = 0; i < detalleReceta.Length; i++)
                {
                    CesfamNegocio.DetalleReceta bssDetalleReceta = new DetalleReceta();

                    string[] var = detalleReceta[i].Split(';');
                    int idProducto = Convert.ToInt32(var[0]);
                    int idReceta = idRecetaNew;
                    int cantidad = Convert.ToInt32(var[1]);
                    int horas = Convert.ToInt32(var[2]);
                    int dias = Convert.ToInt32(var[3]);
                    string unidadMedida = var[4];
                    Random RndId = new Random();
                    int idDetalleReceta = RndId.Next(1, 9999999);
                    while (bssDetalleReceta.Read(idDetalleReceta))
                    {
                        idDetalleReceta = RndId.Next(1, 9999999);
                    }
                    int CantidadTotal = this.CalcularCantidad(cantidad, horas, dias, unidadMedida, idProducto);
                    bssDetalleReceta.CrearDetalleReceta(idProducto, idReceta, CantidadTotal, idDetalleReceta, string.Format("Pendiente Entrega"));
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private int CalcularCantidad(int cantidad, int horas, int dias, string unidadMedida, int idProducto)
        {
            CesfamNegocio.Producto bssProducto = new Producto();
            int CantidadTotal = 0;
            if (unidadMedida == "Cantidad")
            {
                int cantVecesXdia = 24 / horas;
                int Resto = 24 % horas;

                if ((Resto * dias) > horas)
                {
                    CantidadTotal = ((Resto / horas) + cantVecesXdia) * cantidad;

                }
                else
                {
                    CantidadTotal = cantVecesXdia * cantidad;
                }
            }
            else if (unidadMedida.Equals("CC"))
            {
                bssProducto.Read(idProducto);
                int cantVecesXdia = 24 / horas;
                int Resto = 24 % horas;
                int Contenido;
                if ((Resto * dias) > horas)
                {
                    Contenido = ((Resto / horas) + cantVecesXdia) * cantidad;
                }
                else
                {
                    Contenido = cantVecesXdia * cantidad;
                }

                if (bssProducto.Gramaje >= Contenido)
                {
                    CantidadTotal = 1;
                }
                else
                {
                    if (Contenido % bssProducto.Gramaje == 0)
                    {
                        CantidadTotal = (Contenido / bssProducto.Gramaje);
                    }
                    else
                    {
                        CantidadTotal = (Contenido / bssProducto.Gramaje) + 1;
                    }
                }
            }
            return CantidadTotal;
        }
        public bool Read(int idReceta)
        {
            try
            {
                CesfamDatos.RECETA receta = Acceso.ModeloCesfam.RECETA.First(tp => tp.ID_RECETA == this.IdReceta);
                this.IdReceta = (int)receta.ID_RECETA;
                this.EstadoReceta = receta.ESTADO_RECETA;
                this.FechaReceta = receta.FECHA_RECETA;
                this.IdTipoReceta = (int)receta.TIPO_RECETA_ID_TIPO_RECETA;
                this.IdPaciente = (int)receta.PACIENTE_ID_PACIENTE;
                this.Diagnostico = receta.DIAGNOSTICO;
                this.CantidadTiempo = (int)receta.CANTIDAD_TIEMPO;
                this.FechaParaEntrega = receta.FECHA_ENTREGA;
                this.IdUsuario = (int)receta.USUARIO_ID_USUARIO;
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public bool ActualizarRecetaEntregada(int idReceta, bool ConReserva)
        {
            try
            {
                CesfamDatos.RECETA receta = Acceso.ModeloCesfam.RECETA.First(tp => tp.ID_RECETA == idReceta);
                if (receta.TIPO_RECETA_ID_TIPO_RECETA == 1)
                {
                    if (receta.CANTIDAD_TIEMPO > 0)
                    {
                            receta.ESTADO_RECETA = string.Format("Pendiente");
                            receta.FECHA_ENTREGA = receta.FECHA_ENTREGA.AddDays(30);
                            receta.CANTIDAD_TIEMPO = receta.CANTIDAD_TIEMPO - 1;
                            receta.CORREOENVIADO = 0;
                    }
                    else
                    {
                        receta.ESTADO_RECETA = string.Format("Entregada");
                    }
                }
                else if (receta.TIPO_RECETA_ID_TIPO_RECETA == 2)
                {
                    if (ConReserva)
                    {
                        receta.ESTADO_RECETA = string.Format("Pendiente por Reserva");
                    }
                    else
                    {
                        receta.ESTADO_RECETA = string.Format("Entregada");
                    }
                }
                Acceso.ModeloCesfam.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool ActualizarReservaEntregada(int idReceta)
        {
            try
            {
                CesfamDatos.RECETA receta = Acceso.ModeloCesfam.RECETA.First(tp => tp.ID_RECETA == idReceta);
                if (receta.TIPO_RECETA_ID_TIPO_RECETA != 1)
                {
                    receta.ESTADO_RECETA = string.Format("Entregada");
                    Acceso.ModeloCesfam.SaveChanges();
                }               
                return true;
            }
            catch (Exception)
            {
                
                return false;
            }
            
        }
        public bool ActualizarCorreoEnviado(int idReceta) 
        {
            try
            {
                CesfamDatos.RECETA receta = Acceso.ModeloCesfam.RECETA.First(tp => tp.ID_RECETA == idReceta);
                receta.CORREOENVIADO = 1;
                Acceso.ModeloCesfam.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                
                return false;
            }
            
        }
        public bool revisarPrescripciones()
        {
            try
            {
                DateTime fechaMenos = DateTime.Now.AddDays(-5);
                DateTime fechaMas = DateTime.Now.AddDays(5);
                int count = 0;
                var dataReceta = Acceso.ModeloCesfam.RECETA.Where(pre => pre.TIPO_RECETA_ID_TIPO_RECETA == 1 && pre.FECHA_ENTREGA >= fechaMenos && pre.FECHA_ENTREGA < fechaMas && pre.CORREOENVIADO == 0 && pre.ESTADO_RECETA == "Pendiente");
                foreach (var item in dataReceta)
                {
                    CorreosAvisos ccorreo = new CorreosAvisos();
                    string asunto = string.Format("Proxima prescripcion");
                    string mensaje = string.Format("Estimada " + item.PACIENTE.NOMBRE + " " + item.PACIENTE.APELLIDO_PATERNO + ": \n\nJunto con saludar, le informo que su prescripcion estara disponible el " + item.FECHA_ENTREGA.ToString("dd/MM/yyyy") + " para que se acerque a la sucursal a retirar. \n\nSaludos Cordiales. \n\nSe despide Cesfam Farmacia.");
                    if (ccorreo.CorreoElectronico(item.PACIENTE.CORREO, asunto, mensaje))
                    {
                        this.ActualizarCorreoEnviado((int)item.ID_RECETA);
                        count++;
                    }
                }
                if (count > 0) 
                {
                    return true;
                }
                else 
                {
                    return false;
                }
            }
            catch (Exception)
            {

                return false;
            }

        }
        public DataTable ListarRecetas()
        {
            var dt = (from rec in Acceso.ModeloCesfam.RECETA.ToList()
                      where rec.ESTADO_RECETA == "Pendiente" || rec.ESTADO_RECETA == "Pendiente por Reserva"
                      orderby FechaParaEntrega ascending
                      select rec).ToList();
            DataTable dts = new DataTable("Lista");
            dts.Columns.Add("idReceta");
            dts.Columns.Add("Codigo de Paciente");
            dts.Columns.Add("idPaciente");
            dts.Columns.Add("Nombre Paciente");
            dts.Columns.Add("Fecha Emision");
            dts.Columns.Add("Estado Receta");
            dts.Columns.Add("idTipoReceta");
            dts.Columns.Add("Tipo Documento");
            dts.Columns.Add("idUsuario");
            dts.Columns.Add("Nombre Medico");
            dts.Columns.Add("Fecha de Entrega");
            foreach (var data in dt)
            {
                dts.Rows.Add(data.ID_RECETA, data.PACIENTE.CODIGO_LIBRETA, data.PACIENTE_ID_PACIENTE, data.PACIENTE.NOMBRE + " " + data.PACIENTE.APELLIDO_PATERNO, data.FECHA_RECETA, data.ESTADO_RECETA, data.TIPO_RECETA_ID_TIPO_RECETA,
                             data.TIPO_RECETA.NOMBRE_TIPO, data.USUARIO_ID_USUARIO, data.USUARIO.NOMBRE + " " + data.USUARIO.APELLIDO_PATERNO, data.FECHA_ENTREGA);
            }
            return dts;
        }
        public DataTable ListarRecetasXTipo(int idTipoReceta)
        {
            var dt = (from rec in Acceso.ModeloCesfam.RECETA.ToList()
                      where rec.TIPO_RECETA_ID_TIPO_RECETA == idTipoReceta && rec.ESTADO_RECETA == "Pendiente" || rec.ESTADO_RECETA == "Pendiente por Reserva"
                      orderby FechaParaEntrega ascending
                      select rec).ToList();
            DataTable dts = new DataTable("Lista");
            dts.Columns.Add("idReceta");
            dts.Columns.Add("Codigo de Paciente");
            dts.Columns.Add("idPaciente");
            dts.Columns.Add("Nombre Paciente");
            dts.Columns.Add("Fecha Emision");
            dts.Columns.Add("Estado Receta");
            dts.Columns.Add("idTipoReceta");
            dts.Columns.Add("Tipo Documento");
            dts.Columns.Add("idUsuario");
            dts.Columns.Add("Nombre Medico");
            dts.Columns.Add("Fecha de Entrega");
            foreach (var data in dt)
            {
                dts.Rows.Add(data.ID_RECETA, data.PACIENTE.CODIGO_LIBRETA, data.PACIENTE_ID_PACIENTE, data.PACIENTE.NOMBRE + " " + data.PACIENTE.APELLIDO_PATERNO, data.FECHA_RECETA, data.ESTADO_RECETA, data.TIPO_RECETA_ID_TIPO_RECETA,
                             data.TIPO_RECETA.NOMBRE_TIPO, data.USUARIO_ID_USUARIO, data.USUARIO.NOMBRE + " " + data.USUARIO.APELLIDO_PATERNO, data.FECHA_ENTREGA);
            }
            return dts;
        }
        public DataTable ListarReceXCodigoPac(string codigoPaciente)
        {
            var dt = (from rec in Acceso.ModeloCesfam.RECETA.ToList()
                      where rec.PACIENTE.CODIGO_LIBRETA == codigoPaciente && rec.ESTADO_RECETA == "Pendiente" || rec.ESTADO_RECETA == "Pendiente por Reserva"
                      orderby FechaParaEntrega ascending
                      select rec).ToList();
            DataTable dts = new DataTable("Lista");
            dts.Columns.Add("idReceta");
            dts.Columns.Add("Codigo de Paciente");
            dts.Columns.Add("idPaciente");
            dts.Columns.Add("Nombre Paciente");
            dts.Columns.Add("Fecha Emision");
            dts.Columns.Add("Estado Receta");
            dts.Columns.Add("idTipoReceta");
            dts.Columns.Add("Tipo Documento");
            dts.Columns.Add("idUsuario");
            dts.Columns.Add("Nombre Medico");
            dts.Columns.Add("Fecha de Entrega");
            foreach (var data in dt)
            {
                dts.Rows.Add(data.ID_RECETA, data.PACIENTE.CODIGO_LIBRETA, data.FECHA_RECETA, data.ESTADO_RECETA, data.TIPO_RECETA_ID_TIPO_RECETA,
                             data.TIPO_RECETA.NOMBRE_TIPO, data.PACIENTE_ID_PACIENTE, data.PACIENTE.NOMBRE + " " + data.PACIENTE.APELLIDO_PATERNO,
                             data.USUARIO_ID_USUARIO, data.USUARIO.NOMBRE + " " + data.USUARIO.APELLIDO_PATERNO, data.FECHA_ENTREGA);
            }
            return dts;
        }
    }
}
