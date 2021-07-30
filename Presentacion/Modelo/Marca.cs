using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentacion.Modelo
{
    public class Marca
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public Modelo Modelo { get; set; }
        public bool Estado { get; set; }
    }
}
