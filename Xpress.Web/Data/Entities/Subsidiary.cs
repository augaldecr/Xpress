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

        public virtual ICollection<Product> Products { get; set; }
    }
}
