from django.shortcuts import render, redirect
from django.contrib.auth import login, authenticate
from eTone.forms import SignupForm, UploadFileForm, ToneSampleForm
from django.http import HttpResponseRedirect, JsonResponse
from eTone.handlers import upload_file_handler
from scripts.utility import get_tone_link, get_num_links
from eTone.models import Sound
import random

def signup(request):
    if request.method == 'POST':
        form = SignupForm(request.POST)
        if form.is_valid():
            form.save()
            username = form.cleaned_data.get('username')
            raw_password = form.cleaned_data.get('password1')
            user = authenticate(username=username, password=raw_password)
            login(request, user)
            return redirect('home')
    else:
        form = SignupForm()
    return render(request, 'signup.html', {'form': form})

def upload_file(request):
    if request.method == 'POST':
        form = ToneSampleForm(request.POST, request.FILES)
        if form.is_valid():
            accuracy = upload_file_handler(form.cleaned_data.get('f'))
            return JsonResponse({'accuracy' : accuracy})
        else:
            print("Invalid form")
    else :
        num = random.randint(1, get_num_links())
        song_address = get_tone_link(num)
        sound = Sound(name="blah", type_id=num, audio_file=song_address)
        sound.save()
        form = ToneSampleForm(initial={'username' : request.user.username, 'type_id' : num})
    return render(request, 'upload.html', {'form': form, 'sound': sound})

def select_sound_game(request):
    num = random.randint(1, get_num_links())
    song_address = get_tone_link(num)
    sound = Sound(name="blah", type_id=num, audio_file=song_address)
    sound.save()
    return render(request, 'game.html', {'sound': sound})