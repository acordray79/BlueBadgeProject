using BBCoffeeShop.Data;
using BBCoffeeShop.Models.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBCoffeeShop.Services
{
    public class ProductService
    {
        private readonly Guid _userID;

        public ProductService(Guid userID)
        {
            _userID = userID;
        }
        public bool CreateProduct(CreateProduct model)
        {
            var entity =
                new Product()
                {
                    OwnerID = _userID,
                    ProductName = model.ProductName,
                    Price = model.Price,
                    CreatedUtc = DateTimeOffset.Now
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Products.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }
        public IEnumerable<ProductListItem> GetProducts()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Products
                        .Where(e => e.OwnerID == _userID)
                        .Select(
                            e =>
                                new ProductListItem
                                {
                                    ProductID = e.ProductID,
                                    ProductName = e.ProductName,
                                    Price = e.Price,
                                    CreatedUtc = e.CreatedUtc
                                }
                        );

                return query.ToArray();
            }
        }
        public ProductDetail GetProductByID(int productID)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Products
                        .Single(e => e.ProductID == productID && e.OwnerID == _userID);
                return
                    new ProductDetail
                    {
                        ProductID = entity.ProductID,
                        ProductName = entity.ProductName,
                        Price = entity.Price,
                        CreatedUtc = entity.CreatedUtc,
                        ModifiedUtc = entity.ModifiedUtc
                    };
            }
        }
        public bool UpdateProduct(ProductUpdate model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Products
                        .Single(e => e.ProductID == model.ProductID && e.OwnerID == _userID);
                entity.ProductName = model.ProductName;
                entity.Price = model.Price;
                entity.ModifiedUtc = DateTimeOffset.UtcNow;

                return ctx.SaveChanges() == 1;
            }
        }
        public bool DeleteProduct(int noteId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Products
                        .Single(e => e.ProductID == noteId && e.OwnerID == _userID);

                ctx.Products.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}