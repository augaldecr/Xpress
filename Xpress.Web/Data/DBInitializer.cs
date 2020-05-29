using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xpress.Common;
using Xpress.Web.Data.Entities;
using Xpress.Web.Data.Entities.Common;
using Xpress.Web.Data.Entities.Payments;
using Xpress.Web.Data.Entities.Users;
using Xpress.Web.Helpers;

namespace Xpress.Web.Data
{
    public class DBInitializer
    {

        private readonly DataContext _dataContext;
        private readonly IUserHelper _userHelper;

        public DBInitializer(DataContext dataContext,
            IUserHelper userHelper)
        {
            _dataContext = dataContext;
            _userHelper = userHelper;
        }

        public async Task Initialize()
        {
            await _dataContext.Database.EnsureCreatedAsync();

            //Geo
            await CheckCountriesAsync();
            await CheckStatesAsync();
            await CheckCountiesAsync();
            await CheckDistrictsAsync();
            await CheckTownsAsync();

            //Payment's section
            await CheckPaymentMethodsAsync();

            //Franchise's section
            await CheckFranchisesTypesAsync();
            await CheckMarketSegmentsAsync();

            //Franchise Example section
            await CheckFranchisesAsync();
            await CheckSubsidiarysAsync();

            await CheckCategoriesAsync();

            //User's section
            await CheckRoles();

            User admin = await CheckUserAsync("Alonso", "Ugalde",
                "augaldecr@gmail.com", "85090266", "Admin");
            User customer = await CheckUserAsync("Alonso", "Ugalde",
                "augaldecr@hotmail.com", "85090266", "Customer");
            User deliveryGuy = await CheckUserAsync("Alonso", "Ugalde",
                "alonsougaldecr@gmail.com", "85090266", "DeliveryGuy");
            User dispatcher = await CheckUserAsync("Alonso", "Ugalde",
                "augaldecr@cajimenez.com", "85090266", "Dispatcher");
            User franchiseAdmin = await CheckUserAsync("Alonso", "Ugalde",
                "profeugaldecr@gmail.com", "85090266", "FranchiseAdmin");
            User subsidiaryAdmin = await CheckUserAsync("Alonso", "Ugalde",
                "alonso.ugalde.aguilar@mep.go.cr", "85090266", "SubsidiaryAdmin");

            await CheckAdminAsync(admin);
            await CheckCustomerAsync(customer);
            await CheckDeliveryGuyAsync(deliveryGuy);
            await CheckDispatcherAsync(dispatcher);
            await CheckFranchiseAdminAsync(franchiseAdmin);
            await CheckSubsidiaryAdminAsync(subsidiaryAdmin);

            //Example's section
            await CheckProductAsync();
            await CheckSubsidiaryProductsAsync();

            await CheckDeliveryAsync();
            //await CheckDeliveryDetailsAsync();

            await CheckPackageAsync();
            await CheckProductToDeliverAsync(); 
            await CheckCardsAsync();
            await CheckPaymentsAsync();
            await CheckProductPayments();
            await CheckDeliveryPayment();
        }

        private async Task CheckDeliveryPayment()
        {
            if (!_dataContext.DeliveryPayments.Any())
            {
                Delivery delivery = await _dataContext.Deliveries.FirstOrDefaultAsync();
                Payment payment = await _dataContext.Payments.FirstOrDefaultAsync(
                    p => p.Amount.Equals("1200"));

                await _dataContext.DeliveryPayments.AddAsync(new DeliveryPayment
                {
                    Delivery = delivery,
                    Payment = payment,
                });
                await _dataContext.SaveChangesAsync();
            }
        }

        private async Task CheckProductPayments()
        {
            if (!_dataContext.ProductPayments.Any())
            {
                ProductToDeliver product1 = await _dataContext.ProductsToDeliver.FirstOrDefaultAsync(
                    p => p.Product.Product.Barcode.Equals("123456789"));
                ProductToDeliver product2 = await _dataContext.ProductsToDeliver.FirstOrDefaultAsync(
                    p => p.Product.Product.Barcode.Equals("2222222"));
                Payment payment1 = await _dataContext.Payments.FirstOrDefaultAsync(
                    p => p.Amount.Equals("800"));
                Payment payment2 = await _dataContext.Payments.FirstOrDefaultAsync(
                    p => p.Amount.Equals("950"));

                await _dataContext.ProductPayments.AddAsync(new ProductPayment
                {
                    ProductToDeliver = product1,
                    Payment = payment1,
                });
                await _dataContext.ProductPayments.AddAsync(new ProductPayment
                {
                    ProductToDeliver = product2,
                    Payment = payment2,
                });
                await _dataContext.SaveChangesAsync();
            }
        }

        private async Task CheckCardsAsync()
        {
            Customer owner = await _dataContext.Customers.FirstOrDefaultAsync();

            if (!_dataContext.Cards.Any())
            {
                await _dataContext.Cards.AddAsync(new Card
                {
                    Number = "1234567891234567",
                    Owner = owner.User,
                    OwnerName = owner.User.FullName,
                    GoodThru = new DateTime(2023, 10, 01),
                    SecurityNumber = "123",
                });
                await _dataContext.Cards.AddAsync(new Card
                {
                    Number = "9876543210234567",
                    Owner = owner.User,
                    OwnerName = owner.User.FullName,
                    GoodThru = new DateTime(2024, 10, 01),
                    SecurityNumber = "321",
                });
                await _dataContext.SaveChangesAsync();
            }
        }

