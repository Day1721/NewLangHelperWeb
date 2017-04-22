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


class GroupList(APIView):
    renderer_classes = (TemplateHTMLRenderer,)
    template_name = 'app/group_list.html'

    def get(self, request, format=None):
        print("here2")
        groups = DBHandler.get_groups_from_user(request.user)
        serializer = GroupSerializer(groups, many=True, context={'request': request})
        return Response({'groups':groups})

    def post(self, request, format=None):
        serializer = GroupSerializer(data=request.data, context={'request': request})
        if serializer.is_valid():
            serializer.save(user=self.request.user)
            return Response(serializer.data, status=status.HTTP_201_CREATED)
        return Response(serializer.errors, status=status.HTTP_400_BAD_REQUEST)


class GroupDetail(APIView):
    renderer_classes = (TemplateHTMLRenderer,)
    template_name = 'app/group_detail.html'
    def get(self, request, pk, format=None):
        print ('here')
        group = DBHandler.get_group_from_id(pk)
        words = DBHandler.get_words_from_group(group)
        serializer = GroupSerializer(group, context={'request': request})
        return Response({'serializer':serializer, 'group':group})

    def post(self, request, pk):
        group = DBHandler.get_group_from_id(pk)
        serializer = GroupSerializer(group, data=request.data, context={'request': request})
        if not serializer.is_valid():
            return Response({'serializer': serializer, 'group':group})
        serializer.save()
        return Response({'serializer': serializer, 'group':group})


class AddCard(APIView):
    renderer_classes = (TemplateHTMLRenderer,)
    template_name = 'app/add_card.html'

    def get(self, request, pk, format=None):
        #group = DBHandler.get_group_from_id(pk)
        #words = DBHandler.get_words_from_group(group)
        serializer = WordSerializer(context={'request': request})
        return Response({'serializer':serializer, 'pk2':pk})

    def post(self, request, pk):
        group = DBHandler.get_group_from_id(pk)
        serializer = WordSerializer(data=request.data, context={'request': request})
        if not serializer.is_valid():
            return Response({'serializer': serializer, 'pk2':pk})
        serializer.save()
        return Response({'serializer': WordSerializer(context={'request': request}), 'pk2':pk})

