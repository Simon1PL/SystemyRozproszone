// javac -cp gson-2.8.6.jar Message.java MessengerServer.java
// java -cp gson-2.8.6.jar; MessengerServer
import java.util.Scanner;

public class TestMain {

    public static void main(String[] args) {
        Scanner sc = new Scanner(System.in);
        while (true) {
            String msg = sc.nextLine();
            System.out.println(msg);
        }
    }

}
