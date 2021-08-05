using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentacion.Modelo
{
    public class Anticipo
    {
        public int Id { get; set; }
        public int Numero { get; set; }
        public double Importe { get; set; }
        public bool Estado { get; set; }
    }
}
