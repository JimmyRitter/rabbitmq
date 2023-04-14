using System.Text;
using RabbitMQ.Client;

var factory = new ConnectionFactory { HostName = "localhost" };
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

// declare the queue and give it a name
channel.QueueDeclare(queue: "queue-name-3-durable",
                     durable: true,
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);

// here we are parsing as byte array, but could also be JSON or any format (that both sender and consumer must "agree" on)
// const string message = "There you go, my first message!";
var message = GetMessage(args);
var body = Encoding.UTF8.GetBytes(message);

// set messages as durable, so even if rabbitmq restarts, messages are not lost
var properties = channel.CreateBasicProperties();
properties.Persistent = true;

// publish the message to the queue, so it can be read on the consumer
channel.BasicPublish(exchange: string.Empty,
                     routingKey: "queue-name-3-durable",
                     basicProperties: null,
                     body: body);

// just to confirm that everything went well
Console.WriteLine($"Message published successfully: \"{message}\"");

Console.WriteLine(" Press [enter] to exit.");
Console.ReadLine();

static string GetMessage(string[] args)
{
    return ((args.Length > 0) ? string.Join(" ", args) : "Hello World!");
}