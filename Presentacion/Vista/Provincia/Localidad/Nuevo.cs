using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion.Vista.Provincia.Localidad
{
    public partial class Nuevo : Form
    {
        public DataGridView Grilla;
        private Controlador.Localidad Localidad;
        private Controlador.Generico Generico;

        public Nuevo()
        {
            InitializeComponent();
            Generico = new Controlador.Generico();
            Localidad = new Controlador.Localidad("Localidades");
        }

        private void botonCancelar_Click(object sender, EventArgs e)
        {
            Generico.SalirCancelar(this, 2);
        }

        private void botonGuardarLocalidad_Click(object sender, EventArgs e)
        {
            Localidad.ABM(1, this, null, 0, Grilla);
        }
    }
}
