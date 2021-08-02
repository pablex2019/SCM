using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion.Vista.Pedido
{
    public partial class Indice : Form
    {
        public Indice()
        {
            InitializeComponent();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Vista.Pedido.Nuevo nuevo = new Nuevo();
            nuevo.Pedidos = dataGridView1;
            nuevo.Show();
        }
    }
}
