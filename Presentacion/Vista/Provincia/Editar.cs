using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion.Vista.Provincia
{
    public partial class Editar : Form
    {
        public int Id;
        private Controlador.Provincia Provincia;

        public Editar()
        {
            InitializeComponent();
            Provincia = new Controlador.Provincia("Provincias");
        }

        private void Editar_Load(object sender, EventArgs e)
        {
            var _Provincia = Provincia.Obtener(Id);
            txtNombre.Text = _Provincia.Nombre;
            
        }
    }
}
