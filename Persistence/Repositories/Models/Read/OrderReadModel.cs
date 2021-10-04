using System;
using Contracts.Enums;

namespace Persistence.Repositories.Models.Read
{
    public class OrderReadModel
    {
        public Guid Id { get; set; }

        public DateTime DateCreated { get; set; }

        public Status Status { get; set; }

        public string Sku { get; set; }

        public int Qtt { get; set; }

        public DateTime Expiration { get; set; }
    }
}
