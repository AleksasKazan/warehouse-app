using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Persistence.Clients;
using Persistence.Repositories.Models.Read;
using Persistence.Repositories.Models.Write;

namespace Persistence.Repositories
{
    public class WarehouseRepository : IWarehouseRepository
    {
        private const string WarehousesTable = "Warehouses";

        private const string InventoryTable = "Inventory";

        private const string ProductsTable = "Products";

        private const string OrdersTable = "Orders";

        private readonly ISqlClient _sqlClient;

        public WarehouseRepository(ISqlClient sqlClient)
        {
            _sqlClient = sqlClient;
        }

        public Task<IEnumerable<InventoryReadModel>> GetAllInventory()
        {
            var sql = $"SELECT Sku, Warehouse, Stock, Reserved, Ordered FROM {InventoryTable} ORDER BY Sku";

            return _sqlClient.QueryAsync<InventoryReadModel>(sql);
        }
        public Task<IEnumerable<OrderReadModel>> GetAllOrders()
        {
            var sql = $"SELECT Id, DateCreated, Status, Sku, Qtt, Expiration FROM {OrdersTable}";

            return _sqlClient.QueryAsync<OrderReadModel>(sql);
        }

        public Task<IEnumerable<SkuReadModel>> GetAllSku()
        {
            var sql = $"SELECT * FROM {ProductsTable}";

            return _sqlClient.QueryAsync<SkuReadModel>(sql);
        }

        public Task<IEnumerable<InventoryReadModel>> GetSku(string sku)
        {
            var sql = @$"SELECT sku, warehouse, stock, reserved, ordered FROM {InventoryTable} WHERE SKU='{sku}'";

            return _sqlClient.QueryAsync<InventoryReadModel>(sql);
        }

        public Task<OrderReadModel> GetOrder(Guid orderId)
        {
            var sql = @$"SELECT * FROM {OrdersTable} WHERE Id='{orderId}'";

            return _sqlClient.QuerySingleOrDefaultAsync<OrderReadModel>(sql);
        }

        public Task<int> AddOrder(OrdersWriteModel model)
        {
            var sql = @$"INSERT INTO {OrdersTable} (id, datecreated, status, sku, qtt, expiration)
                        VALUES(@id, @datecreated, @status, @sku, @qtt, @expiration)";

            return _sqlClient.ExecuteAsync(sql, model);
        }

        public Task<int> UpdateStock(InventoryWriteModel model)
        {
            var sql = @$"UPDATE {InventoryTable} SET stock=@stock, reserved=@reserved WHERE sku=@sku AND warehouse=@warehouse";

            return _sqlClient.ExecuteAsync(sql, model);
        }
    }
}
