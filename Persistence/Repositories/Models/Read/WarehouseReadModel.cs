using System;
using Contracts.Enums;

namespace Persistence.Repositories.Models.Read
{
    public class WarehouseReadModel
    {
        public Guid Id { get; set; }

        public Location Location { get; set; }

        public int Capacity { get; set; }
    }
}
