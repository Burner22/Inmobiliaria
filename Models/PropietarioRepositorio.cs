using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGeneration;
using MySql.Data.MySqlClient;
using NuGet.ContentModel;
using System.Xml.Linq;
//using MySqlConnector;

namespace Inmobiliaria2.Models
{
    public class PropietarioRepxositorio
    {
        string connectionString = "Server=localhost;User=root;Password=;Database=inmobiliaria;SslMode=none";
        public PropietarioRepositorio()
        {
        }

        public Propietario GetPropietario(int id)
        {
            Propietario res = new Propietario();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string sql = @"SELECT 
                idPropietario, nombre, apellido, dni, telefono, email, domicilio
                FROM propietario WHERE idPropietario = @id";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("id", id);
                    command.CommandType = System.Data.CommandType.Text;
                    connection.Open();
                    var reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        res = new Propietario
                        {
                            Id = reader.GetInt32("idPropietario"),
                            Nombre = reader.GetString("Nombre"),
                            Apellido = reader.GetString("Apellido"),
                            Dni = reader.GetString("Dni"),
                            Telefono = reader.GetString("Telefono"),
                            Email = reader.GetString("Email"),
                            Domicilio = reader.GetString("Domicilio")
                        };
                    }
                    connection.Close();
                }
            }
            return res;
        }

        public IList<Propietario> GetPropietarios()
        {
            IList<Propietario> res = new List<Propietario>();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string sql = @"SELECT 
                idPropietario, nombre, apellido, dni, telefono, email, domicilio
                FROM propietario";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.CommandType = System.Data.CommandType.Text;
                    connection.Open();
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Propietario p = new Propietario
                        {
                            Id = reader.GetInt32("idPropietario"),
                            Nombre = reader.GetString("Nombre"),
                            Apellido = reader.GetString("Apellido"),
                            Dni = reader.GetString("Dni"),
                            Telefono = reader.GetString("Telefono"),
                            Email = reader.GetString("Email"),
                            Domicilio = reader.GetString("Domicilio")
                        };
                        res.Add(p);
                    }
                    connection.Close();
                }
            }
            return res;
        }

        public int Alta(Propietario propietario)
        {
            int con = 0;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string sql = @"INSERT INTO propietario (dni, nombre, apellido, domicilio, telefono, email) VALUES 
                              (@dni, @nombre, @apellido, @domicilio, @telefono, @email)";
                using (MySqlCommand command = new MySqlCommand(sql, connection)) {
                    command.Parameters.AddWithValue("@dni", propietario.Dni);
                    command.Parameters.AddWithValue("@nombre", propietario.Nombre);
                    command.Parameters.AddWithValue("@apellido", propietario.Apellido);
                    command.Parameters.AddWithValue("@domicilio", propietario.Domicilio);
                    command.Parameters.AddWithValue("@telefono", propietario.Telefono);
                    command.Parameters.AddWithValue("@email", propietario.Email);
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
            using(MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string sql = @"DELETE FROM propietario WHERE idPropietario = @id";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@idPropietario", id);
                    connection.Open();
                    con = command.ExecuteNonQuery();
                    connection.Close();             
                }
            }
            return con;
        }

        public int Modificar(Propietario propietario)
        {
            int con = 0;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string sql = @"UPDATE FROM propietarios SET 
                            nombre = @nombre, 
                            dni = @dni, 
                            apellido = @apellido, 
                            telefono = @telefono, 
                            domicilio = @domicilio, 
                            email=@email 
                            WHERE idPropietario = @id ";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@nombre", propietario.nombre);
                    command.Parameters.AddWithValue("@dni", propietario.Dni);
                    command.Parameters.AddWithValue("@apellido", propietario.Apellido);
                    command.Parameters.AddWithValue("@telefono", propietario.Telefono);
                    command.Parameters.AddWithValue("@domicilio", propietario.Domicilio);
                    command.Parameters.AddWithValue("@email", propietario.Email);
                    command.Parameters.AddWithValue("@id", propietario.Id);
                    connection.Open();
                    con = command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return con;
        }

    }
}
