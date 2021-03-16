import com.google.gson.Gson;

import java.io.ByteArrayOutputStream;
import java.io.ObjectOutputStream;
import java.net.DatagramPacket;
import java.net.DatagramSocket;
import java.net.InetAddress;

public class JavaUdpClient {

    public static void main(String args[]) throws Exception
    {
        System.out.println("JAVA UDP CLIENT");
        DatagramSocket socket = null;
        int portNumber = 9008;

        try {
            socket = new DatagramSocket();
            InetAddress address = InetAddress.getByName("localhost");

            // 1. Sending string data:
            // byte[] sendBuffer = "Ping Java Udp".getBytes();

            // 2. Sending object data:
            DataModel data = new DataModel("Ping Java Udp", DataModel.ClientType.JAVA);
            Gson gson = new Gson();
            byte[] sendBuffer = gson.toJson(data).getBytes();

            // Send:
            DatagramPacket sendPacket = new DatagramPacket(sendBuffer, sendBuffer.length, address, portNumber);
            socket.send(sendPacket);

            // Receive response from server(string):
            byte[] receiveBuffer = new byte[1024];
            DatagramPacket receivePacket = new DatagramPacket(receiveBuffer, receiveBuffer.length);
            socket.receive(receivePacket);
            String msg = new String(receivePacket.getData());
            System.out.println("received msg: " + msg);
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
