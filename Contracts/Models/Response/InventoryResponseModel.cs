using System;
using System.Text.Json.Serialization;
using Contracts.Enums;

namespace Contracts.Models.Response
{
    public class InventoryResponseModel
    {
        public string Sku { get; set; }

        public Location Warehouse { get; set; }

        public int Stock { get; set; }

        public int Reserved { get; set; }

        public int Ordered { get; set; }
    }
}
