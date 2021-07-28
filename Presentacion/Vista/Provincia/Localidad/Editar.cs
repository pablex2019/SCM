using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion.Vista.Provincia.Localidad
{
    public partial class Editar : Form
    {
        private int Id;
        public DataGridView Grilla;
        private Controlador.Localidad Localidad;
        private Controlador.Generico Generico;

        public Editar()
        {
            InitializeComponent();
        }
        private void Editar_Load(object sender, EventArgs e)
        {
            var _Localidad = Localidad.Obtener(Id);
            txtCodigoPostal.Text = _Localidad.CodigoPostal;
            txtNombre.Text = _Localidad.Nombre;
        }
        private void botonCancelar_Click(object sender, EventArgs e)
        {
            Generico.SalirCancelar(this, 1);
        }
    }
}
