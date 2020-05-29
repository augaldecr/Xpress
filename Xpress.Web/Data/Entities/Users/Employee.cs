namespace Xpress.Web.Data.Entities.Users
{
    public abstract class Employee : IEntity
    {
        public int Id { get; set; }

        public User User { get; set; }

        public Subsidiary Subsidiary { get; set; }

        public bool Disabled { get; set; }
    }
}
