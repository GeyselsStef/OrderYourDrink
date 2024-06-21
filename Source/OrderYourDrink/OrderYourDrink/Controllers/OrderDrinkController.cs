using Microsoft.AspNetCore.Mvc;
using OrderYourDrink.ViewModels;

namespace OrderYourDrink.Controllers
{
    public class OrderDrinkController : ControllerBase
    {
        private readonly ILogger _logger;

        public OrderDrinkController(ILogger logger)
        {
            this._logger = logger;
        }

        [HttpPost(Name = "OrderDrink")]
        public IActionResult OrderDrink([FromBody] DrinkOrderItemViewModel order)
        {
            _logger.LogInformation("Order received: {order}", order);

            // Process the order
            return Ok();
        }
    }
}
