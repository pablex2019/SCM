using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion.Controlador
{
    public class Usuario
    {
        #region Archivo
        private string Archivo { get; set; }
        private Datos.GestorArchivosTexto GestorArchivosTexto { get; set; }
        private List<Presentacion.Modelo.Usuario> Usuarios { get; set; }
        private string DatosUsuarios { get; set; }
        #endregion
        private Controlador.Generico Generico;
        private Controlador.Perfil Perfil;
        private Controlador.Empleado Empleado;

        public Usuario(string _Archivo)
        {
            this.Archivo = _Archivo;
            this.GestorArchivosTexto = new Datos.GestorArchivosTexto(this.Archivo);
            this.Generico = new Generico();
            this.Perfil = new Perfil("Perfiles");
            this.Empleado = new Empleado("Empleados");
        }
        private void Leer()
        {
            this.DatosUsuarios = this.GestorArchivosTexto.Leer();
            this.Usuarios = this.DatosUsuarios?.Length > 0 ? JsonConvert.DeserializeObject<List<Presentacion.Modelo.Usuario>>(this.DatosUsuarios) : new List<Presentacion.Modelo.Usuario>();
        }
        private void Guardar()
        {
            //Convierto todos los datos a string
            this.DatosUsuarios = JsonConvert.SerializeObject(this.Usuarios);
            this.GestorArchivosTexto.Guardar(this.DatosUsuarios);
        }
        public List<Presentacion.Modelo.Usuario> Listado()
        {
            Leer();
            return this.Usuarios.ToList();
        }
        public Presentacion.Modelo.Usuario Obtener(int id)
        {
            Leer();
            return this.Usuarios.Where(x => x.Id == id).FirstOrDefault();
        }
        public bool Existe(Vista.Empleado.Nuevo Nuevo)
        {
            return this.Usuarios.Any(x => x.Nombre == Nuevo.txtUsuario.Text);
        }
        public int ObtenerUltimoID()
        {
            return this.Usuarios.Max(x => x.Id) + 1;
        }
        public void ABM(int Operacion, Vista.Empleado.Nuevo nuevo, DataGridView GrillaEmpleados,DataGridView GrillaPerfiles, int Id,int IdPerifl)
        {
            Leer();
            Presentacion.Modelo.Usuario usuario = new Presentacion.Modelo.Usuario();
            switch (Operacion)
            {
                 case 1://Alta
                    if (Existe(nuevo) != true)
                    {
                            usuario.Id = Usuarios.Count == 0 ? 1 : ObtenerUltimoID();
                            usuario.Nombre = nuevo.txtUsuario.Text;
                            usuario.Clave = nuevo.txtClave.Text;
                            usuario.Perfil = Perfil.Obtener(IdPerifl);
                            usuario.Empleado = Empleado.ObtenerUltimoEmpleadoRegistrado();
                            this.Usuarios.Add(usuario);
                            Guardar();
                            Generico.LimpiarCampos(nuevo);
                            Generico.FechaActual(nuevo);
                            Generico.ComboBoxEnSeleccione(nuevo);
                            Generico.ElementoAgregado("Empleado");
                            GrillaEmpleados.DataSource = Listado();
                            nuevo.txtEstado.Text = "Disponible";
                            
                    }
                break;
            }
        }
    }
}