        private async Task CheckPaymentsAsync()
        {
            if (!_dataContext.Payments.Any())
            {
                PaymentMethod paymentMethod = await _dataContext.PaymentMethods.FirstOrDefaultAsync();
                Customer customer = await _dataContext.Customers.FirstOrDefaultAsync();

                Payment payment1 = new Payment
                {
                    Amount = 800,
                    Date = new DateTime(2020, 05, 24),
                    PaymentMethod = paymentMethod,
                    User = customer.User,
                };
                Payment payment2 = new Payment
                {
                    Amount = 950,
                    Date = new DateTime(2020, 05, 24),
                    PaymentMethod = paymentMethod,
                    User = customer.User,
                };
                Payment payment3 = new Payment
                {
                    Amount = 1200,
                    Date = new DateTime(2020, 05, 24),
                    PaymentMethod = paymentMethod,
                    User = customer.User,
                };

                await _dataContext.Payments.AddAsync(payment1);
                await _dataContext.Payments.AddAsync(payment2);
                await _dataContext.Payments.AddAsync(payment3);
                await _dataContext.SaveChangesAsync();
            }
        }

        private async Task CheckPackageAsync()
        {
            Customer customer = await _dataContext.Customers.FirstOrDefaultAsync();
            Dispatcher dispatcher = await _dataContext.Dispatchers.FirstOrDefaultAsync();
            Delivery delivery = await _dataContext.Deliveries.FirstOrDefaultAsync();

            if (!_dataContext.Packages.Any())
            {
                await _dataContext.Packages.AddAsync(new Package
                {
                    Customer = customer.User,
                    Sender = dispatcher.User,
                    Price = 1750,
                    Delivery = delivery,
                });
                await _dataContext.SaveChangesAsync();
            }
        }

        private async Task CheckProductToDeliverAsync()
        {
            if (!_dataContext.ProductsToDeliver.Any())
            {
                SubsidiaryProduct product1 = await _dataContext.SubsidiaryProducts.FirstOrDefaultAsync(
                    p => p.Product.Barcode.Equals("123456789"));
                SubsidiaryProduct product2 = await _dataContext.SubsidiaryProducts.FirstOrDefaultAsync(
                    p => p.Product.Barcode.Equals("2222222"));
                Package package = await _dataContext.Packages.FirstOrDefaultAsync();

                await _dataContext.ProductsToDeliver.AddAsync(new ProductToDeliver
                {
                    Product = product1,
                    Package = package,
                });
                await _dataContext.ProductsToDeliver.AddAsync(new ProductToDeliver
                {
                    Product = product2,
                    Package = package,
                });
                await _dataContext.SaveChangesAsync();
            }
        }

        private async Task CheckProductAsync()
        {
            Franchise franchiseSuper = await _dataContext.Franchises.FirstOrDefaultAsync(
                f => f.LegalId.Equals("0000"));
            Franchise franchiseTaqueria = await _dataContext.Franchises.FirstOrDefaultAsync(
                f => f.LegalId.Equals("0001"));
            Category category1 = await _dataContext.Categories.FirstOrDefaultAsync(
                c => c.Name.Equals("Abarrote"));
            Category category2 = await _dataContext.Categories.FirstOrDefaultAsync(
                c => c.Name.Equals("Comida mejicana"));

            if (!_dataContext.Products.Any())
            {
                await _dataContext.Products.AddAsync(new Product
                {
                    Name = "Caja de leche",
                    Barcode = "123456789",
                    Description = "Caja de litro de leche Dos Pinos",
                    Franchise = franchiseSuper,
                    PicturePath = "./images/noimage.png",
                    Price = 800,
                    Category = category1,
                });
                await _dataContext.Products.AddAsync(new Product
                {
                    Name = "Bolsa de café",
                    Barcode = "2222222",
                    Description = "Bolsa de café 1820 350 gr",
                    Franchise = franchiseSuper,
                    PicturePath = "./images/noimage.png",
                    Price = 950,
                    Category = category1,
                });
                await _dataContext.Products.AddAsync(new Product
                {
                    Name = "Hamburguesa",
                    Barcode = "987654321",
                    Description = "Hamburguesa con torta de carne",
                    Franchise = franchiseTaqueria,
                    PicturePath = "./images/noimage.png",
                    Price = 1200,
                    Category = category2,
                });
                await _dataContext.SaveChangesAsync();
            }
        }

        private Task CheckDeliveryDetailsAsync()
        {
            throw new NotImplementedException();
        }

