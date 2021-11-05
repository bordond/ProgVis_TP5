using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace CRUD
{
    
    //Esta clase es la responsable de acceder a la base de datos y ejecutar la consulta
    class CD_Libro
    {
        //creo el string de coneccion
        SqlConnection cn = new SqlConnection("Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=Biblioteca;Data Source=LAPTOP-GNGB3R1F\\BORDOND");
        public void InsertLibro(Libro libro)
        {
            try
            {
                //abro la coneccion
                cn.Open();

                //creo el sql para insertar
                string query = @"
                                 INSERT INTO Libro (Titulo, Edicion, Autor, ISBN,Paginas,Editorial,Ciudadypais,Fechadeedicion) VALUES (@Titulo,@Edicion,@Autor,@ISBN,@Paginas,@Editorial,@Ciudadypais,@Fechadeedicion)";
                //creo los parametrs

                SqlParameter Titulo = new SqlParameter("@Titulo", libro.Titulo);
                SqlParameter Edicion = new SqlParameter("@Edicion", libro.Edicion);
                SqlParameter Autor = new SqlParameter("@Autor", libro.Autor);
                SqlParameter ISBN = new SqlParameter("@ISBN", libro.ISBN);
                SqlParameter Paginas = new SqlParameter("@Paginas", libro.Paginas);
                SqlParameter Editorial = new SqlParameter("@Editorial", libro.Editorial);
                SqlParameter Ciudadypais = new SqlParameter("@Ciudadypais", libro.Ciudadypais);
                SqlParameter Fechadeedicion = new SqlParameter("@Fechadeedicion", libro.Fechadeedicion);
                //creo el comando mandando os parametro
                SqlCommand comando = new SqlCommand(query, cn);
                comando.Parameters.Add(Titulo);
                comando.Parameters.Add(Edicion);
                comando.Parameters.Add(Autor);
                comando.Parameters.Add(ISBN);
                comando.Parameters.Add(Paginas);
                comando.Parameters.Add(Editorial);
                comando.Parameters.Add(Ciudadypais);
                comando.Parameters.Add(Fechadeedicion);
                //Este comando devuelve cantidad de filas afectadas y cero si no
                comando.ExecuteNonQuery();

            }
            catch (Exception)
            {

                throw;
            }
            //cierra la coneccion
            finally
            {
                cn.Close();
            }
        }

        public List<Libro> GetLibro(string search = null)
        {
            //Lista para guardar los Libros
            List<Libro> listalibros = new List<Libro>();
            try
            {
                cn.Open();
                string query = @"SELECT Idlibro,Titulo,Edicion,Autor,ISBN,Paginas,Editorial,Ciudadypais,Fechadeedicion
                                   FROM Libro";
                SqlCommand comando = new SqlCommand();
                if (!string.IsNullOrEmpty(search))
                {
                    //Agrega esto al query para que busque 
                    query += @" WHERE Titulo LIKE @Search OR Edicion LIKE @Search OR Autor LIKE @Search OR ISBN LIKE @Search Editorial LIKE @Search Ciudadypais LIKE @Search ";

                    //que lo buscado aparece en cualquier lugar de la columna
                    comando.Parameters.Add(new SqlParameter("@Search", $"%{search}%"));
                }

                comando.CommandText = query;
                comando.Connection = cn;

                //El data reader Trae todos los registros
                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    //Itero el reader y lo agrego a la lista
                    listalibros.Add(new Libro
                    {
                        Idlibro = int.Parse(reader["IdLibro"].ToString()),
                        Titulo = reader["Titulo"].ToString(),
                        Edicion = reader["Edicion"].ToString(),
                        Autor = reader["Autor"].ToString(),
                        ISBN = reader["ISBN"].ToString(),
                        Paginas = int.Parse(reader["Paginas"].ToString()),
                        Editorial = reader["Editorial"].ToString(),
                        Ciudadypais = reader["Ciudadypais"].ToString(),
                        Fechadeedicion = DateTime.Parse(reader["Fechadeedicion"].ToString()),
                    });
                }
            }
            catch (Exception e)
            {

                throw;
            }
            finally
            {
                cn.Close();
            }
            return listalibros;

        }

        public void UpdateLibro(Libro libro)
        {
            try
            {
                //abro la coneccion
                cn.Open();

                //creo el sql para insertar
                string query = @"
                                 UPDATE  Libro 
                       SET Titulo=@Titulo,Edicion=@Edicion,Autor=@Autor,ISBN=@ISBN,Paginas=@Paginas,Editorial=@Editorial,Ciudadypais=@Ciudadypais,Fechadeedicion=@Fechadeedicion
                          WHERE Idlibro=@Idlibro";
                //creo los parametrs

                SqlParameter Idlibro = new SqlParameter("@Idlibro", libro.Idlibro);
                SqlParameter Titulo = new SqlParameter("@Titulo", libro.Titulo);
                SqlParameter Edicion = new SqlParameter("@Edicion", libro.Edicion);
                SqlParameter Autor = new SqlParameter("@Autor", libro.Autor);
                SqlParameter ISBN = new SqlParameter("@ISBN", libro.ISBN);
                SqlParameter Paginas = new SqlParameter("@Paginas", libro.Paginas);
                SqlParameter Editorial = new SqlParameter("@Editorial", libro.Editorial);
                SqlParameter Ciudadypais = new SqlParameter("@Ciudadypais", libro.Ciudadypais);
                SqlParameter Fechadeedicion = new SqlParameter("@Fechadeedicion", libro.Fechadeedicion);

                //creo el comando mandando os parametro
                SqlCommand comando = new SqlCommand(query, cn);
                comando.Parameters.Add(Idlibro);
                comando.Parameters.Add(Titulo);
                comando.Parameters.Add(Edicion);
                comando.Parameters.Add(Autor);
                comando.Parameters.Add(ISBN);
                comando.Parameters.Add(Paginas);
                comando.Parameters.Add(Editorial);
                comando.Parameters.Add(Ciudadypais);
                comando.Parameters.Add(Fechadeedicion);

                //Este comando devuelve cantidad de filas afectadas y cero si no
                comando.ExecuteNonQuery();

            }
            catch (Exception)
            {

                //throw;
            }
            //cierra la coneccion
            finally
            {
                cn.Close();
            }
        }
        public void BorrarLibro(int Idlibro)

        {
            try
            {
                //abro la coneccion
                cn.Open();

                //creo el sql para insertar
                string query = @"
                                 DELETE FROM Libro WHERE Idlibro=@Idlibro";
                //creo los parametrs


                //creo el comando mandando el parametro
                SqlCommand comando = new SqlCommand(query, cn);
                //Solo necesito id para borrar
                comando.Parameters.Add(new SqlParameter("@Idlibro", Idlibro));

                //Este comando devuelve cantidad de filas afectadas y cero si no
                comando.ExecuteNonQuery();

            }
            catch (Exception e)
            {

                throw;
            }
            //cierra la coneccion
            finally
            {
                cn.Close();
            }

        }
    }
}
