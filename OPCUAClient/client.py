from opcua import Client
import datetime
import time
from random import randint
import requests
import urllib3

urllib3.disable_warnings(urllib3.exceptions.InsecureRequestWarning)

def post_opc(addspace,ipAddress,angle,rssi,tagEPC):

    try:
        opcResponse = var.call_method("{}:addLog".format(addspace),ipAddress,angle, rssi, tagEPC)
        print("OPC RESPONSE: {}".format(opcResponse))
    except Exception as err:
        print("ERROR OPC RESPONSE: {}".format(err))

def post_log(url, log):

    try:
        httpResponse = requests.post(url, json = log, verify=False)
        print("POST RESPONsE: {}".format(httpResponse.status_code))
    except Exception as err:
        print("ERROR POST RESPONSE: {}".format(err))   


if __name__ == "__main__":
    
    try:
        urlHttp = "https://localhost:7168/api/logs"
        urlOPC = "opc.tcp://localhost:4840"
        client = Client(urlOPC)
        client.connect()

        name = "OPCUA_SIMULATION_SERVER"
        addspace = client.get_namespace_index(name)

        var = client.get_node("ns=2;i=1")
        print(var)

        print("Client started at {}".format(urlOPC))

        while True:
    
            angle = randint(0,180)
            ipAddress = "1.1.1.1"
            tagEPC = "TAG000001"
            rssi = randint(0,1000)

            log = {
                'rssi' : rssi,
                'ipAddress' : ipAddress,
                'angle' : angle,
                'tagEPC' : tagEPC
            }

            post_opc(addspace,ipAddress,angle,rssi,tagEPC)

            post_log(urlHttp, log)

            time.sleep(10)

    except KeyboardInterrupt:
        client.disconnect()
        print("client stopped at {}".format(urlOPC))
    except Exception as e:
        print("Exception {}".format(e))
        client.disconnect()
        print("client stopped at {}".format(urlOPC))
