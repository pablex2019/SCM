using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion.Vista
{
    public partial class IniciarSesion : Form
    {
        private Controlador.Generico Generico;

        public IniciarSesion()
        {
            InitializeComponent();
            Generico = new Controlador.Generico();
        }

        private void botonSalir_Click(object sender, EventArgs e)
        {
            Generico.SalirCancelar(null,1);
        }

        private void botonEntrar_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Vista.Panel().Show();
        }
    }
}
