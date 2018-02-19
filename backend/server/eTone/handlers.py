import os
import tempfile
from scripts.utility import read_wavfile, write_wavfile

def upload_file_handler(f):
    fd, path = tempfile.mkstemp()
    print(path)
    try:
        with os.fdopen(fd, 'wb+') as tmp:
            for chunk in f.chunks():
                tmp.write(chunk)
        rate, data = read_wavfile(path)
        print(rate)
    finally:
        os.remove(path)