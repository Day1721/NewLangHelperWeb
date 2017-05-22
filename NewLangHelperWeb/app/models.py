"""
Definition of models.
"""

from django.db import models
from django.contrib.auth.models import User

# Create your models here.


class WordCard(models.Model):
    firstWord = models.CharField(max_length=100)
    secondWord = models.CharField(max_length=100)


class CardGroup(models.Model):
    user = models.ForeignKey(User)
    name = models.CharField(max_length=100)

    # każde słowo może należeć do wielu grup i każda grupa może mieć wiele słów
    words = models.ManyToManyField(WordCard, blank=True)

    # wstepnie to CharFieldy, potem można zrobić jakiś wybór z listy języków
    firstLanguage = models.CharField(max_length=100, default='PL')
    secondLanguage = models.CharField(max_length=100, default='EN')

    class Meta:
        # kazdy uzytkownik moze miec jedna grupe o danej nazwie
        unique_together = ('user', 'name')
