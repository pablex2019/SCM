using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion.Controlador
{
    public class Generico
    {
        public void SalirCancelar(Form Formulario,int opcion)
        {
            switch (opcion)
            {
                case 1:
                    DialogResult ResultSalir = MessageBox.Show("¿Esta seguro de salir?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    switch (ResultSalir)
                    {
                        case DialogResult.Yes:
                            Application.Exit();
                            break;
                    }
                    break;
                case 2:
                    DialogResult ResultCancelar = MessageBox.Show("¿Esta seguro de cancelar?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    switch (ResultCancelar)
                    {
                        case DialogResult.Yes:
                            Formulario.Close();
                            break;
                    }
                    break;
            }
            
        }
        public void LimpiarCampos(Control control)
        {
            foreach (var i in control.Controls)
            {
                if (i is TextBox)
                {
                    ((TextBox)i).Clear();
                }
            }
        }
        public void ComboBoxEnSeleccione(Control control) 
        {
            foreach (var i in control.Controls)
            {
                if (i is ComboBox)
                {
                    ((ComboBox)i).SelectedIndex=0;
                }
            }
        }
        public void FechaActual(Control control)
        {
            foreach (var i in control.Controls)
            {
                if (i is DateTimePicker)
                {
                    ((DateTimePicker)i).Value = DateTime.Now;
                }
            }
        }
        public List<string> EstadosGenericos()
        {
            List<string> lista = new List<string>();
            lista.Add("Seleccione");
            lista.Add("Habilitado");
            lista.Add("Inhabilitado");
            return lista;
        }
        public List<string> EstadoCivil()
        {
            List<string> lista = new List<string>();
            lista.Add("Seleccione");
            lista.Add("Soltero/a");
            lista.Add("Casaado/a");
            lista.Add("Divorciado/a");
            lista.Add("Viudo/a");
            return lista;
        }
        public List<string> EstadoAnticipoYCuotas()
        {
            List<string> lista = new List<string>();
            lista.Add("Seleccione");
            lista.Add("Pagado");
            lista.Add("Impago");
            return lista;
        }
        public void ElementoAgregado(string elemento)
        {
            MessageBox.Show(elemento + " Agregado/a","",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }
    }
}
