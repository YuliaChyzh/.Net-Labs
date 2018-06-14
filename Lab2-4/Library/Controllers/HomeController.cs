using LibraryStorage;
using LibraryStorage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library.Controllers
{
    public class HomeController : Controller
    {
        LibraryService service = new LibraryService();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ShowBook()
        {
            IEnumerable<Book> books = service.GetBooks();
            return View(books);
        }

        public ActionResult ShowBookwithChoose(int? id)
        {
            User user = service.GetUser(Convert.ToInt32(id));
            Card card = service.GetCard1(Convert.ToInt32(id));

            card.User = user;
            //card.Books= service.s();

            ViewBag.userId = user.Id;

            IEnumerable<Book> books = service.GetBooks();
            return View(books);
        }


        public ActionResult ShowUser()
        {
            IEnumerable<User> users = service.GetUsers();
            return View(users);
        }

        [HttpGet]
        public ActionResult Search(string searchTitle, string searchAuthor, string searchSubject)
        {
            var books = from m in service.GetBooks() select m;
            //var books = service.GetBooks();

            //var subjects = service.GetBooksSubject();

            if (!String.IsNullOrEmpty(searchTitle))
            {
                books = service.GetBooks().Where(s => s.Title.Contains(searchTitle));
            }

            if (!String.IsNullOrEmpty(searchAuthor))
            {
                books = service.GetBooks().Where(s => s.Author.Contains(searchAuthor));
            }

            if (!String.IsNullOrEmpty(searchSubject))
            {
                
                books = service.SearchBooksSubject(searchSubject);
            }

            //ViewBag.selectSubject = subjects;

            return View(books);
        }

        [HttpPost]
        public string Search(string subject, string searchString, bool notUsed)
        {
            return "From [HttpPost]Search: filter on " + searchString +" or "+subject;
        }

        [HttpGet]
        public ActionResult AddBook(int? id, int idUser)
        {
            //User user = service.GetUser(Convert.ToInt32(idUser));
            Card card = service.GetCard1(Convert.ToInt32(idUser));

            //List<Book> books = service.GetBooksinCard(Convert.ToInt32(idUser));

            //SelectList selectBook = new SelectList(from m in service.GetBooks() select m);

            Book book = service.GetBook(Convert.ToInt32(id));

            if ((card.Books.Count < 10) && (book.Flag==1))
            {
                service.AddBookToCard(card, book);
            }

            ViewBag.userName = card.User.Name;
            ViewBag.listbooks = card.Books;

            return View("ShowCard", card);

        }

        [HttpPost]
        public ActionResult AddBook(User user)
        {
            service.AddCard(user);
            return RedirectToAction("Index");
        }

        public ActionResult ShowCard(int? idUser)
        {
            Card card = service.GetCard1(Convert.ToInt32(idUser));
            ViewBag.userName = card.User.Name;
            ViewBag.listbooks = card.Books;
            return View(card);
        }

    }
}