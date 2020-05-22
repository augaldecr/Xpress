﻿using System;
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
        [Display(Name = "Tipo de pago")]
        public PaymentType PaymentType { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [Display(Name = "Monto")]
        public float Amount { get; set; }
    }
}