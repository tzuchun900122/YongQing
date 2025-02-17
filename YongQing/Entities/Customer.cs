﻿using System.ComponentModel.DataAnnotations;

namespace YongQing.Entities
{
    // Northwind.dbo.Customers 的資料模型
    public class Customer
    {
        [Key]
        public required string CustomerID { get; set; }
        public required string CompanyName { get; set; }
        public string? ContactName { get; set; }
        public string? ContactTitle { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Region { get; set; }
        public string? PostalCode { get; set; }
        public string? Country { get; set; }
        public string? Phone { get; set; }
        public string? Fax { get; set; }
    }
}
