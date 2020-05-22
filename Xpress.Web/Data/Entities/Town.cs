using System.ComponentModel.DataAnnotations;
using Xpress.Web.Data.Entities.Common;

namespace Xpress.Web.Data.Entities
{
    //Ciudad, pueblo, villa, etc..
    public class Town : BasicEntity, IEntity
    {
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [Display(Name = "Distrito")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede exceder los {1} caracteres.")]
        public District District { get; set; }
    }
}