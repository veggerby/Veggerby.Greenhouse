import os, uuid, time

from datetime import datetime

from azure.storage.blob import BlobServiceClient, BlobClient, ContainerClient
from picamera import PiCamera, Color

# Retrieve the connection string for use with the application. The storage
# connection string is stored in an environment variable on the machine
# running the application called AZURE_STORAGE_CONNECTION_STRING. If the environment variable is
# created after the application is launched in a console or with Visual Studio,
# the shell or application needs to be closed and reloaded to take the
# environment variable into account.
connect_str = os.getenv('AZURE_STORAGE_CONNECTION_STRING')

# Create the BlobServiceClient object which will be used to create a container client
blob_service_client = BlobServiceClient.from_connection_string(connect_str)

# Create a unique name for the container
container_name = "greenhouse"

# Create the container
#container_client = blob_service_client.create_container(container_name)

# Create a file in local data directory to upload and download
local_path = "/home/pi/img/"
local_file_name = "image.jpg"
upload_file_path = os.path.join(local_path, local_file_name)

remote_file_name = 'photo_' + now.isoformat() + 'Z.jpg'

blob_client = blob_service_client.get_blob_client(container=container_name, blob=remote_file_name)

# Upload the created file
with open(upload_file_path, "rb") as data:
    blob_client.upload_blob(data)

try:
    now = datetime.utcnow()

    camera = PiCamera()
    camera.resolution = (1024, 768)
    camera.annotate_background = Color('blue')
    camera.annotate_foreground = Color('white')

    camera.annotate_text = now.strftime("%m/%d/%Y, %H:%M:%S")
    camera.capture('/home/pi/img/image.jpg')

except Exception as ex:
    print('Exception:')
    print(ex)
