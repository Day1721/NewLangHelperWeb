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
        fields = ('first_word', 'second_word')


class GroupSerializer(serializers.HyperlinkedModelSerializer):
    #words = serializers.HyperlinkedRelatedField(many=True, view_name='cardgroup-detail', read_only=True)
    words = WordSerializer(many=True)
    class Meta:
        model = CardGroup
        fields = ('url', 'name', 'first_language', 'second_language', 'words')

