from scipy import io

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