using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_EF_App.Core.Domain
{
    public class Author
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public virtual ICollection<Book> Books { get; set; }
        public Author()
        {

        }
        public Author(string firstname, string lastname)
        {
            Firstname = firstname;
            Lastname = lastname;
            Books = new HashSet<Book>();
        }
    }
}
