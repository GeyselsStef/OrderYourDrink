using ChoseYourDrink.BLL;
using Microsoft.AspNetCore.Mvc;
using OrderYourDrink.ViewModels;

namespace OrderYourDrink.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderDrinkController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IQueueService _queueService;

        public OrderDrinkController(ILogger<OrderDrinkController> logger, IQueueService queueService)
        {
            _logger = logger;
            _queueService = queueService;
        }

        [HttpPost(Name = "OrderDrink")]
        public async Task<IActionResult> OrderDrink([FromBody] DrinkOrderItemViewModel order)
        {
            _logger.LogInformation("Order received: {order}", order);

            try
            {
                if (order?.Drink == null || string.IsNullOrWhiteSpace(order.Drink.DrinkName))
                {
                    return BadRequest("DrinkName is empty");
                }

                if (order?.User == null || order.User.Age < 18)
                {
                    return BadRequest("You cannot order a drink if you are under 18 years old.");
                }

                string id = await _queueService.SendMessageAsync(order, _queueService.MessageQueues["OrderQueue"]);
                return Ok($"Your drink is ordered: {order.Drink.DrinkName} ({id})");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
