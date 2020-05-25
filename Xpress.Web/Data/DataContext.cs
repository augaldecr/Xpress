using Microsoft.EntityFrameworkCore;
using Xpress.Web.Data.Entities;
using Xpress.Web.Data.Entities.Common;
using Xpress.Web.Data.Entities.Payments;
using Xpress.Web.Data.Entities.Users;

namespace Xpress.Web.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<County> Counties { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Delivery> Deliveries { get; set; }
        public DbSet<DeliveryDetail> DeliveryDetails { get; set; }
        public DbSet<DeliveryGuy> DeliveryGuys { get; set; }
        public DbSet<DeliveryPayment> DeliveryPayments { get; set; }
        public DbSet<Dispatcher> Dispatchers { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<FranchiseAdmin> FranchiseAdmins { get; set; }
        public DbSet<Franchise> Franchises { get; set; }
        public DbSet<FranchiseType> FranchiseTypes { get; set; }
        public DbSet<MarketSegment> MarketSegments { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductPayment> ProductPayments { get; set; }
        public DbSet<ProductToDeliver> ProductsToDeliver { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<Subsidiary> Subsidiaries { get; set; }
        public DbSet<SubsidiaryAdmin> SubsidiaryAdmins { get; set; }
        public DbSet<SubsidiaryProduct> SubsidiaryProducts { get; set; }
        public DbSet<Town> Towns { get; set; }
    }
}