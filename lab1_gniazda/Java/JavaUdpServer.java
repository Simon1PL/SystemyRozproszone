import com.google.gson.Gson;
import java.net.DatagramPacket;
import java.net.DatagramSocket;
import java.net.InetAddress;
import java.util.Arrays;

public class JavaUdpServer {

    public static void main(String args[])
    {
        System.out.println("JAVA UDP SERVER");
        DatagramSocket socket = null;
        int portNumber = 9008;

        try{
            socket = new DatagramSocket(portNumber);
            byte[] receiveBuffer = new byte[1024];

            while(true) {
                Arrays.fill(receiveBuffer, (byte)0);
                DatagramPacket receivePacket = new DatagramPacket(receiveBuffer, receiveBuffer.length);
                socket.receive(receivePacket);

                // Receive string:
                // String msg = new String(receivePacket.getData());
                // System.out.println("received msg: " + msg);

                // Receive int(in little_endian order):
                // int nb = ByteBuffer.wrap(receiveBuffer).order(ByteOrder.LITTLE_ENDIAN).getInt();
                // System.out.println("received msg: " + nb);

                // Receive object:
                Gson gson = new Gson();
                DataModel data = gson.fromJson(new String(receivePacket.getData()).trim(), DataModel.class);
                System.out.println("received msg: " + data.msg);

                // Send response to client:
                InetAddress senderAddress = receivePacket.getAddress();
                int senderPort = receivePacket.getPort();
                // byte[] sendBuffer = "Response from server UDP: ".getBytes();
                // byte[] sendBuffer = ByteBuffer.allocate(4).order(ByteOrder.LITTLE_ENDIAN).putInt(++nb).array();
                byte[] sendBuffer = ("Pong " + data.clientType).getBytes();
                DatagramPacket sendResponse = new DatagramPacket(sendBuffer, sendBuffer.length, senderAddress, senderPort);
                socket.send(sendResponse);
            }
        }
        catch(Exception e){
            e.printStackTrace();
        }
        finally {
            if (socket != null) {
                socket.close();
            }
        }
    }
}
