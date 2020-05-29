using System.Collections.Generic;

namespace Xpress.Web.Data.Entities.Users
{
    public class DeliveryGuy : IEntity
    {
        public int Id { get; set; }

        public User User { get; set; }

        public virtual ICollection<Delivery> Deliveries { get; set; }

        public bool Disabled { get; set; }
    }
}
