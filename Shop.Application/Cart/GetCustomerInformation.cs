using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Cart
{
    public class GetCustomerInformation
    {
        private ISession _session;

        public GetCustomerInformation(ISession session)
        {
            _session = session;
        }

        public class Response
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string PhoneNumber { get; set; }
            public string Addres1 { get; set; }
            public string Addres2 { get; set; }
            public string City { get; set; }
            public string PostCode { get; set; }
        }

        public Response Do()
        {
            var stringObject = _session.GetString("customer-info");

            if(string.IsNullOrEmpty(stringObject))
            {
                return null;
            }

            var customerInformation = JsonConvert.DeserializeObject<CustomerInformation>(stringObject);

            return new Response
            {
                FirstName = customerInformation.FirstName,
                LastName = customerInformation.LastName,
                Email = customerInformation.Email,
                PhoneNumber = customerInformation.PhoneNumber,
                Addres1 = customerInformation.Addres1,
                Addres2 = customerInformation.Addres2,
                City = customerInformation.City,
                PostCode = customerInformation.PostCode,
            };
        }
    }
}
