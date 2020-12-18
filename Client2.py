import asyncio
import websockets

server_adress =""

async def hello(uri):
    try: 
     async with websockets.connect(uri) as websocket:
        await websocket.send("Gserv")
        global server_adress 
        server_adress = await websocket.recv()       
    except BaseException:
        print("Current port close")

i=0
count_ports = 3


while i<count_ports and server_adress=="": 
 port = 8001+i
 uri="ws://25.20.18.47:"+str(port)
 try:
  asyncio.get_event_loop().run_until_complete(hello(uri))
 except BaseException:
    print("Check next port") 
 i=i+1
 

print("Server adress: "+server_adress)