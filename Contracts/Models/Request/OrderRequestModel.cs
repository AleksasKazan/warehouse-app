using System;

namespace Contracts.Models.Request
{
    public class OrderRequestModel
    {
        public string Sku { get; set; }

        public int Qtt { get; set; }

        public DateTime Expiration { get; set; }
    }
}
