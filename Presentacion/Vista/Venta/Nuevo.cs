using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion.Vista.Venta
{
    public partial class Nuevo : Form
    {
        public int IdPedido, IdOpcionFinanciacion, CantidadCuotas;
        public DataGridView Ventas;
        private Controlador.Generico Generico;
        private Controlador.Financiacion Financiacion;
        private Controlador.Pedido Pedido;
        private Controlador.Venta Venta;

        public Nuevo()
        {
            InitializeComponent();
            Financiacion = new Controlador.Financiacion("Financiaciones");
            Venta = new Controlador.Venta("Ventas");
            Pedido = new Controlador.Pedido("Pedidos");
            Generico = new Controlador.Generico();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Vista.Venta.Financiacion.Nuevo nuevo = new Financiacion.Nuevo();
            nuevo.Financiaciones = dgvFinanciaciones;
            nuevo.Show();
        }

        private void Nuevo_Load(object sender, EventArgs e)
        {
            txtNumero.Text = Venta.ObtenerNumeroUltimaVentaRegistrado().ToString();
            dgvPedidos.DataSource = Pedido.Listado();
            dgvFinanciaciones.DataSource = Financiacion.Listado();
        }

        private void dgvPedidos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            IdPedido = Convert.ToInt32(dgvPedidos.Rows[e.RowIndex].Cells[0].Value.ToString());
            txtCantidadAnticipos.Text = Pedido.ObtenerCantidadAnticipos(IdPedido).ToString();
            txtImporteTotal.Text = Pedido.ObtenerSumaTotalImporteAnticipos(IdPedido).ToString();
        }
        private void dgvFinanciaciones_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            IdOpcionFinanciacion = Convert.ToInt32(dgvFinanciaciones.Rows[e.RowIndex].Cells[0].Value.ToString());
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {

        }

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            /*Como generar el calculo*/
            var resultado = Venta.CalcularValorCuotas(IdOpcionFinanciacion,txtCantidadAnticipos.Text,txtImporteTotal.Text,txtPropuesta.Text,this,IdPedido,Ventas);
            dgvCuotas.DataSource = resultado;
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Generico.SalirCancelar(this, 2);
        }

        
    }
}
