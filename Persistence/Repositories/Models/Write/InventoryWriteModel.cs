using System;
using Contracts.Enums;

namespace Persistence.Repositories.Models.Write
{
    public class InventoryWriteModel
    {
        public Guid Id { get; set; }

        public string Sku { get; set; }

        public Location Warehouse { get; set; }

        public int Stock { get; set; }

        public int Reserved { get; set; }

        public int Ordered { get; set; }
    }
}
