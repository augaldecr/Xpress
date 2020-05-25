using System.ComponentModel.DataAnnotations;

namespace Xpress.Web.Data.Entities
{
    public class SubsidiaryProduct : IEntity
    {
        public int Id { get; set; }

        public Product Product { get; set; }

        public double Rating { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        public Subsidiary Subsidiary { get; set; }

        public bool Active { get; set; }
    }
}
