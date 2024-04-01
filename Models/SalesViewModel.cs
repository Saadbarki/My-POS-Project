using System;

namespace POS_API.Models
{
    public class SalesViewModel
    {
        public int SaleID { get; set; }
        public DateTime Date { get; set; }
        public int Customer_id { get; set; }
        public decimal Total { get; set; }
        public string Paid { get; set; } // Change data type to string
        public string Status { get; set; }
        public string Biller { get; set; }
        public decimal Tax { get; set; }
        public string ReferenceNo { get; set; } // Add this property
        public string Customer { get; set; } // Add this property
        public decimal OrderDiscount { get; set; } // Add this property
        public decimal Shipping { get; set; } // Add this property
    }
}
