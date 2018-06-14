using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryStorage.Models
{
    public class Card
    {
        public Card()
        {
            Books = new List<Book>();
        }
        public int Id { get; set; }
        public User User { get; set; }
        public ICollection<Book> Books { get; set; }
    }
}
