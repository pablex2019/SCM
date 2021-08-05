using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentacion.Modelo
{
    public class Ubicacion
    {
        public int Id { get; set; }
        public string Calle { get; set; }
        public string Numero { get; set; }
        public string Zona { get; set; }
        public string Sector { get; set; }
        public string Barrio { get; set; }
        public string Manzana { get; set; }
        public string Casa { get; set; }
        public string Departamento { get; set; }
        public string Piso { get; set; }
        public string Referencia { get; set; }
        public Localidad Localidad { get; set; }
    }
}
