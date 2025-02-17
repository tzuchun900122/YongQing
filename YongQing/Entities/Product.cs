using System.ComponentModel.DataAnnotations;

namespace YongQing.Entities
{
    // Northwind.dbo.Products 的資料模型
    public class Product
    {
        [Key]
        public required int ProductID { get; set; }
        public required string ProductName { get; set; }
        public int? SupplierID { get; set; }
        public int? CategoryID { get; set; }
        public string? QuantityPerUnit { get; set; }
        public decimal? UnitPrice { get; set; }
        public short? UnitsInStock { get; set; }
        public short? UnitsOnOrder { get; set; }
        public short? ReorderLevel { get; set; }
        public required bool Discontinued { get; set; }
    }
}
