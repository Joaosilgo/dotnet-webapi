

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


            //Events

            var events = new Event[]
          {
           new Event { Title = "OktoberFest", Name= "OktoberFest 2021", Description="The Oktoberfest is the world's largest Volksfest (beer festival and travelling funfair). It is held annually in Munich, Bavaria, Germany. It is a 16 to 18 day folk festival running from mid- or late September to the first Sunday in October, with more than six million people from around the world attending the event every year. Locally, it is called d’Wiesn, after the colloquial name for the fairgrounds, Theresienwiese. The Oktoberfest is an important part of Bavarian culture, having been held since the year 1810. Other cities across the world also hold Oktoberfest celebrations that are modeled after the original Munich event." , Price = "Free", URL="https://dotnet-webapi.herokuapp.com/", DateStart = new DateTime(2021, 09, 10),  DateEnd = new DateTime(2021, 11, 01)  },
           new Event { Title = "Euro", Name= "UEFA EURO 2020", Description="The 2020 UEFA European Football Championship, commonly referred to as UEFA Euro 2020, or simply Euro 2020, is the 16th UEFA European Championship, the quadrennial international men's football championship of Europe organised by the Union of European Football Associations (UEFA)." , Price = "Free", URL="https://dotnet-webapi.herokuapp.com/", DateStart = new DateTime(2021, 09, 10),  DateEnd = new DateTime(2021, 11, 01)  },
           new Event { Title = "The Winter Games", Name= "The Winter Games 2021", Description="The Winter Games are one of the world’s major international sporting events held every four years with winter-themed sports involving snow and ice. It currently is a three-week event with athletes around the world and takes place in February. These top world athletes compete in 15 various sports from ice hockey and figure skating to bobsledding and curling. An Olympic medal is the greatest honor an athlete can complete for, and the national pride and sense of community that these events bring out makes for an unforgettable experience." , Price = "Free", URL="https://dotnet-webapi.herokuapp.com/", DateStart = new DateTime(2021, 09, 10),  DateEnd = new DateTime(2021, 11, 01)  },
           new Event { Title = "Wimbledon, London, UK", Name= "Wimbledon, London, UK 2020", Description="The Wimbledon Tennis Tournament is held every year and is the largest of the Grand Slam tournaments. The only international tennis tournament to be played on grass courts, a lot of care goes into preserving the greens and the formalities of this event. Expect certain traditions to be held in place as you attend – strict dress codes for the athletes, strawberries and cream for the attendees, and because it takes place in the UK, the British Royal family will be there. An elaborate queuing process makes Wimbledon one of very few international sporting events where spectators can obtain tickets the day of" , Price = "Free", URL="https://dotnet-webapi.herokuapp.com/", DateStart = new DateTime(2021, 09, 10),  DateEnd = new DateTime(2021, 11, 01)  },
  new Event { Title = "Cannes Film Festival, Cannes, France", Name= "Cannes Film Festival, Cannes, France 2021", Description="The Cannes Festival, named until 2002 as the International Film Festival (Festival International du Film) and known in English as the Cannes Film Festival, is an annual film festival held in Cannes, France, which previews new films of all genres, including documentaries, from around the world. Founded in 1946, it is considered the most prestigious film festival in the world and is one of the most publicized. The invitation-only festival is held annually (usually in May) at the Palais des Festivals et des Congrès." , Price = "Free", URL="https://dotnet-webapi.herokuapp.com/", DateStart = new DateTime(2021, 09, 10),  DateEnd = new DateTime(2021, 11, 01)  },
new Event { Title = "Day of the Dead, Mexico City, Mexico", Name= "Day of the Dead, Mexico City, Mexico 2021", Description="Day of the Dead is a Mexican holiday celebrated throughout Mexico, in particular, the Central and South regions, and acknowledged around the world in other cultures. The holiday focuses on gatherings of family and friends to pray for and remember friends and family members who have died and help support their spiritual journey. In 2008, the tradition was inscribed in the Representative List of the Intangible Cultural Heritage of Humanity by UNESCO. In Spanish, the holiday is called Día de los Muertos." , Price = "Free", URL="https://dotnet-webapi.herokuapp.com/", DateStart = new DateTime(2021, 09, 10),  DateEnd = new DateTime(2021, 11, 01)  },

          };
            foreach (Event e in events)
            {
                context.Event.Add(e);
            }
            context.SaveChanges();





        }
    }
}