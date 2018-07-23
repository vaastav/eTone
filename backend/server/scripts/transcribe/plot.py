import os
import time
import matplotlib.pylab
import matplotlib.pyplot


class Plotter(object):
    def __init__(self):
        self.font = {'fontname': 'DejaVu Sans'}

    def plot_transcription_result(self, name, data_dict, all_notes):
        items = [(float(timestamp), val) for timestamp, val in
                 data_dict.items()]
        timestamps, notes = zip(*items)
        pitches = [all_notes[x] for x in notes]
        matplotlib.pyplot.figure()
        matplotlib.pyplot.plot(timestamps, pitches, '-o')
        matplotlib.pyplot.yticks(pitches, notes)
        matplotlib.pyplot.title(os.path.basename(
            os.path.normpath(name)), **self.font)
        matplotlib.pyplot.xlabel('time (ms)', **self.font)
        matplotlib.pyplot.ylabel('note', rotation=0, labelpad=15, **self.font)
        matplotlib.pylab.savefig(
                '{0}.png'.format('static/trials/' + name))
