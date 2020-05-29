namespace Xpress.Web.Data.Entities.Users
{
    public class Admin : IEntity
    {
        public int Id { get; set; }

        public User User { get; set; }

        public bool Disabled { get; set; }
    }
}
