using Microsoft.AspNetCore.Mvc;

namespace Xpress.Web.Models.Users
{
    public class EditUserViewModel : UserViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }
    }
}
