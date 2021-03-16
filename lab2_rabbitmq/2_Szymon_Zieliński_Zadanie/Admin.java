import java.io.BufferedReader;
import java.io.InputStreamReader;

public class Admin {

    public static void main(String[] argv) throws Exception {
        BufferedReader br = new BufferedReader(new InputStreamReader(System.in));
        System.out.println("Commands: ");
        System.out.println("all \"your message to suppliers and teams\"");
        System.out.println("s \"your message to suppliers\"");
        System.out.println("t \"your message to teams\"");

        QueuesController queuesController = new QueuesController();

        queuesController.createAndBindQueue("adminQueue", QueuesController.SUPPLIER_EXCHANGE, "#");
        queuesController.createAndBindQueue("adminQueue", QueuesController.TEAM_EXCHANGE, "#");
        queuesController.receiveMessage("adminQueue", queuesController.defaultConsumer);

        while (true) {
            String message = br.readLine();
            if (message.startsWith("all ")) {
                queuesController.sendMessage(QueuesController.ADMIN_EXCHANGE, "supplier.team", message.substring(4));
            }
            else if (message.startsWith("s ")) {
                queuesController.sendMessage(QueuesController.ADMIN_EXCHANGE, "supplier", message.substring(2));
            }
            else if (message.startsWith("t ")) {
                queuesController.sendMessage(QueuesController.ADMIN_EXCHANGE, "team", message.substring(2));
            }
            else {
                System.out.println("Incorrect command");
            }
        }
    }
}
