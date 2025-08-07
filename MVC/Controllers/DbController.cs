using APP.Domain;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Text;

namespace MVC.Controllers
{
    public class DbController : Controller
    {
        private readonly Db _db;

        public DbController(Db db)
        {
            _db = db;
        }

        public IActionResult Seed()
        {
            #region Deleting Current Data
            //var productStores = _db.ProductStores.ToList();
            //_db.ProductStores.RemoveRange(productStores);

            //var stores = _db.Stores.ToList();
            //_db.Stores.RemoveRange(stores);

            var products = _db.Products.ToList();
            _db.Products.RemoveRange(products);

            var categories = _db.Categories.ToList();
            _db.Categories.RemoveRange(categories);

            //var users = _db.Users.ToList();
            //_db.Users.RemoveRange(users);

            //var roles = _db.Roles.ToList();
            //_db.Roles.RemoveRange(roles);

            //if (roles.Count > 0) // for reseting role ids to 1
            //{
            //    _db.Database.ExecuteSqlRaw("dbcc CHECKIDENT ('Roles', RESEED, 0)");
            //}
            #endregion

            #region Inserting Initial Data
            //_db.Stores.Add(new Store()
            //{
            //    Name = "Hepsiburada"
            //});
            //_db.Stores.Add(new Store()
            //{
            //    Name = "Vatan"
            //});
            //_db.SaveChanges();

            _db.Categories.Add(new Category()
            {
                Name = "Computer",
                Description = "Includes computer related products such as desktops, laptops, servers, etc.",
                Products = new List<Product>()
                {
                    new Product()
                    {
                        Name = "Laptop",
                        IsContinued = true,
                        UnitPrice = 3000.5m,
                        StockAmount = 100,
                        Description = "Laptops for business operations",
                        //ProductStores = new List<ProductStore>()
                        //{
                        //    new ProductStore()
                        //    {
                        //        StoreId = _db.Stores.SingleOrDefault(s => s.Name == "Hepsiburada").Id
                        //    }
                        //}
                    },
                    new Product()
                    {
                        Name = "Mouse",
                        IsContinued = true,
                        UnitPrice = 20.5m,
                        StockAmount = 500,
                        //ProductStores = new List<ProductStore>()
                        //{
                        //    new ProductStore()
                        //    {
                        //        StoreId = _db.Stores.SingleOrDefault(s => s.Name == "Hepsiburada").Id
                        //    },
                        //    new ProductStore()
                        //    {
                        //        StoreId = _db.Stores.SingleOrDefault(s => s.Name == "Vatan").Id
                        //    }
                        //}
                    },
                    new Product()
                    {
                        Name = "Keyboard",
                        UnitPrice = 40,
                        StockAmount = 400,
                        IsContinued = true,
                        //ProductStores = new List<ProductStore>()
                        //{
                        //    new ProductStore()
                        //    {
                        //        StoreId = _db.Stores.SingleOrDefault(s => s.Name == "Hepsiburada").Id
                        //    },
                        //    new ProductStore()
                        //    {
                        //        StoreId = _db.Stores.SingleOrDefault(s => s.Name == "Vatan").Id
                        //    }
                        //}
                    },
                    new Product()
                    {
                        Name = "Monitor",
                        UnitPrice = 2500,
                        StockAmount = 200,
                        //ProductStores = new List<ProductStore>()
                        //{
                        //    new ProductStore()
                        //    {
                        //        StoreId = _db.Stores.SingleOrDefault(s => s.Name == "Vatan").Id
                        //    }
                        //}
                    }
                }
            });
            _db.Categories.Add(new Category()
            {
                Name = "Home Theater System",
                Description = "Includes home theater system products such as reveivers, speakers, equalizers, etc.",
                Products = new List<Product>()
                {
                    new Product()
                    {
                        Name = "Speaker",
                        UnitPrice = 2500,
                        StockAmount = 50,
                        IsContinued = true
                    },
                    new Product()
                    {
                        Name = "Receiver",
                        UnitPrice = 5000,
                        StockAmount = 25,
                        IsContinued = true,
                        //ProductStores = new List<ProductStore>()
                        //{
                        //    new ProductStore()
                        //    {
                        //        StoreId = _db.Stores.SingleOrDefault(s => s.Name == "Vatan").Id
                        //    }
                        //}
                    },
                    new Product()
                    {
                        Name = "Equalizer",
                        UnitPrice = 1000,
                        StockAmount = 30,
                        //ProductStores = new List<ProductStore>()
                        //{
                        //    new ProductStore()
                        //    {
                        //        StoreId = _db.Stores.SingleOrDefault(s => s.Name == "Hepsiburada").Id
                        //    },
                        //    new ProductStore()
                        //    {
                        //        StoreId = _db.Stores.SingleOrDefault(s => s.Name == "Vatan").Id
                        //    }
                        //}
                    }
                }
            });
            _db.Categories.Add(new Category()
            {
                Name = "Medicine",
                Products = new List<Product>()
                {
                    new Product()
                    {
                        Name = "Majezik",
                        Description = "Medicine products such as pain killers, antibiotics, vitamins, etc.",
                        ExpirationDate = DateTime.Parse("12/23/2026", new CultureInfo("en-US")),
                        UnitPrice = 5,
                        StockAmount = 750,
                        IsContinued = true
                    }
                }
            });

            //_db.Roles.Add(new Role()
            //{
            //    Name = "Admin",
            //    Users = new List<User>()
            //    {
            //        new User()
            //        {
            //            IsActive = true,
            //            Password = "admin",
            //            UserName = "admin"
            //        }
            //    }
            //});
            //_db.Roles.Add(new Role()
            //{
            //    Name = "User",
            //    Users = new List<User>()
            //    {
            //        new User()
            //        {
            //            IsActive = true,
            //            Password = "user",
            //            UserName = "user"
            //        }
            //    }
            //});

            _db.SaveChanges();
            #endregion

            return Content("<label style=\"color:red;\"><b>Database seed successful.</b></label>", "text/html", Encoding.UTF8);
        }
    }
}
