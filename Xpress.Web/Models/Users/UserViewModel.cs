using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Xpress.Web.Models.Users
{
    public class UserViewModel
    {
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [Display(Name = "Nombre")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [Display(Name = "Apellido(s)")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener más de {1} carácteres.")]
        public string LastName { get; set; }

        [Display(Name = "Phone Number")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener más de {1} carácteres.")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Picture")]
        public IFormFile PictureFile { get; set; }

        [Display(Name = "Picture")]
        public string PicturePath { get; set; }

        [Display(Name = "Picture")]
        public string PictureFullPath => string.IsNullOrEmpty(PicturePath)
            ? "https://taxiwebcr.azurewebsites.net//images/noimage.png"
            : $"https://taxicr.blob.core.windows.net/users/{PicturePath}";
    }
}