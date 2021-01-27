using System;
using System.Collections;
using System.Collections.Generic;

namespace ConsimpleMiddleNetAssignment.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public DateTime Birthday { get; set; }
        public DateTime RegistrationDate { get; set; }

        public IEnumerable<Order> Orders { get; set; }
    }
}