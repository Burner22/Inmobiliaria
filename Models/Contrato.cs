namespace Inmobiliaria2.Models
{
    using Microsoft.VisualStudio.Web.CodeGeneration;
    using System;

    public class Contrato
    {
        private int idContrato;
        private Inmueble inmueble;
        private Inquilino inquilino;
        private DateTime fecha_inicio;
        private DateTime fecha_fin;
        private double monto_alquiler;
        private bool vigente;

        // Constructor por defecto
        public Contrato()
        {
            this.idContrato = 0;
            this.inmueble = null;
            this.inquilino = null;
            this.fecha_inicio = DateTime.MinValue;
            this.fecha_fin = DateTime.MinValue;
            this.monto_alquiler = 0;
            this.vigente = false;
        }

        // Constructor con parámetros
        public Contrato(int idContrato, Inmueble inmueble, Inquilino inquilino,
                        DateTime fecha_inicio, DateTime fecha_fin,
                        double monto_alquiler, bool vigente)
        {
            this.idContrato = idContrato;
            this.inmueble = inmueble;
            this.inquilino = inquilino;
            this.fecha_inicio = fecha_inicio;
            this.fecha_fin = fecha_fin;
            this.monto_alquiler = monto_alquiler;
            this.vigente = vigente;
        }

        // Getters y setters
        public int IdContrato
        {
            get { return idContrato; }
            set { idContrato = value; }
        }

        public Inmueble Inmueble
        {
            get { return inmueble; }
            set { inmueble = value; }
        }

        public Inquilino Inquilino
        {
            get { return inquilino; }
            set { inquilino = value; }
        }

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

        public bool Vigente
        {
            get { return vigente; }
            set { vigente = value; }
        }
    }

}
