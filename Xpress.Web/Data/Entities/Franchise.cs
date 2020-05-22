using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Xpress.Web.Data.Entities
{
    //Name of the Franchise 
    public class Franchise : BasicEntity
    {
        [Display(Name = "Legal ID")]
        [Required(ErrorMessage = "The field {0} is required")]
        public string LegalId { get; set; }

        [Display(Name = "Legal name")]
        [Required(ErrorMessage = "The field {0} is required")]
        public string LegalName { get; set; }

        //Logo
        [Display(Name = "Picture")]
        [Required(ErrorMessage = "The field {0} is required")]
        public string PicturePath { get; set; }

        [Display(Name = "Picture")]
        public string PictureFullPath => string.IsNullOrEmpty(PicturePath)
            ? "https://taxiwebcr.azurewebsites.net//images/noimage.png"
            : $"https://taxicr.blob.core.windows.net/users/{PicturePath}";

        //Supermarket, restaurant, Drugstore, etc
        [Display(Name = "Franchise type")]
        [Required(ErrorMessage = "The field {0} is required")]
        public FranchiseType FranchiseType { get; set; }

        //Asian, Mexican, Criollo, Medicine, Combini, etc
        [Display(Name = "Market segment")]
        public MarketSegment MarketSegment { get; set; }

        public int Rating { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}