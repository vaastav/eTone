from django import forms
from django.contrib.auth.forms import UserCreationForm
from django.contrib.auth.models import User
from eTone.models import ToneSample

class SignupForm(UserCreationForm):
    first_name = forms.CharField(max_length=30, required=True)
    last_name = forms.CharField(max_length=30, required=True)
    email = forms.EmailField(max_length=255, help_text='Required')
    # TODO Create user ID

    class Meta:
        model = User
        fields = ('username', 'first_name', 'last_name', 'email', 'password1', 'password2')

class UploadFileForm(forms.Form):
    title = forms.CharField(max_length=50)
    uploaded_file = forms.FileField()

class ToneSampleForm(forms.ModelForm):
    class Meta:
        model = ToneSample
        fields = ('f', 'username', 'type_id')