using POS_API.DatabaseLogic;
using POS_API.Models;
using POS_API.POS_Database;
using System;
using System.Collections.Generic;

namespace POS_API.BusinessLogic
{
    public class ReturnBusiness
    {
        private readonly ReturnDAT _returnDAT;

        public ReturnBusiness(ReturnDAT returnDAT)
        {
            _returnDAT = returnDAT ?? throw new ArgumentNullException(nameof(returnDAT));
        }

        public ResponseViewModel AddReturn(ReturnViewModel returnRequest)
        {
            var returnData = new Return
            {
                Date = returnRequest.Date,
                ReferenceNo = returnRequest.ReferenceNo,
                Biller = returnRequest.Biller,
                Customer = returnRequest.Customer,
                GrandTotal = returnRequest.GrandTotal,
                OrderTax = returnRequest.OrderTax,
                OrderDiscount = returnRequest.OrderDiscount
            };

            return _returnDAT.AddReturn(returnData);
        }

        public List<ReturnViewModel> GetReturnList()
        {
            return _returnDAT.GetReturnList();
            }

        public ResponseViewModel UpdateReturn(ReturnViewModel returnRequest)
        {
            var returnData = new Return
            {
                return_id = returnRequest.ReturnId,
                Date = returnRequest.Date,
                ReferenceNo = returnRequest.ReferenceNo,
                Biller = returnRequest.Biller,
                Customer = returnRequest.Customer,
                GrandTotal = returnRequest.GrandTotal,
                OrderTax = returnRequest.OrderTax,
                OrderDiscount = returnRequest.OrderDiscount
            };

            return _returnDAT.UpdateReturn(returnData);
        }

        public ResponseViewModel DeleteReturn(int returnId)
        {
            return _returnDAT.DeleteReturn(returnId);
        }
    }
}
