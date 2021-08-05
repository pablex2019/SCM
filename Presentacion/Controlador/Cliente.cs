using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion.Controlador
{
    public class Cliente
    {
        #region Archivo
        private string Archivo { get; set; }
        private Datos.GestorArchivosTexto GestorArchivosTexto { get; set; }
        private List<Presentacion.Modelo.Cliente> Clientes { get; set; }
        private string DatosClientes { get; set; }
        #endregion
        private Controlador.Generico Generico;

        public Cliente(string _Archivo)
        {
            this.Archivo = _Archivo;
            this.GestorArchivosTexto = new Datos.GestorArchivosTexto(this.Archivo);
            this.Generico = new Generico();
        }
        private void Leer()
        {
            this.DatosClientes = this.GestorArchivosTexto.Leer();
            this.Clientes = this.DatosClientes?.Length > 0 ? JsonConvert.DeserializeObject<List<Presentacion.Modelo.Cliente>>(this.DatosClientes) : new List<Presentacion.Modelo.Cliente>();
        }
        private void Guardar()
        {
            //Convierto todos los datos a string
            this.DatosClientes = JsonConvert.SerializeObject(this.Clientes);
            this.GestorArchivosTexto.Guardar(this.DatosClientes);
        }
        public List<Presentacion.Modelo.Cliente> Listado()
        {
            Leer();
            return this.Clientes.ToList();
        }
        public Presentacion.Modelo.Cliente Obtener(int id)
        {
            Leer();
            return this.Clientes.Where(x => x.Id == id).FirstOrDefault();
        }
        public bool Existe(Vista.Cliente.Nuevo Nuevo)
        {
            return this.Clientes.Any(x => x.Nombre == Nuevo.txtNombre.Text && x.Apellido == Nuevo.txtApellido.Text);
        }
        public int ObtenerUltimoID()
        {
            return Clientes.Max(x => x.Id) + 1;
        }
        public int ObtenerNumeroUltimoClienteRegistrado()
        {
            Leer();
            return Clientes.Count == 0 ? 1 : Clientes.Max(x => x.Numero);
        }
        public void ABM(int Operacion, Vista.Cliente.Nuevo nuevo, DataGridView Grilla, int Id)
        {
            Leer();
            Presentacion.Modelo.Cliente cliente = new Presentacion.Modelo.Cliente();
            if (nuevo.cboEstadoCivil.Text == "Seleccione")
            {
                MessageBox.Show("Debe seleccionar un estado", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (Convert.ToInt32(nuevo.txtEdad.Text) < 18)
            {
                MessageBox.Show("No puede registrarse como cliente porque es menor a 18 años", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                switch (Operacion)
                {
                    case 1://Alta
                        if (Existe(nuevo) != true)
                        {
                            cliente.Id = Clientes.Count == 0 ? 1 : ObtenerUltimoID();
                            cliente.Numero = Convert.ToInt32(nuevo.txtNumero.Text);
                            cliente.Nombre = nuevo.txtNombre.Text;
                            cliente.Apellido = nuevo.txtApellido.Text;
                            cliente.FechaNacimiento = nuevo.dtpFechaNacimiento.Value;
                            cliente.EstadoCivil = nuevo.cboEstadoCivil.Text;
                            cliente.TelefonoCelular = nuevo.txtTelefonoCelular.Text;
                            cliente.TelefonoFijo = nuevo.txtTelefonoFijo.Text;
                            cliente.TelefonoAlternativo = nuevo.txtTelefonoAlternativo.Text;
                            cliente.CorreoElectronico = nuevo.txtCorreoElectronico.Text;
                            cliente.CorreoElectronicoAlternativo = nuevo.txtCorreoElectronicoAlternativo.Text;
                            cliente.Estado = nuevo.txtEstado.Text == "Disponible" ? false : true;
                            this.Clientes.Add(cliente);
                            Guardar();
                            Generico.LimpiarCampos(nuevo);
                            Generico.FechaActual(nuevo);
                            Generico.ComboBoxEnSeleccione(nuevo);
                            Generico.ElementoAgregado("Cliente");
                            Grilla.DataSource = Listado();
                        }
                        break;
                }
            }
        }
    }
}
