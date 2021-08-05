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
        private int IdColor, IdMarca,IdModelo;
        public DataGridView Autos;
        private Controlador.Auto Auto;
        private Controlador.Generico Generico;
        private Controlador.Color Color;
        private Controlador.Marca Marca;
        private Controlador.Modelo Modelo;

        public Nuevo()
        {
            InitializeComponent();
            Generico = new Controlador.Generico();
            Auto = new Controlador.Auto("Autos");
            Color = new Controlador.Color("Colores");
            Marca = new Controlador.Marca("Marcas");
            Modelo = new Controlador.Modelo("Modelos");
        }
        private void Nuevo_Load(object sender, EventArgs e)
        {
            txtEstado.Text = "Disponible";
            txtEstado.Enabled = false;
            dgvColores.DataSource = Color.Listado();
            dgvMarcas.DataSource = Marca.Listado();
            dgvModelos.DataSource = Modelo.Listado();
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
        private void dgvModelos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            IdModelo = Convert.ToInt32(dgvModelos.Rows[e.RowIndex].Cells[0].Value.ToString());
        }
        private void btnNuevoColor_Click(object sender, EventArgs e)
        {
            Vista.Auto.Color.Nuevo nuevo = new Vista.Auto.Color.Nuevo();
            nuevo.Colores = dgvColores;
            nuevo.Show();
        }

        private void btnNuevoMarca_Click(object sender, EventArgs e)
        {
            Vista.Auto.Marca.Nuevo nuevo = new Vista.Auto.Marca.Nuevo();
            nuevo.Marcas = dgvMarcas;
            nuevo.Show();
        }

        private void btnNuevoModelo_Click(object sender, EventArgs e)
        {
            Vista.Auto.Modelo.Nuevo nuevo = new Vista.Auto.Modelo.Nuevo();
            nuevo.Modelos = dgvModelos;
            nuevo.Show();
        }
        private void botonGuardar_Click(object sender, EventArgs e)
        {
            Auto.ABM(1, this, null, 0,IdColor,IdMarca, IdModelo, dgvMarcas,dgvColores,Autos,dgvModelos);
        }
    }
}
