using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.EntityFrameworkCore;
using SmartcomStore.Data;
using SmartcomStore.Data.Models;
using SmartcomStore.Data.Models.Identity;
using SmartcomStore.Models;
using SmartcomStore.Models.ResponseModels;
using SmartcomStore.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static SmartcomStore.Data.Models.Order;

namespace SmartcomStore.Services
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _dataContext;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        public OrderService(ApplicationDbContext dataContext,
            IMapper mapper,
            UserManager<User> userManager
            )
        {
            _dataContext = dataContext;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<BaseResponseModel<OrderDto>> CreateOrderAsync(CreateOrderDto order)
        {

            if (order == null)
                return new BaseResponseModel<OrderDto> { Status = false, Error = "Order is not correct" };

            if (order.Items.Count() == 0)
                return new BaseResponseModel<OrderDto> { Status = false, Error = "Can't create order because items are not indicated" };

            var currentUser = await _userManager.FindByIdAsync(order.CustomerId.ToString());

            if (currentUser == null )
                return new BaseResponseModel<OrderDto> { Status = false, Error = "User is not found" };

            if(currentUser.IsDeleted)
                return new BaseResponseModel<OrderDto> { Status = false, Error = "User is deleted" };



            var totalPrices = await GetTotalPriceOrNullIfItemIsNotExistAsync(order.Items, currentUser.Discount);
            if(totalPrices == null)
                return new BaseResponseModel<OrderDto> { Status = false, Error = "Can't create order because some item is not exist" };


            var newOrder = _mapper.Map<Order>(order);
            newOrder.Status = OrderStatus.New;

            var createdOrder = await _dataContext.Orders.AddAsync(newOrder);

            if (createdOrder.State == EntityState.Added)
            {

                var listOrderItems = CreateOrderItemsList(order.Items, createdOrder.Entity.Id, totalPrices);
                await _dataContext.OrderItems.AddRangeAsync(listOrderItems);
                await _dataContext.SaveChangesAsync();
                return new BaseResponseModel<OrderDto> { Status = true, Data = _mapper.Map<OrderDto>(createdOrder.Entity) };
            }

            return new BaseResponseModel<OrderDto> { Status = false, Error = "Something wrong" };
        }




        private async Task<Dictionary<Guid,decimal>> GetTotalPriceOrNullIfItemIsNotExistAsync(List<CreateOrderItemDto> items, decimal discount)
        {
            Dictionary<Guid, decimal> totalPrices = new Dictionary<Guid, decimal>();
            foreach (var item in items)
            {
                var currentItem = await _dataContext.Products.FirstOrDefaultAsync(x => x.Id == item.ItemId && !x.IsDeleted);
                if (currentItem == null)
                {
                    return null;
                }
                var totalPrice = CalculateTotalPrice(discount, currentItem.Price);
                totalPrices.Add(currentItem.Id, totalPrice);
            }
            return totalPrices;
        }

        private decimal CalculateTotalPrice(decimal discount, decimal price)
        {
            if (discount == 0)
                return price;
            return price * discount / 100;
        }
        private List<OrderItem> CreateOrderItemsList(List<CreateOrderItemDto> items, Guid orderId, Dictionary<Guid, decimal> totalPrices)
        {
            List<OrderItem> orderItems = new List<OrderItem>();
            foreach(var item in items)
            {
                orderItems.Add(new OrderItem
                {
                    ItemId = item.ItemId,
                    OrderId = orderId,
                    ItemsCount = item.ItemsCount,
                    ItemPrice = totalPrices[item.ItemId]
                });
            }
            return orderItems;
        }

        public async Task<BaseResponseModel<OrderDto>> GetById(Guid id)
        {
            var order = await _dataContext.Orders
                .Include(x => x.OrderItems)
                .ThenInclude(x => x.Product)
                .FirstOrDefaultAsync(x => x.Id == id);


            if(order == null)
                return new BaseResponseModel<OrderDto> { Status = false, Error = "Order is not found" };

            var orderDto = _mapper.Map<OrderDto>(order);
            orderDto.Items = _mapper.Map<IEnumerable<OrderItemDto>>(order.OrderItems);

            return new BaseResponseModel<OrderDto> { Status = true, Data = orderDto };

        }

        public async Task<BaseResponseModel<IEnumerable<OrderDto>>> GetOrderByCustomer(Guid id)
        {
            var currentUser = await _userManager.FindByIdAsync(id.ToString());

            if (currentUser == null)
                return new BaseResponseModel<IEnumerable<OrderDto>> { Status = false, Error = "User is not found" };

            if (currentUser.IsDeleted)
                return new BaseResponseModel<IEnumerable<OrderDto>> { Status = false, Error = "User is deleted" };

            var orders = await _dataContext.Orders
                .Include(x=> x.OrderItems)
                .ThenInclude(x => x.Product)
                .Where(x => x.CustomerId == id).ToListAsync();



            if(orders.Count == 0)
                return new BaseResponseModel<IEnumerable<OrderDto>> { Status = false, Error = "Orders by this customer are not found" };

            var ordersDto = _mapper.Map<IEnumerable<OrderDto>>(orders);

            return new BaseResponseModel<IEnumerable<OrderDto>> { Status = true, Data = ordersDto };
        }


        public Task<BaseResponseModel> DeleteOrder(Guid id)
        {
            throw new NotImplementedException();
        }



        public Task<BaseResponseModel<IEnumerable<OrderDto>>> GetOrders()
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponseModel<OrderDto>> CreateOrderAsync(string name, decimal price, string category)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponseModel<ProductDto>> UpdateOrderStatus()
        {
            throw new NotImplementedException();
        }
    }
}
