using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion.Controlador
{
    public class Localidad
    {
        #region Archivo
        private string Archivo { get; set; }
        private Datos.GestorArchivosTexto GestorArchivosTexto { get; set; }
        private List<Modelo.Localidad> Localidades { get; set; }
        private string DatosLocalidades { get; set; }
        #endregion
        private Controlador.Generico Generico;

        public Localidad(string _Archivo)
        {
            this.Archivo = _Archivo;
            this.GestorArchivosTexto = new Datos.GestorArchivosTexto(this.Archivo);
            this.Generico = new Generico();
        }
        private void Leer()
        {
            this.DatosLocalidades = this.GestorArchivosTexto.Leer();
            this.Localidades = this.DatosLocalidades?.Length > 0 ? JsonConvert.DeserializeObject<List<Modelo.Localidad>>(this.DatosLocalidades) : new List<Modelo.Localidad>();
        }
        private void Guardar()
        {
            //Convierto todos los datos a string
            this.DatosLocalidades = JsonConvert.SerializeObject(this.Localidades);
            this.GestorArchivosTexto.Guardar(this.DatosLocalidades);
        }
        public List<Modelo.Localidad> Listado()
        {
            Leer();
            return this.Localidades.ToList();
        }
        public Modelo.Localidad Obtener(int id)
        {
            Leer();
            return this.Localidades.Where(x => x.Id == id).FirstOrDefault();
        }
        public bool Existe(Vista.Provincia.Localidad.Nuevo Nuevo)
        {
            return this.Localidades.Any(x => x.CodigoPostal == Nuevo.txtCodigoPostal.Text || x.Nombre == Nuevo.txtNombre.Text);
        }
        public int ObtenerUltimoID()
        {
            return Localidades.Max(x => x.Id) + 1;
        }
        public void ABM(int Operacion, Vista.Provincia.Localidad.Nuevo Nuevo, Vista.Provincia.Localidad.Editar Editar, int Id, DataGridView Grilla)
        {
            Leer();
            switch (Operacion)
            {
                case 1://Alta
                    Modelo.Localidad Localidad = new Modelo.Localidad();
                    if (Localidades.Count > 0)
                    {
                        if (Existe(Nuevo) != true)
                        {
                            Localidad.Id = ObtenerUltimoID();
                            Localidad.CodigoPostal = Nuevo.txtCodigoPostal.Text;
                            Localidad.Nombre = Nuevo.txtNombre.Text;
                            this.Localidades.Add(Localidad);
                            Guardar();
                            Generico.LimpiarCampos(Nuevo);
                            Grilla.DataSource = Listado();
                        }
                    }
                    else
                    {
                        Localidad.Id = 1;
                        Localidad.CodigoPostal = Nuevo.txtCodigoPostal.Text;
                        Localidad.Nombre = Nuevo.txtNombre.Text;
                        this.Localidades.Add(Localidad);
                        Guardar();
                        Generico.LimpiarCampos(Nuevo);
                        Grilla.DataSource = Listado();
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
