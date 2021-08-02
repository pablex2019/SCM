using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion.Vista.Provincia
{
    public partial class Nuevo : Form
    {
        public DataGridView Provincias;
        private Controlador.Provincia Provincia;
        private Controlador.Localidad Localidad;
        private Controlador.Generico Generico;

        public Nuevo()
        {
            InitializeComponent();
            Generico = new Controlador.Generico();
            Localidad = new Controlador.Localidad("Localidades");
            Provincia = new Controlador.Provincia("Provincias");
        }
        private void Nuevo_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = Localidad.Listado();
        }
        private void botonCancelar_Click(object sender, EventArgs e)
        {
            Generico.SalirCancelar(this, 2);
        }

        private void botonNuevaLocalidad_Click(object sender, EventArgs e)
        {
            Vista.Provincia.Localidad.Nuevo Nuevo = new Vista.Provincia.Localidad.Nuevo();
            Nuevo.Grilla = dataGridView1;
            Nuevo.Show();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Provincia.ABM(1, this, null, 0, Provincias,dataGridView1);
        }
    }
}
