using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion.Controlador
{
    public class Marca
    {
        #region Archivo
        private string Archivo { get; set; }
        private Datos.GestorArchivosTexto GestorArchivosTexto { get; set; }
        private List<Presentacion.Modelo.Marca> Marcas { get; set; }
        private string DatosMarcas { get; set; }
        #endregion
        private Controlador.Generico Generico;

        public Marca(string _Archivo)
        {
            this.Archivo = _Archivo;
            this.GestorArchivosTexto = new Datos.GestorArchivosTexto(this.Archivo);
            this.Generico = new Generico();
        }
        private void Leer()
        {
            this.DatosMarcas = this.GestorArchivosTexto.Leer();
            this.Marcas = this.DatosMarcas?.Length > 0 ? JsonConvert.DeserializeObject<List<Presentacion.Modelo.Marca>>(this.DatosMarcas) : new List<Presentacion.Modelo.Marca>();
        }
        private void Guardar()
        {
            //Convierto todos los datos a string
            this.DatosMarcas = JsonConvert.SerializeObject(this.Marcas);
            this.GestorArchivosTexto.Guardar(this.DatosMarcas);
        }
        public List<Presentacion.Modelo.Marca> Listado()
        {
            Leer();
            return this.Marcas.ToList();
        }
        public Presentacion.Modelo.Marca Obtener(int id)
        {
            Leer();
            return this.Marcas.Where(x => x.Id == id).FirstOrDefault();
        }
        public bool Existe(Vista.Auto.Marca.Nuevo Nuevo)
        {
            return this.Marcas.Any(x => x.Descripcion == Nuevo.txtDescripcion.Text);
        }
        public int ObtenerUltimoID()
        {
            return Marcas.Max(x => x.Id) + 1;
        }
        public void ABM(int Operacion, Vista.Auto.Marca.Nuevo nuevo, DataGridView Grilla, int Id)
        {
            Leer();
            Presentacion.Modelo.Marca marca = new Presentacion.Modelo.Marca();
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
                             marca.Id = Marcas.Count == 0 ? 1 : ObtenerUltimoID();
                             marca.Descripcion = nuevo.txtDescripcion.Text;
                             marca.Estado = nuevo.cboEstado.Text == "Habilitado" ? false : true;
                             this.Marcas.Add(marca);
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
