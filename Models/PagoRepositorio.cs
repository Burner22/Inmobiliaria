using MySql.Data.MySqlClient;
using System.Data;

namespace Inmobiliaria2.Models
{
    public class PagoRepositorio
    {
        string connectionString = "Server=localhost;User=root;Password=;Database=inmobiliaria;SslMode=none";


        public IList<Pago> GetPagos()
        {
            IList<Pago> pagos = new List<Pago>();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                var sql = @"SELECT * FROM pago i 
                            JOIN contrato p ON i.idPago = p.idContrato 
                            JOIN inquilino ON inquilino.idInquilino = p.idInquilino
                            JOIN inmueble ON inmueble.idInmueble = p.idInmueble";
                            //JOIN propietario ON propietario.idPropietario = inmueble.idPropietario
                

                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Pago pago = new Pago()
                            {
                                IdPago = reader.GetInt32("idPago"),
                                idContrato = reader.GetInt32("idContrato"),
                                FechaPago = reader.GetDateTime("fecha_pago"),
                                Cuota = reader.GetDouble("cuota"),
                                NroCuota = reader.GetInt32("nro_cuota"),
                                Anulado = reader.GetBoolean("anulado"),
                                Contrato = new Contrato()
                                {
                                    IdContrato = reader.GetInt32("idContrato"),
                                    FechaInicio = reader.GetDateTime("fecha_inicio"),
                                    FechaFin = reader.GetDateTime("fecha_fin"),
                                    MontoAlquiler = reader.GetDouble("monto_alquiler"),
                                    Vigente = reader.GetBoolean("vigente"),
                                    //IdInquilino = reader.GetInt32("idInquilino"),
                                    //IdInmueble = reader.GetInt32("idInmueble")
                                    Inmueble = new Inmueble()
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
                                        IdPropietario = reader.GetInt32("idPropietario")
                                    },
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
                                    }
                                },
                            };
                            pagos.Add(pago);
                        }
                    }
                }
            }
            return pagos;
        }

        public Pago GetPago(int id)
        {
            Pago pago = new Pago();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                var sql = @"SELECT * FROM pago i 
                            JOIN contrato p ON i.idPago = p.idContrato 
                            JOIN inquilino ON inquilino.idInquilino = p.idInquilino
                            JOIN inmueble ON inmueble.idInmueble = p.idInmueble
                            WHERE idPago = @id";
                //JOIN propietario ON propietario.idPropietario = inmueble.idPropietario


                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("id", id);
                    command.CommandType = System.Data.CommandType.Text;
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            pago = new Pago()
                            {
                                IdPago = reader.GetInt32("idPago"),
                                idContrato = reader.GetInt32("idContrato"),
                                FechaPago = reader.GetDateTime("fecha_pago"),
                                Cuota = reader.GetDouble("cuota"),
                                NroCuota = reader.GetInt32("nro_cuota"),
                                Anulado = reader.GetBoolean("anulado"),
                                Contrato = new Contrato()
                                {
                                    IdContrato = reader.GetInt32("idContrato"),
                                    FechaInicio = reader.GetDateTime("fecha_inicio"),
                                    FechaFin = reader.GetDateTime("fecha_fin"),
                                    MontoAlquiler = reader.GetDouble("monto_alquiler"),
                                    Vigente = reader.GetBoolean("vigente"),
                                    //IdInquilino = reader.GetInt32("idInquilino"),
                                    //IdInmueble = reader.GetInt32("idInmueble")
                                    Inmueble = new Inmueble()
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
                                        IdPropietario = reader.GetInt32("idPropietario")
                                    },
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
                                    }
                                },
                            };
                        }
                        connection.Close();
                    }
                }
            }
            return pago;
        }

        public int Alta(Pago pago)
        {
            int con = 0;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string sql = @"INSERT INTO pago
                            (idContrato, cuota,nro_cuota, fecha_pago, anulado)
                            VALUES (@idContrato, @cuota,@nro_cuota, @fecha_pago, @anulado)";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@idContrato", pago.IdContrato);
                    command.Parameters.AddWithValue("@cuota", pago.Cuota);
                    command.Parameters.AddWithValue("@nro_cuota", pago.NroCuota);
                    command.Parameters.AddWithValue("@fecha_pago", pago.FechaPago);
                    command.Parameters.AddWithValue("@anulado", pago.Anulado);
                    connection.Open();
                    con = Convert.ToInt32(command.ExecuteScalar());
                    connection.Close();
                }
            }
            return con;
        }

        public int Modificar(Pago pago)
        {
            int con = 0;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string sql = @"UPDATE pago SET 
                              idContrato=@idContrato, 
                                cuota=@cuota,
                                   nro_cuota=@nro_cuota,
                                fecha_pago=@fecha_pago, 
                                anulado=@anulado
                                WHERE idPago = @idPago";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@idContrato", pago.IdContrato);
                    command.Parameters.AddWithValue("@cuota", pago.Cuota);
                    command.Parameters.AddWithValue("@nro_cuota", pago.NroCuota);
                    command.Parameters.AddWithValue("@fecha_pago", pago.FechaPago);
                    command.Parameters.AddWithValue("@anulado", pago.Anulado);
                    command.Parameters.AddWithValue("@idPago", pago.IdPago);
                    connection.Open();
                    con = command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return con;
        }

        public int Baja(Pago pago)
        {
            int res = -1;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string sql = @"UPDATE pago set anulado=1 WHERE idPago = @idPago";
                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.Parameters.AddWithValue("@idPago", pago.IdPago);
                    connection.Open();
                    res = command.ExecuteNonQuery();
                    connection.Close();
                }
            }

            return res;
        }

        public IList<Pago> BuscarPorContrato(int IdContrato)
        {
            List<Pago> res = new List<Pago>();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string sql = @"SELECT p.idPago, p.cuota,p.nro_cuota , p.fecha_pago,p.anulado, p.idContrato
                    FROM pago p INNER JOIN contrato c ON p.idContrato = c.idContrato
                     WHERE c.idContrato=@IdContrato";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.CommandType = System.Data.CommandType.Text;
                    command.Parameters.AddWithValue("IdContrato", IdContrato);
                    connection.Open();
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Pago pago = new Pago()
                        {
                            IdPago = reader.GetInt32("idPago"),
                            idContrato = reader.GetInt32("idContrato"),
                            FechaPago = reader.GetDateTime("fecha_pago"),
                            Cuota = reader.GetDouble("cuota"),
                            NroCuota = reader.GetInt32("nro_cuota"),
                            Anulado = reader.GetBoolean("anulado"),
                            Contrato = new Contrato()
                            {
                                IdContrato = reader.GetInt32("idContrato"),


                            },
                        };
                        res.Add(pago);
                    }
                    connection.Close();
                }
            }
            return res;
        }

    }
}
