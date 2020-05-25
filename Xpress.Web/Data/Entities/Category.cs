using System.ComponentModel.DataAnnotations;

namespace Xpress.Web.Data.Entities
{
    public class Category : BasicEntity 
    {
        [Required(ErrorMessage = "The field {0} is required")]
        public Franchise Franchise { get; set; }
    }
}
