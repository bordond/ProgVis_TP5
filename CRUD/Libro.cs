using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD
{
    public class Libro
    {
        public int Idlibro { get; set; }
        public string Titulo { get; set; }
        public string Edicion { get; set; }
        public string Autor { get; set; }
        public string ISBN { get; set; }
        public int Paginas { get; set; }
        public string Editorial { get; set; }
        public string Ciudadypais { get; set; }
        public DateTime Fechadeedicion { get; set; }

        //public Libro(string titulo, string edicion, string autor, string isbn, int paginas, string editorial, string ciudadypais, DateTime fechadeedicion)
        //{
        //    titulo = Titulo;
        //    edicion = Edicion;
        //    autor = Autor;
        //    isbn = ISBN;
        //    paginas = Paginas;
        //    editorial = Editorial;
        //    ciudadypais = Ciudadypais;
        //    fechadeedicion = Fechadeedicion;
        //}
    }
}
