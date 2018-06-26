import scipy.stats as ss
import scipy.fftpack as sf
import scipy.spatial as ssp
import scipy.signal as ssig
import numpy as np
import matplotlib.pyplot as plt
#from utility import read_wavfile
from .utility import convert_dict_to_array, compare_transcription
from math import pow, log2


A4 = 440
C0 = A4*pow(2, -4.75)
name = ["C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B"]

def pitch(freq):
    h = round(12*log2(freq/C0))
    octave = h // 12
    n = h % 12
    return name[n] + str(octave)

def plot_sounds(original, trial):
    """ Plot the sound waves for a visual comparison

    Args:
        original (1d-array): The original sound that the user needs to reproduce
        trial  (1d-array): The reproduced sound
    """
    fig, (ax_original, ax_trial) = plt.subplots(2, 1, sharex=True)
    ax_original.plot(original)
    ax_original.set_title('Original signal')
    ax_trial.plot(trial)
    ax_trial.set_title('Reproduced signal')
    fig.tight_layout()
    fig.savefig('tmp.png')

def plot_sounds3(original, trial, aligned_trial):
    fig, (ax_original, ax_trial, ax_aligned) = plt.subplots(3, 1, sharex=True)
    ax_original.plot(original)
    ax_original.set_title('Original Signal')
    ax_trial.plot(trial)
    ax_trial.set_title('Reproduced Signal')
    ax_aligned.plot(aligned_trial)
    ax_aligned.set_title('Aligned signal')
    fig.tight_layout()
    fig.savefig('tmp.png')

def cross_correlation(target, trial):
    return ssig.correlate(target, trial, 'full')

def pearson_correlation(target, trial):
    """ Matches the tone of 2 sounds by using their pearson's correlation
    
        Args:
        target (1d-array): The original sound that the user needs to reproduce
        trial  (1d-array): The reproduced sound

    Returns:
        accuracy (double): The accuracy, between 0-100 of how close the reproduced sound is to the original sound
    """
    coeff_matrix = np.corrcoef(target, trial)
    accuracy = abs(coeff_matrix[0, 1])
    return 100 * (1 + accuracy) / 2

def discrete_cosine_transform(vector):
    return sf.dct(vector, norm='ortho')

def cosine_similarity(target, trial):
    accuracy = 1 - ssp.distance.cosine(target, trial)
    return accuracy * 100

def match_tone(target, trial):
    """ Matches the tone of the 2 sounds

    Args:
        target (1d-array): The original sound that the user needs to reproduce
        trial  (1d-array): The reproduced sound

    Returns:
        accuracy (double): The accuracy, between 0-100 of how close the reproduced sound is to the original sound
    """
    target2 = discrete_cosine_transform(target)
    trial2 = discrete_cosine_transform(trial)
    val = int(abs((len(trial2) - len(target2)) / 2))
    val2 = val
    if (val * 2) % 2 == 1:
        val2 += 1
    if len(target2) < len(trial2):
        target2 = np.pad(target2, (val, val2), 'mean')
    else:
        trial2 = np.pad(trial2, (val, val2), 'mean')
    print(len(target2))
    print(len(trial2))
    lag = np.argmax(ssig.correlate(target2, trial2))
    trial3 = np.roll(trial2, shift=int(np.ceil(lag+1)))
    plot_sounds3(target2, trial2, trial3)
    return pearson_correlation(target2, trial3)
    #return cosine_similarity(target2, trial3)

def match_tone2(target, trial):
    target2 = discrete_cosine_transform(target)
    trial2 = discrete_cosine_transform(trial)
    target3 = target2
    trial3 = trial2
    if len(target2) < len(trial2):
        lag = np.argmax(ssig.correlate(target2, trial2))
        trial3 = np.roll(trial2, shift=int(np.ceil(lag+1)))
        trial3 = trial3[:len(target2)]
        #target3 = np.pad(target2, (0, len(trial2) - len(target2)), 'constant', constant_values=(0,0))
    elif len(target2) > len(trial2):
        lag = np.argmax(ssig.correlate(trial2, target2))
        target3 = np.roll(target2, shift=int(np.ceil(lag+1)))
        target3 = target3[:len(trial2)]
        #trial3 = np.pad(trial2, (0, len(target2) - len(trial2)), 'constant', constant_values=(0,0))
    else:
        lag = np.argmax(ssig.correlate(target2, trial2))
        trial3 = np.roll(trial2, shift=int(np.ceil(lag+1)))
    print(len(target3))
    print(len(trial3))
    return cosine_similarity(target3, trial3)

def match_tone3(target, trial):
    M = min(len(target), len(trial))
    spec1 = sf.rfft(target, n=M)
    spec2 = sf.rfft(trial, n=M)
    lag = np.argmax(ssig.correlate(spec1, spec2))
    print(lag)
    spec3 = np.roll(spec2, shift=int(np.ceil(lag+1)))
    return cosine_similarity(spec1, spec3) 

def match_tone4(target, trial):
    """ Matches tone based on pitch transcriptions

    Args:
        target : Ordered Dict, transcription of the target file
        trial  : Ordered Dict, transcription of the trial file
    """
    target_arr = convert_dict_to_array(target)
    trial_arr = convert_dict_to_array(trial)
    penalty = compare_transcription(target_arr, trial_arr)
    return 100 - penalty * 5

def maxFrequency(X, F_sample, Low_cutoff=80, High_cutoff= 300):
    """ Searching presence of frequencies on a real signal using FFT

    Args:
        X: 1-D numpy array, the real time domain audio signal (single channel time series)
        Low_cutoff: float, frequency components below this frequency will not pass the filter (physical frequency in unit of Hz)
        High_cutoff: float, frequency components above this frequency will not pass the filter (physical frequency in unit of Hz)
        F_sample: float, the sampling frequency of the signal (physical frequency in unit of Hz)
    """        

    M = X.size # let M be the length of the time series
    Spectrum = sf.rfft(X, n=M) 
    [Low_cutoff, High_cutoff, F_sample] = map(float, [Low_cutoff, High_cutoff, F_sample])

    #Convert cutoff frequencies into points on spectrum
    [Low_point, High_point] = map(lambda F: F/F_sample * M, [Low_cutoff, High_cutoff])
    print(Low_point)
    print(High_point)
    print(Spectrum)
    #maximumFrequency = np.where(Spectrum == np.max(Spectrum[Low_point : High_point])) # Calculating which frequency has max power.
    maximumFrequency = 0
    return maximumFrequency

''' def main():
    rate1, data1 = read_wavfile("../../sounds/yoruba/mid_low_high_low_3.wav")
    rate2, data2 = read_wavfile("../../sounds/yoruba/mid_low_high_low.wav")
    print(len(data1))
    print(len(data2))
    print(match_tone(data1, data2))
    print(match_tone2(data1, data2))
    print(match_tone3(data1, data2))
    print(maxFrequency(data1, rate1))
    print(maxFrequency(data2, rate2))

if __name__ == '__main__':
    main() '''