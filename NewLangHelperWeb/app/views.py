"""
Definition of views.
"""

from django.shortcuts import render
from django.http import HttpRequest, HttpResponse
from django.template import RequestContext
from datetime import datetime
from rest_framework import status
from rest_framework.decorators import api_view
from rest_framework.response import Response


def home(request):
    """Renders the home page."""
    assert isinstance(request, HttpRequest)
    return render(
        request,
        'app/index.html',
        {
            'title': 'Home Page',
            'year': datetime.now().year,
        }
    )


def contact(request):
    """Renders the contact page."""
    assert isinstance(request, HttpRequest)
    return render(
        request,
        'app/contact.html',
        {
            'title': 'Contact',
            'message': 'Your contact page.',
            'year': datetime.now().year,
        }
    )


def about(request):
    """Renders the about page."""
    assert isinstance(request, HttpRequest)
    return render(
        request,
        'app/about.html',
        {
            'title': 'About',
            'message': 'Your application description page.',
            'year': datetime.now().year,
        }
    )


from django.contrib.auth.models import User, Group
from .models import *
from rest_framework import viewsets
from .serializers import *
from django.views.decorators.http import require_POST, require_GET, require_http_methods
from .DBHandler import DBHandler

from django.http import HttpResponseRedirect
from rest_framework.views import APIView
from rest_framework.response import Response
from rest_framework import status
from rest_framework import generics, permissions
from rest_framework.decorators import api_view
from rest_framework.response import Response
from rest_framework.reverse import reverse
from rest_framework.renderers import AdminRenderer, TemplateHTMLRenderer
from rest_framework.authentication import TokenAuthentication
from rest_framework.permissions import IsAuthenticated


# Usage (need to be logged in)
# localhostL/groups
# GET - returns groups
# POST - creates a group
# {"name":name, "first_language":default, "second_language":default}
class GroupList(APIView):
    permission_classes = [IsAuthenticated]

    def get(self, request, format=None):
        print(request.user)
        groups = DBHandler.get_groups_from_user(request.user)
        serializer = GroupSerializer(groups, many=True, context={'request': request})
        resp = Response(serializer.data, status=status.HTTP_200_OK)
        resp["Access-Control-Allow-Headers"] = "Authentication"
        resp["Access-Control-Allow-Methods"] = "GET,POST,PUT,DELETE,OPTIONS"
        resp["Access-Control-Allow-Origin"] = '*'
        return resp

    def post(self, request, format=None):
        serializer = GroupSerializer(data=request.data, context={'request': request})
        if serializer.is_valid():
            serializer.save(user=self.request.user)
            return Response(serializer.data, status=status.HTTP_201_CREATED)
        return Response(serializer.errors, status=status.HTTP_400_BAD_REQUEST)


# Usage
# localhost:/groups/id/
# GET - Get a group details
# POST - Change a group model
# {'name':'name', 'first_language':'first', 'second_language':'second'}
class GroupDetail(APIView):
    permission_classes = [IsAuthenticated]

    def get(self, request, pk, format=None):
        group = DBHandler.get_group_from_id(pk)
        words = DBHandler.get_words_from_group(group)
        serializer = GroupSerializer(group, context={'request': request})
        return Response(serializer.data, status=status.HTTP_200_OK)

    def post(self, request, pk):
        group = DBHandler.get_group_from_id(pk)
        serializer = GroupSerializer(group, data=request.data, context={'request': request})
        if not serializer.is_valid():
            return Response(serializer.data, status=status.HTTP_400_BAD_REQUEST)
        serializer.save()
        return Response(serializer.data, status=status.HTTP_202_ACCEPTED)

    def delete(self, request, pk):
        group = DBHandler.get_group_from_id(pk)
        group.delete()
        return Response(status=status.HTTP_204_NO_CONTENT)


# Usage
# localhost:/groups/id/add_card
# { 'first_word':'word', 'second_word':'word'} adds a word
class AddCard(APIView):
    permission_classes = [IsAuthenticated]

    def get(self, request, pk, format=None):
        serializer = WordSerializer(context={'request': request})
        return Response(serializer.data, status=status.HTTP_200_OK)

    def post(self, request, pk):
        group = DBHandler.get_group_from_id(pk)
        serializer = WordSerializer(data=request.data, context={'request': request})
        if not serializer.is_valid():
            return Response(serializer.data, status=status.HTTP_400_BAD_REQUEST)
        word = serializer.save()
        DBHandler.add_wordcard_to_group(word, group)

        return Response({}, status=status.HTTP_201_CREATED)


# Usage
# localhost:/groups/group_id/word/word_id
# POST : { 'first_word':'word', 'second_word':'word'} updates a word

class CardDetail(APIView):
    permission_classes = [IsAuthenticated]

    def get(self, request, pk, word_pk):
        word = DBHandler.get_word_from_id(word_pk)
        serializer = WordSerializer(word, context={'request': request})
        return Response(serializer.data, status=status.HTTP_200_OK)

    def post(self, request, pk, word_pk):
        word = DBHandler.get_word_from_id(word_pk)
        serializer = WordSerializer(word, data=request.data, context={'request': request})
        if not serializer.is_valid():
            return Response(serializer.data, status=status.HTTP_400_BAD_REQUEST)
        serializer.save()
        return Response(serializer.data, status=status.HTTP_202_ACCEPTED)

    def delete(self, request, pk, word_pk):
        word = DBHandler.get_word_from_id(word_pk)
        word.delete()
        return Response({}, status=status.HTTP_204_NO_CONTENT)

# Trzeba dodać potem IsOwner czy coś w tym stylu
