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
    public partial class FormMain : Form
    {
        //Creo una instancia de la capa de negocios
        private CN_Libro _librologic;
        public FormMain()
        {
            InitializeComponent();
           _librologic = new CN_Libro();
            CargarLibro();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            FormLibro formlibro = new FormLibro();
            formlibro.ShowDialog(this);
        }
        private void FormMain_Load(object sender, EventArgs e)
        {
            CargarLibro();
        }

        //Carga Libros de la base de datos en la grilla
        public void CargarLibro(String txtBuscar = null)
        {
            List<Libro> listalibros = _librologic.GetLibro(txtBuscar);
            dataGridView1.DataSource = listalibros;
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            //cell va a ser nulo si no hace click en editar , o sea e la columna de editar. El dataGridviewLinkcell es alguna de las celdas de las columnas de editar o eliminar
            DataGridViewLinkCell cell = (DataGridViewLinkCell)dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
            //Veo que link apreto para saber que hacer
            if (cell.Value.ToString() == "Editar")
            {
                //llamo al formulario de Libro para poder editar
                FormLibro formlibro = new FormLibro();
                //metodo que carga el libro seleccionado
                formlibro.LoadLibro(new Libro
                {
                    Idlibro = int.Parse((dataGridView1.Rows[e.RowIndex].Cells[0]).Value.ToString()),
                    Titulo = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString(),
                    Edicion = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString(),
                    Autor = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString(),
                    ISBN = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString(),
                    Paginas = int.Parse(dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString()),
                    Editorial = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString(),
                    Ciudadypais = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString(),
                    Fechadeedicion = DateTime.Parse(dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString()),
                });
                //Muestra el formulario de edicion
                formlibro.ShowDialog(this);
            }
            //si selecciona eliminar
            else if (cell.Value.ToString() == "Eliminar")
            {
                BorrarLibro(int.Parse((dataGridView1.Rows[e.RowIndex].Cells[0]).Value.ToString()));
                CargarLibro();
            }
        }

        private void BorrarLibro(int Idlibro)
        {
            _librologic.BorrarLibro(Idlibro);
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarLibro(txtBuscar.Text);
            txtBuscar.Text = string.Empty;
        }
    }
}
