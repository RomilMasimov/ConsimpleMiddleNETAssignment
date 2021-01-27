using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ConsimpleMiddleNetAssignment.Models;
using ConsimpleMiddleNetAssignment.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace ConsimpleMiddleNetAssignment.Services
{
    public class ShopService
    {
        private readonly ApplicationDbContext _dbContext;

        public ShopService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<ClientBirthdayViewModel> BirthdayList(DateTime birthdayDate)
        {
            return _dbContext.Clients
                .Where(m => m.Birthday.Equals(birthdayDate))
                .Select(m => new ClientBirthdayViewModel()
                {
                    Name = m.Name,
                    Surname = m.Surname,
                    Patronymic = m.Patronymic
                });
        }

        public IEnumerable<RecentlyBuyerViewModel> RecentlyBuyersList(int numberOfDays)
        {
            DateTime fromDate = DateTime.Today.AddDays(-numberOfDays);
            return _dbContext.Orders
                .Include(m => m.Client)
                .Where(m => m.OrderDate >= fromDate)
                .GroupBy(m => m.Client)
                .Select(g => new RecentlyBuyerViewModel()
                {
                    Id = g.Key.Id,
                    LastOrderDate = g.Select(m => m.OrderDate).Max(),
                    Name = g.Key.Name,
                    Surname = g.Key.Surname,
                    Patronymic = g.Key.Patronymic
                });
        }

        public Dictionary<string, int> DemandedCategories(int clientId)
        {
            var ordersId = _dbContext.Orders
                .Where(m => m.ClientId == clientId)
                .Select(m => m.Id);

            return _dbContext.OrderProducts
                .Include(m => m.Product)
                .ThenInclude(m => m.Category)
                .Where(m => ordersId.Any(oi => oi == m.OrderId))
                .GroupBy(m => m.Product.Category.Name)
                .ToDictionary(m => m.Key, 
                    m => m.Sum(p => p.Quantity));
        }
    }
}