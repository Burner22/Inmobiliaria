namespace Inmobiliaria2.Models
{
    public class Propietario
    {
        private int id;
        private string dni;
        public string nombre;
        private string apellido;
        private string telefono;
        private string email;
        private string domicilio;

        public Propietario()
        {
            // Constructor vac�o
        }

        public Propietario(string nombre)
        {
            this.nombre = nombre;
        }

        public Propietario(string dni, string nombre, string apellido, string telefono, string email, string domicilio)
        {
            this.dni = dni;
            this.nombre = nombre;
            this.apellido = apellido;
            this.telefono = telefono;
            this.email = email;
            this.domicilio = domicilio;
        }

        public Propietario(int id, string dni, string nombre, string apellido, string telefono, string email, string domicilio)
        {
            this.id = id;
            this.dni = dni;
            this.nombre = nombre;
            this.apellido = apellido;
            this.telefono = telefono;
            this.email = email;
            this.domicilio = domicilio;
        }

        // Getters y setters para cada atributo

        public int Id { get => id; set => id = value; }

        public string Dni { get => dni; set => dni = value; }

        public string Apellido { get => apellido; set => apellido = value; }

        public string Nombre { get => nombre; set => nombre = value; }

        public string Domicilio { get => domicilio; set => domicilio = value; }

        public string Email { get => email; set => email = value; }

        public string Telefono { get => telefono; set => telefono = value; }
    }
}
