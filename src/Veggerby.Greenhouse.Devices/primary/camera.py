import io, os, uuid, time

from datetime import datetime

from azure.core.exceptions import ResourceExistsError
from azure.storage.blob import BlobServiceClient, BlobClient, ContainerClient
from picamera import PiCamera, Color

now = datetime.utcnow()

def take_photo(hflip = False):
    stream = io.BytesIO()

    with PiCamera() as camera:
        camera.vflip = hflip
        camera.resolution = (1024, 768)
        time.sleep(1) # Camera warm-up time

        camera.annotate_background = Color('blue')
        camera.annotate_foreground = Color('white')

        camera.annotate_text = now.strftime("%m/%d/%Y, %H:%M:%S")
        camera.capture(stream, 'jpeg')

    stream.truncate()
    stream.seek(0)

    return stream


def take_and_upload_photo(container_name, hflip = False):
    try:
        now = datetime.utcnow()

        connect_str = os.getenv('AZURE_STORAGE_CONNECTION_STRING')

        blob_service_client = BlobServiceClient.from_connection_string(
            connect_str)

        try:
            blob_service_client.create_container(container_name)
            #properties = new_container.get_container_properties()
        except ResourceExistsError:
            print("Container already exists.")

        remote_file_name = now.strftime('%Y\\%m\\%d') + '\\photo_' + \
            now.replace(microsecond=0).isoformat() + 'Z.jpg'

        blob_client = blob_service_client.get_blob_client(
            container=container_name, blob=remote_file_name)

        stream = take_photo(hflip)

        # Upload the created file
        blob_client.upload_blob(stream)
    except Exception as ex:
        print('Exception:')
        print(ex)
