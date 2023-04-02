using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

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
        private string latitud;
        private string longitud;
        public int idPropietario;

        // Constructor por defecto
        public Inmueble() { }

        public Inmueble(Propietario propietario) 
        {
            this.propietario = propietario;
        }

        public Inmueble(int idInmueble, Propietario propietario, string direccion, bool disponible)
        {
            this.direccion = direccion; 
            this.disponible=disponible;
            this.idInmueble = idInmueble;
            this.propietario= propietario;
                    
        }

        public Inmueble(Propietario propietario, string direccion, string tipo, string uso, int ambientes, double precio, bool disponible, string latitud, string longitud)
        {
            this.propietario = propietario;
            this.direccion = direccion;
            this.tipo = tipo;
            this.uso = uso;
            this.ambientes = ambientes;
            this.precio = precio;
            this.disponible = disponible;
            this.latitud = latitud;
            this.longitud = longitud;
        }

        public Inmueble(int idPropietario, string direccion, string tipo, string uso, int ambientes, double precio, bool disponible, string latitud, string longitud)
        {
            this.idPropietario = idPropietario;
            this.direccion = direccion;
            this.tipo = tipo;
            this.uso = uso;
            this.ambientes = ambientes;
            this.precio = precio;
            this.disponible = disponible;
            this.latitud = latitud;
            this.longitud = longitud;
        }

        // Constructor con parámetros
        public Inmueble(int idInmueble, Propietario propietario, string direccion, string tipo, string uso, int ambientes, double precio, bool disponible, string latitud, string longitud)
        {
            this.idInmueble = idInmueble;
            this.propietario = propietario;
            this.direccion = direccion;
            this.tipo = tipo;
            this.uso = uso;
            this.ambientes = ambientes;
            this.precio = precio;
            this.disponible = disponible;
            this.latitud = latitud;
            this.longitud = longitud;
        }



        // Getters y setters
        public int IdInmueble
        {
            get { return idInmueble; }
            set { idInmueble = value; }
        }
        [Display(Name = "Dueño")]
        public int IdPropietario { get; set; }
        [ForeignKey(nameof(IdPropietario))]
        public Propietario Propietario { get; set; }

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
        public string Longitud
        {
            get { return longitud; }
            set { longitud = value; }
        }
        public string Latitud
        {
            get { return latitud; }
            set { latitud = value; }
        }
    }

}
