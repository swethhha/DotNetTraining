
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShopTrackPro.MVC.Models
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime OrderDate { get; set; }

        // Make sure OrderItemViewModel exists too
        public List<OrderItemViewModel> Items { get; set; } = new();

        // Calculated GrandTotal
        public decimal GrandTotal => Items.Sum(i => i.Total);
    }
}
