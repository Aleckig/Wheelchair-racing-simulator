import asyncio
from bleak import BleakClient

CSC_SERVICE_UUID = "00001816-0000-1000-8000-00805f9b34fb"
CSC_CHARACTERISTIC_UUID = "00002a5b-0000-1000-8000-00805f9b34fb"
DEVICE_MAC_ADDRESS = "F0:15:8A:61:5F:E6"  # device's MAC address
WHEEL_CIRCUMFERENCE = 0.262  # in meters

# Initialize with None for the first run
previous_wheel_revolutions = None
previous_event_time = None

def calculate_speed(wheel_revolutions, event_time, previous_wheel_revolutions, previous_event_time):
    if previous_wheel_revolutions is None or previous_event_time is None:
        # Insufficient data for speed calculation
        return None
    
    # Calculate revolutions difference
    revs_difference = wheel_revolutions - previous_wheel_revolutions
    
    # Time is reported in 1/1024 seconds, convert to hours for speed calculation
    time_difference = (event_time - previous_event_time) / (1024 * 3600)
    
    # Correct for potential rollover
    if time_difference < 0:
        time_difference += (1 << 16) / (1024 * 3600)  # assuming 16 bit counter
    
    # Distance in kilometers
    distance_km = (revs_difference * WHEEL_CIRCUMFERENCE) / 1000
    
    # Speed in km/h
    speed_kmh = distance_km / time_difference if time_difference > 0 else 0
    
    return speed_kmh

async def notification_handler(sender, data):
    global previous_wheel_revolutions, previous_event_time
    # Unpack data 
    wheel_revolutions, event_time = int.from_bytes(data[1:5], byteorder='little'), int.from_bytes(data[5:7], byteorder='little')

    speed_kmh = calculate_speed(wheel_revolutions, event_time, previous_wheel_revolutions, previous_event_time)
    if speed_kmh is not None:
        print(f"Speed: {speed_kmh:.2f} km/h")

    # Update for next calculation
    previous_wheel_revolutions, previous_event_time = wheel_revolutions, event_time

async def run_ble_client(address, loop):
    async with BleakClient(address, loop=loop) as client:
        x = await client.is_connected()
        print(f"Connected: {x}")

        await client.start_notify(CSC_CHARACTERISTIC_UUID, notification_handler)
        await asyncio.sleep(30)  # Keep listening for notifications for 30 seconds
        await client.stop_notify(CSC_CHARACTERISTIC_UUID)

if __name__ == "__main__":
    loop = asyncio.get_event_loop()
    loop.run_until_complete(run_ble_client(DEVICE_MAC_ADDRESS, loop))