        private async Task CheckDeliveryAsync()
        {
            if (!_dataContext.Deliveries.Any())
            {
                DeliveryGuy deliveryGuy = await _dataContext.DeliveryGuys.FirstOrDefaultAsync();

                await _dataContext.Deliveries.AddAsync(new Delivery
                {
                    CreationDate = new DateTime(2020, 05, 24),
                    SendDate = new DateTime(2020, 05, 24),
                    DeliveryDate = new DateTime(2020, 05, 24),
                    Commission = 10,
                    CommissionBilled = 120,
                    Price = 1200,
                    Qualification = 4,
                    Remarks = "Excelente servicio",
                    DeliveryGuy = deliveryGuy,
                    Source = "Mini super",
                    SourceLatitude = 10.209025,
                    SourceLongitude = -83.798296,
                    Target = "Mi casa",
                    TargetLatitude = 10.212478,
                    TargetLongitude = -83.798241,
                    State = DeliveryState.Delivered,
                });
                await _dataContext.SaveChangesAsync();
            }
        }

        private async Task CheckSubsidiaryProductsAsync()
        {
            Subsidiary subsidiarySuper = await _dataContext.Subsidiaries.FirstOrDefaultAsync(
                s => s.Franchise.Name.Equals("Super El buen precio"));
            Subsidiary subsidiaryTaqueria = await _dataContext.Subsidiaries.FirstOrDefaultAsync(
                s => s.Franchise.Name.Equals("Taquería La Esquina"));
            Product cajaLeche = await _dataContext.Products.FirstOrDefaultAsync(
                p => p.Barcode.Equals("123456789"));
            Product bolsaCafe = await _dataContext.Products.FirstOrDefaultAsync(
                p => p.Barcode.Equals("2222222"));
            Product hamburguesa = await _dataContext.Products.FirstOrDefaultAsync(
                p => p.Barcode.Equals("987654321"));

            if (!_dataContext.SubsidiaryProducts.Any())
            {
                await _dataContext.SubsidiaryProducts.AddAsync(new SubsidiaryProduct
                {
                    Subsidiary = subsidiarySuper,
                    Product = cajaLeche,
                    Rating = 4,
                    Active = true,
                });
                await _dataContext.SubsidiaryProducts.AddAsync(new SubsidiaryProduct
                {
                    Subsidiary = subsidiarySuper,
                    Product = bolsaCafe,
                    Rating = 4.75,
                    Active = true,
                });
                await _dataContext.SubsidiaryProducts.AddAsync(new SubsidiaryProduct
                {
                    Subsidiary = subsidiaryTaqueria,
                    Product = hamburguesa,
                    Rating = 4.5,
                    Active = true,
                });
                await _dataContext.SaveChangesAsync();
            }
        }

        private async Task CheckSubsidiarysAsync()
        {
            Franchise franchiseSuper = await _dataContext.Franchises.FirstOrDefaultAsync(
                f => f.LegalId.Equals("0000"));
            Franchise franchiseTaqueria = await _dataContext.Franchises.FirstOrDefaultAsync(
                f => f.LegalId.Equals("0001"));
            Town town = await _dataContext.Towns.FirstOrDefaultAsync(
                t => t.Name.Equals("Guápiles"));

            if (!_dataContext.Subsidiaries.Any())
            {
                await _dataContext.Subsidiaries.AddAsync(new Subsidiary
                {
                    Address = "A 100 mts de ahí",
                    Franchise = franchiseSuper,
                    CardOnly = false,
                    Town = town,
                });
                await _dataContext.Subsidiaries.AddAsync(new Subsidiary
                {
                    Address = "A 300 mts de ahí",
                    Franchise = franchiseTaqueria,
                    CardOnly = true,
                    Town = town,
                });
                await _dataContext.SaveChangesAsync();
            }
        }

        private async Task CheckMarketSegmentsAsync()
        {
            if (!_dataContext.MarketSegments.Any())
            {
                await _dataContext.MarketSegments.AddAsync(new MarketSegment
                {
                    Name = "General",
                });
                await _dataContext.MarketSegments.AddAsync(new MarketSegment
                {
                    Name = "Asian food",
                });
                await _dataContext.MarketSegments.AddAsync(new MarketSegment
                {
                    Name = "Mexican food",
                });
                await _dataContext.MarketSegments.AddAsync(new MarketSegment
                {
                    Name = "Criollo food",
                });
                await _dataContext.SaveChangesAsync();
            }
        }

        private async Task CheckPaymentMethodsAsync()
        {
            if (!_dataContext.PaymentMethods.Any())
            {
                await _dataContext.PaymentMethods.AddAsync(new PaymentMethod
                {
                    Name = "Card",
                });
                await _dataContext.PaymentMethods.AddAsync(new PaymentMethod
                {
                    Name = "Cash",
                });
                await _dataContext.SaveChangesAsync();
            }
        }

        private async Task CheckCategoriesAsync()
        {
            var franchiseRest = await _dataContext.Franchises.FirstOrDefaultAsync(
                f => f.LegalId == "0001");
            var franchiseSuper = await _dataContext.Franchises.FirstOrDefaultAsync(
                f => f.LegalId == "0000");

            if (!_dataContext.Categories.Any())
            {
                await _dataContext.Categories.AddAsync(new Category
                {
                    Name = "Comida mejicana",
                    Franchise = franchiseRest,
                });
                await _dataContext.Categories.AddAsync(new Category
                {
                    Name = "Abarrote",
                    Franchise = franchiseSuper,
                });
                await _dataContext.SaveChangesAsync();
            }
        }

