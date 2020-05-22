using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Xpress.Common;
using Xpress.Web.Data.Entities.Users;

namespace Xpress.Web.Data.Entities
{
    public class Delivery
    {
        [Display(Name = "Delivery guy")]
        [Required(ErrorMessage = "The field {0} is required")]
        public DeliveryGuy DeliveryGuy { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Start date")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm}", ApplyFormatInEditMode = false)]
        [Required(ErrorMessage = "The field {0} is required")]
        public DateTime CreationDate { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Start date")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm}", ApplyFormatInEditMode = false)]
        public DateTime CreationDateLocal => CreationDate.ToLocalTime();

        [DataType(DataType.DateTime)]
        [Display(Name = "Start date")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm}", ApplyFormatInEditMode = false)]
        [Required(ErrorMessage = "The field {0} is required")]
        public DateTime SendDate { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Start date")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm}", ApplyFormatInEditMode = false)]
        public DateTime SendDateLocal => SendDate.ToLocalTime();

        [DataType(DataType.DateTime)]
        [Display(Name = "Start date")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm}", ApplyFormatInEditMode = false)]
        public DateTime? DeliveryDate { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Start date")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm}", ApplyFormatInEditMode = false)]
        public DateTime? DeliveryDateLocal => DeliveryDate?.ToLocalTime();

        [MaxLength(500, ErrorMessage = "The field {0} must have {1} characters")]
        [Required(ErrorMessage = "The field {0} is required")]
        public string Source { get; set; }

        [MaxLength(500, ErrorMessage = "The field {0} must have {1} characters")]
        [Required(ErrorMessage = "The field {0} is required")]
        public string Target { get; set; }

        public float Qualification { get; set; }

        [Display(Name = "Source latitude")]
        [Required(ErrorMessage = "The field {0} is required")]
        public double SourceLatitude { get; set; }

        [Display(Name = "Source longitude")]
        [Required(ErrorMessage = "The field {0} is required")]
        public double SourceLongitude { get; set; }

        [Display(Name = "Target latitude")]
        [Required(ErrorMessage = "The field {0} is required")]
        public double TargetLatitude { get; set; }

        [Display(Name = "Target longitude")]
        [Required(ErrorMessage = "The field {0} is required")]
        public double TargetLongitude { get; set; }

        public string Remarks { get; set; }

        //Price of the delivery
        [Required(ErrorMessage = "The field {0} is required")]
        public double Price { get; set; }

        //Percentage of my commission
        [Required(ErrorMessage = "The field {0} is required")]
        public double Commission { get; set; }

        //Value (money) of my commission
        [Display(Name = "Comission")]
        [Required(ErrorMessage = "The field {0} is required")]
        public double CommissionBilled { get; set; }

        public DeliveryState State { get; set; }

        public virtual ICollection<DeliveryDetail> DeliveryDetails { get; set; }
    }
}
