using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentacion.Modelo
{
    public class LineaDeVentas
    {
        public int Id { get; set; }
        public int Numero { get; set; }
        public int Cantidad { get; set; }
        public DateTime FechaInicioYFin { get; set; }
        public int Estado { get; set; }
    }
}
