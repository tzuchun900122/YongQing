using System.ComponentModel.DataAnnotations;

namespace YongQing.Models
{
    // Northwind.dbo.Orders 的資料模型
    public class Order
    {
        [Key]
        public required int OrderId { get; set; }
        public String? CustomerID { get; set; }
        public int? EmployeeID { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? RequiredDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public int? ShipVia { get; set; }
        public decimal? Freight { get; set; }
        public String? ShipName { get; set; }
        public String? ShipAddress { get; set; }
        public String? ShipCity { get; set; }
        public String? ShipRegion { get; set; }
        public String? ShipPostalCode { get; set; }
        public String? ShipCountry { get; set; }

    }
}
