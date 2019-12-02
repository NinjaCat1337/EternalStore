using EternalStore.Api.Contracts.Order.Requests;
using EternalStore.ApplicationLogic.OrderManagement.DTO;
using EternalStore.ApplicationLogic.OrderManagement.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace EternalStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderManager orderManager;

        public OrderController(IOrderManager orderManager) => this.orderManager = orderManager;

        [HttpGet("orders", Name = "GetOrders")]
        public async Task<IActionResult> Get()
        {
            var orders = await orderManager.GetAllOrdersAsync();
            return Ok(orders);
        }

        [HttpGet("orders/{idOrder}", Name = "GetOrder")]
        public async Task<IActionResult> Get(int idOrder)
        {
            var order = await orderManager.GetOrderAsync(idOrder);
            return Ok(order);
        }

        [HttpPost("orders", Name = "AddOrder")]
        public async Task<IActionResult> Post([FromBody] OrderCreationRequest request)
        {
            var order = new OrderDTO
            {
                DeliveryDate = request.DeliveryDate,
                CustomerName = request.CustomerName,
                CustomerNumber = request.CustomerNumber,
                CustomerAddress = request.CustomerAddress,
                AdditionalInformation = request.AdditionalInformation,
                OrderItems = request.OrderItems.ToList()
            };

            var idOrder = await orderManager.CreateOrderAsync(order);

            foreach (var orderItem in request.OrderItems)
            {
                await orderManager.AddOrderItemAsync(idOrder, orderItem.IdCategory, orderItem.IdProduct, orderItem.Qty);
            }

            return Ok();
        }

        [HttpPut("orders/{idOrder}", Name = "EditOrder")]
        public async Task<IActionResult> Put([FromBody] OrderModificationRequest request)
        {
            var order = new OrderDTO
            {
                DeliveryDate = request.DeliveryDate,
                CustomerName = request.CustomerName,
                CustomerNumber = request.CustomerNumber,
                CustomerAddress = request.CustomerAddress,
                AdditionalInformation = request.AdditionalInformation
            };

            await orderManager.UpdateOrderAsync(order);
            return Ok();
        }

        [HttpDelete("order/{idOrder}", Name = "DeleteOrder")]
        public async Task<IActionResult> Delete(int idOrder)
        {
            await orderManager.DeleteOrderAsync(idOrder);
            return Ok();
        }

        [HttpPatch("orders/{idOrder}/approved", Name = "Approved")]
        public async Task<IActionResult> SetApproved(int idOrder)
        {
            await orderManager.SetApprovedAsync(idOrder);
            return Ok();
        }

        [HttpPatch("orders/{idOrder}/delivered", Name = "Delivered")]
        public async Task<IActionResult> SetDelivered(int idOrder)
        {
            await orderManager.SetDeliveredAsync(idOrder);
            return Ok();
        }

        [HttpPost("orders/{idOrder}/items", Name = "AddOrderItem")]
        public async Task<IActionResult> Post([FromBody] OrderItemAdditionRequest request)
        {
            await orderManager.AddOrderItemAsync(request.IdOrder, request.IdCategory, request.IdProduct, request.Qty);
            return Ok();
        }

        [HttpDelete("orders/{idOrder}/items/{idOrderItem}", Name = "RemoveOrderItem")]
        public async Task<IActionResult> Delete([FromBody] OrderItemDeletionRequest request)
        {
            await orderManager.RemoveOrderItemAsync(request.IdOrder, request.IdOrderItem);
            return Ok();
        }
    }
}
