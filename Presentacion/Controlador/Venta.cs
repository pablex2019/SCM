using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion.Controlador
{
    public class Venta
    {
        #region Archivo
        private string Archivo { get; set; }
        private Datos.GestorArchivosTexto GestorArchivosTexto { get; set; }
        private List<Presentacion.Modelo.Venta> Ventas { get; set; }
        private string DatosVentas { get; set; }
        #endregion
        private Controlador.Generico Generico;
        private Controlador.Pedido Pedido;
        private Controlador.Cuota Cuota;
        private Controlador.Financiacion Financiacion;

        public Venta(string _Archivo)
        {
            this.Archivo = _Archivo;
            this.GestorArchivosTexto = new Datos.GestorArchivosTexto(this.Archivo);
            this.Generico = new Generico();
            Pedido = new Controlador.Pedido("Pedidos");
            Cuota = new Controlador.Cuota("Cuotas");
            Financiacion = new Controlador.Financiacion("Financiaciones");
        }
        private void Leer()
        {
            this.DatosVentas = this.GestorArchivosTexto.Leer();
            this.Ventas = this.DatosVentas?.Length > 0 ? JsonConvert.DeserializeObject<List<Presentacion.Modelo.Venta>>(this.DatosVentas) : new List<Presentacion.Modelo.Venta>();
        }
        private void Guardar()
        {
            //Convierto todos los datos a string
            this.DatosVentas = JsonConvert.SerializeObject(this.Ventas);
            this.GestorArchivosTexto.Guardar(this.DatosVentas);
        }
        public List<Presentacion.Modelo.Venta> Listado()
        {
            Leer();
            return this.Ventas.ToList();
        }
        public Presentacion.Modelo.Venta Obtener(int id)
        {
            Leer();
            return this.Ventas.Where(x => x.Id == id).FirstOrDefault();
        }
        public bool Existe(Vista.Venta.Nuevo Nuevo)
        {
            return this.Ventas.Any(x => x.Numero == Convert.ToInt32(Nuevo.txtNumero.Text));
        }
        public int ObtenerUltimoID()
        {
            return Ventas.Max(x => x.Id) + 1;
        }
        public int ObtenerNumeroUltimaVentaRegistrado()
        {
            Leer();
            return Ventas.Count == 0 ? 1 : Ventas.Max(x => x.Numero);
        }
        public List<Presentacion.Modelo.Cuota> CalcularValorCuotas(int IdOpcionFinanciacion, string CantidadAnticipos, string ImporteAnticipos, string Propuesta,Vista.Venta.Nuevo nuevo,int IdPedido,DataGridView GrillaVentas)
        {
            //List<Presentacion.Modelo.Cuota> lista = new List<Presentacion.Modelo.Cuota>();
            Presentacion.Modelo.Venta venta = new Presentacion.Modelo.Venta();
            venta.Cuotas = new List<Presentacion.Modelo.Cuota>();

            if (Convert.ToInt32(CantidadAnticipos) < 4)
            {
                MessageBox.Show("La cantidad de anticipos es menor a 4");
            }
            else
            {
                var diferencia = Convert.ToDouble(Propuesta) - Convert.ToDouble(ImporteAnticipos);
                var CantidadCuotasFinanciacion = Financiacion.ObtenerCantidadCuotasFinanciacionSeleccionada(IdOpcionFinanciacion);
                var ImporteCuota = diferencia / Convert.ToInt32(CantidadCuotasFinanciacion);
                Presentacion.Modelo.Cuota cuota = new Presentacion.Modelo.Cuota();
                int Id = 1;
                int NumeroCuota = 1;
                venta.Id = Ventas.Count > 0 ? ObtenerUltimoID() : 1;
                venta.Numero = Convert.ToInt32(nuevo.txtNumero.Text);
                venta.Propuesta = float.Parse(nuevo.txtPropuesta.Text);
                venta.FechaRegistro = nuevo.dtpFechRegistro.Value;
                venta.OpcionesFinanciacion = Financiacion.Obtener(IdOpcionFinanciacion);
                venta.Pedido = Pedido.Obtener(IdPedido);
                for (var i = 0; i < CantidadCuotasFinanciacion; i++)
                {
                    cuota.Id = Id;
                    cuota.Numero = NumeroCuota; 
                    cuota.Importe = ImporteCuota;
                    cuota.Estado = false;
                    Cuota.ABM(1,cuota);
                    Id++;
                    NumeroCuota++;
                    venta.Cuotas.Add(cuota);
                    //lista.Add(cuota);
                }
                this.Ventas.Add(venta);
                Guardar();
                GrillaVentas.DataSource = Listado();
                
            }
            return Cuota.Listado();
        }
        public void ABM()//(int Operacion, Vista.Venta.Nuevo Nuevo, int Id, int IdPedido, int IdOpciónFinanciacion, DataGridView GrillaVentas, DataGridView GrillaFinanciaciones, DataGridView GrillaCuotas, DataGridView GrillaPedidos)
        {
            Listado();
            /*
            Leer();
            Presentacion.Modelo.Venta venta = new Presentacion.Modelo.Venta();
            Presentacion.Modelo.Cuota cuota = new Presentacion.Modelo.Cuota();
            venta.Cuotas = new List<Presentacion.Modelo.Cuota>();
            if (GrillaPedidos.Rows.Count == 0)
            {
                MessageBox.Show("No hay pedidos registrados", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (GrillaFinanciaciones.Rows.Count == 0)
                {
                    MessageBox.Show("No hay opciones de financiacion registradas", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (GrillaCuotas.Rows.Count == 0)
                    {
                        MessageBox.Show("No hay cuotas calculadas", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        switch (Operacion)
                        {
                            case 1://Alta
                                
                                if (Existe(Nuevo) != true)
                                {
                                    
                                    foreach (DataGridViewRow i in GrillaCuotas.Rows)
                                    {
                                        cuota.Id = Convert.ToInt32(i.Cells[0].Value);
                                        cuota.Numero = Convert.ToInt32(i.Cells[1].Value);
                                        cuota.Importe = Convert.ToDouble(i.Cells[2].Value);
                                        //cuota.Venta = this.Obtener(venta.Id);
                                        venta.Cuotas.Add(cuota);
                                    }
                                    Guardar();
                                    Generico.LimpiarCampos(Nuevo);
                                    Generico.ElementoAgregado("Venta");
                                    GrillaPedidos.DataSource = Pedido.Listado();
                                    GrillaFinanciaciones.DataSource = Financiacion.Listado();
                                    GrillaVentas.DataSource = Listado();
                                }
                                break;
                        }
                    }
                }
                    
            }
            */
        }
    }
}
