using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion.Vista.Venta
{
    public partial class Indice : Form
    {
        public Indice()
        {
            InitializeComponent();
        }

        private void btnVerVentas_Click(object sender, EventArgs e)
        {
            Vista.LineaDeVentas.Ver ver = new LineaDeVentas.Ver();
            ver.Ventas = dgvVentas;
            ver.Show();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Vista.Venta.Nuevo nuevo = new Vista.Venta.Nuevo();
            nuevo.Ventas = dgvVentas;
            nuevo.Show();
        }

        private void Indice_Load(object sender, EventArgs e)
        {
            
        }
    }
}
