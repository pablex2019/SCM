using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion.Controlador
{
    public class Pedido
    {
        #region Archivo
        private string Archivo { get; set; }
        private Datos.GestorArchivosTexto GestorArchivosTexto { get; set; }
        private List<Presentacion.Modelo.Pedido> Pedidos { get; set; }
        private string DatosPedidos { get; set; }
        #endregion
        private Controlador.Generico Generico;
        private Controlador.Cliente Cliente;
        private Controlador.Empleado Empleado;
        private Controlador.Auto Auto;

        public Pedido(string _Archivo)
        {
            this.Archivo = _Archivo;
            this.GestorArchivosTexto = new Datos.GestorArchivosTexto(this.Archivo);
            this.Generico = new Generico();
            Cliente = new Cliente("Clientes");
            Empleado = new Empleado("Empleados");
            Auto = new Auto("Autos");
        }
        private void Leer()
        {
            this.DatosPedidos = this.GestorArchivosTexto.Leer();
            this.Pedidos = this.DatosPedidos?.Length > 0 ? JsonConvert.DeserializeObject<List<Presentacion.Modelo.Pedido>>(this.DatosPedidos) : new List<Presentacion.Modelo.Pedido>();
        }
        private void Guardar()
        {
            //Convierto todos los datos a string
            this.DatosPedidos = JsonConvert.SerializeObject(this.Pedidos);
            this.GestorArchivosTexto.Guardar(this.DatosPedidos);
        }
        public List<Presentacion.Modelo.Pedido> Listado()
        {
            Leer();
            return this.Pedidos.ToList();
        }
        public Presentacion.Modelo.Pedido Obtener(int id)
        {
            Leer();
            return this.Pedidos.Where(x => x.Id == id).FirstOrDefault();
        }
        public bool Existe(Vista.Pedido.Nuevo Nuevo)
        {
            return this.Pedidos.Any(x => x.Numero == Convert.ToInt32(Nuevo.txtNumero.Text));
        }
        public int ObtenerUltimoID()
        {
            return Pedidos.Max(x => x.Id) + 1;
        }
        public void ABM(int Operacion, Vista.Pedido.Nuevo Nuevo,int Id, int IdAuto, int IdCliente, int IdEmpleado, DataGridView GrillaAutos,DataGridView GrillaClientes, DataGridView GrillaEmpleados, DataGridView GrillaPedidos,DataGridView GrillaAnticipos)
        {
            Leer();
            Presentacion.Modelo.Pedido pedido = new Presentacion.Modelo.Pedido();
            Presentacion.Modelo.Anticipo anticipo = new Presentacion.Modelo.Anticipo();
            pedido.Anticipos = new List<Presentacion.Modelo.Anticipo>();
            switch (Operacion)
            {
                case 1://Alta
                    if (GrillaAnticipos.Rows.Count == 0)
                    {
                        MessageBox.Show("No hay anticipo registrado", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        if (GrillaEmpleados.Rows.Count == 0)
                        {
                            MessageBox.Show("No hay empleados registrados", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            if (GrillaClientes.Rows.Count == 0)
                            {
                                MessageBox.Show("No hay clientes registrados", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {
                                if (GrillaAutos.Rows.Count == 0)
                                {
                                    MessageBox.Show("No hay autos registrados", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                else
                                {
                                    if (Existe(Nuevo) != true)
                                    {
                                        pedido.Id = Pedidos.Count > 0 ? ObtenerUltimoID() : 1;
                                        pedido.Numero = Convert.ToInt32(Nuevo.txtNumero.Text);
                                        pedido.FechaSolicitud = Nuevo.dtpFechaSolicitud.Value;
                                        pedido.Cliente = Cliente.Obtener(IdCliente);
                                        pedido.Empleado = Empleado.Obtener(IdEmpleado);
                                        pedido.Auto = Auto.Obtener(IdAuto);
                                        foreach(DataGridViewRow i in GrillaAnticipos.Rows)
                                        {
                                            anticipo.Id = Convert.ToInt32(i.Cells[0].Value);
                                            anticipo.Numero = Convert.ToInt32(i.Cells[1].Value);
                                            anticipo.Importe = Convert.ToDouble(i.Cells[2].Value);
                                            anticipo.Estado = Convert.ToBoolean(i.Cells[3].Value);
                                            pedido.Anticipos.Add(anticipo);
                                        } 
                                        this.Pedidos.Add(pedido);
                                        Guardar();
                                        Generico.LimpiarCampos(Nuevo);
                                        Generico.ElementoAgregado("Pedido");
                                        GrillaPedidos.DataSource = Listado();
                                    }
                                }
                            }
                        }
                    }
                    break;
            }
        }
    }
}
