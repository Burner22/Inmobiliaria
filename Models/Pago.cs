namespace Inmobiliaria2.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Xml.Linq;

    public class Pago
    {
        private int idPago;
        private Contrato contrato;
        public int idContrato;
        private double cuota;
        private DateTime fecha_pago;
        private bool anulado;
        private int nro_cuota;
        

        public Pago(int idPago, Contrato contrato, double cuota, DateTime fecha_pago, bool anulado, int idContrato, int nro_cuota)
        {
            this.idPago = idPago;
            this.contrato = contrato;
            this.cuota = cuota;
            this.fecha_pago = fecha_pago;
            this.anulado = anulado;
            this.idContrato = idContrato;
            this.nro_cuota = nro_cuota;
        }

        public Pago()
        {
        }

        public int NroCuota
        {
            get { return nro_cuota; }
            set { nro_cuota = value; }
        }
        public int IdPago
        {
            get { return idPago; }
            set { idPago = value; }
        }
        [Display(Name = "Contrato")]
        public int IdContrato { get; set; }

        [ForeignKey(nameof(IdContrato))]
        public Contrato Contrato { get; set; }

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
