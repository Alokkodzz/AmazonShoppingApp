using Amazon.SQS;
using Amazon.SQS.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OrderService.Data;
using OrderService.Models;
using System.Text.Json;

namespace OrderService.Services
{
    public class OrderService : IOrderService
    {
        private readonly AppDbContext _db;
        private readonly IAmazonSQS _sqsClient;
        private readonly string _queueUrl;

        public OrderService(AppDbContext db, IAmazonSQS sqsClient, IConfiguration config)
        {
            _db = db;
            _sqsClient = sqsClient;
            _queueUrl = config["SqsQueueUrl"] ?? throw new ArgumentNullException("SqsQueueUrl configuration is missing");
        }

        public async Task<Order> CreateOrderAsync(Order order)
        {
            order.CreatedAt = DateTime.UtcNow;
            _db.Orders.Add(order);
            await _db.SaveChangesAsync();

            var orderMessage = JsonSerializer.Serialize(order);

            var sendMessageRequest = new SendMessageRequest
            {
                QueueUrl = _queueUrl,
                MessageBody = orderMessage,
            };

            await _sqsClient.SendMessageAsync(sendMessageRequest);

            return order;
        }
    }
}
