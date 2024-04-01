using System;

namespace POS_API.Models
{
    public class ReturnViewModel
    {
        public int ReturnId { get; set; }
        public DateTime Date { get; set; }
        public string ReferenceNo { get; set; }
        public string Biller { get; set; }
        public string Customer { get; set; }
        public decimal? GrandTotal { get; set; }
        public string OrderTax { get; set; }
        public decimal? OrderDiscount { get; set; }
    }
}
