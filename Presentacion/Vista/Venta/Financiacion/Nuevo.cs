using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion.Vista.Venta.Financiacion
{
    public partial class Nuevo : Form
    {
        public DataGridView Financiaciones;
        private Controlador.Generico Generico;
        private Controlador.Financiacion Financiacion;

        public Nuevo()
        {
            InitializeComponent();
            Generico = new Controlador.Generico();
            Financiacion = new Controlador.Financiacion("Financiaciones");
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Generico.SalirCancelar(this, 2);
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Financiacion.ABM(1, this, Financiaciones,0);
        }

        private void Nuevo_Load(object sender, EventArgs e)
        {
            cboEstado.DataSource = Generico.EstadosGenericos();
        }
    }
}
