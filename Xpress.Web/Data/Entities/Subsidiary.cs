using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Xpress.Web.Data.Entities.Common;

namespace Xpress.Web.Data.Entities
{
    public class Subsidiary : IEntity
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        public Franchise Franchise { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        public Town Town { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        public string Address { get; set; }

        [Display(Name = "¿Sólo tarjeta? No efectivo")]
        [Required(ErrorMessage = "The field {0} is required")]
        public bool CardOnly { get; set; }

        public bool Disabled { get; set; }

        public virtual ICollection<SubsidiaryProduct> Products { get; set; }
    }
}
