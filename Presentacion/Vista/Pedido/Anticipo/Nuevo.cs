using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion.Vista.Pedido.Anticipo
{
    public partial class Nuevo : Form
    {
        public DataGridView Anticipos;
        private Controlador.Anticipo Anticipo;
        private Controlador.Generico Generico;

        public Nuevo()
        {
            InitializeComponent();
            Anticipo = new Controlador.Anticipo("Anticipos");
            Generico = new Controlador.Generico();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Generico.SalirCancelar(this, 2);
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Anticipo.ABM(1, this, Anticipos, 0);
        }

        private void Nuevo_Load(object sender, EventArgs e)
        {
            cboEstado.DataSource = Generico.EstadoAnticipoYCuotas();
        }
    }
}
