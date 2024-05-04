using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Order_ms.Models;
using Order_ms.Services;
using RabbitMq;
using RabbitMq_Messages;

namespace Order_ms.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ISendEndpointProvider _sendEndpointProvider;
        private readonly IOrderDataAccess _orderDataAccess;

        public OrderController(
          ISendEndpointProvider sendEndpointProvider,
          IOrderDataAccess orderDataAccess
          )
        {
            _sendEndpointProvider = sendEndpointProvider;
            _orderDataAccess = orderDataAccess;

        }

        [HttpPost]
        [Route("createorder")]
        public async Task<IActionResult> CreateOrder([FromBody] OrderModel orderModel)
        {
            //Save it in DB

            await _orderDataAccess.SaveOrder(orderModel);

            //Queue Order

            var endPoint = await _sendEndpointProvider.
                GetSendEndpoint(new Uri("queue:" + BusConstants.OrderQueue));

            await endPoint.Send<IOrderMessage>(new
            {
                OrderId = orderModel.OrderId,
                ProductName = orderModel.ProductName,
                PaymentCardNumber = orderModel.CardNumber
            });

            return Ok("Success");
        }

        [HttpGet]
        [Route("getAll")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(_orderDataAccess.GetAllOrders().Result);
        }

        [HttpPost]
        [Route("createorderstatemachine")]
        public async Task<IActionResult> CreateOrderUsingStateMachine([FromBody] OrderModel orderModel)
        {
            var endpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri("queue:" + BusConstants.SagaBusQueue));

            await endpoint.Send<IOrderStartEvent>(new
            {
                OrderId = orderModel.OrderId,
                PaymentCardNumber = orderModel.CardNumber,
                ProductName = orderModel.ProductName
            });

            return Ok("Success");
        }
    }
}
