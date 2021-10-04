using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.Models.Request;
using Contracts.Models.Response;
using Microsoft.AspNetCore.Mvc;
using Persistence.Repositories;
using Persistence.Repositories.Models.Write;

namespace WarehouseApp.Controllers
{
    [ApiController]
    public class WarehouseController : ControllerBase
    {
        private readonly IWarehouseRepository _warehouseRepository;

        public WarehouseController(IWarehouseRepository warehouseRepository)
        {
            _warehouseRepository = warehouseRepository;
        }

        [Route("inventory")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InventoryResponseModel>>> GetAllInventory()
        {
            var inventory = await _warehouseRepository.GetAllInventory();
            var response = inventory.Select(item => new InventoryResponseModel
            {
                Sku = item.Sku,
                Warehouse = item.Warehouse,
                Stock = item.Stock,
                Reserved = item.Reserved,
                Ordered = item.Ordered
            });
            return Ok(response);
        }

        [Route("sku")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SkuResponseModel>>> GetAllSku()
        {
            var products = await _warehouseRepository.GetAllSku();
            var response = products.Select(item => new SkuResponseModel
            {
                Sku = item.Sku,
                Description = item.Description
            });
            return Ok(response);
        }

        [Route("orders")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderResponseModel>>> GetAllOrders()
        {
            var orders = await _warehouseRepository.GetAllOrders();
            var response = orders.Select(order => new OrderResponseModel
            {
                Id = order.Id,
                DateCreated = order.DateCreated,
                Sku = order.Sku,
                Status = order.Status,
                Qtt = order.Qtt,
                Expiration = order.Expiration
            });
            return Ok(response);
        }

        [Route("orders/{orderId}")]
        [HttpGet]
        public async Task<ActionResult<OrderResponseModel>> GetOrder(Guid orderId)
        {
            var orders = await _warehouseRepository.GetOrder(orderId);
            var response = new OrderResponseModel
            {
                Id = orders.Id,
                DateCreated = orders.DateCreated,
                Sku = orders.Sku,
                Status = orders.Status,
                Qtt = orders.Qtt,
                Expiration = orders.Expiration
            };
            return Ok(response.ToString());
        }

        [Route("sku/{sku}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InventoryResponseModel>>> GetSku(string sku)
        {
            var products = await _warehouseRepository.GetSku(sku);
            var response = products.Select(item => new InventoryResponseModel
            {
                Sku = item.Sku,
                Warehouse = item.Warehouse,
                Stock = item.Stock,
                Reserved = item.Reserved,
                Ordered = item.Ordered
            });
            return Ok(response);
        }

        [HttpPut]
        [Route("reservation")]
        public async Task<ActionResult<int>> ReserveSku(OrderRequestModel request)
        {
            var products = await _warehouseRepository.GetSku(request.Sku);

            //var singleWarehouse = products.OrderByDescending(x => x.Stock >= request.Qtt).FirstOrDefault()?.Warehouse;
            //var singleStock = products.OrderByDescending(x => x.Stock >= request.Qtt).FirstOrDefault()?.Stock;
            //var singleReserved = products.OrderByDescending(x => x.Stock >= request.Qtt).FirstOrDefault()?.Reserved;

            var warehousesList = products.OrderByDescending(x => x.Stock >= request.Qtt).Select(v => v.Warehouse).ToList();
            var stockList = products.OrderByDescending(x => x.Stock >= request.Qtt).Select(v => v.Stock).ToList();
            var reservedList = products.OrderByDescending(x => x.Stock >= request.Qtt).Select(v => v.Reserved).ToList();
            Guid orderId = Guid.NewGuid();

            var countStock = products.Select(v => v.Stock).Sum();
            if (countStock < request.Qtt)
            {
                return BadRequest($"Not enought qtt: {countStock} in warehouses");
            }
            else
            {
                if (stockList[0] < request.Qtt)
                {
                    await _warehouseRepository.UpdateStock(new InventoryWriteModel
                    {
                        Sku = request.Sku,
                        Warehouse = warehousesList[0],
                        Stock = 0,
                        Reserved = reservedList[0] + stockList[0]
                    });

                    if (stockList[1] < request.Qtt - stockList[0])
                    {
                        await _warehouseRepository.UpdateStock(new InventoryWriteModel
                        {
                            Sku = request.Sku,
                            Warehouse = warehousesList[1],
                            Stock = 0,
                            Reserved = reservedList[1] + stockList[1]
                        });
                        await _warehouseRepository.UpdateStock(new InventoryWriteModel
                        {
                            Sku = request.Sku,
                            Warehouse = warehousesList[2],
                            Stock = stockList[2] + stockList[1] + stockList[0] - request.Qtt,
                            Reserved = reservedList[2] - stockList[1] - stockList[0] + request.Qtt
                        });
                    }
                    else
                    {
                        await _warehouseRepository.UpdateStock(new InventoryWriteModel
                        {
                            Sku = request.Sku,
                            Warehouse = warehousesList[1],
                            Stock = stockList[1] + stockList[0] - request.Qtt,
                            Reserved = reservedList[1] - stockList[0] + request.Qtt
                        });
                    }
                }
                else
                {
                    await _warehouseRepository.UpdateStock(new InventoryWriteModel
                    {
                        Sku = request.Sku,
                        Warehouse = warehousesList[0],
                        Stock = stockList[0] - request.Qtt,
                        Reserved = reservedList[0] + request.Qtt
                    });
                }
                await _warehouseRepository.AddOrder(new OrdersWriteModel
                {
                    Id = orderId,
                    DateCreated = DateTime.Now,
                    Status = 0,
                    Sku = request.Sku,
                    Qtt = request.Qtt,
                    Expiration = request.Expiration
                });
            }
            return Ok();
        }        
    }
}
