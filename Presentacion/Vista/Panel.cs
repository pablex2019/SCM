using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion.Vista
{
    public partial class Panel : Form
    {
        private Controlador.Generico Generico;

        public Panel()
        {
            InitializeComponent();
            Generico = new Controlador.Generico();
        }

        private void menuAutos_Click(object sender, EventArgs e)
        {
            new Vista.Auto.Indice().Show();
        }

        private void menuSalir_Click(object sender, EventArgs e)
        {
            Generico.SalirCancelar(null,1);
        }

        private void menuColores_Click(object sender, EventArgs e)
        {
            new Vista.Auto.Color.Indice().Show();
        }

        private void menuMarcas_Click(object sender, EventArgs e)
        {

        }

        private void Panel_Load(object sender, EventArgs e)
        {

        }

        private void menuProvincias_Click(object sender, EventArgs e)
        {
            new Vista.Provincia.Indice().Show();
        }
    }
}
