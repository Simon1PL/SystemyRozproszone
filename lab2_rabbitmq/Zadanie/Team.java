import java.io.BufferedReader;
import java.io.InputStreamReader;

public class Team {

    public static void main(String[] argv) throws Exception {
        BufferedReader br = new BufferedReader(new InputStreamReader(System.in));
        System.out.println("Podaj nazwe ekipy: ");
        String name = br.readLine();

        QueuesController queuesController = new QueuesController();

        // BIND FROM SUPPLIER
        queuesController.createAndBindQueue(name, QueuesController.TEAM_EXCHANGE, name);
        // BIND FROM ADMIN
        queuesController.createAndBindQueue(name, QueuesController.ADMIN_EXCHANGE, "#.team.#");
        // RECEIVE
        queuesController.receiveMessage(name, queuesController.defaultConsumer);

        boolean exit = false;
        System.out.println("Podaj zlecenia po przecinku lub w kolejnych wiadomosciach(np. tlen, buty, plecak) lub wpisz exit by zamknac: ");
        while (!exit) {
            // SEND ORDER
            for (String item : br.readLine().replaceAll("\\s","").split(",")) {
                if (item.equals("exit")) {
                    exit = true;
                    break;
                }
                queuesController.createAndBindQueue(item, QueuesController.SUPPLIER_EXCHANGE, item);
                queuesController.sendMessage(QueuesController.SUPPLIER_EXCHANGE, item, "Ekipa " + name + " zamówiła " + item);
                System.out.println("Zamówiono " + item);
            }
        }
        queuesController.close();
    }
}
