from pydantic import BaseModel

class Log(BaseModel):
    rssi : int
    ip_address : str
    angle : int
    tag_epc : str
