using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Persistence.Repositories.Models.Read;
using Persistence.Repositories.Models.Write;

namespace Persistence.Repositories
{
    public interface IWarehouseRepository
    {
        Task<IEnumerable<InventoryReadModel>> GetAllInventory();

        Task<IEnumerable<OrderReadModel>> GetAllOrders();

        Task<IEnumerable<SkuReadModel>> GetAllSku();

        Task<IEnumerable<InventoryReadModel>> GetSku(string sku);

        Task<OrderReadModel> GetOrder(Guid orderId);

        Task<int> AddOrder(OrdersWriteModel model);

        Task<int> UpdateStock(InventoryWriteModel model);

    }
}
