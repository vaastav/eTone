from django.db import models

class ToneSample(models.Model):
    f = models.FileField()
    username = models.CharField(max_length=255)
    type_id = models.IntegerField()

    class Meta:
        app_label = 'eTone' 

class Sound(models.Model):
    name = models.CharField(max_length=255)
    type_id = models.IntegerField()
    audio_file = models.FileField()

class Score(models.Model):
    username = models.CharField(max_length=255)
    score = models.FloatField()