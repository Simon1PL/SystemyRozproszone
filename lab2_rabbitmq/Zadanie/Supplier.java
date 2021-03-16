import com.rabbitmq.client.*;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.nio.charset.StandardCharsets;

public class Supplier {

    public static void main(String[] argv) throws Exception {
        BufferedReader br = new BufferedReader(new InputStreamReader(System.in));
        System.out.println("Podaj nazwe dostawcy: ");
        String name = br.readLine();
        System.out.println("Podaj produkty tego sprzedawcy po przecinku(np. tlen, buty, plecak): ");
        String[] items = br.readLine().replaceAll("\\s","").split(",");

        QueuesController queuesController = new QueuesController();

        // RECEIVE FROM ADMIN
        queuesController.createAndBindQueue(name, QueuesController.ADMIN_EXCHANGE, "#.supplier.#");
        queuesController.receiveMessage(name, queuesController.defaultConsumer);

        for (String item : items) {
            queuesController.createAndBindQueue(item, QueuesController.SUPPLIER_EXCHANGE, item);
        }

        Consumer teamsOrdersConsumer = new DefaultConsumer(queuesController.getChannel()) {
            @Override
            public void handleDelivery(String consumerTag, Envelope envelope, AMQP.BasicProperties properties, byte[] body) throws IOException {
                String message = new String(body, StandardCharsets.UTF_8);
                System.out.println(message);
                this.getChannel().basicAck(envelope.getDeliveryTag(), false);
                // SEND ORDER
                queuesController.sendMessage(QueuesController.TEAM_EXCHANGE, message.split(" ")[1], "Dostawca " + name + " dostarczył " + message.split(" ")[3] + " do " + message.split(" ")[1]);
                System.out.println("Dostarczono " + message.split(" ")[3] + " do " + message.split(" ")[1]);
            }
        };

        // RECEIVE ORDER
        for (String item : items) {
            queuesController.receiveMessage(item, teamsOrdersConsumer);
        }

        while (true) {
            System.out.println("Wpisz exit by zamknac");
            if (br.readLine().equals("exit")) {
                queuesController.close();
                break;
            }
        }
    }
}
