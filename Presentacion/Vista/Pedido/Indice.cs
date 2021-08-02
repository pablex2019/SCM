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
        private Controlador.Pedido Pedido;

        public Indice()
        {
            InitializeComponent();
            Pedido = new Controlador.Pedido("Pedidos");
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Vista.Pedido.Nuevo nuevo = new Nuevo();
            nuevo.Pedidos = dataGridView1;
            nuevo.Show();
        }

        private void Indice_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = Pedido.Listado();
        }
    }
}
