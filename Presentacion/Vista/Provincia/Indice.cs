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
    public partial class Indice : Form
    {
        public Indice()
        {
            InitializeComponent();
        }

        private void botonNuevo_Click(object sender, EventArgs e)
        {
            Vista.Provincia.Nuevo nuevo = new Nuevo();
            nuevo.Provincias = dataGridView1;
            nuevo.Show();
        }
    }
}
