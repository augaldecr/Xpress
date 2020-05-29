namespace Xpress.Web.Data.Entities.Users
{
    public class FranchiseAdmin : IEntity 
    {
        public int Id { get; set; }

        public User User { get; set; }

        public Franchise Franchise { get; set; }

        public bool Disabled { get; set; }
    }
}