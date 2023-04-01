namespace Inmobiliaria2.Models
{
    public class Inquilino
    {
        private int idInquilino;
        private string dni;
        private string cuil_cuit;
        private string nombre;
        private string apellido;
        private string telefono;
        private string email;
        private string domicilio_trabajo;

        // Constructor por defecto
        public Inquilino() { }

        // Constructor con parámetros
        public Inquilino(int idInquilino, string dni, string cuil_cuit, string nombre, string apellido, string telefono, string email, string domicilio_trabajo)
        {
            this.idInquilino = idInquilino;
            this.dni = dni;
            this.cuil_cuit = cuil_cuit;
            this.nombre = nombre;
            this.apellido = apellido;
            this.telefono = telefono;
            this.email = email;
            this.domicilio_trabajo = domicilio_trabajo;
        }

        // Getters y setters
        public int IdInquilino
        {
            get { return idInquilino; }
            set { idInquilino = value; }
        }

        public string Dni
        {
            get { return dni; }
            set { dni = value; }
        }

        public string Cuil_cuit
        {
            get { return cuil_cuit; }
            set { cuil_cuit = value; }
        }

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        public string Apellido
        {
            get { return apellido; }
            set { apellido = value; }
        }

        public string Telefono
        {
            get { return telefono; }
            set { telefono = value; }
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public string Domicilio_trabajo
        {
            get { return domicilio_trabajo; }
            set { domicilio_trabajo = value; }
        }
    }

}
