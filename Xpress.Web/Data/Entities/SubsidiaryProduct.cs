using System.ComponentModel.DataAnnotations;

namespace Xpress.Web.Data.Entities
{
    public class SubsidiaryProduct : Product
    {
        public int Rating { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        public Subsidiary Subsidiary { get; set; }
    }
}
