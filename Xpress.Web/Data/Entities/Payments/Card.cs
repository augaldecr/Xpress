using System;
using System.ComponentModel.DataAnnotations;

namespace Xpress.Web.Data.Entities.Payments
{
    public class Card : IEntity
    {
        public int Id { get; set; }

        [Display(Name = "Nombre de titular")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede exceder los {1} caracteres")]
        public string OwnerName { get; set; }

        //[CreditCard]
        [DataType(DataType.CreditCard)]
        [Display(Name = "Número de tarjeta")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string Number { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Fecha de caducidad")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public DateTime GoodThru { get; set; }

        [Display(Name = "Número de seguridad")]
        [MaxLength(3, ErrorMessage = "El campo {0} no puede exceder los {1} carácteres")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string SecurityNumber { get; set; }

        public User Owner { get; set; }

        public bool Disabled { get; set; }
    }
}