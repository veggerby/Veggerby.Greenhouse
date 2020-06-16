import socket
from camera import take_and_upload_photo

take_and_upload_photo(socket.gethostname())