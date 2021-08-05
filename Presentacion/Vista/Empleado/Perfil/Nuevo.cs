using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion.Vista.Empleado.Perfil
{
    public partial class Nuevo : Form
    {
        public DataGridView Perfiles;
        private Controlador.Perfil Perfil;
        private Controlador.Generico Generico;

        public Nuevo()
        {
            InitializeComponent();
            Generico = new Controlador.Generico();
            Perfil = new Controlador.Perfil("Perfiles");
        }

        private void Nuevo_Load(object sender, EventArgs e)
        {
            cboEstado.DataSource = Generico.EstadosGenericos();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Generico.SalirCancelar(this, 2);
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Perfil.ABM(1, this, Perfiles, 0);
        }
    }
}
