namespace Inmobiliaria2.Models
{
    public class Inmueble
    {
        private int idInmueble;
        private Propietario propietario;
        private string direccion;
        private string tipo;
        private string uso;
        private int ambientes;
        private double precio;
        private bool disponible;

        // Constructor por defecto
        public Inmueble() { }

        // Constructor con parámetros
        public Inmueble(int idInmueble, Propietario propietario, string direccion, string tipo, string uso, int ambientes, double precio, bool disponible)
        {
            this.idInmueble = idInmueble;
            this.propietario = propietario;
            this.direccion = direccion;
            this.tipo = tipo;
            this.uso = uso;
            this.ambientes = ambientes;
            this.precio = precio;
            this.disponible = disponible;
        }

        // Getters y setters
        public int IdInmueble
        {
            get { return idInmueble; }
            set { idInmueble = value; }
        }

        public Propietario Propietario
        {
            get { return propietario; }
            set { propietario = value; }
        }

        public string Direccion
        {
            get { return direccion; }
            set { direccion = value; }
        }

        public string Tipo
        {
            get { return tipo; }
            set { tipo = value; }
        }

        public string Uso
        {
            get { return uso; }
            set { uso = value; }
        }

        public int Ambientes
        {
            get { return ambientes; }
            set { ambientes = value; }
        }

        public double Precio
        {
            get { return precio; }
            set { precio = value; }
        }

        public bool Disponible
        {
            get { return disponible; }
            set { disponible = value; }
        }
    }

}
