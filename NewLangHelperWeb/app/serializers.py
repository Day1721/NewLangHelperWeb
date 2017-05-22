from django.contrib.auth.models import User
from rest_framework import serializers
from app.models import CardGroup, WordCard


class UserSerializer(serializers.HyperlinkedModelSerializer):
    class Meta:
        model = User
        fields = ('url', 'username', 'email', 'groups')


class WordSerializer(serializers.HyperlinkedModelSerializer):
    class Meta:
        model = WordCard
        fields = ('pk', 'firstWord', 'secondWord')


class GroupSerializer(serializers.HyperlinkedModelSerializer):
    words = WordSerializer(many=True)

    class Meta:
        model = CardGroup
        fields = ('pk', 'url', 'name', 'firstLanguage', 'secondLanguage', 'words')

