import json
import socket

serverIP = "127.0.0.1"
serverPort = 9008

print('PYTHON UDP CLIENT')
client = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
# msg = "żółta gęś"
# client.sendto(bytes(msg, 'UTF-8'), (serverIP, serverPort))

# msg = 300
# msg_bytes = msg.to_bytes(4, byteorder='little')
# client.sendto(bytes(msg_bytes), (serverIP, serverPort))
# print("Send to server: " + str(msg))

data = {
    "msg": "Ping Python Udp",
    "clientType": "PYTHON"
}

client.sendto(bytes(json.dumps(data), 'UTF-8'), (serverIP, serverPort))

buff, address = client.recvfrom(1024)
# print("Received from server: " + str(int.from_bytes(buff, byteorder='little')))
print("Received from server: " + str(buff, 'utf-8'))
