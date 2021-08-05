using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion.Controlador
{
    public class Perfil
    {
        #region Archivo
        private string Archivo { get; set; }
        private Datos.GestorArchivosTexto GestorArchivosTexto { get; set; }
        private List<Presentacion.Modelo.Perfil> Perfiles { get; set; }
        private string DatosPerfiles { get; set; }
        #endregion
        private Controlador.Generico Generico;

        public Perfil(string _Archivo)
        {
            this.Archivo = _Archivo;
            this.GestorArchivosTexto = new Datos.GestorArchivosTexto(this.Archivo);
            this.Generico = new Generico();
        }
        private void Leer()
        {
            this.DatosPerfiles = this.GestorArchivosTexto.Leer();
            this.Perfiles = this.DatosPerfiles?.Length > 0 ? JsonConvert.DeserializeObject<List<Presentacion.Modelo.Perfil>>(this.DatosPerfiles) : new List<Presentacion.Modelo.Perfil>();
        }
        private void Guardar()
        {
            //Convierto todos los datos a string
            this.DatosPerfiles = JsonConvert.SerializeObject(this.Perfiles);
            this.GestorArchivosTexto.Guardar(this.DatosPerfiles);
        }
        public List<Presentacion.Modelo.Perfil> Listado()
        {
            Leer();
            return this.Perfiles.ToList();
        }
        public Presentacion.Modelo.Perfil Obtener(int id)
        {
            Leer();
            return this.Perfiles.Where(x => x.Id == id).FirstOrDefault();
        }
        public bool Existe(Vista.Empleado.Perfil.Nuevo Nuevo)
        {
            return this.Perfiles.Any(x => x.Nombre == Nuevo.txtDescripcion.Text);
        }
        public int ObtenerUltimoID()
        {
            return Perfiles.Max(x => x.Id) + 1;
        }
        public void ABM(int Operacion, Vista.Empleado.Perfil.Nuevo nuevo, DataGridView Grilla, int Id)
        {
            Leer();
            Presentacion.Modelo.Perfil perfil = new Presentacion.Modelo.Perfil();
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
                            perfil.Id = Perfiles.Count == 0 ? 1 : ObtenerUltimoID();
                            perfil.Nombre = nuevo.txtDescripcion.Text;
                            perfil.Estado = nuevo.cboEstado.Text == "Habilitado" ? false : true;
                            this.Perfiles.Add(perfil);
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
