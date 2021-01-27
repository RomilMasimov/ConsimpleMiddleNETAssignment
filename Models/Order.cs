using System;

namespace ConsimpleMiddleNetAssignment.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string Code { get; set; } // номер в тз
        public DateTime OrderDate { get; set; }
        public decimal Amount { get; set; }
    }
}