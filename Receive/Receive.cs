using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

var factory = new ConnectionFactory { HostName = "localhost" };
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

channel.QueueDeclare(queue: "queue-name-1",
                     durable: false,
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);


Console.WriteLine(" [*] Waiting for messages.");

// used to consume messages from a channel
var consumer = new EventingBasicConsumer(channel);

// attached the event handler, for when it received a new published message
consumer.Received += (model, ea) =>
{
    // This extracts the message body from the event arguments and converts it to a byte array
    var body = ea.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    Console.WriteLine($" [x] Received {message}");
};

channel.BasicConsume(queue: "queue-name-1",
                     autoAck: true,
                     consumer: consumer);

Console.WriteLine(" Press [enter] to exit.");
Console.ReadLine();