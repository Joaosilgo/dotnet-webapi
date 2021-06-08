

using dotnet_webapi.Models;
using System;
using System.Linq;

namespace dotnet_webapi.Data
{
    public static class DbInitializer
    {
        public static void Initialize(DataContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Category.Any())
            {
                return;   // DB has been seeded
            }

            var categories = new Category[]
            {
            new Category{Name="Cafe/Coffee Spot",Description="A café/coffee spot at an ideal location can prove to be a money-spinning business option. You should ask every young gun out there about their favorite hangout spot, the most common response you are going to get is a café/coffee spot, apart from bars of course. Similar to the restaurant business, the success ratio of this small business model also depends on how you the business owner brings their own flavor to the coffee shop to give it a different taste among the rest of coffee spots. The best part of owning a coffee shop is that caffeine is addictive. So if you deliver great service & quality you will gain loyal customers."},
            new Category{Name="IT & Internet",Description="Development; front end, games, apps, etc.Freelance IT services; Podcasting; Search engine optimization consultant ; Website design and/or maintenance"},
            new Category{Name="Retailer",Description="Owns or works for a retail store (this includes online stores). Store must sell more than one manufacturer’s product line or brand to consumers. Not applicable if you are only selling your own brand. You do not qualify as a Retailer if you only sell one product line or brand. According to our company classifications, if your company owns the brand name on all products sold in your store, you are classified as a Manufacturer."},
            new Category{Name="Food Service",Description="Any business operating in the industry related to preparing, distributing or selling prepared/ready-to-eat foods. Includes restaurants, cafeterias, and catering operations."},
            new Category{Name="Manufacturer",Description="Manufactures finished products that are ready for an end consumer. OR a third party that produces a finished product for a company (Contract Manufacturer). OR private label company utilizing contract manufacturers."},
            new Category{Name="Business Services",Description="Companies who provide support to the business functions of Retailers, Distributors, Health Practitioners, Manufacturers, or Suppliers. Schools and universities, government workers, and those that work for or represent the people and companies in the previous categories in such areas as advertising / public relations, banking, consultants, labs, research firms, transportation/logistics companies, demonstration/sampling companies, packaging, and warehousing."},
            new Category{Name="Investor",Description="Business investors are organizations or a group of people who give capital to start or grow a business and in exchange for this, they gain some control of the business and formulate agreements."},
            new Category{Name="Distributor (Finished Goods)",Description="Includes Wholesalers, Distributors, Brokers, Importer and Exporters of finished products. To qualify as a Distributor, you must be a 3rd party distributor of more than one manufacturer's product line or brand in addition to any brands owned by your company."}
            };
            foreach (Category s in categories)
            {
                context.Category.Add(s);
            }
            context.SaveChanges();

            var businesses = new Business[]
            {
            new Business{CategoryId=1,Name="Cornucopia",Description="",MobilePhone="+351 964 575 619"},
            new Business{CategoryId=2,Name="The Corner Store",Description="",MobilePhone="+351 964 575 619"},
            new Business{CategoryId=3,Name="Sweet Spot",Description="",MobilePhone="+351 964 575 619"},
            new Business{CategoryId=4,Name="Decorama Boutique",Description="",MobilePhone="+351 964 575 619"},
            new Business{CategoryId=5,Name="One of a Kind Studio",Description="",MobilePhone="+351 964 575 619"},
            new Business{CategoryId=6,Name="Not Just Groceries",Description="",MobilePhone="+351 964 575 619"},
            new Business{CategoryId=7,Name="The Full Cart",Description="",MobilePhone="+351 964 575 619"}
            };
            foreach (Business c in businesses)
            {
                context.Business.Add(c);
            }
            context.SaveChanges();


            var users = new User[]
           {
           new User { Id = 1, Username = "batman", Password = "batman", Role = "manager" },
           new User { Id = 2, Username = "robin", Password = "robin", Role = "employee" }
           };
            foreach (User u in users)
            {
                context.User.Add(u);
            }
            context.SaveChanges();





        }
    }
}