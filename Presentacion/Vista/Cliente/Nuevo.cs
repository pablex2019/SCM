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
    public partial class Nuevo : Form
    {
        public DataGridView Clientes;
        private Controlador.Generico Generico;
        private Controlador.Cliente Cliente;

        public Nuevo()
        {
            InitializeComponent();
            Cliente = new Controlador.Cliente("Clientes");
            Generico = new Controlador.Generico();
        }

        private void Nuevo_Load(object sender, EventArgs e)
        {
            txtNumero.Text = Clientes.Rows.Count == 0 ? Cliente.ObtenerNumeroUltimoClienteRegistrado().ToString() : "1";
            cboEstadoCivil.DataSource = Generico.EstadoCivil();
            txtEdad.Enabled = false;
            txtNumero.Enabled = false;
            txtEstado.Enabled = false;
            txtEstado.Text = "Disponible";
        }

        private void dtpFechaNacimiento_ValueChanged(object sender, EventArgs e)
        {
            var AnioSeleccionado = dtpFechaNacimiento.Value.Year;
            var AnioActual = DateTime.Now.Year;
            var Diferencia = AnioActual - AnioSeleccionado;
            txtEdad.Text = Diferencia.ToString();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Cliente.ABM(1, this, Clientes, 0);
        }
    }
}
