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
        public string Numero { get; set; }
        public float Propuesta { get; set; }
        public Pedido Pedido { get; set; }
        public DateTime FechaAlta { get; set; }
        public OpcionesFinanciacion OpcionesFinanciacion { get; set; }
        public LineaDeVentas LineaDeVentas { get; set; }
        public bool Estado { get; set; }
    }
}
