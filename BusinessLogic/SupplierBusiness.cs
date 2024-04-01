using Org.BouncyCastle.Asn1.Ocsp;
using POS_API.BusinessLogic;
using POS_API.DatabaseLogic;
using POS_API.Models;
using POS_API.POS_Database;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using System.Web.Http;

namespace POS_API.BusinessLogic
{
    public class SupplierBusiness
    {
        private readonly SupplierDAT _supplierDAT;

        public SupplierBusiness()
        {
            _supplierDAT = new SupplierDAT();
        }

        public ResponseViewModel AddSupplier(SupplierViewModel supplierRequest)
        {
            var supplier = new Supplier
            {
                supplier_name = supplierRequest.SupplierName,
                contact_person = supplierRequest.ContactPerson,
                email = supplierRequest.Email,
                phone_number = supplierRequest.PhoneNumber,
                address = supplierRequest.Address,
                city = supplierRequest.City,
                country = supplierRequest.Country,
                postal_code = supplierRequest.PostalCode
            };

            return _supplierDAT.AddSupplier(supplier);
        }

        public List<Supplier> GetSupplierList()
        {
            return _supplierDAT.GetSupplierList();
        }

        public ResponseViewModel UpdateSupplier(SupplierViewModel supplierRequest)
        {
            var supplier = new Supplier
            {
                supplier_id = supplierRequest.SupplierID,
                supplier_name = supplierRequest.SupplierName,
                contact_person = supplierRequest.ContactPerson,
                email = supplierRequest.Email,
                phone_number = supplierRequest.PhoneNumber,
                address = supplierRequest.Address,
                city = supplierRequest.City,
                country = supplierRequest.Country,
                postal_code = supplierRequest.PostalCode
            };

            return _supplierDAT.UpdateSupplier(supplier);
        }

        public ResponseViewModel DeleteSupplier(int supplierId)
        {
            return _supplierDAT.DeleteSupplier(supplierId);
        }
    }
}