        private async Task CheckFranchisesTypesAsync()
        {
            if (!_dataContext.FranchiseTypes.Any())
            {
                await _dataContext.FranchiseTypes.AddAsync(new FranchiseType
                {
                    Name = "Fast food",
                });
                await _dataContext.FranchiseTypes.AddAsync(new FranchiseType
                {
                    Name = "MiniSuper",
                });
                await _dataContext.SaveChangesAsync();
            }
        }

        private async Task CheckFranchisesAsync()
        {
            FranchiseType super = await _dataContext.FranchiseTypes.FirstOrDefaultAsync(
                t => t.Name.Equals("MiniSuper"));
            FranchiseType fastFood = await _dataContext.FranchiseTypes.FirstOrDefaultAsync(
                t => t.Name.Equals("Fast food"));
            MarketSegment marketSegment1 = await _dataContext.MarketSegments.FirstOrDefaultAsync(
                m => m.Name.Equals("General"));
            MarketSegment marketSegment2 = await _dataContext.MarketSegments.FirstOrDefaultAsync(
                m => m.Name.Equals("Mexican food"));

            if (!_dataContext.Franchises.Any())
            {
                await _dataContext.Franchises.AddAsync(new Franchise
                {
                    LegalId = "0000",
                    LegalName = "Huang S.A.",
                    Name = "Super El buen precio",
                    FranchiseType = super,
                    PicturePath = "./images/noimage.png",
                    MarketSegment = marketSegment1,
                    Rating = 5,
                });
                await _dataContext.Franchises.AddAsync(new Franchise
                {
                    LegalId = "0001",
                    LegalName = "José Pérez",
                    Name = "Taquería La Esquina",
                    FranchiseType = fastFood,
                    PicturePath = "./images/noimage.png",
                    MarketSegment = marketSegment1,
                    Rating = 5,
                });
                await _dataContext.SaveChangesAsync();
            }
        }

        #region User's
        private async Task CheckRoles()
        {
            await _userHelper.CheckRoleAsync("Admin");
            await _userHelper.CheckRoleAsync("Customer");
            await _userHelper.CheckRoleAsync("DeliveryGuy");
            await _userHelper.CheckRoleAsync("Dispatcher");
            await _userHelper.CheckRoleAsync("FranchiseAdmin");
            await _userHelper.CheckRoleAsync("SubsidiaryAdmin");
        }

        private async Task CheckFranchiseAdminAsync(User franchiseAdmin)
        {
            Franchise franchise = await _dataContext.Franchises.FirstOrDefaultAsync();

            if (!_dataContext.FranchiseAdmins.Any())
            {
                await _dataContext.FranchiseAdmins.AddAsync(new FranchiseAdmin
                { User = franchiseAdmin, Franchise = franchise });
                await _dataContext.SaveChangesAsync();
            }
        }

        private async Task CheckSubsidiaryAdminAsync(User subsidiaryAdmin)
        {
            Subsidiary subsidiary = await _dataContext.Subsidiaries.FirstOrDefaultAsync();

            if (!_dataContext.SubsidiaryAdmins.Any())
            {
                await _dataContext.SubsidiaryAdmins.AddAsync(new SubsidiaryAdmin
                { User = subsidiaryAdmin, Subsidiary = subsidiary });
                await _dataContext.SaveChangesAsync();
            }
        }

        private async Task CheckDispatcherAsync(User dispatcher)
        {
            Subsidiary subsidiary = await _dataContext.Subsidiaries.FirstOrDefaultAsync();
            if (!_dataContext.Dispatchers.Any())
            {
                await _dataContext.Dispatchers.AddAsync(new Dispatcher
                { User = dispatcher, Subsidiary = subsidiary });
                await _dataContext.SaveChangesAsync();
            }
        }

        private async Task CheckDeliveryGuyAsync(User deliveryGuy)
        {
            if (!_dataContext.DeliveryGuys.Any())
            {
                await _dataContext.DeliveryGuys.AddAsync(new DeliveryGuy { User = deliveryGuy });
                await _dataContext.SaveChangesAsync();
            }
        }

        private async Task CheckCustomerAsync(User user)
        {
            if (!_dataContext.Customers.Any())
            {
                Customer customer = new Customer
                {
                    User = user,
                };

                await _dataContext.Customers.AddAsync(customer);

                await _dataContext.SaveChangesAsync();
            }
        }

        private async Task CheckAdminAsync(User user)
        {
            if (!_dataContext.Admins.Any())
            {
                _dataContext.Admins.Add(new Admin { User = user });
                await _dataContext.SaveChangesAsync();
            }
        }

        private async Task<User> CheckUserAsync(string firstName, string lastName,
            string email, string phone, string role)
        {
            User user = await _userHelper.GetUserByEmailAsync(email);

            if (user == null)
            {
                Town town = _dataContext.Towns.FirstOrDefault(t => t.Name.Equals("Guápiles"));

                user = new User
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    UserName = email,
                    PhoneNumber = phone,
                    EmailConfirmed = true,
                };

                await _userHelper.AddUserAsync(user, "123456");
                await _userHelper.AddUserToRoleAsync(user, role);
            }

