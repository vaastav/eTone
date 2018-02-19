from django.shortcuts import render, redirect
from django.contrib.auth import login, authenticate
from eTone.forms import SignupForm, UploadFileForm
from django.http import HttpResponseRedirect
from eTone.handlers import upload_file_handler

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
        if request.FILES['myfile']:
            upload_file_handler(request.FILES['myfile'])
            return redirect('home')
        else:
            print("Invalid form")
    else :
        form = UploadFileForm()
    return render(request, 'upload.html', {'form': form})