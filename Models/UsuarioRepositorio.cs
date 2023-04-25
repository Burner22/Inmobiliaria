using MySql.Data.MySqlClient;
using System.Data;

namespace Inmobiliaria2.Models
{
    public class UsuarioRepositorio
    {


        string connectionString = "Server=localhost;User=root;Password=;Database=inmobiliaria;SslMode=none";
        public UsuarioRepositorio()
        {

        }

        public int Alta(Usuario e)
        {
            int res = -1;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = @"INSERT INTO usuario 
					(nombre, apellido, avatar, email, clave, rol) 
					VALUES (@nombre, @apellido, @avatar, @email, @clave, @rol);
					SELECT LAST_INSERT_ID()";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@nombre", e.Nombre);
                    command.Parameters.AddWithValue("@apellido", e.Apellido);
                    if (String.IsNullOrEmpty(e.Avatar))
                        command.Parameters.AddWithValue("@avatar", DBNull.Value);
                    else
                        command.Parameters.AddWithValue("@avatar", e.Avatar);
                    command.Parameters.AddWithValue("@email", e.Email);
                    command.Parameters.AddWithValue("@clave", e.Clave);
                    command.Parameters.AddWithValue("@rol", e.Rol);
                    connection.Open();
                    res = Convert.ToInt32(command.ExecuteScalar());
                    e.Id = res;
                    connection.Close();
                }
            }
            return res;
        }
        public int Baja(Usuario usuario)
        {
            int res = -1;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string sql = @"DELETE FROM usuario WHERE idUsuario = @id";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@id", usuario.Id);
                    connection.Open();
                    res = command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return res;
        }
        public Usuario ObtenerPorId(int id)
		{
			Usuario e = null;
			using (MySqlConnection connection = new MySqlConnection(connectionString))
			{
				string sql = @"SELECT idUsuario, nombre, apellido, avatar, email, clave, rol FROM usuario
					WHERE idUsuario=@id";
				using (MySqlCommand command = new MySqlCommand(sql, connection))
				{
					command.Parameters.AddWithValue("@id", id);
					command.CommandType = CommandType.Text;
					connection.Open();
					var reader = command.ExecuteReader();
					if (reader.Read())
					{
						e = new Usuario
						{
							Id = reader.GetInt32(0),
							Nombre = reader.GetString(1),
							Apellido = reader.GetString(2),
							Avatar = reader["avatar"].ToString(),
							Email = reader.GetString(4),
							Clave = reader.GetString(5),
							Rol = reader.GetInt32(6),
						};
					}
					connection.Close();
				}
			}
			return e;
		}
		public int Modificacion(Usuario e)
        {
            int res = -1;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = @"UPDATE usuario
					SET nombre=@nombre, apellido=@apellido, email=@email, rol=@rol, avatar=@avatar
					WHERE idUsuario = @id";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@nombre", e.Nombre);
                    command.Parameters.AddWithValue("@apellido", e.Apellido);
                    command.Parameters.AddWithValue("@email", e.Email);
					command.Parameters.AddWithValue("@avatar", e.Avatar);
                    command.Parameters.AddWithValue("@rol", e.Rol);
                    command.Parameters.AddWithValue("@id", e.Id);
                    connection.Open();
                    res = Convert.ToInt32(command.ExecuteScalar());
                    connection.Close();
                }
            }
            return res;
        }
        public int ModificarAvatar(Usuario e)
        {
            int res = -1;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = @"UPDATE usuario
					SET avatar=@avatar
					WHERE idUsuario = @id";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@avatar", e.Avatar);
                    command.Parameters.AddWithValue("@id", e.Id);
                    connection.Open();
                    res = Convert.ToInt32(command.ExecuteScalar());
                    connection.Close();
                }
            }
            return res;
        }
        public int ModificarClave(Usuario e)
        {
            int res = -1;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string sql = @"UPDATE usuario SET clave=@clave 
					        WHERE idUsuario = @id";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@clave", e.Clave);
                    command.Parameters.AddWithValue("@id", e.Id);
                    connection.Open();
                    res = command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return res;
        }
        public int EditarPerfil(Usuario e)
        {
            int res = -1;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string sql = @"UPDATE usuario SET nombre=@nombre, apellido=@apellido, avatar=@avatar, email=@email, rol=@rol
                    WHERE idUsuario = @id";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@nombre", e.Nombre);
                    command.Parameters.AddWithValue("@apellido", e.Apellido);
                    command.Parameters.AddWithValue("@avatar", e.Avatar);
                    command.Parameters.AddWithValue("@email", e.Email);
                    command.Parameters.AddWithValue("@rol", e.Rol);
                    command.Parameters.AddWithValue("@id", e.Id);
                    connection.Open();
                    res = command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return res;
        }
        public IList<Usuario> ObtenerTodos()
        {
            IList<Usuario> res = new List<Usuario>();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = @"
					SELECT idUsuario, nombre, apellido, avatar, email, clave, rol
					FROM usuario";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    connection.Open();
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Usuario e = new Usuario
                        {
                            Id = reader.GetInt32("idUsuario"),
                            Nombre = reader.GetString("nombre"),
                            Apellido = reader.GetString("apellido"),
                            Avatar = reader.IsDBNull(reader.GetOrdinal("avatar")) ? null : reader.GetString("avatar"),
                            Email = reader.GetString("email"),
                            Clave = reader.GetString("clave"),
                            Rol = reader.GetInt32("rol"),
                        };
                        res.Add(e);
                    }
                    connection.Close();
                }
            }
            return res;
        }
		public Usuario ObtenerPorEmail(string email)
        {
            Usuario? e = null;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = @"SELECT
					idUsuario, nombre, apellido, avatar, email, clave, rol FROM usuario
					WHERE email=@email";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@email", email);
                    connection.Open();
                    var reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        e = new Usuario
                        {
                            Id = reader.GetInt32("idUsuario"),
                            Nombre = reader.GetString("nombre"),
                            Apellido = reader.GetString("apellido"),
                            Avatar = reader.IsDBNull(reader.GetOrdinal("avatar")) ? null : reader.GetString("avatar"),
                            Email = reader.GetString("email"),
                            Clave = reader.GetString("clave"),
                            Rol = reader.GetInt32("rol"),
                        };
                    }
                    connection.Close();
                }
            }
            return e;
        }
    }
}