            return user;
        }
        #endregion

        #region Geo
        private async Task CheckTownsAsync()
        {
            if (!_dataContext.Towns.Any())
            {
                //Select the districts
                District guapiles = _dataContext.Districts.FirstOrDefault(d => d.Name.Equals("Guápiles"));
                District guacimo = _dataContext.Districts.FirstOrDefault(d => d.Name.Equals("Guápiles"));

                //Add the towns
                await _dataContext.Towns.AddAsync(new Town { Name = "Guápiles", District = guapiles });
                await _dataContext.Towns.AddAsync(new Town { Name = "Guácimo", District = guacimo });
            }
        }

        private async Task CheckDistrictsAsync()
        {
            if (!_dataContext.Districts.Any())
            {
                await AddSanJoseProvinceDistritsAsync();
                await AddLimonProvinceDistrictsAsync();
            }
        }

        private async Task CheckCountiesAsync()
        {
            State sanJose = _dataContext.States.FirstOrDefault(p => p.Name.Equals("San José"));
            State alajuela = _dataContext.States.FirstOrDefault(p => p.Name.Equals("Alajuela"));
            State cartago = _dataContext.States.FirstOrDefault(p => p.Name.Equals("Cartago"));
            State heredia = _dataContext.States.FirstOrDefault(p => p.Name.Equals("Heredia"));
            State guanacaste = _dataContext.States.FirstOrDefault(p => p.Name.Equals("Guanacaste"));
            State puntarenas = _dataContext.States.FirstOrDefault(p => p.Name.Equals("Puntarenas"));
            State limon = _dataContext.States.FirstOrDefault(p => p.Name.Equals("Limón"));

            if (!_dataContext.Counties.Any())
            {
                await AddSanJoseCountiesAsync(sanJose);
                await AddAlajuelaCountiesAsync(alajuela);
                await AddCartagoCountiesAsync(cartago);
                await AddHerediaCountiesAsync(heredia);
                await AddGuanacasteCountiesAsync(guanacaste);
                await AddPuntarenasCountiesAsync(puntarenas);
                await AddLimonCountiesAsync(limon);
            }
        }

        private async Task CheckStatesAsync()
        {
            Country country = _dataContext.Countries.FirstOrDefault(p => p.Name.Equals("Costa Rica"));
            if (!_dataContext.States.Any())
            {
                await _dataContext.States.AddAsync(new State { Name = "San José", Country = country });
                await _dataContext.States.AddAsync(new State { Name = "Alajuela", Country = country });
                await _dataContext.States.AddAsync(new State { Name = "Cartago", Country = country });
                await _dataContext.States.AddAsync(new State { Name = "Heredia", Country = country });
                await _dataContext.States.AddAsync(new State { Name = "Guanacaste", Country = country });
                await _dataContext.States.AddAsync(new State { Name = "Puntarenas", Country = country });
                await _dataContext.States.AddAsync(new State { Name = "Limón", Country = country });
                await _dataContext.SaveChangesAsync();
            }
        }

        private async Task CheckCountriesAsync()
        {
            if (!_dataContext.Countries.Any())
            {
                await _dataContext.Countries.AddAsync(new Country { Name = "Costa Rica" });
                await _dataContext.Countries.AddAsync(new Country { Name = "Panamá" });
                await _dataContext.SaveChangesAsync();
            }
        }

        private async Task AddSanJoseCountiesAsync(State state)
        {
            await _dataContext.Counties.AddAsync(new County { Name = "San José", State = state });
            await _dataContext.Counties.AddAsync(new County { Name = "Escazú", State = state });
            await _dataContext.Counties.AddAsync(new County { Name = "Desamparados", State = state });
            await _dataContext.Counties.AddAsync(new County { Name = "Puriscal", State = state });
            await _dataContext.Counties.AddAsync(new County { Name = "Tarrazú", State = state });
            await _dataContext.Counties.AddAsync(new County { Name = "Aserrí", State = state });
            await _dataContext.Counties.AddAsync(new County { Name = "Mora", State = state });
            await _dataContext.Counties.AddAsync(new County { Name = "Goicoechea", State = state });
            await _dataContext.Counties.AddAsync(new County { Name = "Santa Ana", State = state });
            await _dataContext.Counties.AddAsync(new County { Name = "Alajuelita", State = state });
            await _dataContext.Counties.AddAsync(new County { Name = "Vázquez de Coronado", State = state });
            await _dataContext.Counties.AddAsync(new County { Name = "Acosta", State = state });
            await _dataContext.Counties.AddAsync(new County { Name = "Tibás", State = state });
            await _dataContext.Counties.AddAsync(new County { Name = "Moravia", State = state });
            await _dataContext.Counties.AddAsync(new County { Name = "Montes de Oca", State = state });
            await _dataContext.Counties.AddAsync(new County { Name = "Turrubares", State = state });
            await _dataContext.Counties.AddAsync(new County { Name = "Dota", State = state });
            await _dataContext.Counties.AddAsync(new County { Name = "Curridabat", State = state });
            await _dataContext.Counties.AddAsync(new County { Name = "Pérez Zeledón", State = state });
            await _dataContext.Counties.AddAsync(new County { Name = "León Cortés Castro", State = state });
            await _dataContext.SaveChangesAsync();
        }

