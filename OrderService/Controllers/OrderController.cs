using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Amazon.SQS;
using Amazon.SQS.Model;
using System.Text.Json;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly OrderDbContext _dbContext;
    private readonly IAmazonSQS _sqsClient;
    private readonly string _queueUrl;

    public OrdersController(OrderDbContext dbContext, IAmazonSQS sqsClient, IConfiguration config)
    {
        _dbContext = dbContext;
        _sqsClient = sqsClient;
        _queueUrl = config["SqsQueueUrl"] ?? throw new ArgumentNullException("SqsQueueUrl");
    }

    [HttpPost]
    public async Task<IActionResult> PlaceOrder(OrderDto dto)
    {
        var order = new Order
        {
            UserId = dto.UserId,
            ProductId = dto.ProductId,
            Quantity = dto.Quantity,
            Status = "Pending",
            OrderDate = DateTime.UtcNow
        };

        _dbContext.Orders.Add(order);
        await _dbContext.SaveChangesAsync();

        // Send order info to SQS
        var messageBody = JsonSerializer.Serialize(new
        {
            order.Id,
            order.UserId,
            order.ProductId,
            order.Quantity,
            order.OrderDate
        });

        await _sqsClient.SendMessageAsync(new SendMessageRequest
        {
            QueueUrl = _queueUrl,
            MessageBody = messageBody
        });

        return Ok(order);
    }
}


