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
                string sql = @"SELECT * FROM inmueble i JOIN propietario p ON i.idPropietario = p.idPropietario WHERE i.idInmueble=@id;";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("id", id);
                    command.CommandType = System.Data.CommandType.Text;
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
                            Tipo = reader.GetString("tipo"),
                            Ambientes = reader.GetInt32("ambientes"),
                            Precio = reader.GetDouble("precio"),
                            Latitud = reader.GetString("latitud"),
                            Longitud = reader.GetString("longitud"),
                            Propietario = new Propietario
                            {
                                Id = reader.GetInt32("idPropietario"),
                                Nombre = reader.GetString("Nombre"),
                                Apellido = reader.GetString("Apellido"),
                                Dni = reader.GetString("Dni"),
                                Telefono = reader.GetString("Telefono"),
                                Email = reader.GetString("Email"),
                                Domicilio = reader.GetString("Domicilio")
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

        public int Modificar(Inmueble inmueble)
        {
            int con = 0;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string sql = @"UPDATE inmueble SET 
                              idPropietario=@Propietario, 
                                direccion=@direccion, 
                                tipo=@tipo, uso=@uso, 
                                ambientes=@ambientes, 
                                precio=@precio,
                                disponible=@disponible,
                                latitud=@latitud,
                                longitud=@longitud
                                WHERE idInmueble = @idInmueble";
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
                    command.Parameters.AddWithValue("@idInmueble", inmueble.IdInmueble);
                    connection.Open();
                    con = command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return con;
        }



    }
}
