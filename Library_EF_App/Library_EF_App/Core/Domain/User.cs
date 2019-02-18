using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_EF_App.Core.Domain
{
    public class User
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public ICollection<Order> Orders { get; set; }

        public User()
        {
            Orders = new HashSet<Order>();
        }
    }
}
