using Microsoft.EntityFrameworkCore;
using Order_ms.Data;
using Order_ms.Models;

namespace Order_ms.Services
{
    public class OrderDataAccess : IOrderDataAccess
    {
        private readonly OrderDbContext _db;

        public OrderDataAccess(OrderDbContext db)
        {
            _db = db;    
        }
        public async Task<List<OrderModel>> GetAllOrders()
        {
            return await _db.Order.ToListAsync();
        }

        public async Task SaveOrder(OrderModel orderModel)
        {
            _db.Order.Add(orderModel);
            await _db.SaveChangesAsync();
        }
    }
}
