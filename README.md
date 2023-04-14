# RabbitMQ tutorial

## Requirements
- Erlang
- RabbitMQ service

## Afer installation
- RabbitMQ runs as a service on Windows. It must be manually started from services app

## Running the apps
### Consumer & Publisher (simplest example)
- There are 2 projects: Received (consumer) and Sender (publisher)
- To run each of the projects, run `dotnet run` on each folder respectively 

### Round-robin dispatching (Task Queue)
- THere are 2 projects used for task queue: `NewTask` and `Worker`
- To see this in action, open 3 terminal tabs, where 2 are from Worker folder, and 1 from NewTask
- `NewTask` expect an argument, so run it the following way: `dotnet run "here goes my message"`
- If no argument is sent, it uses "Hello, World!" as a default message.
- Both Workers will receive the message (1 each time, they don't receive both the same message)
- This is because RabbitMQ splits the load in sequence, so all instances will have the same number of messages ish.

## Management UI
- First you need to enable the plugin. How to do that can be found in [this link](https://www.rabbitmq.com/management.html#getting-started)
- Can be accessed on `http://localhost:15672`