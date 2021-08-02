using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentacion.Modelo
{
    public class Pedido
    {
        public int Id { get; set; }
        public int Numero { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public Cliente Cliente { get; set; }
        public Empleado Empleado { get; set; }
        public Auto Auto { get; set; }
        public List<Anticipo> Anticipos { get; set; }
        public bool Estado { get; set; }
    }
}
