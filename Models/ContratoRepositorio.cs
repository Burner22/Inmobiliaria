using MySql.Data.MySqlClient;
using NuGet.Protocol.Plugins;

namespace Inmobiliaria2.Models
{
    public class ContratoRepositorio
    {
        public ContratoRepositorio()
        {
        }

        string connectionString = "Server=localhost;User=root;Password=;Database=inmobiliaria;SslMode=none";

        public IList<Contrato> GetContratos()
        {
            IList<Contrato> contratos = new List<Contrato>();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string sql = @"SELECT * FROM contrato JOIN inmueble JOIN inquilino JOIN propietario 
                                WHERE contrato.idInmueble = inmueble.idInmueble 
                                AND contrato.idInquilino = inquilino.idInquilino AND propietario.idPropietario = inmueble.idPropietario";
                //string sql = @"Select * from contrato";

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
                            //IdInmueble = reader.GetInt32("idInmueble"),
                            //IdInquilino = reader.GetInt32("idInquilino"),
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
                        contratos.Add(contrato);
                    }
                    connection.Close();
                }
            }
            return contratos;
        }

        public Contrato GetContrato(int id)
        {
            Contrato contrato = new Contrato();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string sql = @"SELECT * FROM contrato JOIN inmueble JOIN inquilino 
                                WHERE contrato.idInmueble = inmueble.idInmueble 
                                AND contrato.idInquilino = inquilino.idInquilino
                                AND contrato.idContrato = @id";

                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.CommandType = System.Data.CommandType.Text;
                    connection.Open();
                    var reader = command.ExecuteReader();

                    if(reader.Read())
                    {
                        contrato = new Contrato()
                        {
                            IdContrato = reader.GetInt32("idContrato"),
                            FechaInicio = reader.GetDateTime("fecha_inicio"),
                            FechaFin = reader.GetDateTime("fecha_fin"),
                            MontoAlquiler = reader.GetDouble("monto_alquiler"),
                            Vigente = reader.GetBoolean("vigente"),
                            //IdInmueble = reader.GetInt32("idInmueble"),
                            //IdInquilino = reader.GetInt32("idInquilino"),
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
                                idPropietario = reader.GetInt32("idPropietario"),
                                Direccion = reader.GetString("direccion"),
                                Tipo = reader.GetString("tipo"),
                                Uso = reader.GetString("uso"),
                                Ambientes = reader.GetInt32("ambientes"),
                                Precio = reader.GetDouble("precio"),
                                Disponible = reader.GetBoolean("disponible")
                            }

                        };
                    }
                    connection.Close();
                }
            }
            return contrato;
        }

        public int Alta(Contrato contrato)
        {
            int con = 0;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string sql = @"INSERT INTO contrato (idInmueble, idInquilino, fecha_inicio, fecha_fin, monto_alquiler, vigente)
                                VALUES (@idInmueble, @idInquilino, @fecha_inicio, @fecha_fin, @monto_alquiler, @vigente); SELECT LAST_INSERT_ID();";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@idInmueble", contrato.IdInmueble);
                    command.Parameters.AddWithValue("@idInquilino", contrato.IdInquilino);
                    command.Parameters.AddWithValue("@fecha_inicio", contrato.FechaInicio);
                    command.Parameters.AddWithValue("@fecha_fin", contrato.FechaFin);
                    command.Parameters.AddWithValue("@monto_alquiler", contrato.MontoAlquiler);
                    command.Parameters.AddWithValue("@vigente", true);
                    connection.Open();
                    con = Convert.ToInt32(command.ExecuteScalar());
                    connection.Close();
                }
            }
            return con;
        }

        public int Modificar(Contrato contrato)
        {
            int con = 0;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string sql = @"UPDATE contrato SET 
                                idInmueble = @idInmueble,
                                idInquilino = @idInquilino,
                                fecha_inicio = @fecha_inicio,
                                fecha_fin = @fecha_fin,
                                monto_alquiler = @monto_alquiler,
                                vigente = @vigente
                                WHERE idContrato = @idContrato";

                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@idInmueble", contrato.IdInmueble);
                    command.Parameters.AddWithValue("@idInquilino", contrato.IdInquilino);
                    command.Parameters.AddWithValue("@fecha_inicio", contrato.FechaInicio);
                    command.Parameters.AddWithValue("@fecha_fin", contrato.FechaFin);
                    command.Parameters.AddWithValue("@monto_alquiler", contrato.MontoAlquiler);
                    command.Parameters.AddWithValue("@vigente", contrato.Vigente);
                    connection.Open();
                    con = command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return con;
        }

        public int Baja(Contrato contrato)
        {
            int res = -1;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string sql = @"UPDATE contrato set borrado=1 WHERE idContrato = @idContrato";
                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.Parameters.AddWithValue("@idInmueble", contrato.IdContrato);
                    connection.Open();
                    res = command.ExecuteNonQuery();
                    connection.Close();
                }
            }

            return res;
        }

        public IList<Contrato> DesdeHasta(DateTime desde, DateTime hasta)
        {
            IList<Contrato> res = new List<Contrato>();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string sql = @"SELECT * FROM contrato JOIN inmueble JOIN inquilino JOIN propietario 
                                WHERE contrato.idInmueble = inmueble.idInmueble 
                                AND contrato.idInquilino = inquilino.idInquilino AND propietario.idPropietario = inmueble.idPropietario 
                                AND contrato.fecha_inicio > @desde AND contrato.fecha_fin < @hasta AND contrato.vigente = 1";
                //string sql = @"Select * from contrato";

                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("desde", desde);
                    command.Parameters.AddWithValue("hasta", hasta);
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
                            //IdInmueble = reader.GetInt32("idInmueble"),
                            //IdInquilino = reader.GetInt32("idInquilino"),
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
                    connection.Close();
                }
            }



            return res;
        }

        public IList<Inmueble> ContratoDesdeHasta(DateTime desde, DateTime hasta)
        {
            IList<Inmueble> res = new List<Inmueble>();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string sql = @" SELECT i.idInmueble,i.idPropietario,i.direccion,i.tipo,i.uso,i.ambientes,i.precio,i.disponible,i.latitud,i.longitud,
                                P.idPropietario ,P.dni, P.nombre,P.apellido,P.domiclio,P.telefono,P.email 
                                From(SELECT * FROM Inmueble i left join 
                                (SELECT idInmueble FROM contrato c WHERE ((c.fecha_inicio between @desde  and @hasta) 
                                or (c.fecha_fin between @desde and @hasta)) and c.idInmueble != @id) x on (i.idInmueble = x.idInmueble)
                                where x.idInmueble is null and i.disponible = 0) i  INNER JOIN propietario P ON i.idPropietario = P.idPropietario;";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@desde", desde);
                    command.Parameters.AddWithValue("@hasta", hasta);

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

       



    }
}
