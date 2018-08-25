import socket, time, json, sys, os
try:
    import BaseHTTPServer
except:
    os.system("pip install BaseHTTPServer")
    time.sleep(5)
    import BaseHTTPServer

HOST_NAME = ""
PORT_NUMBER = 8090


class handler(BaseHTTPServer.BaseHTTPRRequestHandler):
    def do_HEAD(self, s):
        #might need to change (might need to send headers only if request is valid)
        s.send_response(200)
        s.send_header("Content-type", "text/html")
        s.send_header("Access-Control-Allow-Origin" ,"*")
        s.end_headers()

    def do_GET(self):
        #This is the general handler for GET requests
        self.handle_request()
        pass
    def do_POST(self):
        #This is the general handler for POST requests
        self.handle_request()
        pass
    def get_parameters(self, path):
        parts = path.split("/", 1)
        l = len(parts)
        if(l <=1 ):
            return None,None
        last = parts[-1]
        arguments = last.split('?', 1)
        last = arguments[0]
        if(len(arguments) <= 1):
            return last, None
        parameters = arguments[1]
        parameters = parameters.split("&")
        dict = {}
        for i in parameters:
            parts = item.split('=')
            dict[parts[0]] = parts[1]
        return last, dict

    def handle_request(self):
        #TODO : complete handle request
        pass
        
