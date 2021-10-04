using System;
using Contracts.Enums;

namespace Persistence.Repositories.Models.Write
{
    public class WarehouseWriteModel
    {
        public Guid Id { get; set; }

        public Location Location { get; set; }

        public int Capacity { get; set; }
    }
}
