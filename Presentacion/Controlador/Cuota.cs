using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentacion.Controlador
{
    public class Cuota
    {
        #region Archivo
        private string Archivo { get; set; }
        private Datos.GestorArchivosTexto GestorArchivosTexto { get; set; }
        private List<Presentacion.Modelo.Cuota> Cuotas { get; set; }
        private string DatosCuotas { get; set; }
        #endregion
        private Controlador.Generico Generico;

        public Cuota(string _Archivo)
        {
            this.Archivo = _Archivo;
            this.GestorArchivosTexto = new Datos.GestorArchivosTexto(this.Archivo);
            this.Generico = new Generico();
        }
        private void Leer()
        {
            this.DatosCuotas = this.GestorArchivosTexto.Leer();
            this.Cuotas = this.DatosCuotas?.Length > 0 ? JsonConvert.DeserializeObject<List<Presentacion.Modelo.Cuota>>(this.DatosCuotas) : new List<Presentacion.Modelo.Cuota>();
        }
        public void Guardar()
        {
            //Convierto todos los datos a string
            this.DatosCuotas = JsonConvert.SerializeObject(this.Cuotas);
            this.GestorArchivosTexto.Guardar(this.DatosCuotas);
        }
        public List<Presentacion.Modelo.Cuota> Listado()
        {
            Leer();
            return this.Cuotas.ToList();
        }
        public Presentacion.Modelo.Cuota Obtener(int id)
        {
            Leer();
            return this.Cuotas.Where(x => x.Id == id).FirstOrDefault();
        }
        public bool Existe(Presentacion.Modelo.Cuota cuota)
        {
            return this.Cuotas.Any(x=>x.Numero == cuota.Numero);
        }
        public int ObtenerUltimoID()
        {
            return Cuotas.Max(x => x.Id) + 1;
        }
        public int ObtenerNumeroUltimaCuotaRegistrada()
        {
            return Cuotas.Max(x => x.Id) + 1;
        }
        public void ABM(int Operacion, Presentacion.Modelo.Cuota cuota)
        {
            Leer();
            Presentacion.Modelo.Cuota _cuota = new Presentacion.Modelo.Cuota();
            switch (Operacion)
            {
                 case 1://Alta
                    if (Existe(cuota) != true)
                    {
                        _cuota.Id = cuota.Id;
                        _cuota.Numero = cuota.Numero;
                        _cuota.Importe = cuota.Importe;
                        _cuota.Estado = cuota.Estado; 
                        this.Cuotas.Add(cuota);
                        Guardar();
                    }
                    break;
                }
            }
        }
}
