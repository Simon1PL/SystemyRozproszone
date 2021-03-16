import com.rabbitmq.client.BuiltinExchangeType;
import com.rabbitmq.client.Channel;
import com.rabbitmq.client.Connection;
import com.rabbitmq.client.ConnectionFactory;

import java.io.BufferedReader;
import java.io.InputStreamReader;

public class Z1_Producer {

    public static void main(String[] argv) throws Exception {

        // info
        System.out.println("Z1 PRODUCER");

        // connection & channel
        ConnectionFactory factory = new ConnectionFactory();
        factory.setHost("localhost");
        Connection connection = factory.newConnection();
        Channel channel = connection.createChannel();

        // queue
        String QUEUE_NAME = "queue2";
        channel.queueDeclare(QUEUE_NAME, false, false, true, null);
        // PARAMETERS:
        // queue name
        // durable - true if we are declaring a durable queue (the queue will survive a server restart)
        // exclusive - true if we are declaring an exclusive queue (restricted to this connection)
        // autoDelete - true if we are declaring an autodelete queue (server will delete it when no longer in use)
        // other properties (construction arguments) for the queue

        // exchange
        String EXCHANGE_NAME = "exchange1";
        channel.exchangeDeclare(EXCHANGE_NAME, BuiltinExchangeType.FANOUT);

        // producer (publish msg)
        BufferedReader br = new BufferedReader(new InputStreamReader(System.in));
        int i = 0;
        while (i++ < 10) {
            String message = br.readLine();

            channel.basicPublish(EXCHANGE_NAME, "", null, message.getBytes());
            // PARAMETERS:
            // exchange - the exchange to publish the message to
            // routingKey - the routing key (just QUEUE_NAME for default exchange!) channel.basicPublish("", QUEUE_NAME, null, message.getBytes());
            // mandatory - true if the 'mandatory' flag is to be set
            // props - other properties for the message - routing headers etc
            //body - the message body

            System.out.println("Sent: " + message);
        }

        // close
        channel.exchangeDelete(EXCHANGE_NAME, false);
        // PARAMETERS:
        // exchange - the name of the exchange
        // ifUnused - true to indicate that the exchange is only to be deleted if it is unused
        channel.close();
        connection.close();
    }
}
