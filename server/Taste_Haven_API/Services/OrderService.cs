#nullable enable

#region

using AutoMapper;
using Taste_Haven_API.Middlewares.Exceptions;
using Taste_Haven_API.Models;
using Taste_Haven_API.Models.Dto;
using Taste_Haven_API.Repositories;

#endregion

namespace Taste_Haven_API.Services;

public interface IOrderService
{
    Task<OrderHeader> GetByIdAsync(int id);
    Task<int> AddAsync(OrderHeaderCreateDto orderHeaderDto);
    Task UpdateAsync(int id, string status);
    Task DeleteAsync(int id);

    Task<OrderHeaderResponseDto> GetAllAsync(string? userId, string? searchString, string? status, int pageNumber,
        int pageSize);
}

public class OrderService(IOrderRepository orderRepository, IMapper mapper) : IOrderService
{
    public async Task<OrderHeader> GetByIdAsync(int id)
    {
        if (id <= 0) throw new InvalidOrderIdException();

        var order = await orderRepository.GetByIdAsync(id);
        if (order == null) throw new OrderNotFoundException();

        return order;
    }

    public async Task<int> AddAsync(OrderHeaderCreateDto orderHeaderDto)
    {
        var order = mapper.Map<OrderHeader>(orderHeaderDto);
        order.OrderDate = DateTime.Now;

        return await orderRepository.AddAsync(order);
    }

    public async Task UpdateAsync(int id, string status)
    {
        var orderFromDb = await orderRepository.GetByIdAsync(id);
        if (orderFromDb == null) throw new OrderNotFoundException();
        orderFromDb.Status = status;

        await orderRepository.UpdateAsync(orderFromDb);
    }

    public async Task DeleteAsync(int id)
    {
        if (id <= 0) throw new InvalidOrderIdException();

        var order = await orderRepository.GetByIdAsync(id);
        if (order == null) throw new OrderNotFoundException();

        await orderRepository.DeleteAsync(order);
    }

    public async Task<OrderHeaderResponseDto> GetAllAsync(string? userId, string? searchString, string? status,
        int pageNumber, int pageSize)
    {
        var orders = await orderRepository.GetAllAsync();

        if (!string.IsNullOrEmpty(userId)) orders = orders.Where(o => o.ApplicationUserId == userId);

        if (!string.IsNullOrEmpty(searchString))
            orders = orders
                .Where(o => o.PickupPhoneNumber.Contains(searchString) ||
                            o.PickupEmail.Contains(searchString) ||
                            o.PickupName.Contains(searchString))
                .ToList();

        if (!string.IsNullOrEmpty(status))
            orders = orders
                .Where(o => o.Status.Equals(status, StringComparison.OrdinalIgnoreCase))
                .ToList();

        var list = orders.Skip((pageNumber - 1) * pageSize).Take(pageSize);

        return new OrderHeaderResponseDto
        {
            OrderHeaders = list,
            TotalRecords = orders.Count()
        };
    }
}