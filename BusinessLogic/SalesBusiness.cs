using POS_API.DatabaseLogic;
using POS_API.Models; // Add this using directive
using POS_API.POS_Database;
using System;
using System.Collections.Generic;

namespace POS_API.BusinessLogic
{
    public class SalesBusiness
    {
        private readonly SalesDAT _salesDAT;

        public SalesBusiness(SalesDAT salesDAT)
        {
            _salesDAT = salesDAT ?? throw new ArgumentNullException(nameof(salesDAT));
        }

        public ResponseViewModel AddSales(SalesViewModel salesRequest)
        {
            var sale = new Sale
            {
                Date = salesRequest.Date,
                ReferenceNo = salesRequest.ReferenceNo,
                Biller = salesRequest.Biller,
                Customer = salesRequest.Customer,
                Tax = salesRequest.Tax,
                OrderDiscount = salesRequest.OrderDiscount,
                Shipping = salesRequest.Shipping,
                Paid = salesRequest.Paid,
                Status = salesRequest.Status
            };

            return _salesDAT.AddSales(sale);
        }

        public List<SalesViewModel> GetSalesList()
        {
            return _salesDAT.GetSalesList();
        }

        public ResponseViewModel UpdateSales(int saleId, SalesViewModel updatedSales)
        {
            var sale = new Sale
            {
                SaleID = saleId,
                Date = updatedSales.Date,
                ReferenceNo = updatedSales.ReferenceNo,
                Biller = updatedSales.Biller,
                Customer = updatedSales.Customer,
                Tax = updatedSales.Tax,
                OrderDiscount = updatedSales.OrderDiscount,
                Shipping = updatedSales.Shipping,
                Paid = updatedSales.Paid,
                Status = updatedSales.Status
            };

            return _salesDAT.UpdateSales(sale);
        }

        public ResponseViewModel DeleteSales(int saleId)
        {
            return _salesDAT.DeleteSales(saleId);
        }

        public SalesViewModel GetSalesById(int saleId)
        {
            return _salesDAT.GetSalesById(saleId);
        }
    }
}
