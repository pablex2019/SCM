using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentacion.Modelo
{
    public class Venta
    {
        public int Id { get; set; }
        public int Numero { get; set; }
        public float Propuesta { get; set; }
        public DateTime FechaRegistro { get; set; }
        public Pedido Pedido { get; set; }
        public Financiacion OpcionesFinanciacion { get; set; }
        public List<Cuota> Cuotas { get; set; }
        public bool Estado { get; set; }
    }
}
