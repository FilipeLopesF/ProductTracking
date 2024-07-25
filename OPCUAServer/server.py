from opcua import Server,uamethod, ua
import time

@uamethod
def addLog(parent, ipAddress, angle, rssi, epc, timestamp):
    print("--Called method AddLog--")
    print("Node is {}".format(parent.to_string()))
    print("IPAddress is {}".format(ipAddress))
    print("Angle is {}".format(angle))
    print("RSSI is {}".format(rssi))
    print("TagEPC is {}".format(epc))
    print("Timestamp is {}".format(timestamp))

    try:
    
        Logs = server.get_node(parent.to_string())
        logsChildren = Logs.get_children()
        ipAddressAlreadyExists = False

        for node in logsChildren:
            if node.get_display_name().Text == ipAddress:
                IPAddress = node
                ipAddressAlreadyExists = True

        if not ipAddressAlreadyExists:
            IPAddress = Logs.add_object(addspace, ipAddress)
            
        timestampString = timestamp.strftime("%Y-%m-%dT%H:%M:%S")
        TimeStamp = IPAddress.add_object(addspace, timestampString)
        TimeStamp.add_variable(addspace,"Angle", angle)
        TimeStamp.add_variable(addspace,"RSSI", rssi)
        TimeStamp.add_variable(addspace,"TagEPC", epc)

    except Exception as e:
        print(e)
        return 500

    return 200

if __name__ == "__main__":

    try:
        server = Server()

        url = "opc.tcp://localhost:4840"
        server.set_endpoint(url)

        name = "OPCUA_SIMULATION_SERVER"
        addspace = server.register_namespace(name)

        node = server.get_objects_node()

        inargipAddress = ua.Argument()
        inargipAddress.Name = "ipAddress"
        inargipAddress.DataType = ua.NodeId(ua.ObjectIds.String)
        inargipAddress.ValueRank = -1
        inargipAddress.ArrayDimensions = []
        inargipAddress.Description = ua.LocalizedText("IpAddress String name for Object")

        inargangle = ua.Argument()
        inargangle.Name = "angle"
        inargangle.DataType = ua.NodeId(ua.ObjectIds.Int64)
        inargangle.ValueRank = -1
        inargangle.ArrayDimensions = []
        inargangle.Description = ua.LocalizedText("Angle Value")

        inargrssi = ua.Argument()
        inargrssi.Name = "rssi"
        inargrssi.DataType = ua.NodeId(ua.ObjectIds.Int64)
        inargrssi.ValueRank = -1
        inargrssi.ArrayDimensions = []
        inargrssi.Description = ua.LocalizedText("RSSI Value")

        inargepc = ua.Argument()
        inargepc.Name = "epc"
        inargepc.DataType = ua.NodeId(ua.ObjectIds.String)
        inargepc.ValueRank = -1
        inargepc.ArrayDimensions = []
        inargepc.Description = ua.LocalizedText("EPC Value")

        inargtimestamp = ua.Argument()
        inargtimestamp.Name = "timestamp"
        inargtimestamp.DataType = ua.NodeId(ua.ObjectIds.DateTime)
        inargtimestamp.ValueRank = -1
        inargtimestamp.ArrayDimensions = []
        inargtimestamp.Description = ua.LocalizedText("Timestamp Value")

        outarg = ua.Argument()
        outarg.Name = "Result"
        outarg.DataType = ua.NodeId(ua.ObjectIds.Int64)
        outarg.ValueRank = -1
        outarg.ArrayDimensions = []
        outarg.Description = ua.LocalizedText("Add Log result")

        logs = node.add_object(addspace, "Logs")
        logs.add_method(addspace, "addLog", addLog, [inargipAddress,inargangle, inargrssi, inargepc,inargtimestamp],[outarg])

        server.start()
        print("Server started at {}".format(url))

        while True:

            time.sleep(2)
            
    except KeyboardInterrupt:
        server.stop()
        print("server stopped at {}".format(url))

    
    