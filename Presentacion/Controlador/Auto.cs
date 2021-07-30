using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion.Controlador
{
    public class Auto
    {
        #region Archivo
        private string Archivo { get; set; }
        private Datos.GestorArchivosTexto GestorArchivosTexto { get; set; }
        private List<Modelo.Auto> Autos { get; set; }
        private string DatosAutos { get; set; }
        #endregion
        private Controlador.Generico Generico;

        public Auto(string _Archivo)
        {
            this.Archivo = _Archivo;
            this.GestorArchivosTexto = new Datos.GestorArchivosTexto(this.Archivo);
            this.Generico = new Generico();
        }
        private void Leer()
        {
            this.DatosAutos = this.GestorArchivosTexto.Leer();
            this.Autos = this.DatosAutos?.Length > 0 ? JsonConvert.DeserializeObject<List<Modelo.Auto>>(this.DatosAutos) : new List<Modelo.Auto>();
        }
        private void Guardar()
        {
            //Convierto todos los datos a string
            this.DatosAutos = JsonConvert.SerializeObject(this.Autos);
            this.GestorArchivosTexto.Guardar(this.DatosAutos);
        }
        public List<Modelo.Auto> Listado()
        {
            Leer();
            return this.Autos.ToList();
        }
        public Modelo.Auto Obtener(int id)
        {
            Leer();
            return this.Autos.Where(x => x.Id == id).FirstOrDefault();
        }
        public bool Existe(Vista.Auto.Nuevo Nuevo)
        {
            return this.Autos.Any(x => x.Matricula == Nuevo.txtMatricula.Text);
        }
        public int ObtenerUltimoID()
        {
            return Autos.Max(x => x.Id) + 1;
        }
        public void ABM(int Operacion, Vista.Auto.Nuevo Nuevo, Vista.Auto.Editar Editar, int Id,int IdColor,int IdMarca, DataGridView GrillaAutos)
        {
            Leer();
            switch (Operacion)
            {
                case 1://Alta
                    Modelo.Auto Auto = new Modelo.Auto();
                    if (Existe(Nuevo) != true)
                    {
                        Auto.Id = Autos.Count >0 ? ObtenerUltimoID():1;
                        Auto.Matricula = Nuevo.txtMatricula.Text;
                        Auto.Precio = float.Parse(Nuevo.txtPrecio.Text);
                        /*Contiar Mañana*/
                        Auto.Color = Color;
                        Auto.Marca = Marca;
                        Auto.Estado = Nuevo.txtEstado.Text;
                        this.Autos.Add(Auto);
                        Guardar();
                        Generico.LimpiarCampos(Nuevo);
                        GrillaAutos.DataSource = Listado();
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
