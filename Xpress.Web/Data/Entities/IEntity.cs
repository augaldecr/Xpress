namespace Xpress.Web.Data.Entities
{
    public interface IEntity
    {
        int Id { get; set; }

        bool Disabled { get; set; }
    }
}
