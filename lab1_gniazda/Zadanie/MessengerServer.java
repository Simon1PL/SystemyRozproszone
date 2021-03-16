import com.google.gson.Gson;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.PrintWriter;
import java.net.*;
import java.util.*;
import java.util.concurrent.*;
import java.util.concurrent.atomic.AtomicBoolean;

public class MessengerServer {

    public static void main(String[] args) {
        Gson gson = new Gson();
        String hostName = "localhost";
        int tcpPortNumber = 12345;
        int udpPortNumber = 12345;
        int multicastPortNumber = 12346;
        String multicastAddress = "230.0.0.0";
        ConcurrentHashMap<Integer, Optional<Socket>> clientsSocket = new ConcurrentHashMap<>();
        ConcurrentLinkedQueue<Integer> emptySlots = new ConcurrentLinkedQueue<>();
        ExecutorService pool = Executors.newCachedThreadPool();

        System.out.println("JAVA TCP/UDP SERVER");

        // UDP - unicast(receive and send):
        new Thread(() -> {
            try (DatagramSocket udpSocket = new DatagramSocket(udpPortNumber)) {
                byte[] receiveBuffer = new byte[1024];
                while (true) {
                    Arrays.fill(receiveBuffer, (byte) 0);
                    DatagramPacket receivePacket = new DatagramPacket(receiveBuffer, receiveBuffer.length);
                    udpSocket.receive(receivePacket);
                    Message message = gson.fromJson(new String(receivePacket.getData()).trim(), Message.class);
                    message.printWithDate();
                    for (int i = 0; i < clientsSocket.size(); i++) {
                        if (clientsSocket.get(i).isPresent()) {
                            byte[] sendBuffer = receivePacket.getData();
                            DatagramPacket sendPacket = new DatagramPacket(sendBuffer, sendBuffer.length,  InetAddress.getByName(hostName), clientsSocket.get(i).get().getPort());
                            udpSocket.send(sendPacket);
                        }
                    }
                }
            } catch (IOException e) {
                e.printStackTrace();
            }
        }).start();

        // UDP - multicast(receive):
        new Thread(() -> {
            try (MulticastSocket multicastSocket = new MulticastSocket(multicastPortNumber)) {
                /*InetSocketAddress group = new InetSocketAddress(multicastAddress, multicastPortNumber);
                multicastSocket.joinGroup(group, NetworkInterface.getByName("wlan1")); // "lo"*/
                InetAddress group = InetAddress.getByName(multicastAddress);
                multicastSocket.joinGroup(group);
                byte[] receiveBuffer = new byte[1024];
                while (true) {
                    Arrays.fill(receiveBuffer, (byte) 0);
                    DatagramPacket receivePacket = new DatagramPacket(receiveBuffer, receiveBuffer.length);
                    multicastSocket.receive(receivePacket);
                    Message message = gson.fromJson(new String(receivePacket.getData()).trim(), Message.class);
                    message.printWithDate();
                }
            } catch (IOException e) {
                e.printStackTrace();
            }
        }).start();

        // TCP(accept clients, receive and send messages):
        try (ServerSocket serverSocket = new ServerSocket(tcpPortNumber)) {
            AtomicBoolean disconnectAll = new AtomicBoolean(false);
            while (true) {
                int clientIndex;
                if (emptySlots.isEmpty()) {
                    clientIndex = clientsSocket.size();
                }
                else {
                    clientIndex = emptySlots.remove();
                }
                Socket clientSocket = serverSocket.accept();
                BufferedReader in = new BufferedReader(new InputStreamReader(clientSocket.getInputStream()));
                clientsSocket.put(clientIndex, Optional.of(clientSocket));

                pool.execute(() -> {
                    try {
                        String msg = in.readLine();
                        Message message = gson.fromJson(msg, Message.class);
                        if (message.msgType == Message.MessageType.INIT) {
                            Message.println("\"" + message.userName + "\"" + " connected " + "\t" + message.date, Message.Color.RED);
                        }
                        else {
                            Message.println("\"" + message.userName + "\"" + " connected without init message " + "\t" + message.date, Message.Color.RED);
                        }
                        for (int i = 0; i < clientsSocket.size(); i++) {
                            if (clientsSocket.get(i).isPresent() && i != clientIndex) {
                                message.msg = "dołącza";
                                message.msgType = Message.MessageType.INFO;
                                new PrintWriter(clientsSocket.get(i).orElseThrow().getOutputStream(), true).println(gson.toJson(message));
                            }
                        }

                        while (true) {
                            try {
                                msg = in.readLine();
                            } catch (IOException e) {
                                break;
                            }
                            if (msg == null) {
                                break;
                            }
                            message = gson.fromJson(msg, Message.class);
                            if (message.msg.equals("quit server")) {
                                disconnectAll.set(true);
                                System.out.println("set atomic boolean to true");
                                for (int i = 0; i < clientsSocket.size(); i++) {
                                    if (clientsSocket.get(i).isPresent()) {
                                        clientsSocket.get(i).get().close();
                                    }
                                }
                                Message.println("Disconnected all clients " + "\t" + message.date, Message.Color.RED);
                                serverSocket.close();
                                break;
                            }
                            else {
                                message.printWithDate();
                                for (int i = 0; i < clientsSocket.size(); i++) {
                                    if (clientsSocket.get(i).isPresent() && i != clientIndex) {
                                        new PrintWriter(clientsSocket.get(i).get().getOutputStream(), true).println(msg);
                                    }
                                }
                            }
                        }
                        if (!disconnectAll.get()) {
                            System.out.println("atomic boolean: " + disconnectAll.get());
                            clientsSocket.get(clientIndex).orElseThrow().close();
                            clientsSocket.put(clientIndex, Optional.empty());
                            emptySlots.add(clientIndex);
                            Message.println("\"" + message.userName + "\"" + " disconnected " + "\t" + message.date, Message.Color.RED);
                            for (int i = 0; i < clientsSocket.size(); i++) {
                                if (clientsSocket.get(i).isPresent() && i != clientIndex) {
                                    message.msg = "opuścił chat";
                                    message.msgType = Message.MessageType.INFO;
                                    new PrintWriter(clientsSocket.get(i).orElseThrow().getOutputStream(), true).println(gson.toJson(message));
                                }
                            }
                        }
                    } catch (IOException e) {
                        e.printStackTrace();
                    }
                });
            }
        } catch (IOException e) {
            // e.printStackTrace();
        } finally {
            pool.shutdown();
            Message.println("Server quit " + "\t" + new Date(), Message.Color.RED);
            System.exit(0);
        }
    }

}
