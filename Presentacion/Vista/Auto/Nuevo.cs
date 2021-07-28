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
    public partial class Nuevo : Form
    {
        private Controlador.Generico Generico;

        public Nuevo()
        {
            InitializeComponent();
            Generico = new Controlador.Generico();
        }

        private void botonCancelar_Click(object sender, EventArgs e)
        {
            Generico.SalirCancelar(this,2);
        }

        private void botonGuardar_Click(object sender, EventArgs e)
        {

        }

        private void Nuevo_Load(object sender, EventArgs e)
        {

        }
    }
}
