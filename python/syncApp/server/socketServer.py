import socket
import sys, os, json

PORT_NUMBER = 2020
address = ('', PORT_NUMBER)

soc_server = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
soc_server.bind(address)

while(True):
    data, address = sock.recvfrom(4096)
    handle(data

def handle(data, client):
    if(not os.path.isdir("storage/" + client)):
        os.makedirs("storage/" + client)
    if(data[0] == '0'):
        #create directory
        folder = data[1:-1]
        if(not os.path.isdir("storage/" + client + "/" + folder):
           os.makedirs(folder)
    elif(data[0] == '1'):
        #update server storage
        dic = json.loads(data[1:-1])
           
        folder = dic["dir"]
        source = "storage/" + client + "/" + folder
        if(os.path.isdir(source):
            deletions = dic["deletions"]
            additions = dic["additions"]
            #updates   = dic["updates"]

            #stage changes to remote storage
            for f in deletions:
                os.remove(source + "/" + f)
            for f in additions:
                with open(source + "/" + f, 'w') as file:
                    file.write(dic[f])
           
    elif(data[0] == '2'):
        #get local directory
        folder = data[1:-1]
        source = "/storage/" + client + "/" + folder
        dic = {}
        dic["files"] = os.listdir(os.getcwd() + source)
        for f in dic["files"]:
           with open(source + "/" + f, 'r') as file:
               dic[f] = file.read()
        sock.sendto(client, json.dumps(dic))
    #keep the handle function, add handlers

        

    
