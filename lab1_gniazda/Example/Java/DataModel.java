import java.io.Serializable;

public class DataModel implements Serializable {
    public String msg;
    public ClientType clientType;

    public DataModel(String msg, ClientType clientType) {
        this.msg = msg;
        this.clientType = clientType;
    }

    public enum ClientType {
        JAVA("Java"),
        PYTHON("Python");

        private final String text;

        ClientType(String text) {
            this.text = text;
        }

        @Override
        public String toString() {
            return this.text;
        }
    }

}

