using Order_ms.Models;

namespace Order_ms.Services
{
    public interface IOrderDataAccess
    {
        Task<List<OrderModel>> GetAllOrders();
        Task SaveOrder(OrderModel orderModel);
    }
}
