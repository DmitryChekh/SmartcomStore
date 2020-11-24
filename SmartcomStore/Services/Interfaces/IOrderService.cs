using SmartcomStore.Data.Models;
using SmartcomStore.Models;
using SmartcomStore.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using static SmartcomStore.Data.Models.Order;

namespace SmartcomStore.Services.Interfaces
{
    public interface IOrderService
    {
        Task<BaseResponseModel<OrderDto>> CreateOrderAsync(CreateOrderDto createOrderDto);
        Task<BaseResponseModel<OrderDto>> GetById(Guid id);
        Task<BaseResponseModel<IEnumerable<OrderDto>>> GetOrderByCustomer(Guid id, OrderStatus? orderStatus = null);

        Task<BaseResponseModel<UpdateOrderStatusDto>> UpdateOrderStatus(Guid id, DateTime shipmentDate);

        // Task<BaseResponseModel<IEnumerable<OrderDto>>> GetOrders();

        //// Task<BaseResponseModel<IEnumerable<ProductDto>>> GetProducts(ProductsSearchResourceParameters productsSearchResourceParameters); сделать фильтр
        // Task<BaseResponseModel> DeleteOrder(Guid id);
    }


}
