import os
import tempfile
from scripts.utility import read_wavfile, write_wavfile
from scripts.tonematcher import plot_sounds, match_tone

def upload_file_handler(f):
    fd, path = tempfile.mkstemp()
    accuracy = 0.00
    try:
        with os.fdopen(fd, 'wb+') as tmp:
            for chunk in f.chunks():
                tmp.write(chunk)
        rate, data = read_wavfile(path)
        accuracy = match_tone(data, data)
    finally:
        os.remove(path)
    return accuracy