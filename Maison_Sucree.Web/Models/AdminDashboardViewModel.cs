namespace Maison_Sucree.Web.Models
{
    public class AdminDashboardViewModel
    {
        public double TotalRevenue { get; set; }
        public int TotalOrders { get; set; }
        public int TotalCustomers { get; set; }
        public int ActiveOrders { get; set; }

        public int PendingCount { get; set; }
        public int ApprovedCount { get; set; }
        public int ReadyForPickupCount { get; set; }
        public int CompletedCount { get; set; }
        public int CancelledCount { get; set; }

        public List<OrderHeaderDto> RecentOrders { get; set; } = new();
    }
}
