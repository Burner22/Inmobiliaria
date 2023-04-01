namespace Inmobiliaria2.Models
{
    using System;

    public class Pago
    {
        private int idPago;
        private Contrato contrato;
        private double cuota;
        private DateTime fecha_pago;
        private bool anulado;

        public Pago(int idPago, Contrato contrato, double cuota, DateTime fecha_pago, bool anulado)
        {
            this.idPago = idPago;
            this.contrato = contrato;
            this.cuota = cuota;
            this.fecha_pago = fecha_pago;
            this.anulado = anulado;
        }

        public Pago(int idPago, Contrato contrato, double cuota, DateTime fecha_pago)
            : this(idPago, contrato, cuota, fecha_pago, false)
        {
        }

        public int IdPago
        {
            get { return idPago; }
            set { idPago = value; }
        }

        public Contrato Contrato
        {
            get { return contrato; }
            set { contrato = value; }
        }

        public double Cuota
        {
            get { return cuota; }
            set { cuota = value; }
        }

        public DateTime FechaPago
        {
            get { return fecha_pago; }
            set { fecha_pago = value; }
        }

        public bool Anulado
        {
            get { return anulado; }
            set { anulado = value; }
        }
    }

}
