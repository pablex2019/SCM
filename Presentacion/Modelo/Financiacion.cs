using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentacion.Modelo
{
    public class Financiacion
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public int CantidadCuotas { get; set; }
        public bool Estado { get; set; }
    }
}
