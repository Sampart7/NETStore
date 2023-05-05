﻿using Microsoft.Data.SqlClient;
using Shop.Database;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Orders
{
    public class CreateOrder
    {
        private ApplicationDbContext _ctx;

        public CreateOrder(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        public class Request
        {
            public string StripeReference { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string PhoneNumber { get; set; }
            public string Addres1 { get; set; }
            public string Addres2 { get; set; }
            public string City { get; set; }
            public string PostCode { get; set; }
            public List<Stock> Stocks { get; set; }
        }

        public class Stock
        {
            public int StockId { get; set; }
            public int Qty { get; set; }
        }

        public async Task<bool> Do(Request request)
        {
            var stocksToUpdate = _ctx.Stock.Where(x => request.Stocks.Select(y => y.StockId).Contains(x.Id)).ToList();

            foreach (var stock in stocksToUpdate)
            {
                stock.Qty = stock.Qty - request.Stocks.FirstOrDefault(x => x.StockId == stock.Id).Qty;
            }

            var order = new Order
            {
                OrderRef = CreateOrderReference(),
                StripeReference = request.StripeReference,

                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                Addres1 = request.Addres1,
                Addres2 = request.Addres2,
                City = request.City,
                PostCode = request.PostCode,

                OrderStocks = request.Stocks.Select(x => new OrderStock
                {
                    StockId = x.StockId,
                    Qty = x.Qty,
                }).ToList()
            };

            _ctx.Orders.Add(order);

            return await _ctx.SaveChangesAsync() > 0;
        }

        public string CreateOrderReference()
        {
            var chars = "QWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbnm1234567890";
            var result = new char[12];
            var random = new Random();

            do
            {
                for (int i = 0; i < result.Length; i++)
                {
                    result[i] = chars[random.Next(chars.Length)];
                }
            } 
            while (_ctx.Orders.Any(x => x.OrderRef == new string(result))) ;

            return new string(result);
        }
    }
}