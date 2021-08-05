using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion.Controlador
{
    public class Financiacion
    {
        #region Archivo
        private string Archivo { get; set; }
        private Datos.GestorArchivosTexto GestorArchivosTexto { get; set; }
        private List<Presentacion.Modelo.Financiacion> Financiaciones { get; set; }
        private string DatosFinanciaciones { get; set; }
        
        #endregion
        private Controlador.Generico Generico;
        private Controlador.Cuota Cuota;

        public Financiacion(string _Archivo)
        {
            this.Archivo = _Archivo;
            this.GestorArchivosTexto = new Datos.GestorArchivosTexto(this.Archivo);
            this.Generico = new Generico();
            Cuota = new Cuota("Cuotas");
        }
        private void Leer()
        {
            this.DatosFinanciaciones = this.GestorArchivosTexto.Leer();
            this.Financiaciones = this.DatosFinanciaciones?.Length > 0 ? JsonConvert.DeserializeObject<List<Presentacion.Modelo.Financiacion>>(this.DatosFinanciaciones) : new List<Presentacion.Modelo.Financiacion>();
        }
        private void Guardar()
        {
            //Convierto todos los datos a string
            this.DatosFinanciaciones = JsonConvert.SerializeObject(this.Financiaciones);
            this.GestorArchivosTexto.Guardar(this.DatosFinanciaciones);
        }
        public List<Presentacion.Modelo.Financiacion> Listado()
        {
            Leer();
            return this.Financiaciones.ToList();
        }
        public Presentacion.Modelo.Financiacion Obtener(int id)
        {
            Leer();
            return this.Financiaciones.Where(x => x.Id == id).FirstOrDefault();
        }
        public bool Existe(Vista.Venta.Financiacion.Nuevo Nuevo)
        {
            return this.Financiaciones.Any(x => x.CantidadCuotas == Convert.ToInt32(Nuevo.txtCantidadCuotas.Text));
        }
        public int ObtenerUltimoID()
        {
            return Financiaciones.Max(x => x.Id) + 1;
        }
        public int ObtenerCantidadCuotasFinanciacionSeleccionada(int Id)
        {
            Leer();
            return Financiaciones.First(x => x.Id == Id).CantidadCuotas;
        }
        public void ABM(int Operacion, Vista.Venta.Financiacion.Nuevo nuevo, DataGridView GrillaFinanciaciones, int Id)
        {
            Leer();
            Presentacion.Modelo.Financiacion financiacion = new Presentacion.Modelo.Financiacion();
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
                            financiacion.Id = Financiaciones.Count == 0 ? 1 : ObtenerUltimoID();
                            financiacion.Descripcion = nuevo.txtDescripcion.Text;
                            financiacion.CantidadCuotas = Convert.ToInt32(nuevo.txtCantidadCuotas.Text);
                            financiacion.Estado = nuevo.cboEstado.Text == "Habilitado" ? false : true;
                            this.Financiaciones.Add(financiacion);
                            Guardar();
                            Generico.LimpiarCampos(nuevo);
                            Generico.FechaActual(nuevo);
                            Generico.ComboBoxEnSeleccione(nuevo);
                            Generico.ElementoAgregado("Financiación");
                            GrillaFinanciaciones.DataSource = Listado();
                        }
                        break;
                }
            }
        }
    }
}
