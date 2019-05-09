using BBCoffeeShop.Data;
using BBCoffeeShop.Models.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBCoffeeShop.Services
{
    public class CustomerService
    {
        private readonly Guid _userID;

        public CustomerService(Guid userID)
        {
            _userID = userID;
        }
        public bool CreateCustomer(CustomerCreate model)
        {
            var entity =
                new Customer()
                {
                    OwnerID = _userID,
                    CustomerName = model.CustomerName,
                    CustomerEmail = model.CustomerEmail,
                    CreditCard = model.CreditCard,
                    CreatedUtc = DateTimeOffset.Now
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Customers.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }
        public IEnumerable<CustomerListItem> GetCustomer()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Customers
                        .Select(
                            e =>
                                new CustomerListItem
                                {
                                    CustomerID = e.CustomerID,
                                    CustomerName = e.CustomerName,
                                    CustomerEmail = e.CustomerEmail,
                                    CreditCard = e.CreditCard,
                                    CreatedUtc = e.CreatedUtc
                                }
                        );

                return query.ToArray();
            }
        }
        public CustomerDetail GetCustomerByID(int customerID)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Customers
                        .Single(e => e.CustomerID == customerID);
                return
                    new CustomerDetail
                    {
                        CustomerID = entity.CustomerID,
                        CustomerName = entity.CustomerName,
                        CustomerEmail = entity.CustomerEmail,
                        CreditCard = entity.CreditCard,
                        CreatedUtc = entity.CreatedUtc,
                        ModifiedUtc = entity.ModifiedUtc
                    };
            }
        }
        public bool UpdateCustomer(CustomerUpdate model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Customers
                        .Single(e => e.CustomerID == model.CustomerID && e.OwnerID == _userID);
                entity.CustomerName = model.CustomerName;
                entity.CustomerEmail = model.CustomerEmail;
                entity.CreditCard = model.CreditCard;
                entity.ModifiedUtc = DateTimeOffset.UtcNow;

                return ctx.SaveChanges() == 1;
            }
        }
        public bool DeleteCustomer(int customerId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Customers
                        .Single(e => e.CustomerID == customerId && e.OwnerID == _userID);

                ctx.Customers.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
