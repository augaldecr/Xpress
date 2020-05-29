using System;
using System.ComponentModel.DataAnnotations;

namespace Xpress.Web.Data.Entities.Payments
{
    public class Payment : IEntity
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [Display(Name = "Cliente")]
        public User User { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [Display(Name = "Fecha de inicio")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [Display(Name = "Método de pago")]
        public PaymentMethod PaymentMethod { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [Display(Name = "Monto")]
        public float Amount { get; set; }

        public bool Disabled { get; set; }
    }
}