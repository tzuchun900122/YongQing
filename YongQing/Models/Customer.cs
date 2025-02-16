﻿using System.ComponentModel.DataAnnotations;

namespace YongQing.Models
{
    // Northwind.dbo.Customers 的資料模型
    public class Customer
    {
        [Key]
        public required String CustomerID { get; set; }
        public required String CompanyName { get; set; }
        public String? ContactName { get; set; }
        public String? ContactTitle { get; set; }
        public String? Address { get; set; }
        public String? City { get; set; }
        public String? Region { get; set; }
        public String? PostalCode { get; set; }
        public String? Country { get; set; }
        public String? Phone { get; set; }
        public String? Fax { get; set; }
    }
}
