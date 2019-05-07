using BBCoffeeShop.Data;
using BBCoffeeShop.Models.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBCoffeeShop.Services
{
    public class AdminService
    {
        private readonly Guid _userID;

        public AdminService(Guid userID)
        {
            _userID = userID;
        }
        public bool CreateAdmin(AdminCreate model)
        {
            var entity =
                new Admin()
                {
                    OwnerID = _userID,
                    AdminName = model.AdminName
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Admins.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }
        public IEnumerable<AdminListItem> GetCustomer()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Admins
                        .Where(e => e.OwnerID == _userID)
                        .Select(
                            e =>
                                new AdminListItem
                                {
                                    AdminID = e.AdminID,
                                    AdminName = e.AdminName
                                }
                        );

                return query.ToArray();
            }
        }
        public AdminDetail GetAdminByID(int adminID)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Admins
                        .Single(e => e.AdminID == adminID && e.OwnerID == _userID);
                return
                    new AdminDetail
                    {
                        AdminID = entity.AdminID,
                        AdminName = entity.AdminName
                    };
            }
        }
        public bool UpdateAdmin(AdminUpdate model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Admins
                        .Single(e => e.AdminID == model.AdminID && e.OwnerID == _userID);
                entity.AdminName = model.AdminName;

                return ctx.SaveChanges() == 1;
            }
        }
        public bool DeleteAdmin(int adminId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Admins
                        .Single(e => e.AdminID == adminId && e.OwnerID == _userID);

                ctx.Admins.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
