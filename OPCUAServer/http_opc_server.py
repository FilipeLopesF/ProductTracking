from opcua import Server,uamethod, ua
import requests
from fastapi import FastAPI, HTTPException
from models import Log
import time
from datetime import datetime
import threading
import json
import remoteapiconfig as conf

app = FastAPI()

@app.get('/')
async def root():
    return {"Hello":"World"}

@app.post('/api/v1/registerLog')
def register_log(log : Log):

    try:
        logsChildren = logs.get_children()
        ipAddressAlreadyExists = False

        for node in logsChildren:
            if node.get_display_name().Text == log.ip_address:
                IPAddress = node
                ipAddressAlreadyExists = True

        if not ipAddressAlreadyExists:
            IPAddress = logs.add_object(addspace, log.ip_address)
            
        timestampString = datetime.now().strftime("%Y-%m-%dT%H:%M:%S")
        TimeStamp = IPAddress.add_object(addspace, timestampString)
        TimeStamp.add_variable(addspace,"Angle", log.angle)
        TimeStamp.add_variable(addspace,"RSSI", log.rssi)
        TimeStamp.add_variable(addspace,"TagEPC", log.tag_epc)
    except Exception as e:
        raise HTTPException(
            status_code=500,
            detail=f"{e}"
        )
        
    if log.angle < -56 or log.angle > 56:
        print(f"Log.angle is {log.angle}")
        raise HTTPException(
            status_code=400,
            detail="Will be ignored since angle is not between correct range"
        )
    else:
        const_angle = 12
        angle = (log.angle + 90) + const_angle

    #Register in Web Server
    payload = {
        'rssi': log.rssi,
        'ipAddress': log.ip_address,
        'angle': angle,
        'tagEPC': log.tag_epc
    }
    print("Payload:",payload)
    result = requests.post('http://' + conf.remote_api_url + '/api/logs',json=payload, verify=False, timeout=6)
    print("HTTP Result:",result.text)
    if result.status_code != 201:
        raise HTTPException(
            status_code=500, 
            detail=f"something went wrong writing to server"
            ) 
    
    return {"opc_result" : 201, "http_result" : result.status_code}

def start_server():
    server.start()
    print("Server started at {}".format(url))

try:
    server = Server()

    url = "opc.tcp://192.168.1.169:4840"
    server.set_endpoint(url)

    name = "OPCUA_SIMULATION_SERVER"
    addspace = server.register_namespace(name)

    node = server.get_objects_node()
    logs = node.add_object(addspace, "Logs")

    t1 = threading.Thread(target=start_server)
    t1.start()
        
except KeyboardInterrupt:
    server.stop()
    print("server stopped at {}".format(url))

    
