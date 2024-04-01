using POS_API.DatabaseLogic;
using POS_API.Models;
using POS_API.POS_Database;
using System;
using System.Collections.Generic;

namespace POS_API.BusinessLogic
{
    public class CustomerBusiness
    {
        private readonly CustomerDAT _customerDAT;

        public CustomerBusiness(CustomerDAT customerDAT)
        {
            _customerDAT = customerDAT ?? throw new ArgumentNullException(nameof(customerDAT));
        }

        public ResponseViewModel AddCustomer(CustomerViewModel customerRequest)
        {
            var customer = new POS_API.POS_Database.Customer
            {
                customer_name = customerRequest.CustomerName,
                email = customerRequest.Email,
                phone_number = customerRequest.PhoneNumber,
                address = customerRequest.Address,
                city = customerRequest.City,
                country = customerRequest.Country,
                postal_code = customerRequest.PostalCode
            };

            return _customerDAT.AddCustomer(customer);
        }

        public List<Customer> GetCustomerList()
        {
            return _customerDAT.GetCustomerList();
        }

        public ResponseViewModel UpdateCustomer(CustomerViewModel customerRequest)
        {
            var customer = new POS_API.POS_Database.Customer
            {
                customer_id = customerRequest.CustomerId,
                customer_name = customerRequest.CustomerName,
                email = customerRequest.Email,
                phone_number = customerRequest.PhoneNumber,
                address = customerRequest.Address,
                city = customerRequest.City,
                country = customerRequest.Country,
                postal_code = customerRequest.PostalCode
            };

            return _customerDAT.UpdateCustomer(customer);
        }

        public ResponseViewModel DeleteCustomer(int customerId)
        {
            return _customerDAT.DeleteCustomer(customerId);
        }
    }
}
