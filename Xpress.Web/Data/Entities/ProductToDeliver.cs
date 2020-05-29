namespace Xpress.Web.Data.Entities
{
    public class ProductToDeliver : IEntity
    {
        public int Id { get; set; }

        public SubsidiaryProduct Product { get; set; }

        public Package Package { get; set; }

        public bool Disabled { get; set; }
    }
}
