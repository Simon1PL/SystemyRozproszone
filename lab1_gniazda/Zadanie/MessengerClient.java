import com.google.gson.Gson;

import java.io.*;
import java.net.*;
import java.util.Arrays;
import java.util.Locale;

public class MessengerClient {
    private static final Gson gson = new Gson();
    private static String name;

    public static void main(String[] args) throws IOException {

        String hostName = "localhost";
        // Localhost: "localhost"
        // Wlan wi-fi: "192.168.0.110"
        // Wlan połączenie lokalne: "192.168.137.1"
        // Ethernet: "172.25.180.17"
        // Getting probable current machine ip address:
        /*Enumeration<NetworkInterface> myNetworkInterfaces = NetworkInterface.getNetworkInterfaces();
        while (myNetworkInterfaces.hasMoreElements()) {
            NetworkInterface networkInterface = myNetworkInterfaces.nextElement();
            if (!networkInterface.isLoopback()  && networkInterface.isUp()) {
                Enumeration<InetAddress> inetAddressEnumeration = networkInterface.getInetAddresses();
                while (inetAddressEnumeration.hasMoreElements()) {
                    InetAddress inetAddress = inetAddressEnumeration.nextElement();
                    if (inetAddress instanceof Inet6Address || !inetAddress.isReachable(3000))
                        continue;
                    String ip = inetAddress.getHostAddress();
                    System.out.println("probable ip: " + ip);
                }
            }
        }*/

        String multicastAddress = "230.0.0.0";
        int tcpPortNumber = 12345;
        int multicastPortNumber = 12346;
        int udpPortNumber = 12345;

        BufferedReader consoleIn = new BufferedReader(new InputStreamReader(System.in));
        // Scanner consoleIn = new Scanner(System.in);

        System.out.println("Podaj nick:");
        name = consoleIn.readLine();

        // create sockets
        // 1. UDP - multicast
        MulticastSocket multicastSocket = new MulticastSocket(multicastPortNumber);
        // multicastSocket.setOption(StandardSocketOptions.IP_MULTICAST_LOOP, false); // blocks messages to myself(turn off cause it blocks messages on localhost)
        InetAddress group = InetAddress.getByName(multicastAddress);
        multicastSocket.joinGroup(group);
        /*InetSocketAddress group = new InetSocketAddress(multicastAddress, multicastPortNumber);
        multicastSocket.joinGroup(group, NetworkInterface.getByName("wlan1")); // "lo"*/
        // 2. TCP
        Socket tcpSocket = new Socket(hostName, tcpPortNumber);
        PrintWriter tcpOut = new PrintWriter(tcpSocket.getOutputStream(), true);
        BufferedReader tcpIn = new BufferedReader(new InputStreamReader(tcpSocket.getInputStream()));
        // 3. UDP - unicast
        DatagramSocket udpSocket = new DatagramSocket(tcpSocket.getLocalPort());

        // send init message
        Message initMessage = new Message(null, name, Message.MessageType.INIT);
        tcpOut.println(gson.toJson(initMessage));

        System.out.println("Command \"U\" - sending picture by UDP");
        System.out.println("Command \"M\" - sending picture by UDP multicast address, directly to another clients");
        System.out.println("Command \"quit\" - quit client");
        System.out.println("Command \"quit server\" - quit whole server");
        System.out.println("Wiadomości:");

        // thread 1: send msg
        new Thread(() -> {
            while (true) {
                try {
                    // creating message
                    String msg;
                    try {
                        msg = consoleIn.readLine();
                        // TO DO: anulowac czekanie na wpis jakos, nie dziala: "System.in.close();" ani "consoleIn.close();"
                    } catch (IOException e) {
                        break;
                    }
                    // don't send empty messages
                    if (msg.isBlank()) {
                        continue;
                    }
                    // UDP - unicast(send)
                    if (msg.trim().toLowerCase(Locale.ROOT).equals("u")) {
                        Message message = new Message(getRandomAsciiArt(), name, Message.MessageType.PICTURE);
                        Message.println(message.msg, Message.Color.GREEN);
                        byte[] sendBuffer = gson.toJson(message).getBytes();
                        DatagramPacket sendPacket = new DatagramPacket(sendBuffer, sendBuffer.length,  InetAddress.getByName(hostName), udpPortNumber);
                        udpSocket.send(sendPacket);
                    }
                    // UDP - multicast(send)
                    else  if (msg.trim().toLowerCase(Locale.ROOT).equals("m")) {
                        Message message = new Message(getRandomAsciiArt(), name, Message.MessageType.PICTURE);
                        Message.println(message.msg, Message.Color.GREEN);
                        byte[] sendBuffer = gson.toJson(message).getBytes();
                        DatagramPacket sendPacket = new DatagramPacket(sendBuffer, sendBuffer.length,  group/*group.getAddress()*/, multicastPortNumber);
                        multicastSocket.send(sendPacket);
                    }
                    // close client
                    else  if (msg.trim().toLowerCase(Locale.ROOT).equals("quit")) {
                        tcpSocket.close();
                        break;
                    }
                    // TCP(send) ("close server" is special command)
                    else {
                        Message message = new Message(msg, name);
                        tcpOut.println(gson.toJson(message));
                    }
                } catch (IOException e) {
                    // problem with sending msg (socket.send())
                    e.printStackTrace();
                }
            }
        }).start();

        // thread 2: UDP - multicast(receive)
        new Thread(() -> receiveUdp(multicastSocket)).start();

        // thread 3: UDP - unicast(receive)
        new Thread(() -> receiveUdp(udpSocket)).start();

        // thread 4: TCP(receive)
        try {
            while (true) {
                String response = tcpIn.readLine();
                Message responseMessage = gson.fromJson(response, Message.class);
                if (responseMessage == null) {
                    Message.println("Server został wyłączony", Message.Color.RED);
                    break;
                }
                responseMessage.print();
            }
        } catch (IOException e) {
            if (e.getMessage().equals("Socket closed")) {
                Message.println("Disconnected successfully", Message.Color.RED);
            }
            else if (e.getMessage().equals("Connection reset")) {
                Message.println("Server padł", Message.Color.RED);
            }
            else {
                e.printStackTrace();
            }
        } finally {
            tcpSocket.close();
            udpSocket.close();
            //multicastSocket.leaveGroup(group, NetworkInterface.getByName("wlan1")); // "lo"
            multicastSocket.leaveGroup(group);
            multicastSocket.close();
            System.in.close();
        }
    }

    private static void receiveUdp(DatagramSocket socket) {
        byte[] receiveBuffer = new byte[1024];
        while (true) {
            Arrays.fill(receiveBuffer, (byte) 0);
            DatagramPacket receivePacket = new DatagramPacket(receiveBuffer, receiveBuffer.length);
            try {
                socket.receive(receivePacket);
            } catch (IOException e) {
                break;
            }
            Message message = gson.fromJson(new String(receivePacket.getData()).trim(), Message.class);
            if (!message.userName.equals(name)) { // warning: works only if name is unique
                message.print();
            }
        }
    }

    private static String getRandomAsciiArt() {
        int random = (int)(Math.random() * 100);
        if (random % 2 == 0) {
            return "   |\\---/|\n"
                    + "   | ,_, |\n"
                    + "    \\_`_/-..----.\n"
                    + " ___/ `   ' ,\"\"+ \\\n"
                    + "(__...'   __\\    |`.___.';\n"
                    + "  (_,...'(_,.`__)/'.....+";
        } else {
            return "|\\---/|\n"
                    + "| o_o |\n"
                    + " \\_^_/";
        }
    }
}
