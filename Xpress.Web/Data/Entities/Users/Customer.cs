using System.Collections.Generic;

namespace Xpress.Web.Data.Entities.Users
{
    public class Customer : IEntity
    {
        public int Id { get; set; }

        public User User { get; set; }

        public virtual ICollection<Package> Packages { get; set; }

        public bool Disabled { get; set; }
    }
}
