import scipy.io.wavfile
from django.conf import settings
import os
from .transcribe.music import SongSplitter
from .transcribe.music.splitter import get_node_distance
from .transcribe.pitch import Mpm
from .transcribe.plot import Plotter
import itertools

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

def convert_dict_to_array(d):
    array = []
    for k, v in d.items():
        array += [v]
    return array

def compare_transcription(t1, t2):
    penalty = 0
    for v1, v2 in zip(t1, t2):
        penalty += get_node_distance(v1, v2)
    return penalty

def get_transcription(filename):
    song = SongSplitter(filename)
    song.set_pitch_detector(Mpm())
    song.set_plotter(Plotter())
    song.plot_transcription()
    return song.get_transcription()

def get_tone_link(id):
    filename = tones_dict[id] + '.wav'
    return filename

def get_num_links():
    return len(tones_dict)
