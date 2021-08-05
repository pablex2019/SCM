using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion.Controlador
{
    public class Anticipo
    {
        #region Archivo
        private string Archivo { get; set; }
        private Datos.GestorArchivosTexto GestorArchivosTexto { get; set; }
        private List<Presentacion.Modelo.Anticipo> Anticipos { get; set; }
        private string DatosAnticipos { get; set; }
        #endregion
        private Controlador.Generico Generico;

        public Anticipo(string _Archivo)
        {
            this.Archivo = _Archivo;
            this.GestorArchivosTexto = new Datos.GestorArchivosTexto(this.Archivo);
            this.Generico = new Generico();
        }
        private void Leer()
        {
            this.DatosAnticipos = this.GestorArchivosTexto.Leer();
            this.Anticipos = this.DatosAnticipos?.Length > 0 ? JsonConvert.DeserializeObject<List<Presentacion.Modelo.Anticipo>>(this.DatosAnticipos) : new List<Presentacion.Modelo.Anticipo>();
        }
        private void Guardar()
        {
            //Convierto todos los datos a string
            this.DatosAnticipos = JsonConvert.SerializeObject(this.Anticipos);
            this.GestorArchivosTexto.Guardar(this.DatosAnticipos);
        }
        public List<Presentacion.Modelo.Anticipo> Listado()
        {
            Leer();
            return this.Anticipos.ToList();
        }
        public Presentacion.Modelo.Anticipo Obtener(int id)
        {
            Leer();
            return this.Anticipos.Where(x => x.Id == id).FirstOrDefault();
        }
        public bool Existe(Vista.Pedido.Anticipo.Nuevo Nuevo)
        {
            return this.Anticipos.Any(x => x.Numero == Convert.ToInt32(Nuevo.txtNumero.Text));
        }
        public int ObtenerUltimoID()
        {
            return Anticipos.Max(x => x.Id) + 1;
        }
        public void ABM(int Operacion, Vista.Pedido.Anticipo.Nuevo nuevo, DataGridView Grilla, int Id)
        {
            Leer();
            Presentacion.Modelo.Anticipo anticipo = new Presentacion.Modelo.Anticipo();
            if (nuevo.cboEstado.Text == "Seleccione")
            {
                MessageBox.Show("Debe seleccionar un estado", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                switch (Operacion)
                {
                    case 1://Alta
                        if (Existe(nuevo) != true)
                        {
                            anticipo.Id = Anticipos.Count == 0 ? 1 : ObtenerUltimoID();
                            anticipo.Numero = Convert.ToInt32(nuevo.txtNumero.Text);
                            anticipo.Importe = Convert.ToInt32(nuevo.txtImporte.Text);
                            anticipo.Estado = nuevo.cboEstado.Text == "Habilitado" ? false : true;
                            this.Anticipos.Add(anticipo);
                            Guardar();
                            Generico.LimpiarCampos(nuevo);
                            Generico.ComboBoxEnSeleccione(nuevo);
                            Generico.ElementoAgregado("Anticipo");
                            Grilla.DataSource = Listado();
                        }
                        break;
                }
            }
        }
    }
}
