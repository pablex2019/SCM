using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentacion.Modelo
{
    public class Cuota
    {
        public int Id { get; set; }
        public int Numero { get; set; }
        public float Importe { get; set; }
        public Venta Financiacion { get; set; }
        public bool Estado { get; set; }
    }
}
