using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion.Vista.LineaDeVentas
{
    public partial class Ver : Form
    {
        public DataGridView Ventas;
        private Controlador.Generico Generico;

        public Ver()
        {
            InitializeComponent();
            Generico = new Controlador.Generico();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Generico.SalirCancelar(this, 2);
        }
    }
}
