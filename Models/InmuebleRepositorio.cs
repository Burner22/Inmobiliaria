using MySql.Data.MySqlClient;
using System.Data;

namespace Inmobiliaria2.Models
{
    public class InmuebleRepositorio
    {
        string connectionString = "Server=localhost;User=root;Password=;Database=inmobiliaria;SslMode=none";

        public InmuebleRepositorio()
        {
        }

        public IList<Inmueble> GetInmuebles()
        {
            IList<Inmueble> res = new List<Inmueble>();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string sql = @"SELECT * FROM inmueble i JOIN propietario p ON i.idPropietario = p.idPropietario ";
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
                        res.Add(inm);
                    }
                    connection.Close();
                }
            }
            return res;
        }

        public IList<Inmueble> GetInmueblesDisponibles()
        {
            IList<Inmueble> res = new List<Inmueble>();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string sql = @"SELECT * FROM inmueble i JOIN propietario p ON i.idPropietario = p.idPropietario WHERE disponible=1 ";
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

        public int Baja(Inmueble inmueble)
        {
            int res = -1;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string sql = @"UPDATE inmueble set borrado=1 WHERE idInmueble = @idInmueble";
                using(MySqlCommand command = connection.CreateCommand())
                {
                    command.Parameters.AddWithValue("@idInmueble", inmueble.IdInmueble);
                    connection.Open() ;
                    res = command.ExecuteNonQuery() ;
                    connection.Close();
                }
            }

            return res;
        }

        public IList<Inmueble> BuscarPorPropietario(int idPropietario)
        {
            IList<Inmueble> res = new List<Inmueble>();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                /*string sql = @"SELECT 
                idInmueble, idPropietario,direccion, disponible
                FROM inmueble";*/
                string sql = @"SELECT i.idInmueble, i.direccion,i.disponible, i.uso, i.tipo, i.ambientes,i.precio,i.latitud,i.longitud,
                   p.idPropietario, p.nombre, p.apellido,p.dni,p.telefono,p.email,p.domicilio
                    FROM inmueble i INNER JOIN propietario p ON i.idInmueble = p.idPropietario
                     WHERE p.idPropietario=@idPropietario";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.CommandType = System.Data.CommandType.Text;
                    command.Parameters.AddWithValue("idPropietario", idPropietario);
                    connection.Open();
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Inmueble i = new Inmueble
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
                        res.Add(i);
                    }
                    connection.Close();
                }
            }
            return res;
        }

        public IList<Inmueble> Disponibles()
        {
            IList<Inmueble> res = new List<Inmueble>();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                /*string sql = @"SELECT 
                idInmueble, idPropietario,direccion, disponible
                FROM inmueble";*/
                string sql = @"SELECT * FROM inmueble i JOIN propietario p ON i.idPropietario = p.idPropietario WHERE disponible = 1 ";
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
                        res.Add(inm);
                    }
                    connection.Close();
                }
            }
            return res;
        }

        public int ActualizarInmueble(int id)
        {
            int res = -1;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string sql = @"UPDATE inmueble set disponible=0 WHERE idInmueble = @idInmueble";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@idInmueble", id);
                    connection.Open();
                    res = command.ExecuteNonQuery();
                    connection.Close();
                }
            }

            return res;
        }

        public IList<Contrato> ContratosDeInmueble(int id)
        {
            IList<Contrato> res = new List<Contrato>();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string sql = @"SELECT * FROM contrato WHERE idInmueble = @idInmueble";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.CommandType = System.Data.CommandType.Text;
                    connection.Open();
                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Contrato contrato = new Contrato()
                        {
                            IdContrato = reader.GetInt32("idContrato"),
                            FechaInicio = reader.GetDateTime("fecha_inicio"),
                            FechaFin = reader.GetDateTime("fecha_fin"),
                            MontoAlquiler = reader.GetDouble("monto_alquiler"),
                            Vigente = reader.GetBoolean("vigente"),
                            Inquilino = new Inquilino()
                            {
                                IdInquilino = reader.GetInt32("idInquilino"),
                                Nombre = reader.GetString("Nombre"),
                                Cuil_cuit = reader.GetString("Cuil_cuit"),
                                Apellido = reader.GetString("Apellido"),
                                Dni = reader.GetString("Dni"),
                                Telefono = reader.GetString("Telefono"),
                                Email = reader.GetString("Email"),
                                Domicilio_trabajo = reader.GetString("Domicilio_trabajo"),
                                Domicilio = reader.GetString("Domicilio"),
                                Nombre_garante = reader.GetString("Nombre_garante"),
                                Dni_garante = reader.GetString("Dni_garante")
                            },
                            Inmueble = new Inmueble()
                            {
                                IdInmueble = reader.GetInt32("idInmueble"),
                                Direccion = reader.GetString("direccion"),
                                Tipo = reader.GetString("tipo"),
                                Uso = reader.GetString("uso"),
                                Ambientes = reader.GetInt32("ambientes"),
                                Precio = reader.GetDouble("precio"),
                                Disponible = reader.GetBoolean("disponible"),
                                //idPropietario = reader.GetInt32("idPropietario")
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
                            }

                        };
                        res.Add(contrato);
                    }

                }
            }

            return res;

        }

    }
}
