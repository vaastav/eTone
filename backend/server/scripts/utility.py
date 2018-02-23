import scipy.io.wavfile
from django.conf import settings
import os

tones_dict = {1 : 'Tone_Series_1', 2 : 'Tone_Series_2', 3 : 'mid_mid_mid_mid', 4 : 'mid_low_high_low', 5 : 'mid_high_mid_high', 6 : 'high_mid_low_low_mid_mid', 7 : 'high_mid_low_low_low_mid_low_low'}

def read_wavfile(filename):
    """ Function to read the contents of a wav file

    Args:
        filename (str): Name of the wav file to be opened

    Returns:
        rate (int): Sample rate of the file
        data (nd-array): Contents of the wav file
    """
    rate, data = scipy.io.wavfile.read(filename)
    return rate, data

def write_wavfile(filename, rate, data):
    """ Function to write the wav contents to a temporary wav file

    Args:
        filename (str): Name of the new file to be created
        rate (int): Sample rate of the file
        data (nd-array): Contents of the wav file
    """
    scipy.io.wavfile.write(filename, rate, data)

def get_tone_link(id):
    filename = tones_dict[id] + '.wav'
    return filename

def get_num_links():
    return len(tones_dict)
