using System;

namespace ConsimpleMiddleNetAssignment.ViewModels
{
    public class RecentlyBuyerViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public DateTime LastOrderDate { get; set; }
    }
}