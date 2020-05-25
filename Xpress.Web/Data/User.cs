using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Xpress.Web.Data
{
    public class User : IdentityUser
    {
        [Display(Name = "Last Name")]
        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        public string LastName { get; set; }

        [Display(Name = "Picture")]
        //[Required(ErrorMessage = "The field {0} is required")]
        public string PicturePath { get; set; }

        [Display(Name = "Picture")]
        public string PictureFullPath => string.IsNullOrEmpty(PicturePath)
            ? "https://taxiwebcr.azurewebsites.net//images/noimage.png"
            : $"https://taxicr.blob.core.windows.net/users/{PicturePath}";

        [Display(Name = "Full name")]
        public string FullName => $"{LastName} {FirstName}";
    }
}
