import com.rabbitmq.client.*;

import java.io.IOException;
import java.nio.charset.StandardCharsets;
import java.util.concurrent.TimeoutException;

public class QueuesController {
    public static final String SUPPLIER_EXCHANGE = "suppliers";
    public static final String TEAM_EXCHANGE = "teams";
    public static final String ADMIN_EXCHANGE = "admin";
    private Connection connection;
    private Channel channel;
    public final Consumer defaultConsumer;

    public QueuesController() throws IOException, TimeoutException {
        createConnection();
        createExchange(QueuesController.SUPPLIER_EXCHANGE, BuiltinExchangeType.TOPIC);
        createExchange(QueuesController.TEAM_EXCHANGE, BuiltinExchangeType.TOPIC);
        createExchange(QueuesController.ADMIN_EXCHANGE, BuiltinExchangeType.TOPIC);
        defaultConsumer = createDefaultConsumer();
    }

    //region public methods
    public void createAndBindQueue(String queueName, String exchangeName, String routingKey) throws IOException {
        channel.queueDeclare(queueName, false, false, true, null);
        channel.queueBind(queueName, exchangeName, routingKey);
    }

    public void createExchange(String exchangeName, BuiltinExchangeType exchangeType) throws IOException {
        channel.exchangeDeclare(exchangeName, exchangeType);
    }

    public void sendMessage(String exchangeName, String routingKey, String message) throws IOException {
        channel.basicPublish(exchangeName, routingKey, null, message.getBytes());
    }

    public void receiveMessage(String queueName, Consumer consumer) throws IOException {
        channel.basicConsume(queueName, false, consumer);
    }

    public void close() throws IOException, TimeoutException {
        channel.close();
        connection.close();
    }

    public Channel getChannel() {
        return channel;
    }

    private Consumer createDefaultConsumer() {
        return new DefaultConsumer(channel) {
            @Override
            public void handleDelivery(String consumerTag, Envelope envelope, AMQP.BasicProperties properties, byte[] body) throws IOException {
                String message = new String(body, StandardCharsets.UTF_8);
                System.out.println(message);
                this.getChannel().basicAck(envelope.getDeliveryTag(), false);
            }
        };
    }
    //endregion

    //region private methods
    private void createConnection() throws IOException, TimeoutException { // creates connection and channel
        ConnectionFactory factory = new ConnectionFactory();
        factory.setHost("localhost");
        connection = factory.newConnection();
        channel = connection.createChannel();
        channel.basicQos(1);
    }
    //endregion
}
