"""
Definition of models.
"""

from django.db import models
from django.contrib.auth.models import User

# Create your models here.


class AppUser(models.Model):
    user = models.OneToOneField(User, on_delete=models.CASCADE)
    # tu pojawi się to co będzie potrzebne o użytkowniku


class WordCard(models.Model):
    first_word = models.CharField(max_length=100)
    second_word = models.CharField(max_length=100)

    #wstepnie to CharFieldy, potem można zrobić jakiś wybór z listy języków
    first_language = models.CharField(max_length=100)
    second_language = models.CharField(max_length=100)


class CardGroup(models.Model):
    user = models.ForeignKey(AppUser)
    name = models.CharField(max_length=100)

    # każde słowo może należeć do wielu grup i każda grupa może mieć wiele słów
    words = models.ManyToManyField(WordCard)

    class Meta:
        # kazdy uzytkownik moze miec jedna grupe o danej nazwie
        unique_together = ('user', 'name')

