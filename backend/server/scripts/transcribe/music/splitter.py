from collections import OrderedDict
import numpy
from pydub import AudioSegment
from pydub.utils import get_array_type
from .notemap import notemap, notearr, Human_Lower_Bound, Human_Upper_Bound
import os

class SongSplitter(object):
    def __init__(self,
                 path,
                 pitch_detector=None,
                 plotter=None,
                 ms_increment=100):
        self.filename = os.path.splitext(os.path.basename(path))[0]
        sound = AudioSegment.from_file(file=path,
                                       format="wav").set_channels(1)
        self.sound_raw = numpy.frombuffer(
                sound._data,
                dtype=get_array_type(
                    sound.sample_width * 8)).astype(
                            numpy.float64, copy=False)
        self.sound_raw.setflags(write=1)

        self.raw_length = len(self.sound_raw)
        self.ms_increment = ms_increment
        self.raw_increment = int(self.ms_increment *
                                 (len(self.sound_raw) /
                                  sound.duration_seconds / 1000))
        self.sample_rate = sound.frame_rate
        self.pitch_detector = pitch_detector
        self.plotter = plotter

    def set_pitch_detector(self, p):
        self.pitch_detector = p

    def set_plotter(self, p):
        self.plotter = p

    def _iterate(self):
        tstamp = 0
        left_limit = 0
        while left_limit < self.raw_length:
            yield self.sound_raw[left_limit:min(
                left_limit + self.raw_increment, self.raw_length - 1)], tstamp
            left_limit += self.raw_increment
            tstamp += self.ms_increment

    def plot_transcription(self):
        if not self.pitch_detector or not self.plotter:
            raise ValueError('Call set_pitch_detector and set_plotter')
        data = OrderedDict()
        for sound_chunk, tstamp in self._iterate():
            pitch = self.pitch_detector.get_pitch(
                    sound_chunk, self.sample_rate)
            if pitch != -1:
                key = list(notemap.keys())[
                        numpy.abs(numpy.array(list(
                            notemap.values())) - pitch).argmin()]
                if is_human_pitch(key):
                    data[tstamp] = key
        self.plotter.plot_transcription_result(
                self.filename, data, notemap)

    def get_transcription(self):
        if not self.pitch_detector or not self.plotter:
            raise ValueError('Call set_pitch_detector and set_plotter')
        data = OrderedDict()
        for sound_chunk, tstamp in self._iterate():
            pitch = self.pitch_detector.get_pitch(
                    sound_chunk, self.sample_rate)
            if pitch != -1:
                data[tstamp] = list(notemap.keys())[
                        numpy.abs(numpy.array(list(
                            notemap.values())) - pitch).argmin()]
        return data


def get_node_distance(n1, n2):
    return int(abs(notearr.index(n1) - notearr.index(n2)))

def is_human_pitch(n):
    return notearr.index(n) > notearr.index(Human_Lower_Bound) and notearr.index(n) < notearr.index(Human_Upper_Bound)