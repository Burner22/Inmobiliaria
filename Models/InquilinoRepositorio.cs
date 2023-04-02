using MySql.Data.MySqlClient;

namespace Inmobiliaria2.Models
{
    public class InquilinoRepositorio
    {
        string connectionString = "Server=localhost;User=root;Password=;Database=inmobiliaria;SslMode=none";
        public InquilinoRepositorio()
        {
        }

        public Inquilino GetInquilino(int id)
        {
            Inquilino res = new Inquilino();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string sql = @"SELECT 
                idInquilino, nombre,cuil_cuit, apellido, dni, telefono, email, 
                domicilio_trabajo, domicilio, nombre_garante, dni_garante
                FROM inquilino WHERE idInquilino = @id";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("id", id);
                    command.CommandType = System.Data.CommandType.Text;
                    connection.Open();
                    var reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        res = new Inquilino
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

                        };
                    }
                    connection.Close();
                }
            }
            return res;
        }

        public IList<Inquilino> GetInquilinos()
        {
            IList<Inquilino> res = new List<Inquilino>();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string sql = @"SELECT 
                idInquilino, nombre,cuil_cuit, apellido, dni, telefono, email, 
                domicilio_trabajo, domicilio, nombre_garante, dni_garante
                FROM inquilino";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.CommandType = System.Data.CommandType.Text;
                    connection.Open();
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Inquilino inq = new Inquilino
                        {
                            IdInquilino = reader.GetInt32("idInquilino"),
                            Nombre = reader.GetString("Nombre"),
                            Apellido = reader.GetString("Apellido"),
                            Dni = reader.GetString("Dni"),
                            Cuil_cuit = reader.GetString("Cuil_cuit"),
                            Telefono = reader.GetString("Telefono"),
                            Email = reader.GetString("Email"),
                            Domicilio_trabajo = reader.GetString("Domicilio_trabajo"),
                            Domicilio = reader.GetString("Domicilio"),
                            Nombre_garante = reader.GetString("Nombre_garante"),
                            Dni_garante = reader.GetString("Dni_garante")

                        };
                        res.Add(inq);
                    }
                    connection.Close();
                }
            }
            return res;
        }

        public int Alta(Inquilino inquilino)
        {
            int con = 0;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string sql = @"INSERT INTO inquilino (dni, cuil_cuit, nombre, apellido, telefono, email,
                                domicilio_trabajo, domicilio, nombre_garante, dni_garante) VALUES 
                              (@dni,@cuil_cuit, @nombre, @apellido, @telefono, @email,
                                        @domicilio_trabajo, @domicilio, @nombre_garante, @dni_garante)";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@dni", inquilino.Dni);
                    command.Parameters.AddWithValue("@cuil_cuit", inquilino.Cuil_cuit);
                    command.Parameters.AddWithValue("@nombre", inquilino.Nombre);
                    command.Parameters.AddWithValue("@domicilio_trabajo", inquilino.Domicilio_trabajo);
                    command.Parameters.AddWithValue("@apellido", inquilino.Apellido);
                    command.Parameters.AddWithValue("@domicilio", inquilino.Domicilio);
                    command.Parameters.AddWithValue("@telefono", inquilino.Telefono);
                    command.Parameters.AddWithValue("@email", inquilino.Email);
                    command.Parameters.AddWithValue("@dni_garante", inquilino.Dni_garante);
                    command.Parameters.AddWithValue("@nombre_garante", inquilino.Nombre_garante);
                    connection.Open();
                    con = Convert.ToInt32(command.ExecuteScalar());
                    connection.Close();

                }
            }
            return con;
        }

        public int Baja(int id)
        {
            int con = 0;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string sql = @"DELETE FROM inquilino WHERE idInquilino = @idInquilino";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@idInquilino", id);
                    connection.Open();
                    con = command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return con;
        }

        public int Modificar(Inquilino inquilino)
        {
            int con = 0;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string sql = @"UPDATE inquilino SET 
                            nombre = @nombre, 
                            dni = @dni,
                            cuil_cuit = @cuil_cuit,
                            apellido = @apellido, 
                            telefono = @telefono, 
                            domicilio = @domicilio, 
                            email=@email,
                            domicilio_trabajo = @domicilio_trabajo,
                            dni_garante = @dni_garante,
                            nombre_garante = @nombre_garante
                            WHERE idInquilino = @idInquilino ";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@idInquilino", inquilino.IdInquilino);
                    command.Parameters.AddWithValue("@dni", inquilino.Dni);
                    command.Parameters.AddWithValue("@cuil_cuit", inquilino.Cuil_cuit);
                    command.Parameters.AddWithValue("@nombre", inquilino.Nombre);
                    command.Parameters.AddWithValue("@domicilio_trabajo", inquilino.Domicilio_trabajo);
                    command.Parameters.AddWithValue("@apellido", inquilino.Apellido);
                    command.Parameters.AddWithValue("@domicilio", inquilino.Domicilio);
                    command.Parameters.AddWithValue("@telefono", inquilino.Telefono);
                    command.Parameters.AddWithValue("@email", inquilino.Email);
                    command.Parameters.AddWithValue("@dni_garante", inquilino.Dni_garante);
                    command.Parameters.AddWithValue("@nombre_garante", inquilino.Nombre_garante);
                    connection.Open();
                    con = command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return con;
        }


    }
}
