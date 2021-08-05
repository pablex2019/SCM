using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion.Vista.Auto.Color
{
    public partial class Nuevo : Form
    {
        public DataGridView Colores;
        private Controlador.Color Color;
        private Controlador.Generico Generico;

        public Nuevo()
        {
            InitializeComponent();
            Generico = new Controlador.Generico();
            Color = new Controlador.Color("Colores");
        }

        private void botonCancelar_Click(object sender, EventArgs e)
        {
            Generico.SalirCancelar(this,2);
        }

        private void botonGuardar_Click(object sender, EventArgs e)
        {
            Color.ABM(1, this,Colores,0);
        }

        private void Nuevo_Load(object sender, EventArgs e)
        {
            cboEstado.DataSource = Generico.EstadosGenericos();
        }
    }
}
