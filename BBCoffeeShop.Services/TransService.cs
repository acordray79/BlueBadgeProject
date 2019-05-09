using BBCoffeeShop.Data;
using BBCoffeeShop.Models.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBCoffeeShop.Services
{
    public class TransService
    {
        public bool CreateTrans(TransCreate model)
        {
            var entity =
                new Transaction()
                {
                    CustomerID = model.CustomerID,
                    ProductID = model.ProductID,
                    CreatedUtc = DateTimeOffset.Now
                };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Transactions.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }
        public IEnumerable<TransListItem> GetTrans()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Transactions
                        .Select(
                            e =>
                                new TransListItem
                                {
                                    TransactionID = e.TransactionID,
                                    CustomerID = e.CustomerID,
                                    ProductID = e.ProductID,
                                    CreatedUtc = e.CreatedUtc
                                }
                        );

                return query.ToArray();
            }
        }
        public TransDetail GetTransByID(int transID)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Transactions
                        .Single(e => e.TransactionID == transID);
                return
                    new TransDetail
                    {
                        TransactionID = entity.TransactionID,
                        CustomerName = entity.Customer.CustomerName,
                        ProductName = entity.Product.ProductName,
                        CreatedUtc = entity.CreatedUtc,
                        ModifiedUtc = entity.ModifiedUtc
                    };
            }
        }
        public bool UpdateTrans(TransUpdate model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Transactions
                        .Single(e => e.TransactionID == model.TransactionID);
                entity.CustomerID = model.CustomerID;
                entity.ProductID = model.ProductID;
                entity.ModifiedUtc = DateTimeOffset.UtcNow;

                return ctx.SaveChanges() == 1;
            }
        }
        public bool DeleteTrans(int transId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Transactions
                        .Single(e => e.TransactionID == transId);

                ctx.Transactions.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
