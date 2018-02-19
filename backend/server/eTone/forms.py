from django import forms
from django.contrib.auth.forms import UserCreationForm
from django.contrib.auth.models import User

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
    request_num = forms.IntegerField()
    user_id = forms.IntegerField()
    uploaded_file = forms.FileField()