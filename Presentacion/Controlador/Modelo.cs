using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion.Controlador
{
    public class Modelo
    {
        #region Archivo
        private string Archivo { get; set; }
        private Datos.GestorArchivosTexto GestorArchivosTexto { get; set; }
        private List<Presentacion.Modelo.Modelo> Modelos { get; set; }
        private string DatosModelos { get; set; }
        #endregion
        private Controlador.Generico Generico;

        public Modelo(string _Archivo)
        {
            this.Archivo = _Archivo;
            this.GestorArchivosTexto = new Datos.GestorArchivosTexto(this.Archivo);
            this.Generico = new Generico();
        }
        private void Leer()
        {
            this.DatosModelos = this.GestorArchivosTexto.Leer();
            this.Modelos = this.DatosModelos?.Length > 0 ? JsonConvert.DeserializeObject<List<Presentacion.Modelo.Modelo>>(this.DatosModelos) : new List<Presentacion.Modelo.Modelo>();
        }
        private void Guardar()
        {
            //Convierto todos los datos a string
            this.DatosModelos = JsonConvert.SerializeObject(this.Modelos);
            this.GestorArchivosTexto.Guardar(this.DatosModelos);
        }
        public List<Presentacion.Modelo.Modelo> Listado()
        {
            Leer();
            return this.Modelos.ToList();
        }
        public Presentacion.Modelo.Modelo Obtener(int id)
        {
            Leer();
            return this.Modelos.Where(x => x.Id == id).FirstOrDefault();
        }
        public bool Existe(Vista.Auto.Modelo.Nuevo Nuevo)
        {
            return this.Modelos.Any(x => x.Descripcion == Nuevo.txtDescripcion.Text);
        }
        public int ObtenerUltimoID()
        {
            return Modelos.Max(x => x.Id) + 1;
        }
        public void ABM(int Operacion, Vista.Auto.Modelo.Nuevo nuevo, DataGridView Grilla, int Id)
        {
            Leer();
            Presentacion.Modelo.Modelo modelo = new Presentacion.Modelo.Modelo();
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
                            modelo.Id = Modelos.Count == 0 ? 1 : ObtenerUltimoID();
                            modelo.Descripcion = nuevo.txtDescripcion.Text;
                            modelo.Estado = nuevo.cboEstado.Text == "Habilitado" ? false : true;
                            this.Modelos.Add(modelo);
                            Guardar();
                            Generico.LimpiarCampos(nuevo);
                            Generico.ComboBoxEnSeleccione(nuevo);
                            Grilla.DataSource = Listado();
                        }
                        break;
                }
            }
        }
    }
}
