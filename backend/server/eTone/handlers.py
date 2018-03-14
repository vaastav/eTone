import os
import tempfile
from scripts.utility import read_wavfile, write_wavfile, get_tone_link
from scripts.tonematcher import plot_sounds, match_tone

def upload_file_handler(f, id=1):
    fd, path = tempfile.mkstemp()
    accuracy = 0.00
    try:
        with os.fdopen(fd, 'wb+') as tmp:
            for chunk in f.chunks():
                tmp.write(chunk)
        rate, data = read_wavfile(path)
        filename = 'static/' + get_tone_link(id)
        rate2, data2 = read_wavfile(filename)
        accuracy = match_tone(data, data2)
    finally:
        os.remove(path)
    return accuracy