using LibraryStorage.Models;
using System;
using System.Data.Entity;
using System.Linq;

namespace LibraryStorage
{
    public class DataContext : DbContext
    {
        public DataContext()
            : base("name=DataContext")
        {
            Database.SetInitializer<DataContext>(new DataInitializer());
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Card> Cards { get; set; }

    }

    class DataInitializer : DropCreateDatabaseAlways<DataContext>
    {
        protected override void Seed(DataContext context)
        {
            base.Seed(context);

            context.Users.Add(new User { Name = "Кравець Анастасія" });
            context.Users.Add(new User { Name = "Литвинюк Дмитро" });
            context.Users.Add(new User { Name = "Макаренко Антон" });
            context.Users.Add(new User { Name = "Сеніва Катерина" });
            context.Users.Add(new User { Name = "Чиж Юлія" });

            context.Books.Add(new Book { Author = "Рей Бредбері", Title = "451 градус за Фаренгейтом", Subject = "роман-антиутопія", Flag = 1 });
            context.Books.Add(new Book { Author = "Джордж Орвелл", Title = "1984", Subject = "роман-антиутопія", Flag = 1 });
            context.Books.Add(new Book { Author = "Михайло Булгаков", Title = "Мастер и Маргарита", Subject = "роман", Flag = 1 });
            context.Books.Add(new Book { Author = "Еріх Марія Ремарк", Title = "Три товариші", Subject = "роман", Flag = 1 });
            context.Books.Add(new Book { Author = "Оскар Уайльд", Title = "Портрет Доріана Грея", Subject = "роман", Flag = 1 });
            context.Books.Add(new Book { Author = "Деніел Кіз", Title = "Квіти для Елджернона", Subject = "твір-розповідь", Flag = 1 });
            context.Books.Add(new Book { Author = "Антуан де Сент-Екзюпері", Title = "Маленький принц", Subject = "філософський роман, казка", Flag = 1 });
            context.Books.Add(new Book { Author = "Лев Толстой", Title = "Анна Каренина", Subject = "роман", Flag = 1 });
            context.Books.Add(new Book { Author = "Федор Достоевский", Title = "Преступление и наказание", Subject = "роман", Flag = 1 });
            context.Books.Add(new Book { Author = "Илья Ильф, Евгений Петров", Title = "Двенадцать стульев", Subject = "роман", Flag = 1 });

            context.SaveChanges();

            foreach (var u in context.Users)
            {
                context.Cards.Add(
                    new Card()
                    {
                        User = u
                    });
            }

            context.SaveChanges();
        }
    }
}