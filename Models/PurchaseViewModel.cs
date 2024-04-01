using System;

namespace POS_API.Models
{
    public class PurchaseViewModel
    {
        public int PurchaseId { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string ReferenceNo { get; set; }
        public string Supplier { get; set; }
        public string PurchaseStatus { get; set; }
        public decimal? Total { get; set; }
        public decimal? Paid { get; set; }
        public decimal? Balance { get; set; }
        public string PaymentStatus { get; set; }
    }
}
