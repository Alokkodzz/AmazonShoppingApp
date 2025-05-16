using OrderService.Models;

namespace OrderService.Services
{
    public interface IOrderService
    {
        Task<Order> CreateOrderAsync(Order order);
    }
}
