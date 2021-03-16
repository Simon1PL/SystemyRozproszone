import java.io.Serializable;
import java.util.Date;

public class Message implements Serializable {
    public String msg;
    public String userName;
    public MessageType msgType;
    public Date date;

    public Message(String msg, String userName) {
        this.msg = msg;
        this.userName = userName;
        this.date = new Date();
        this.msgType = MessageType.MESSAGE;
    }

    public Message(String msg, String userName, MessageType msgType) {
        this.msg = msg;
        this.userName = userName;
        this.date = new Date();
        this.msgType = msgType;
    }

    public void print() {
        printMessage();
        System.out.println();
    }

    public void printWithDate() {
        printMessage();
        println("\t" + this.date, Color.WHITE);
    }

    private void printMessage() {
        switch (this.msgType) {
            case INFO -> print(this.userName + " " + this.msg, Color.RED);
            case MESSAGE -> print(this.userName + ": " + this.msg, Color.PURPLE);
            case PICTURE -> print(this.userName + ":\n" + this.msg, Color.PURPLE);
        }
    }

    public static void print(String text, Color color) {
        System.out.print(color + text + Color.RESET);
    }

    public static void println(String text, Color color) {
        System.out.println(color + text + Color.RESET);
    }

    public enum MessageType {
        MESSAGE,
        INIT,
        PICTURE,
        INFO
    }

    public enum Color {
        RESET("\u001B[0m"),
        BLACK("\u001B[30m"),
        RED("\u001B[31m"),
        GREEN("\u001B[32m"),
        YELLOW("\u001B[33m"),
        BLUE("\u001B[34m"),
        PURPLE("\u001B[35m"),
        CYAN("\u001B[36m"),
        WHITE("\u001B[37m");

        private final String text;

        Color(String text) {
            this.text = text;
        }

        @Override
        public String toString() {
            return this.text;
        }
    }
}

