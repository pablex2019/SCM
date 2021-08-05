using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion.Controlador
{
    public class Empleado
    {
        #region Archivo
        private string Archivo { get; set; }
        private Datos.GestorArchivosTexto GestorArchivosTexto { get; set; }
        private List<Presentacion.Modelo.Empleado> Empleados { get; set; }
        private string DatosEmpleados { get; set; }
        #endregion
        private Controlador.Generico Generico;

        public Empleado(string _Archivo)
        {
            this.Archivo = _Archivo;
            this.GestorArchivosTexto = new Datos.GestorArchivosTexto(this.Archivo);
            this.Generico = new Generico();
        }
        private void Leer()
        {
            this.DatosEmpleados = this.GestorArchivosTexto.Leer();
            this.Empleados = this.DatosEmpleados?.Length > 0 ? JsonConvert.DeserializeObject<List<Presentacion.Modelo.Empleado>>(this.DatosEmpleados) : new List<Presentacion.Modelo.Empleado>();
        }
        private void Guardar()
        {
            //Convierto todos los datos a string
            this.DatosEmpleados = JsonConvert.SerializeObject(this.Empleados);
            this.GestorArchivosTexto.Guardar(this.DatosEmpleados);
        }
        public List<Presentacion.Modelo.Empleado> Listado()
        {
            Leer();
            return this.Empleados.ToList();
        }
        public Presentacion.Modelo.Empleado Obtener(int id)
        {
            Leer();
            return this.Empleados.Where(x => x.Id == id).FirstOrDefault();
        }
        public bool Existe(Vista.Empleado.Nuevo Nuevo)
        {
            return this.Empleados.Any(x => x.Nombre == Nuevo.txtNombre.Text && x.Apellido == Nuevo.txtApellido.Text);
        }
        public int ObtenerUltimoID()
        {
            return Empleados.Max(x => x.Id) + 1;
        }
        public Presentacion.Modelo.Empleado ObtenerUltimoEmpleadoRegistrado()
        {
            Leer();
            return Empleados.LastOrDefault();
        }
        public int ObtenerLegajoUltimoEmpleadoRegistrado()
        {
            Leer();
            return Empleados.Count == 0 ? 1 : Empleados.Max(x => x.Legajo);
        }
        public bool ABM(int Operacion, Vista.Empleado.Nuevo nuevo, DataGridView GrillaEmpleados, DataGridView GrillaPerfiles, int Id)
        {
            bool resultado = false;
            Leer();
            Presentacion.Modelo.Empleado empleado = new Presentacion.Modelo.Empleado();
            if (nuevo.cboEstadoCivil.Text == "Seleccione")
            {
                MessageBox.Show("Debe seleccionar un estado", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (Convert.ToInt32(nuevo.txtEdad.Text) < 18)
            {
                MessageBox.Show("No puede registrarse como empleado porque es menor a 18 años", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                switch (Operacion)
                {
                    case 1://Alta
                        if (GrillaPerfiles.Rows.Count == 0)
                        {
                            MessageBox.Show("No hay perfiles registrados", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            if (Existe(nuevo) != true)
                            {
                                /*Alta de Empleado*/
                                empleado.Id = Empleados.Count == 0 ? 1 : ObtenerUltimoID();
                                empleado.Legajo = Convert.ToInt32(nuevo.txtLegajo.Text);
                                empleado.Nombre = nuevo.txtNombre.Text;
                                empleado.Apellido = nuevo.txtApellido.Text;
                                empleado.FechaNacimiento = nuevo.dtpFechaNacimiento.Value;
                                empleado.EstadoCivil = nuevo.cboEstadoCivil.Text;
                                empleado.TelefonoCelular = nuevo.txtTelefonoCelular.Text;
                                empleado.TelefonoFijo = nuevo.txtTelefonoFijo.Text;
                                empleado.TelefonoAlternativo = nuevo.txtTelefonoAlternativo.Text;
                                empleado.CorreoElectronico = nuevo.txtCorreoElectronico.Text;
                                empleado.CorreoElectronicoAlternativo = nuevo.txtCorreoElectronicoAlternativo.Text;
                                empleado.Estado = nuevo.txtEstado.Text == "Disponible" ? false : true;
                                this.Empleados.Add(empleado);
                                Guardar();
                                resultado = true;
                            }
                        }
                        break;
                }
            }
            return resultado;
        }
    }
}
