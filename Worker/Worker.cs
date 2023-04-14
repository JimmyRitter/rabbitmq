using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

var factory = new ConnectionFactory { HostName = "localhost" };
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

channel.QueueDeclare(queue: "queue-name-3-durable",
                     durable: true,
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);

channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

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

    int dots = message.Split('.').Length - 1;
    Thread.Sleep(dots * 1000);

    // here we ackledge the message was received, so the queue is aware of that.
    // if it's marked as "auto-ack", the server assumes that the message has been successfully processed as soon as it is delivered to the consumer
    channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
    
    Console.WriteLine(" [x] Done");
};

channel.BasicConsume(queue: "queue-name-3-durable",
                     autoAck: false,
                     consumer: consumer);

Console.WriteLine(" Press [enter] to exit.");
Console.ReadLine();


