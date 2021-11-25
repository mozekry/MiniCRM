using System;

namespace MiniCRM.API.Models
{
    public class Order
    {
        public int orderId { get; set; }
        public int customerId { get; set; }
        public int menuId { get; set; }   
        public int orderAmount { get; set; }
        public DateTime orderDate { get; set; }
        public DateTime deliveryDate { get; set; }
    }
}