        private async Task AddAlajuelaCountiesAsync(State state)
        {
            await _dataContext.Counties.AddAsync(new County { Name = "Alajuela", State = state });
            await _dataContext.Counties.AddAsync(new County { Name = "San Ramón", State = state });
            await _dataContext.Counties.AddAsync(new County { Name = "Grecia", State = state });
            await _dataContext.Counties.AddAsync(new County { Name = "San Mateo", State = state });
            await _dataContext.Counties.AddAsync(new County { Name = "Atenas", State = state });
            await _dataContext.Counties.AddAsync(new County { Name = "Naranjo", State = state });
            await _dataContext.Counties.AddAsync(new County { Name = "Palmares", State = state });
            await _dataContext.Counties.AddAsync(new County { Name = "Poás", State = state });
            await _dataContext.Counties.AddAsync(new County { Name = "Orotina", State = state });
            await _dataContext.Counties.AddAsync(new County { Name = "San Carlos", State = state });
            await _dataContext.Counties.AddAsync(new County { Name = "Zarcero", State = state });
            await _dataContext.Counties.AddAsync(new County { Name = "Sarchí", State = state });
            await _dataContext.Counties.AddAsync(new County { Name = "Upala", State = state });
            await _dataContext.Counties.AddAsync(new County { Name = "Los Chiles", State = state });
            await _dataContext.Counties.AddAsync(new County { Name = "Guatuso", State = state });
            await _dataContext.Counties.AddAsync(new County { Name = "Río Cuarto", State = state });
            await _dataContext.SaveChangesAsync();
        }

        private async Task AddCartagoCountiesAsync(State state)
        {
            await _dataContext.Counties.AddAsync(new County { Name = "Cartago", State = state });
            await _dataContext.Counties.AddAsync(new County { Name = "Paraíso", State = state });
            await _dataContext.Counties.AddAsync(new County { Name = "La Unión", State = state });
            await _dataContext.Counties.AddAsync(new County { Name = "Jiménez", State = state });
            await _dataContext.Counties.AddAsync(new County { Name = "Turrialba", State = state });
            await _dataContext.Counties.AddAsync(new County { Name = "Alvarado", State = state });
            await _dataContext.Counties.AddAsync(new County { Name = "Oreamuno", State = state }); await _dataContext.Counties.AddAsync(new County { Name = "San Carlos", State = state });
            await _dataContext.Counties.AddAsync(new County { Name = "El Guarco", State = state });
            await _dataContext.SaveChangesAsync();
        }

        private async Task AddHerediaCountiesAsync(State state)
        {
            await _dataContext.Counties.AddAsync(new County { Name = "Heredia", State = state });
            await _dataContext.Counties.AddAsync(new County { Name = "Barva", State = state });
            await _dataContext.Counties.AddAsync(new County { Name = "Santo Domingo", State = state });
            await _dataContext.Counties.AddAsync(new County { Name = "Santa Bárbara", State = state });
            await _dataContext.Counties.AddAsync(new County { Name = "San Rafael", State = state });
            await _dataContext.Counties.AddAsync(new County { Name = "San Isidro", State = state }); await _dataContext.Counties.AddAsync(new County { Name = "San Carlos", State = state });
            await _dataContext.Counties.AddAsync(new County { Name = "Belén", State = state });
            await _dataContext.Counties.AddAsync(new County { Name = "Flores", State = state });
            await _dataContext.Counties.AddAsync(new County { Name = "San Pablo", State = state });
            await _dataContext.Counties.AddAsync(new County { Name = "Sarapiquí", State = state });
            await _dataContext.SaveChangesAsync();
        }

        private async Task AddGuanacasteCountiesAsync(State state)
        {
            await _dataContext.Counties.AddAsync(new County { Name = "Liberia", State = state });
            await _dataContext.Counties.AddAsync(new County { Name = "Nicoya", State = state });
            await _dataContext.Counties.AddAsync(new County { Name = "Santa Cruz", State = state });
            await _dataContext.Counties.AddAsync(new County { Name = "Bagaces", State = state });
            await _dataContext.Counties.AddAsync(new County { Name = "Carrillo", State = state });
            await _dataContext.Counties.AddAsync(new County { Name = "Cañas", State = state });
            await _dataContext.Counties.AddAsync(new County { Name = "Abangares", State = state });
            await _dataContext.Counties.AddAsync(new County { Name = "Tilarán", State = state });
            await _dataContext.Counties.AddAsync(new County { Name = "Nandayure", State = state });
            await _dataContext.Counties.AddAsync(new County { Name = "La Cruz", State = state });
            await _dataContext.Counties.AddAsync(new County { Name = "Hojancha", State = state });
            await _dataContext.SaveChangesAsync();
        }

