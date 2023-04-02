using MySql.Data.MySqlClient;
using System.Data;

namespace Inmobiliaria2.Models
{
    public class InmuebleRepositorio
    {
        string connectionString = "Server=localhost;User=root;Password=;Database=inmobiliaria;SslMode=none";

        public IList<Inmueble> GetInmuebles()
        {
            IList<Inmueble> res = new List<Inmueble>();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string sql = @"SELECT 
                idInmueble, idPropietario,direccion, disponible
                FROM inmueble";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.CommandType = System.Data.CommandType.Text;
                    connection.Open();
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Inmueble inm = new Inmueble
                        {
                            IdInmueble = reader.GetInt32("idInmueble"),                      
                            Direccion = reader.GetString("direccion"),
                            Disponible = reader.GetBoolean("disponible"),
                            Propietario = new Propietario
                            {
                                Id = reader.GetInt32("idPropietario")
                            }

                        };
                        res.Add(inm);
                    }
                    connection.Close();
                }
            }
            return res;
        }

        public Inmueble GetInmueble(int id)
        {
            Inmueble res = new Inmueble();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string sql = @"SELECT * FROM Inmuebles i JOIN Propietarios p ON i.idPropietario = p.IdPropietario
                               WHERE i.idInmueble=@id";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("id", id);
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    var reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        res = new Inmueble
                        {
                            IdInmueble = reader.GetInt32("idInmueble"),
                            Direccion = reader.GetString("direccion"),
                            Disponible = reader.GetBoolean("disponible"),
                            Uso = reader.GetString("uso"),
                            Ambientes = reader.GetInt32("ambientes"),
                            Precio = reader.GetDouble("precio"),
                            Latitud = reader.GetString("latitud"),
                            Longitud = reader.GetString("longitud"),
                            Propietario = new Propietario
                            {
                                Id = reader.GetInt32("idPropietario"),
                                Dni = reader.GetString("dni"),
                                Nombre = reader.GetString("nombre"),
                                Apellido = reader.GetString("apellido"),
                                Domicilio = reader.GetString("domicilio"),
                                Telefono = reader.GetString("telefono"),
                                Email = reader.GetString("email")
                            }

                        };
                    }
                    connection.Close();
                }
            }
            return res;
        }

        public int Alta(Inmueble inmueble)
        {
            int con = 0;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string sql = @"INSERT INTO inmueble
        (idPropietario, direccion, tipo, uso, ambientes, precio,disponible,latitud,longitud)
        VALUES (@Propietario, @direccion, @tipo, @uso, @ambientes, @precio,@disponible,@latitud,@longitud)";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Propietario", inmueble.IdPropietario);
                    command.Parameters.AddWithValue("@direccion", inmueble.Direccion);
                    command.Parameters.AddWithValue("@tipo", inmueble.Tipo);
                    command.Parameters.AddWithValue("@uso", inmueble.Uso);
                    command.Parameters.AddWithValue("@ambientes", inmueble.Ambientes);
                    command.Parameters.AddWithValue("@precio", inmueble.Precio);
                    command.Parameters.AddWithValue("@disponible", inmueble.Disponible);
                    command.Parameters.AddWithValue("@latitud", inmueble.Latitud);
                    command.Parameters.AddWithValue("@longitud", inmueble.Longitud);
                    connection.Open();
                    con = Convert.ToInt32(command.ExecuteScalar());
           
                    connection.Close();

                }
            }
            return con;
        }




    }
}
