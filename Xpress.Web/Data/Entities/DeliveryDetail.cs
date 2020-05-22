using System;
using System.ComponentModel.DataAnnotations;

namespace Xpress.Web.Data.Entities
{
    public class DeliveryDetail : IEntity
    {
        public int Id { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm}", ApplyFormatInEditMode = false)]
        [Required(ErrorMessage = "The field {0} is required")]
        public DateTime Date { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm}", ApplyFormatInEditMode = false)]
        public DateTime DateLocal => Date.ToLocalTime();

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        public Delivery Delivery { get; set; }
    }
}
