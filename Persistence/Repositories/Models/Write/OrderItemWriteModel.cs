using System;
using Contracts.Enums;

namespace Persistence.Repositories.Models.Write
{
    public class OrderItemWriteModel
    {
        public Guid Order_Id { get; set; }

        public string Sku { get; set; }

        public Location Warehouse { get; set; }

        public int Qtt { get; set; }
    }
}
