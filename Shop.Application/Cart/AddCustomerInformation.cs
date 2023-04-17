﻿using Microsoft.AspNetCore.Http;
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
    public class AddCustomerInformation
    {
        private ISession _session;

        public AddCustomerInformation(ISession session)
        {
            _session = session;
        }

        public class Request
        {
            [Required]
            public string FirstName { get; set; }
            [Required]
            public string LastName { get; set; }
            [Required]
            [DataType(DataType.EmailAddress)]
            public string Email { get; set; }
            [Required]
            [DataType(DataType.PhoneNumber)]
            public string PhoneNumber { get; set; }
            [Required]
            public string Addres1 { get; set; }
            public string Addres2 { get; set; }
            [Required]
            public string City { get; set; }
            [Required]
            public string PostCode { get; set; }
        }

        public void Do(Request request)
        {
            var customerInfromation = new CustomerInformation
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                Addres1 = request.Addres1,
                Addres2 = request.Addres2,
                City = request.City,
                PostCode = request.PostCode,
            };

            var stringObject = JsonConvert.SerializeObject(customerInfromation);

            _session.SetString("customer-info", stringObject);
        }
    }
}
