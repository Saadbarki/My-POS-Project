using POS_API.DatabaseLogic;
using POS_API.Models;
using POS_API.POS_Database;
using System;
using System.Collections.Generic;
using System.Linq;

namespace POS_API.BusinessLogic
{
    public class PurchaseBusiness
    {
        private readonly PurchaseDAT _purchaseDAT;

        public PurchaseBusiness(POSEntities db)
        {
            _purchaseDAT = new PurchaseDAT(db);
        }

        public List<PurchaseViewModel> GetAllPurchases()
        {
            List<Purchase> purchases = _purchaseDAT.GetAllPurchases();
            List<PurchaseViewModel> purchaseViewModels = purchases.Select(p => new PurchaseViewModel
            {
                PurchaseId = p.purchase_id,
                PurchaseDate = (DateTime)p.Date,
                ReferenceNo = p.ReferenceNo,
                Supplier = p.Supplier,
                PurchaseStatus = p.PurchaseStatus,
                Total = p.Total,
                Paid = p.Paid,
                Balance = p.Balance,
                PaymentStatus = p.PaymentStatus
            }).ToList();

            return purchaseViewModels;
        }

        public void AddPurchase(PurchaseViewModel purchaseViewModel)
        {
            Purchase purchase = new Purchase
            {
                Date = purchaseViewModel.PurchaseDate,
                ReferenceNo = purchaseViewModel.ReferenceNo,
                Supplier = purchaseViewModel.Supplier,
                PurchaseStatus = purchaseViewModel.PurchaseStatus,
                Total = purchaseViewModel.Total,
                Paid = purchaseViewModel.Paid,
                Balance = purchaseViewModel.Balance,
                PaymentStatus = purchaseViewModel.PaymentStatus
            };

            _purchaseDAT.AddPurchase(purchase);
        }

        // Add other business logic methods as needed
    }
}
