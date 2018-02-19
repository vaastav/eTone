from scipy import signal, io
import scipy.fftpack as sf
import numpy as np
import matplotlib.pyplot as plt

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
    fig.show()

def match_tone(target, trial):
    """ Matches the tone of the 2 sounds

    Args:
        target (1d-array): The original sound that the user needs to reproduce
        trial  (1d-array): The reproduced sound

    Returns:
        accuracy (double): The accuracy, between 0-100 of how close the reproduced sound is to the original sound
    """
    plot_sounds(target, trial)
    return scipy.signal.correlate(target, trial)

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

    maximumFrequency = np.where(Spectrum == np.max(Spectrum[Low_point : High_point])) # Calculating which frequency has max power.

    return maximumFrequency