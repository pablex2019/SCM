using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion.Vista.Auto.Marca
{
    public partial class Nuevo : Form
    {
        public DataGridView Marcas;
        private Controlador.Marca Marca;
        private Controlador.Generico Generico;

        public Nuevo()
        {
            InitializeComponent();
            Generico = new Controlador.Generico();
            Marca = new Controlador.Marca("Marcas");
        }
        private void Nuevo_Load(object sender, EventArgs e)
        {
            cboEstado.DataSource = Generico.EstadosGenericos();
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Generico.SalirCancelar(this, 2);
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Marca.ABM(1, this, Marcas, 0);
        }

        
    }
}
