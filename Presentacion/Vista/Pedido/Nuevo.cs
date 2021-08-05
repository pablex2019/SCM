using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion.Vista.Pedido
{
    public partial class Nuevo : Form
    {
        public int IdCliente, IdAuto, IdEmpleado;
        public DataGridView Pedidos;
        private Controlador.Pedido Pedido;
        private Controlador.Generico Generico;
        private Controlador.Cliente Cliente;
        private Controlador.Empleado Empleado;
        private Controlador.Auto Auto;
        private Controlador.Anticipo Anticipo;

        public Nuevo()
        {
            InitializeComponent();
            Empleado = new Controlador.Empleado("Empleados");
            Cliente = new Controlador.Cliente("Clientes");
            Auto = new Controlador.Auto("Autos");
            Pedido = new Controlador.Pedido("Pedidos");
            Anticipo = new Controlador.Anticipo("Anticipos");
            Generico = new Controlador.Generico();
        }

        private void Nuevo_Load(object sender, EventArgs e)
        {
            txtNumero.Text = Pedido.ObtenerNumeroUltimoPedidoRegistrado().ToString();
            dgvEmpleados.DataSource = Empleado.Listado();
            dgvClientes.DataSource = Cliente.Listado();
            dgvAutos.DataSource = Auto.Listado();
            dgvAnticipos.DataSource = Anticipo.Listado();
        }
        private void dgvClientes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            IdCliente = Convert.ToInt32(dgvClientes.Rows[e.RowIndex].Cells[0].Value.ToString());
        }

        private void dgvEmpleados_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            IdEmpleado = Convert.ToInt32(dgvEmpleados.Rows[e.RowIndex].Cells[0].Value.ToString());
        }

        private void dgvAutos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            IdAuto = Convert.ToInt32(dgvAutos.Rows[e.RowIndex].Cells[0].Value.ToString());
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Generico.SalirCancelar(this, 2);
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Pedido.ABM(1,this,0,IdAuto,IdCliente,IdEmpleado, dgvAutos,dgvClientes,dgvEmpleados, Pedidos,dgvAnticipos);
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Vista.Pedido.Anticipo.Nuevo nuevo = new Anticipo.Nuevo();
            nuevo.Anticipos = dgvAnticipos;
            nuevo.Show();
        }

        
    }
}
