using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentacion.Modelo
{
    public class Auto
    {
        public int Id { get; set; }
        public string Matricula { get; set; }
        public float Precio { get; set; }
        public Marca Marca { get; set; }
        public Modelo Modelo { get; set; }
        public Color Color { get; set; }
        public string Estado { get; set; }
    }
}
