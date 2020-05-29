using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Xpress.Web.Data.Entities
{
    public class Package : IEntity
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        public User Sender { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        public User Customer { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        public Delivery Delivery { get; set; }

        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "The field {0} is required")]
        [MaxLength(50, ErrorMessage = "The field {0} must have {1} characters")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public double Price { get; set; }

        public bool Disabled { get; set; }

        public virtual ICollection<ProductToDeliver> Products { get; set; }
    }
}
