using System.ComponentModel.DataAnnotations;

namespace Xpress.Web.Data.Entities
{
    public abstract class BasicEntity : IEntity
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [MaxLength(50, ErrorMessage = "The field {0} must have {1} characters")]
        public string Name { get; set; }
    }
}
