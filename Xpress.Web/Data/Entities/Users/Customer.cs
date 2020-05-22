using System.Collections.Generic;

namespace Xpress.Web.Data.Entities.Users
{
    public class Customer : User
    {
        public virtual ICollection<Package> Packages { get; set; }
    }
}
