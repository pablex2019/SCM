using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion.Vista.Cliente
{
    public partial class Indice : Form
    {
        private Controlador.Cliente Cliente;

        public Indice()
        {
            InitializeComponent();
            Cliente = new Controlador.Cliente("Clientes");
        }

        private void Indice_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = Cliente.Listado();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Vista.Cliente.Nuevo nuevo = new Nuevo();
            nuevo.Clientes = dataGridView1;
            nuevo.Show();
        }
    }
}
