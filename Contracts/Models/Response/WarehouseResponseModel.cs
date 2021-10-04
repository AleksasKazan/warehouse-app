using System;
using Contracts.Enums;

namespace Contracts.Models.Response
{
    public class WarehouseResponseModel
    {
        public Guid Id { get; set; }

        public Location Location { get; set; }

        public int Capacity { get; set; }
    }
}
