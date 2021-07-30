using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion.Vista.Auto
{
    public partial class Nuevo : Form
    {
        private int IdColor, IdMarca;
        public DataGridView Autos;
        private Controlador.Auto Auto;
        private Controlador.Generico Generico;

        public Nuevo()
        {
            InitializeComponent();
            Generico = new Controlador.Generico();
            Auto = new Controlador.Auto("Autos");
        }

        private void botonCancelar_Click(object sender, EventArgs e)
        {
            Generico.SalirCancelar(this,2);
        }
        private void dgvColores_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            IdColor = Convert.ToInt32(dgvColores.Rows[e.RowIndex].Cells[0].Value.ToString());
        }
        private void dgvMarcas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            IdMarca = Convert.ToInt32(dgvMarcas.Rows[e.RowIndex].Cells[0].Value.ToString());
        }
        private void botonGuardar_Click(object sender, EventArgs e)
        {
            Auto.ABM(1, this, null, 0,IdColor,IdMarca, Autos);
        }

        private void Nuevo_Load(object sender, EventArgs e)
        {
            txtEstado.Text = "Disponible";
            txtEstado.Enabled = false;
        }

        
    }
}
