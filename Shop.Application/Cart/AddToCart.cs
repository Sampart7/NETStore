using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Cart
{
    public class AddToCart
    {
        private ISession _session;

        public AddToCart(ISession session) 
        {
            _session = session;
        }

        public class Request
        {
            public int StockId { get; set; }  
            public int Qty { get; set; }
        }

        public void Do(Request request) 
        {
            var cartProduct = new CartProduct
            {
                StockId = request.StockId,
                Qty = request.Qty,
            };

            var stringObject = JsonConvert.SerializeObject(cartProduct); // Sesja nie przyjmie zwykłego requesta w postaci stringa więc konwertujemy go na obiekt

            _session.SetString("cart", stringObject);

        }
    }
}
