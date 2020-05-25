namespace Xpress.Web.Data.Entities.Payments
{
    public abstract class Bill : IEntity
    {
        public int Id { get; set; }

        public Payment Payment { get; set; }
    }
}