        private async Task AddPuntarenasCountiesAsync(State state)
        {
            await _dataContext.Counties.AddAsync(new County { Name = "Puntarenas", State = state });
            await _dataContext.Counties.AddAsync(new County { Name = "Esparza", State = state });
            await _dataContext.Counties.AddAsync(new County { Name = "Buenos Aires", State = state });
            await _dataContext.Counties.AddAsync(new County { Name = "Montes de Oro", State = state });
            await _dataContext.Counties.AddAsync(new County { Name = "Osa", State = state });
            await _dataContext.Counties.AddAsync(new County { Name = "Quepos", State = state });
            await _dataContext.Counties.AddAsync(new County { Name = "Golfito", State = state });
            await _dataContext.Counties.AddAsync(new County { Name = "Coto Brus", State = state });
            await _dataContext.Counties.AddAsync(new County { Name = "Parrita", State = state });
            await _dataContext.Counties.AddAsync(new County { Name = "Corredores", State = state });
            await _dataContext.Counties.AddAsync(new County { Name = "Garabito", State = state });
            await _dataContext.SaveChangesAsync();
        }

        private async Task AddLimonCountiesAsync(State state)
        {
            await _dataContext.Counties.AddAsync(new County { Name = "Limón", State = state });
            await _dataContext.Counties.AddAsync(new County { Name = "Pococí", State = state });
            await _dataContext.Counties.AddAsync(new County { Name = "Siquirres", State = state });
            await _dataContext.Counties.AddAsync(new County { Name = "Talamanca", State = state });
            await _dataContext.Counties.AddAsync(new County { Name = "Matina", State = state });
            await _dataContext.Counties.AddAsync(new County { Name = "Guácimo", State = state });
            await _dataContext.SaveChangesAsync();
        }

        private async Task AddSanJoseProvinceDistritsAsync()
        {
            await AddSanJoseDistrictsAsync();
            /*await AddEscazuDistrictsAsync();
            await AddDesamparadosDistrictsAsync();
            await AddPuriscalDistrictsAsync();
            await AddTarrazuDistrictsAsync();
            await AddAserriDistrictsAsync();
            await AddMoraDistrictsAsync();
            await AddGoicoecheaDistrictsAsync();
            await AddSantaAnaDistrictsAsync();
            await AddAlajuelitaDistrictsAsync();
            await AddVasquezDeCoronadoDistrictsAsync();
            await AddAcostaDistrictsAsync();
            await AddTibasDistrictsAsync();
            await AddMoraviaDistrictsAsync();
            await AddMontesDeOcaDistrictsAsync();
            await AddTurrubaresDistrictsAsync();
            await AddDotaDistrictsAsync();
            await AddCurridabatDistrictsAsync();
            await AddPerezZeledonDistrictsAsync();
            await AddLeonCortesCastroDistrictsAsync();    */
        }

        private async Task AddLimonProvinceDistrictsAsync()
        {
            await AddLimonDistrictsAsync();
            await AddPocociDistrictsAsync();
            await AddSiquirresDistrictsAsync();
            await AddTalamancaDistrictsAsync();
            await AddMatinaDistrictsAsync();
            await AddGuacimoDistrictsAsync();
        }

        private async Task AddGuacimoDistrictsAsync()
        {
            County county = _dataContext.Counties.FirstOrDefault(p => p.Name.Equals("Guácimo"));
            await _dataContext.Districts.AddAsync(new District { Name = "Guácimo", County = county });
            await _dataContext.Districts.AddAsync(new District { Name = "Mercedes", County = county });
            await _dataContext.Districts.AddAsync(new District { Name = "Pocora", County = county });
            await _dataContext.Districts.AddAsync(new District { Name = "Río Jiménez", County = county });
            await _dataContext.Districts.AddAsync(new District { Name = "Duacarí", County = county });
            await _dataContext.SaveChangesAsync();
        }

        private async Task AddMatinaDistrictsAsync()
        {
            County county = _dataContext.Counties.FirstOrDefault(p => p.Name.Equals("Matina"));
            await _dataContext.Districts.AddAsync(new District { Name = "Matina", County = county });
            await _dataContext.Districts.AddAsync(new District { Name = "Bataán", County = county });
            await _dataContext.Districts.AddAsync(new District { Name = "Carrandí", County = county });
            await _dataContext.SaveChangesAsync();
        }

        private async Task AddTalamancaDistrictsAsync()
        {
            County county = _dataContext.Counties.FirstOrDefault(p => p.Name.Equals("Talamanca"));
            await _dataContext.Districts.AddAsync(new District { Name = "Bratsi", County = county });
            await _dataContext.Districts.AddAsync(new District { Name = "Sixaola", County = county });
            await _dataContext.Districts.AddAsync(new District { Name = "Cahuita", County = county });
            await _dataContext.Districts.AddAsync(new District { Name = "Telire", County = county });
            await _dataContext.SaveChangesAsync();
        }

