from django.urls import re_path
from django.conf.urls.static import static
from django.conf import settings
from django.contrib import admin
from django.contrib.auth import views as auth_views
from django.views.generic.base import TemplateView
from eTone import views as eTone_views

urlpatterns = [
    re_path(r'^admin/', admin.site.urls),
    re_path(r'^$', TemplateView.as_view(template_name='home.html'), name='home'),
    re_path(r'^login/$', auth_views.LoginView.as_view(), {'template_name': 'login.html'}, name='login'),
    re_path(r'^logout/$', auth_views.LogoutView.as_view(), {'template_name': 'logout.html'}, name='logout'),
    re_path(r'^signup/$', eTone_views.signup, name='signup'),
    re_path(r'^upload/$', eTone_views.upload_file, name='upload'),
    re_path(r'^game/$', eTone_views.select_sound_game, name='game'),
    re_path(r'^stats/$', eTone_views.get_stats, name='stats'),
    re_path(r'^api/upload/(?P<typeID>[^/]?)/(?P<filename>[^/]+)$', eTone_views.FileUploadView.as_view())
]

urlpatterns += static(settings.MEDIA_URL, document_root=settings.MEDIA_ROOT)
