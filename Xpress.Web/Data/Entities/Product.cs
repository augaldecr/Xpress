using System.ComponentModel.DataAnnotations;

namespace Xpress.Web.Data.Entities
{
    //Item to sell
    public class Product : BasicEntity
    {
        [MaxLength(500, ErrorMessage = "The field {0} must have {1} characters")]
        public string Description { get; set; }

        public string Barcode { get; set; }

        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "The field {0} is required")]
        [MaxLength(50, ErrorMessage = "The field {0} must have {1} characters")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public double Price { get; set; }

        [Display(Name = "Picture")]
        [Required(ErrorMessage = "The field {0} is required")]
        public string PicturePath { get; set; }

        [Display(Name = "Picture")]
        public string PictureFullPath => string.IsNullOrEmpty(PicturePath)
            ? "https://taxiwebcr.azurewebsites.net//images/noimage.png"
            : $"https://taxicr.blob.core.windows.net/users/{PicturePath}";
    }
}