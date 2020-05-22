using System.Collections.Generic;

namespace Xpress.Web.Data.Entities.Users
{
    public class DeliveryGuy : User
    {
        public virtual ICollection<Delivery> Deliveries { get; set; }
    }
}
