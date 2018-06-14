using LibraryStorage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;

namespace LibraryStorage
{
    public class LibraryService 
    {
        public DataContext db;

        public LibraryService()
        {
            this.db = new DataContext();
        }

        public User GetUser(int id)
        {
            return db.Users.Where(x => x.Id == id).FirstOrDefault();
        }

        public Book GetBook(int id)
        {
            return db.Books.Where(x => x.Id == id).FirstOrDefault();
        }

        public IQueryable<string> GetBooksSubject()
        {
            return db.Books.Select(b => b.Subject).Distinct();
        }

        public IQueryable<Book> SearchBooksSubject(string subject)
        {
            return db.Books.Where(b => b.Subject == subject);
        }

        public void AddCard(User user)
        {
            Card card = new Card();
            card.User = user;
            db.Cards.Add(card);
            db.SaveChanges();
        }

        public Card GetCard(int id)
        {
            return db.Cards.Include(p => p.User).Where(x => x.Id == id).FirstOrDefault();
        }

        public Card GetCard1(int id)
        {
            return db.Cards.Include(c => c.Books)
                .Include(c=>c.User)
                .Where(c => c.User.Id == id).FirstOrDefault();
        }

        public void AddBookToCard(Card card, Book book)
        {
            db.Cards.Attach(card);
            card.Books.Add(book);
            book.Flag = 0;
            db.SaveChanges();
        }

        public void ChangeCard(Card card)
        {
            db.Entry(card).State = EntityState.Modified;
            db.SaveChanges();
        }

        public List<User> GetUsers()
        {
            return db.Users.ToList();
        }

        public List<Book> GetBooks()
        {
            return db.Books.ToList();
        }

        public List<Book> GetBooksinCard(int id)
        {
            Card card= db.Cards.Include(p => p.User).Where(x => x.Id == id).FirstOrDefault();
            return card.Books.ToList();
        }

        public List<Card> GetCards()
        {
            return db.Cards.Include(p => p.User).ToList();
        }

    }
}
