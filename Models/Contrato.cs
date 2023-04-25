namespace Inmobiliaria2.Models
{
    using Microsoft.VisualStudio.Web.CodeGeneration;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Xml.Linq;

    public class Contrato
    {
        private int idContrato;
        public int idInquilino;
        public int idInmueble;
        private Inmueble inmueble;
        private Inquilino inquilino;
        private DateTime fecha_inicio;
        private DateTime fecha_fin;
        private double monto_alquiler;
        private bool vigente;

        public Contrato()
        {
        }

        public Contrato(int idContrato, int idInquilino, int idInmueble, DateTime fecha_inicio, DateTime fecha_fin, double monto_alquiler, bool vigente)
        {
            this.idContrato = idContrato;
            this.idInquilino = idInquilino;
            this.idInmueble = idInmueble;
            this.fecha_inicio = fecha_inicio;
            this.fecha_fin = fecha_fin;
            this.monto_alquiler = monto_alquiler;
            this.vigente = vigente;
        }

        // Constructor con parámetros


        // Getters y setters
        public int IdContrato
        {
            get { return idContrato; }
            set { idContrato = value; }
        }
        [Display(Name = "Inquilino")]
        public int IdInquilino { get; set; }
        [ForeignKey(nameof(IdInquilino))]       
        public Inquilino Inquilino { get; set; } 
        
        [Display(Name = "Inmueble")]       
        public int IdInmueble{get;set; }        
        [ForeignKey(nameof(IdInmueble))]
        public Inmueble Inmueble { get; set; }
        

        public DateTime FechaInicio
        {
            get { return fecha_inicio; }
            set { fecha_inicio = value; }
        }

        public DateTime FechaFin
        {
            get { return fecha_fin; }
            set { fecha_fin = value; }
        }
        

        public double MontoAlquiler
        {
            get { return monto_alquiler; }
            set { monto_alquiler = value; }
        }
        public bool Vigente { get; set; }
    }

}
