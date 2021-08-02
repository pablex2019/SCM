using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion.Controlador
{
    public class Provincia
    {
        #region Archivo
        private string Archivo { get; set; }
        private Datos.GestorArchivosTexto GestorArchivosTexto { get; set; }
        private List<Presentacion.Modelo.Provincia> Provincias { get; set; }
        private string DatosProvincias { get; set; }
        #endregion
        private Controlador.Generico Generico;

        public Provincia(string _Archivo)
        {
            this.Archivo = _Archivo;
            this.GestorArchivosTexto = new Datos.GestorArchivosTexto(this.Archivo);
            this.Generico = new Generico();
        }
        private void Leer()
        {
            this.DatosProvincias = this.GestorArchivosTexto.Leer();
            this.Provincias = this.DatosProvincias?.Length > 0 ? JsonConvert.DeserializeObject<List<Presentacion.Modelo.Provincia>>(this.DatosProvincias) : new List<Presentacion.Modelo.Provincia>();
        }
        private void Guardar()
        {
            //Convierto todos los datos a string
            this.DatosProvincias = JsonConvert.SerializeObject(this.Provincias);
            this.GestorArchivosTexto.Guardar(this.DatosProvincias);
        }
        public List<Presentacion.Modelo.Provincia> Listado()
        {
            Leer();
            return this.Provincias.ToList();
        }
        public Presentacion.Modelo.Provincia Obtener(int id)
        {
            Leer();
            return this.Provincias.Where(x => x.Id == id).FirstOrDefault();
        }
        public bool Existe(Vista.Provincia.Nuevo Nuevo)
        {
            return this.Provincias.Any(x => x.Nombre == Nuevo.txtNombre.Text);
        }
        public int ObtenerUltimoID()
        {
            return Provincias.Max(x => x.Id) + 1;
        }
        public void ABM(int Operacion, Vista.Provincia.Nuevo Nuevo, Vista.Provincia.Editar Editar, int Id, DataGridView Grilla,DataGridView GrillaLocalidades)
        {
            Leer();
            switch (Operacion)
            {
                case 1://Alta
                    if (GrillaLocalidades.Rows.Count == 0)
                    {
                        MessageBox.Show("No hay localidades registradas", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        Presentacion.Modelo.Provincia Provincia = new Presentacion.Modelo.Provincia();
                        if (Existe(Nuevo) != true)
                        {
                            Provincia.Id = Provincias.Count > 0 ? ObtenerUltimoID() : 1;
                            Provincia.Nombre = Nuevo.txtNombre.Text;
                            this.Provincias.Add(Provincia);
                            Guardar();
                            Generico.LimpiarCampos(Nuevo);
                            Grilla.DataSource = Listado();
                        }
                    }
                    break;
                case 2://Edicion
                    /*
                    var _Articulo = ObtenerArticulo(Id);
                    _Articulo.Descripcion = ArticuloEditar.txtDescripcion.Text;
                    _Articulo.PrecioCosto = Convert.ToDouble(ArticuloEditar.txtPrecioCosto.Text);
                    _Articulo.PrecioVenta = Convert.ToDouble(ArticuloEditar.txtPrecioVenta.Text);
                    _Articulo.Cantidad = Convert.ToInt32(ArticuloEditar.txtCantidad.Text);
                    _Articulo.Ganancia = Convert.ToDouble(ArticuloEditar.txtGanancia.Text);
                    Guardar();
                    MessageBox.Show("Articulo Editado", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Grilla.DataSource = ListadoInicial();
                    */
                    break;
                case 3://Baja
                    /*
                    var _Arti = ObtenerArticulo(Id);
                    this.ListaArticulos.Remove(_Arti);
                    Guardar();
                    MessageBox.Show("Articulo Eliminado", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Grilla.DataSource = ListadoInicial();
                    */
                    break;
            }
        }
    }
}
