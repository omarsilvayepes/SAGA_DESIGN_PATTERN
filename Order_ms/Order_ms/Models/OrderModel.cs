using System.ComponentModel.DataAnnotations;

namespace Order_ms.Models
{
   
        public class OrderModel
        {
            [Key]
            public Guid OrderId { get; set; }
            public string ProductName { get; set; }
            public string CardNumber { get; set; }
            public string UserId { get; set; }
        }
}
