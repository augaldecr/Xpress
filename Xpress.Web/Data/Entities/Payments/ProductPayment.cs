namespace Xpress.Web.Data.Entities.Payments
{
    public class ProductPayment : Bill
    {
        public ProductToDeliver ProductToDeliver { get; set; }
    }
}