        private async Task AddSiquirresDistrictsAsync()
        {
            County county = _dataContext.Counties.FirstOrDefault(p => p.Name.Equals("Siquirres"));
            await _dataContext.Districts.AddAsync(new District { Name = "Siquirres", County = county });
            await _dataContext.Districts.AddAsync(new District { Name = "Pacuarito", County = county });
            await _dataContext.Districts.AddAsync(new District { Name = "Florida", County = county });
            await _dataContext.Districts.AddAsync(new District { Name = "Germania", County = county });
            await _dataContext.Districts.AddAsync(new District { Name = "El Cairo", County = county });
            await _dataContext.Districts.AddAsync(new District { Name = "Alegría", County = county });
            await _dataContext.Districts.AddAsync(new District { Name = "Reventazón", County = county });
            await _dataContext.SaveChangesAsync();
        }

        private async Task AddPocociDistrictsAsync()
        {
            County county = _dataContext.Counties.FirstOrDefault(p => p.Name.Equals("Pococí"));
            await _dataContext.Districts.AddAsync(new District { Name = "Guápiles", County = county });
            await _dataContext.Districts.AddAsync(new District { Name = "Jiménez", County = county });
            await _dataContext.Districts.AddAsync(new District { Name = "La Rita", County = county });
            await _dataContext.Districts.AddAsync(new District { Name = "Roxana", County = county });
            await _dataContext.Districts.AddAsync(new District { Name = "Cariari", County = county });
            await _dataContext.Districts.AddAsync(new District { Name = "Colorado", County = county });
            await _dataContext.Districts.AddAsync(new District { Name = "La Colonia", County = county });
            await _dataContext.SaveChangesAsync();
        }

        private async Task AddLimonDistrictsAsync()
        {
            County county = _dataContext.Counties.FirstOrDefault(p => p.Name.Equals("Limón"));
            await _dataContext.Districts.AddAsync(new District { Name = "Limón", County = county });
            await _dataContext.Districts.AddAsync(new District { Name = "Valle La Estrella", County = county });
            await _dataContext.Districts.AddAsync(new District { Name = "Río Blanco", County = county });
            await _dataContext.Districts.AddAsync(new District { Name = "Matama", County = county });
            await _dataContext.SaveChangesAsync();
        }

        private Task AddTurrubaresDistrictsAsync()
        {
            throw new NotImplementedException();
        }

        private Task AddDotaDistrictsAsync()
        {
            throw new NotImplementedException();
        }

        private Task AddCurridabatDistrictsAsync()
        {
            throw new NotImplementedException();
        }

        private Task AddPerezZeledonDistrictsAsync()
        {
            throw new NotImplementedException();
        }

        private Task AddLeonCortesCastroDistrictsAsync()
        {
            throw new NotImplementedException();
        }

        private Task AddMontesDeOcaDistrictsAsync()
        {
            throw new NotImplementedException();
        }

        private Task AddTibasDistrictsAsync()
        {
            throw new NotImplementedException();
        }

        private Task AddMoraviaDistrictsAsync()
        {
            throw new NotImplementedException();
        }

        private Task AddVasquezDeCoronadoDistrictsAsync()
        {
            throw new NotImplementedException();
        }

        private Task AddAcostaDistrictsAsync()
        {
            throw new NotImplementedException();
        }

        private Task AddAlajuelitaDistrictsAsync()
        {
            throw new NotImplementedException();
        }

        private Task AddSantaAnaDistrictsAsync()
        {
            throw new NotImplementedException();
        }

        private Task AddGoicoecheaDistrictsAsync()
        {
            throw new NotImplementedException();
        }

        private Task AddMoraDistrictsAsync()
        {
            throw new NotImplementedException();
        }

        private Task AddAserriDistrictsAsync()
        {
            throw new NotImplementedException();
        }

        private Task AddTarrazuDistrictsAsync()
        {
            throw new NotImplementedException();
        }

        private Task AddPuriscalDistrictsAsync()
        {
            throw new NotImplementedException();
        }

        private Task AddDesamparadosDistrictsAsync()
        {
            throw new NotImplementedException();
        }

        private Task AddEscazuDistrictsAsync()
        {
            throw new NotImplementedException();
        }

        private async Task AddSanJoseDistrictsAsync()
        {
            County county = _dataContext.Counties.FirstOrDefault(p => p.Name.Equals("San José"));
            await _dataContext.Districts.AddAsync(new District { Name = "Carmen", County = county });
            await _dataContext.Districts.AddAsync(new District { Name = "Merced", County = county });
            await _dataContext.Districts.AddAsync(new District { Name = "Hospital", County = county });
            await _dataContext.Districts.AddAsync(new District { Name = "Catedral", County = county });
            await _dataContext.Districts.AddAsync(new District { Name = "Zapote", County = county });
            await _dataContext.Districts.AddAsync(new District { Name = "San Francisco de Dos Ríos", County = county });
            await _dataContext.Districts.AddAsync(new District { Name = "La Uruca", County = county });
            await _dataContext.Districts.AddAsync(new District { Name = "Mata Redonda", County = county });
            await _dataContext.Districts.AddAsync(new District { Name = "Pavas", County = county });
            await _dataContext.Districts.AddAsync(new District { Name = "Hatillo", County = county });
            await _dataContext.Districts.AddAsync(new District { Name = "San Sebastián", County = county });
            await _dataContext.SaveChangesAsync();
        }
        #endregion
    }
}