using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD
{
    class CN_Libro
    {//declaro una variableclase de acceso a datos
        private CD_Libro _cdlibro;
        //Esta Clase es la capa de negocios. Aqui estan las validaciones
        //Este metodo guarda los libros

        //en el constructos Creo una instancia de la clase de acceso a datos 
        public CN_Libro()
        {
            //Creo una instancia del ojeto de acceso a datos en el constructor
            _cdlibro = new CD_Libro();

        }

        public Libro GuardarLibro(Libro libro)
        {
            //Es cero cuando graba un libro nuevo
            if (libro.Idlibro == 0)
                _cdlibro.InsertLibro(libro);
            else
            {
                _cdlibro.UpdateLibro(libro);
            }
            return libro;
        }
        //Devueve en una lista los libros de la base de datos
        public List<Libro> GetLibro(string txtBuscar = null)

        {
            return _cdlibro.GetLibro(txtBuscar);
        }

        public void BorrarLibro(int idlibro)
        {
            _cdlibro.BorrarLibro(idlibro);
        }
    }
}
