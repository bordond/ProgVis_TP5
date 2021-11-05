using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRUD
{
    public partial class FormLibro : Form
    {
        //declaro una variable gloal de la clase CN_Logic puedo acceder desde cualquier clase
        private CN_Libro _librologic;
        //Creo una nuwva instancia de cocntacto para obtener el id. con esta instancia voy a
        //saber si esta editando o grabando un nuevo libro
        private Libro _libro;
        public FormLibro()
        {
            InitializeComponent();
            _librologic = new CN_Libro();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            //Ingreso primero al metodo validar, para verificar los datos de entrada al form.
            if (Validar())
            {
            //guarda el libro
            SaveLibro();
            //cierra la coneccion
            this.Close();
            //actualiza la carga de contactos en la grilla en el formulario main. Llamando al formulario padre para que se actualice
            ((FormMain)this.Owner).CargarLibro();
            }
            else
            {
                MessageBox.Show("Por favor, ingrese los datos nuevamente");
            }
        }
        private bool Validar()
        {
            bool datovalido = true;
            if (txtTitulo.Text.Trim() == "")
            {
                MessageBox.Show("Debe ingresar el Titulo del libro.");
                datovalido = false;
                txtTitulo.Focus();
            }
            if (txtEdicion.Text.Trim() == "")
            {
                MessageBox.Show("Debe ingresar la Edicion del libro.");
                datovalido = false;
                txtEdicion.Focus();
            }
            if (txtAutor.Text.Trim() == "")
            {
                MessageBox.Show("Debe ingresar el Autor del Libro.");
                datovalido = false;
                txtAutor.Focus();
            }
            if (txtIsbn.Text.Trim() == "")
            {
                MessageBox.Show("Debe ingresar el numero ISBN del Libro.");
                datovalido = false;
                txtIsbn.Focus();
            }
            try
            {
                Int32.Parse(txtPaginas.Text.Trim());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Las paginas del Libro deben ser un valor numerico entero.");
                datovalido = false;
            }
            if (txtEditorial.Text.Trim() == "")
            {
                MessageBox.Show("Debe ingresar la Editorial del Libro.");
                datovalido = false;
                txtEditorial.Focus();
            }
            if (txtCiudadypais.Text.Trim() == "")
            {
                MessageBox.Show("Debe ingresar Ciudad - Pais del Libro.");
                datovalido = false;
                txtCiudadypais.Focus();
            }
            try
            {
                //Los MessageBox comentados debajo eran para ver que calculo hacia
                //la diferencia de fechas
                int y;
                DateTime datenow = DateTime.Now;
                TimeSpan timeSpan = datenow - DateTime.Parse(txtFechadeedicion.Text);
                //MessageBox.Show("La diferencias de fechas da: " + timeSpan);
                int x = int.Parse(timeSpan.Days.ToString());
                //MessageBox.Show("La diferencias de fechas en string es: " + x);
                y = x;
                if (y <= 0)//18 años x 365 dias = 6570
                {
                    MessageBox.Show("La fecha de Edicion del libro debe ser menor a la fecha actual.");
                    datovalido = false;
                    txtFechadeedicion.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("La fecha de Edicion del libro debe ser menor a la fecha actual.");
                datovalido = false;
            }
            return datovalido;
        }
        //guarda el libro
        private void SaveLibro()
        {
            Libro libro = new Libro();
            libro.Titulo = txtTitulo.Text;
            libro.Edicion = txtEdicion.Text;
            libro.Autor = txtAutor.Text;
            libro.ISBN = txtIsbn.Text;
            libro.Paginas = Convert.ToInt32(txtPaginas.Text);
            libro.Editorial = txtEditorial.Text;
            libro.Ciudadypais = txtCiudadypais.Text;
            libro.Fechadeedicion = Convert.ToDateTime(txtFechadeedicion.Text);
            //Si el Id es distinto de null este metdo se ejecuto y id tiene algo , y si no
            //ponemos 0. Si entro por edit libro idLibro tiene algo y si no esta vacio
            // libro.IdLibro = _libro != null ? _libro.IdLibro : 0;

            //Si la instancia _libro es distinto de null significa que entro por el edit
            //entonces uso el IdLibro , y si no entro por guardar y guardo 0
            //ya que usa el mismo boton de guardar necesito diferenciar si esta editando o
            //grabando uno nuevo por eso usa _libro
            //? para verdadero y : para falso.
            libro.Idlibro = _libro != null ? libro.Idlibro : 0;
            _librologic.GuardarLibro(libro);
        }
        //se ejecuta cuando selecciona editar en la grilla
        public void LoadLibro(Libro libro)
        {//aqui en esta variabl global copio la instancia e libro para guardarme el ID del
         //libro
            _libro = libro;
            if (libro != null)
            {
                ClearForm();
                txtTitulo.Text = libro.Titulo;
                txtEdicion.Text = libro.Edicion;
                txtAutor.Text = libro.Autor;
                txtIsbn.Text = libro.ISBN;
                txtPaginas.Text = Convert.ToString(libro.Paginas);
                txtEditorial.Text = libro.Editorial;
                txtCiudadypais.Text = libro.Ciudadypais;
                txtFechadeedicion.Text = Convert.ToString(libro.Fechadeedicion);
            }
        }
        private void ClearForm()
        {
            txtTitulo.Text = string.Empty;
            txtEdicion.Text = string.Empty;
            txtAutor.Text = string.Empty;
            txtIsbn.Text = string.Empty;
            txtPaginas.Text = string.Empty;
            txtEditorial.Text = string.Empty;
            txtCiudadypais.Text = string.Empty;
            txtFechadeedicion.Text = string.Empty;
        }
    }
}