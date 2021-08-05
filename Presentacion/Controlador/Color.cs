using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion.Controlador
{
    public class Color
    {
        #region Archivo
        private string Archivo { get; set; }
        private Datos.GestorArchivosTexto GestorArchivosTexto { get; set; }
        private List<Presentacion.Modelo.Color> Colores { get; set; }
        private string DatosColores { get; set; }
        #endregion
        private Controlador.Generico Generico;

        public Color(string _Archivo)
        {
            this.Archivo = _Archivo;
            this.GestorArchivosTexto = new Datos.GestorArchivosTexto(this.Archivo);
            this.Generico = new Generico();
        }
        private void Leer()
        {
            this.DatosColores = this.GestorArchivosTexto.Leer();
            this.Colores = this.DatosColores?.Length > 0 ? JsonConvert.DeserializeObject<List<Presentacion.Modelo.Color>>(this.DatosColores) : new List<Presentacion.Modelo.Color>();
        }
        private void Guardar()
        {
            //Convierto todos los datos a string
            this.DatosColores = JsonConvert.SerializeObject(this.Colores);
            this.GestorArchivosTexto.Guardar(this.DatosColores);
        }
        public List<Presentacion.Modelo.Color> Listado()
        {
            Leer();
            return this.Colores.ToList();
        }
        public Presentacion.Modelo.Color Obtener(int id)
        {
            Leer();
            return this.Colores.Where(x => x.Id == id).FirstOrDefault();
        }
        public bool Existe(Vista.Auto.Color.Nuevo Nuevo)
        {
            return this.Colores.Any(x => x.Nombre == Nuevo.txtDescripcion.Text);
        }
        public int ObtenerUltimoID()
        {
            return Colores.Max(x => x.Id) + 1;
        }
        public void ABM(int Operacion,Vista.Auto.Color.Nuevo nuevo,DataGridView Grilla, int Id)
        {
            Leer();
            Presentacion.Modelo.Color color = new Presentacion.Modelo.Color();
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
                            color.Id = Colores.Count == 0 ? 1 : ObtenerUltimoID();
                            color.Nombre = nuevo.txtDescripcion.Text;
                            color.Estado = nuevo.cboEstado.Text == "Habilitado" ? false : true;
                            this.Colores.Add(color);
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
