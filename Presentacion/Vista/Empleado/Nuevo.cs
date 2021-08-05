using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion.Vista.Empleado
{
    public partial class Nuevo : Form
    {
        private int IdPerfil;
        public DataGridView Empleados;
        private Controlador.Perfil Perfil;
        private Controlador.Generico Generico;
        private Controlador.Empleado Empleado;
        private Controlador.Usuario Usuario;

        public Nuevo()
        {
            InitializeComponent();
            Generico = new Controlador.Generico();
            Empleado = new Controlador.Empleado("Empleados");
            Perfil = new Controlador.Perfil("Perfiles");
            Usuario = new Controlador.Usuario("Usuarios");
        }

        private void Nuevo_Load(object sender, EventArgs e)
        {
            txtLegajo.Text = Empleados.Rows.Count == 0 ? Empleado.ObtenerLegajoUltimoEmpleadoRegistrado().ToString() : "1";
            cboEstadoCivil.DataSource = Generico.EstadoCivil();
            txtEdad.Enabled = false;
            txtLegajo.Enabled = false;
            txtEstado.Enabled = false;
            txtEstado.Text = "Disponible";
            dgvPerfiles.DataSource = Perfil.Listado();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Generico.SalirCancelar(this,2);
        }
        private void dgvPerfiles_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            IdPerfil = Convert.ToInt32(dgvPerfiles.Rows[e.RowIndex].Cells[0].Value.ToString());
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if(Empleado.ABM(1, this, Empleados, dgvPerfiles, 0) == true)
            {
                Usuario.ABM(1, this, Empleados, dgvPerfiles, 0, IdPerfil);
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Vista.Empleado.Perfil.Nuevo nuevo = new Perfil.Nuevo();
            nuevo.Perfiles = dgvPerfiles;
            nuevo.Show();
        }

        private void dtpFechaNacimiento_ValueChanged(object sender, EventArgs e)
        {
            var AnioSeleccionado = dtpFechaNacimiento.Value.Year;
            var AnioActual = DateTime.Now.Year;
            var Diferencia = AnioActual - AnioSeleccionado;
            txtEdad.Text = Diferencia.ToString();
        }
    }
}
