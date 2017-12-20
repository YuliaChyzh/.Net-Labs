/* Labwork 5
 * Autor Yulia Chyzh
 * 
 * Розробити базу даних, що зберігає інформацію про телефони (назва, ціна, виробник),
 * відповідно дані про виробника та модель.
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;

namespace Lab6
{
    class Program
    {
        public class Company
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public ICollection<Phone> Phones { get; set; }
            public Company()
            {
                Phones = new List<Phone>();
            }
        }

        public class Phone
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int Price { get; set; }
            public int CompanyId { get; set; }
            public Company Company { get; set; }
        }
        public class Model
        {
            public string Name { get; set; }
            public string Company { get; set; }
            public int Price { get; set; }
        }

        class PhoneContext : DbContext
        {
            static PhoneContext()
            {
                Database.SetInitializer(new MyContextInitializer());
            }
            public PhoneContext()
                : base("DefaultConnection")
            { }

            public DbSet<Company> Companies { get; set; }
            public DbSet<Phone> Phones { get; set; }
        }

        class MyContextInitializer : DropCreateDatabaseAlways<PhoneContext>
        {
            protected override void Seed(PhoneContext db)
            {
                Company c1 = new Company { Name = "Samsung" };
                Company c2 = new Company { Name = "Apple" };
                db.Companies.Add(c1);
                db.Companies.Add(c2);

                db.SaveChanges();

                Phone p1 = new Phone { Name = "Samsung Galaxy S5", Price = 20000, Company = c1 };
                Phone p2 = new Phone { Name = "Samsung Galaxy S4", Price = 15000, Company = c1 };
                Phone p3 = new Phone { Name = "iPhone5", Price = 28000, Company = c2 };
                Phone p4 = new Phone { Name = "iPhone 4S", Price = 23000, Company = c2 };

                db.Phones.AddRange(new List<Phone>() { p1, p2, p3, p4 });
                db.SaveChanges();
            }
        }

        static void Queries()
        {
            Console.WriteLine("1) Вибiрка елементiв, у яких назва компанiї Samsung:");
            using (PhoneContext db = new PhoneContext())
            {
                var phones = db.Phones.Where(p => p.Company.Name == "Samsung");
                foreach (Phone p in phones)
                    Console.WriteLine("{0}.{1} - {2}", p.Id, p.Name, p.Price);
                Console.WriteLine();
            }
            Console.WriteLine("2) Проекцiя вибiрки на тип Model: ");
            using (PhoneContext db = new PhoneContext())
            {
                var phones = db.Phones.Select(p => new Model
                {
                    Name = p.Name,
                    Price = p.Price,
                    Company = p.Company.Name
                });
                foreach (Model p in phones)
                    Console.WriteLine("{0} ({1}) - {2}", p.Name, p.Company, p.Price);
                Console.WriteLine();
            }
            Console.WriteLine("3) Сортування (OrderBy): ");
            using (PhoneContext db = new PhoneContext())
            {
                var phones = db.Phones.OrderBy(p => p.Name);
                foreach (Phone p in phones)
                    Console.WriteLine("{0}.{1} - {2}", p.Id, p.Name, p.Price);
                Console.WriteLine();
            }
            Console.WriteLine("4) Об'єднання таблиць (Join): ");
            using (PhoneContext db = new PhoneContext())
            {
                var phones = from p in db.Phones
                             join c in db.Companies on p.CompanyId equals c.Id
                             select new { Name = p.Name, Company = c.Name, Price = p.Price };
                foreach (var p in phones)
                    Console.WriteLine("{0} ({1}) - {2}", p.Name, p.Company, p.Price);
                Console.WriteLine();
            }
            Console.WriteLine("5) Групування моделi по виробнику (GroupBy): ");
            using (PhoneContext db = new PhoneContext())
            {
                var groups = from p in db.Phones
                             group p by p.Company.Name into g
                             select new { Name = g.Key, Count = g.Count() };
                foreach (var c in groups)
                    Console.WriteLine("Manufacturer: {0} Count of model: {1}", c.Name, c.Count);
                Console.WriteLine();
            }
            Console.WriteLine("6) Об'єднання двох вибiрок (Union): ");
            using (PhoneContext db = new PhoneContext())
            {
                var phones = db.Phones.Where(p => p.Price < 25000)
                    .Union(db.Phones.Where(p => p.Name.Contains("Samsung")));
                foreach (var item in phones)
                    Console.WriteLine(item.Name);
                Console.WriteLine();
            }
            //використання агрегатних функцій
            Console.WriteLine("7) Пошук к-тi елементiв у вибiрцi (Count): ");
            using (PhoneContext db = new PhoneContext())
            {
                int number1 = db.Phones.Count();
                int number2 = db.Phones.Count(p => p.Name.Contains("Samsung"));

                Console.WriteLine(number1);
                Console.WriteLine(number2);
                Console.WriteLine();
            }
            Console.WriteLine("8) Мiнiмальна i максимальна середня цiна по моделях (Min, Max, Average): ");
            using (PhoneContext db = new PhoneContext())
            {
                int minPrice = db.Phones.Min(p => p.Price);
                int maxPrice = db.Phones.Max(p => p.Price);
                double avgPrice = db.Phones.Where(p => p.Company.Name == "Samsung")
                                    .Average(p => p.Price);

                Console.WriteLine(minPrice);
                Console.WriteLine(maxPrice);
                Console.WriteLine(avgPrice);
            }
        }

        static void Main(string[] args)
        {
            Queries();
            Console.ReadKey();
        }

    }
}
