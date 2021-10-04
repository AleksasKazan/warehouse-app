using Contracts.Enums;

namespace Persistence.Repositories.Models.Read
{
    public class InventoryReadModel
    {
        public int Stock { get; set; }

        public int Reserved { get; set; }

        public int Ordered { get; set; }

        public string Sku { get; set; }

        public Location Warehouse { get; set; }
    }
}
