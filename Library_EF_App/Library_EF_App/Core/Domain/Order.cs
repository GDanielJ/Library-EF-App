using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_EF_App.Core.Domain
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime CheckOutDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }

        public Order(DateTime checkOutDate, DateTime returnDate, int userId)
        {
            CheckOutDate = checkOutDate;
            ReturnDate = returnDate;
            UserId = userId;
        }
    }
}
