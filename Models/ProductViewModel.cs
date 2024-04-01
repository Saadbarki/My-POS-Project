using System;

namespace POS_API.Models
{
    public class ProductViewModel
    {

        public int ProductId { get; set; }
        public object ProductType { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public string BrandName { get; set; }
        public decimal? Cost { get; set; }
        public int? Quantity { get; set; }
        public string Code { get; set; }
        public string BarcodeSymbology { get; set; }
        public string TaxMethod { get; set; }
    }
}
