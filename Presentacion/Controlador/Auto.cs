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
        private List<Presentacion.Modelo.Auto> Autos { get; set; }
        private string DatosAutos { get; set; }
        #endregion
        private Controlador.Generico Generico;
        private Controlador.Color Color;
        private Controlador.Marca Marca;
        private Controlador.Modelo Modelo;

        public Auto(string _Archivo)
        {
            this.Archivo = _Archivo;
            this.GestorArchivosTexto = new Datos.GestorArchivosTexto(this.Archivo);
            this.Generico = new Generico();
            Color = new Color("Colores");
            Marca = new Marca("Marcas");
            Modelo = new Modelo("Modelos");
        }
        private void Leer()
        {
            this.DatosAutos = this.GestorArchivosTexto.Leer();
            this.Autos = this.DatosAutos?.Length > 0 ? JsonConvert.DeserializeObject<List<Presentacion.Modelo.Auto>>(this.DatosAutos) : new List<Presentacion.Modelo.Auto>();
        }
        private void Guardar()
        {
            //Convierto todos los datos a string
            this.DatosAutos = JsonConvert.SerializeObject(this.Autos);
            this.GestorArchivosTexto.Guardar(this.DatosAutos);
        }
        public List<Presentacion.Modelo.Auto> Listado()
        {
            Leer();
            return this.Autos.ToList();
        }
        public Presentacion.Modelo.Auto Obtener(int id)
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
        public void ABM(int Operacion, Vista.Auto.Nuevo Nuevo, Vista.Auto.Editar Editar, int Id,int IdColor,int IdMarca, int IdModelo, DataGridView GrillaMarcas, DataGridView GrillaColores,DataGridView GrillaAutos, DataGridView GrillaModelos)
        {
            Leer();
            switch (Operacion)
            {
                case 1://Alta
                    if (GrillaColores.Rows.Count == 0)
                    {
                        MessageBox.Show("No hay colores registrados", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        if (GrillaMarcas.Rows.Count == 0)
                        {
                            MessageBox.Show("No hay marcas registrados", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            if (GrillaModelos.Rows.Count == 0)
                            {
                                MessageBox.Show("No hay modelos de autos registrados", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {
                                Presentacion.Modelo.Auto Auto = new Presentacion.Modelo.Auto();
                                if (Existe(Nuevo) != true)
                                {
                                    Auto.Id = Autos.Count > 0 ? ObtenerUltimoID() : 1;
                                    Auto.Matricula = Nuevo.txtMatricula.Text;
                                    Auto.Precio = float.Parse(Nuevo.txtPrecio.Text);
                                    Auto.Color = Color.Obtener(IdColor);
                                    Auto.Marca = Marca.Obtener(IdMarca);
                                    Auto.Modelo = Modelo.Obtener(IdModelo);
                                    Auto.Estado = Nuevo.txtEstado.Text;
                                    this.Autos.Add(Auto);
                                    Guardar();
                                    Generico.LimpiarCampos(Nuevo);
                                    GrillaAutos.DataSource = Listado();
                                }
                            }
                        }
                    }
                    break;
            }            
        }
    }
}
