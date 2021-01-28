using System;
using System.Threading.Tasks;
using ConsimpleMiddleNetAssignment.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConsimpleMiddleNetAssignment.Controllers
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    [Produces("application/json")]
    public class ShopController : ControllerBase
    {
        private readonly ShopService _service;

        public ShopController(ShopService service)
        {
            _service = service;
        }

        /// <summary>
        /// Список именинников
        /// </summary>
        /// <param name="birthdayDate"></param>
        /// <returns>
        /// Возвращает список клиентов (id, ФИО) у которых сегодня день рождение.
        /// </returns>
        [HttpGet("BirthdayList/{birthdayDate}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetBirthdayList(DateTime birthdayDate)
        {
            if (birthdayDate > DateTime.Now)
            {
                ModelState.AddModelError("ClientId", "Enter an existing date.");
                return ValidationProblem();
            }

            return Ok(_service.BirthdayList(birthdayDate));
        }

        /// <summary>
        /// Последние покупатели
        /// </summary>
        /// <param name="numberOfDays"></param>
        /// <returns>
        /// Возвращает список клиентов (id, ФИО), совершивших покупку за последние N
        /// дней.Для каждого клиента также необходимо выводить дату его последней
        /// покупки.
        /// </returns>
        [HttpGet("RecentlyBuyersList/{numberOfDays}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetRecentlyBuyersList(int numberOfDays)
        {
            if (numberOfDays < 0)
                return BadRequest();

            return Ok(_service.RecentlyBuyersList(numberOfDays));
        }

        /// <summary>
        /// Востребованные категории
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns>
        /// Возвращает список категорий продуктов, которые покупал найденный клиент.
        /// Для каждой категории возвращает количество купленных единиц.
        /// </returns>
        [HttpGet("PopularCategories/{clientId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetPopularCategories(int clientId)
        {
            if (clientId <= 0)
                return BadRequest();

            var client = await _service.GetClientAsync(clientId);
            if (client == null)
            {
                ModelState.AddModelError("ClientId", "User don't exist.");
                return ValidationProblem();
            }

            return Ok(_service.DemandedCategories(clientId));
        }
    }
}