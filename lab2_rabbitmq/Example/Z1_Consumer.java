import com.rabbitmq.client.*;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.nio.charset.StandardCharsets;

public class Z1_Consumer {

    public static void main(String[] argv) throws Exception {

        // info
        System.out.println("Z1 CONSUMER");

        // connection & channel
        ConnectionFactory factory = new ConnectionFactory();
        factory.setHost("localhost");
        Connection connection = factory.newConnection();
        Channel channel = connection.createChannel();

        // queue
        String QUEUE_NAME = "queue2";
        channel.queueDeclare(QUEUE_NAME, false, false, false, null);
        // PARAMETERS:
        // queue name
        // durable - true if we are declaring a durable queue (the queue will survive a server restart)
        // exclusive - true if we are declaring an exclusive queue (restricted to this connection)
        // autoDelete - true if we are declaring an autodelete queue (server will delete it when no longer in use)
        // other properties (construction arguments) for the queue
        // consumer (handle msg)

        // exchange
        String EXCHANGE_NAME = "exchange1";
        channel.exchangeDeclare(EXCHANGE_NAME, BuiltinExchangeType.DIRECT);

        Consumer consumer = new DefaultConsumer(channel) {
            @Override
            // PARAMETERS:
            // consumerTag - the consumer tag associated with the consumer
            // envelope - packaging data for the message
            // properties - content header data for the message
            // body - the message body (opaque, client-specific byte array)
            public void handleDelivery(String consumerTag, Envelope envelope, AMQP.BasicProperties properties, byte[] body) throws IOException {
                String message = new String(body, StandardCharsets.UTF_8);
                System.out.println("Received: " + message);
                int timeToSleep = 1000;
                try {
                    timeToSleep = Integer.parseInt(message);
                } catch (NumberFormatException e) {
                    e.printStackTrace();
                }
                try {
                    Thread.sleep(timeToSleep * 1000);
                } catch (InterruptedException e) {
                    e.printStackTrace();
                }
                System.out.println("End: " + message);
                channel.basicAck(envelope.getDeliveryTag(), false);
            }
        };

        // start listening
        System.out.println("Waiting for messages...");

        // String queueName = channel.queueDeclare().getQueue();
        // queueDeclare() - Actively declare a server-named exclusive, autodelete, non-durable queue

        channel.queueBind(QUEUE_NAME, EXCHANGE_NAME, "");
        // last parameter: "" - from every publish / routingKey - from publish with the key / from publish with topic

        channel.basicQos(1);
        // PARAMETERS:
        // prefetchCount - maximum number of messages that the server will deliver, 0 if unlimited (must be between 0 and 65535 - unsigned short in AMQP 0-9-1)
        // global - true if the settings should be applied to the entire channel rather than each consumer

        channel.basicConsume(QUEUE_NAME, false, consumer);
        // PARAMETERS:
        // queue - the name of the queue
        // autoAck - true if the server should consider messages acknowledged once delivered; false if the server should expect explicit acknowledgements
        // callback - an interface to the consumer object
        // PARAMETERS 2:
        // queue - the name of the queue
        // autoAck - true if the server should consider messages acknowledged once delivered; false if the server should expect explicit acknowledgements
        // arguments - a set of arguments for the consume
        // deliverCallback - callback when a message is delivered
        // cancelCallback - callback when the consumer is cancelled

        // close
//        channel.exchangeDelete(EXCHANGE_NAME, false);
        // PARAMETERS:
        // exchange - the name of the exchange
        // ifUnused - true to indicate that the exchange is only to be deleted if it is unused
//        channel.close();
//        connection.close();
    }
}
