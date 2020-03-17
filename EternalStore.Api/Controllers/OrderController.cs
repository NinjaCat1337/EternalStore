using EternalStore.Api.Contracts.Order.Requests;
using EternalStore.Api.Contracts.Order.Responses;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using EternalStore.ApplicationLogic.StoreManagement.DTO;
using EternalStore.ApplicationLogic.StoreManagement.Interfaces;

namespace EternalStore.Api.Controllers
{
    [Route("api/store/")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderManager orderManager;

        public OrderController(IOrderManager orderManager) => this.orderManager = orderManager;

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "1")]
        [HttpGet("orders", Name = "GetOrders")]
        public async Task<IActionResult> Get(int count, int page, bool ascending)
        {
            var ordersToSkip = (page - 1) * count;
            var (responseOrders, allOrdersCount) = await orderManager.GetAllOrdersAsync(ordersToSkip, count, ascending);

            var response = new GetOrdersResponse
            {
                Orders = responseOrders.ToList(),
                OrdersCount = allOrdersCount
            };

            return Ok(response);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "1")]
        [HttpGet("orders/{idOrder}", Name = "GetOrder")]
        public async Task<IActionResult> Get(int idOrder)
        {
            var order = await orderManager.GetOrderAsync(idOrder);
            return Ok(order);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "1")]
        [HttpPost("orders/search", Name = "Search")]
        public async Task<IActionResult> Search([FromBody]OrderSearchRequest request)
        {
            var ordersToSkip = (request.Page - 1) * request.Count;
            var (responseOrders, filteredOrdersCount) = await orderManager.SearchOrdersAsync(ordersToSkip, request.Count, request.Ascending,
                request.OrderDateFrom, request.OrderDateTo, request.DeliveryDateFrom, request.DeliveryDateTo, request.IsApproved, request.IsDelivered);

            var ordersForResponse = responseOrders.ToList();

            var response = new GetOrdersResponse
            {
                Orders = ordersForResponse,
                OrdersCount = filteredOrdersCount
            };

            return Ok(response);
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

            var response = new OrderCreationResponse { IdOrder = idOrder };

            return Ok(response);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "1")]
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

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "1")]
        [HttpDelete("order/{idOrder}", Name = "DeleteOrder")]
        public async Task<IActionResult> Delete(int idOrder)
        {
            await orderManager.DeleteOrderAsync(idOrder);
            return Ok();
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "1")]
        [HttpPatch("orders/{idOrder}/approved", Name = "Approved")]
        public async Task<IActionResult> SetApproved(int idOrder)
        {
            await orderManager.SetApprovedAsync(idOrder);
            return Ok();
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "1")]
        [HttpPatch("orders/{idOrder}/delivered", Name = "Delivered")]
        public async Task<IActionResult> SetDelivered(int idOrder)
        {
            await orderManager.SetDeliveredAsync(idOrder);
            return Ok();
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "1")]
        [HttpPost("orders/{idOrder}/items", Name = "AddOrderItem")]
        public async Task<IActionResult> Post([FromBody] OrderItemAdditionRequest request)
        {
            await orderManager.AddOrderItemAsync(request.IdOrder, request.IdCategory, request.IdProduct, request.Qty);
            return Ok();
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "1")]
        [HttpDelete("orders/{idOrder}/items/{idOrderItem}", Name = "RemoveOrderItem")]
        public async Task<IActionResult> Delete([FromBody] OrderItemDeletionRequest request)
        {
            await orderManager.RemoveOrderItemAsync(request.IdOrder, request.IdOrderItem);
            return Ok();
        }
    }
}
