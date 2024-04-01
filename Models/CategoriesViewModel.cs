using System;
using System.Collections.Generic;

namespace POS_API.Models
{
    public class CategoriesViewModel
    {
        public int CategoryId { get; set; }
        public string ProductName { get; set; }
        public string CategoryCode { get; set; }
        public string CategoryName { get; set; }
    }
}
