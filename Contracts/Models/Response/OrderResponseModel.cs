using System;
using Contracts.Enums;

namespace Contracts.Models.Response
{
    public class OrderResponseModel
    {
        public Guid Id { get; set; }

        public DateTime DateCreated { get; set; }

        public Status Status { get; set; }

        public string Sku { get; set; }

        public int Qtt { get; set; }

        public DateTime Expiration { get; set; }

        public override string ToString()
        {
            return $"Order / Invoice Id: {Id}, Date created: {DateCreated}, Status: {Status}, \n" +
                $"SKU: {Sku}, quantity: {Qtt}, Reservation expiration date: {Expiration}";
        }
    }
}
