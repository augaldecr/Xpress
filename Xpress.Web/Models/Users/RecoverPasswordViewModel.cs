using System.ComponentModel.DataAnnotations;

namespace Xpress.Web.Models.Users
{
    public class RecoverPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}