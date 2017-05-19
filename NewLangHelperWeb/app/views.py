from .serializers import *
from .DBHandler import DBHandler
from app.permissions import IsAuthenticatedOrOptions
from rest_framework.views import APIView
from rest_framework import status
from rest_framework.response import Response
from rest_framework.permissions import IsAuthenticated, IsAuthenticatedOrReadOnly


# Usage (need to be logged in)
# /groups/
# GET - returns groups
# POST - creates a group
# {"name":name, "first_language":default, "second_language":default}


class GroupList(APIView):
    permission_classes = [IsAuthenticatedOrOptions]

    def get(self, request, format=None):
        print(request.user)
        groups = DBHandler.get_groups_from_user(request.user)
        serializer = GroupSerializer(groups, many=True, context={'request': request})
        resp = Response(serializer.data, status=status.HTTP_200_OK)
        return resp

    def post(self, request, format=None):
        serializer = GroupSerializer(data=request.data, context={'request': request})
        if serializer.is_valid():
            serializer.save(user=self.request.user)
            return Response(serializer.data, status=status.HTTP_201_CREATED)
        return Response(serializer.errors, status=status.HTTP_400_BAD_REQUEST)


# Usage
# /groups/{id}/
# GET - Get a group details
# POST - Change a group model
# {'name':'name', 'first_language':'first', 'second_language':'second'}


class GroupDetail(APIView):
    permission_classes = [IsAuthenticatedOrOptions]

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
# /groups/{id}/add-card
# { 'first_word':'word', 'second_word':'word'} adds a word


class AddCard(APIView):
    permission_classes = [IsAuthenticatedOrReadOnly]

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
# /groups/{group_id}/word/{word_id}
# POST : { 'first_word':'word', 'second_word':'word'} updates a word


class CardDetail(APIView):
    permission_classes = [IsAuthenticatedOrReadOnly]

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
