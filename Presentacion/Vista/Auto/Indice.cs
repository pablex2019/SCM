using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion.Vista.Auto
{
    public partial class Indice : Form
    {
        private DataGridView Autos;
        private Controlador.Generico Generico;

        public Indice()
        {
            InitializeComponent();
            Generico = new Controlador.Generico();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Vista.Auto.Nuevo nuevo = new Nuevo();
            nuevo.Autos = dataGridView1;
            nuevo.Show();
        }
    }
}
