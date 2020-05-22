namespace Xpress.Web.Data.Entities.Users
{
    public class Employee : IEntity
    {
        public int Id { get; set; }

        public User User { get; set; }

        public Subsidiary Subsidiary { get; set; }
    }
}
