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
        private Controlador.Localidad Localidad;
        private Controlador.Generico Generico;

        public Nuevo()
        {
            InitializeComponent();
            Generico = new Controlador.Generico();
            Localidad = new Controlador.Localidad("Localidades");
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

        
    }
}
