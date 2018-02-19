from django.db import models

class ToneSample(models.Model):
    f = models.FileField()
    username = models.CharField(max_length=255)
    type_id = models.IntegerField()

    class Meta:
        app_label = 'eTone' 