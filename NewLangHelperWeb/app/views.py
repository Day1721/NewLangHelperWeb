from .serializers import *
from .DBHandler import DBHandler
from app.permissions import IsAuthenticatedOrOptions
from rest_framework.views import APIView
from rest_framework import status
from rest_framework.response import Response
from rest_framework.permissions import IsAuthenticated, IsAuthenticatedOrReadOnly
from app.models import CardGroup


def group_exists(pk):
    group = DBHandler.get_group_from_id(pk)

    if group == None:
        return False, group

    return True, group


def word_exists(group_pk, pk):
    exists, group = group_exists(group_pk)

    if not exists:
        return False, None

    word = DBHandler.get_word_from_id(pk)

    if word in DBHandler.get_words_from_group(group):
        return True, word

    return False, None

# Usage (need to be logged in)
# /groups/
# GET - returns groups
# POST - creates a group, gets a JSON not a list
# {"name":name, "first_language":default, "second_language":default}


class GroupList(APIView):
    permission_classes = [IsAuthenticatedOrOptions]

    def get(self, request, format=None):
        groups = DBHandler.get_groups_from_user(request.user)
        serializer = GroupSerializer(groups, many=True, context={'request': request})
        return Response(serializer.data, status=status.HTTP_200_OK)

    def post(self, request, format=None):
        serializer = GroupSerializer(data=request.data, context={'request': request})
        if serializer.is_valid():
            if request.user.cardgroup_set.filter(name=request.data['name']).count() == 1: # TODO dbhandler
                return Response({
                    'error': 'Group with the same name already exists.'
                }, status=status.HTTP_400_BAD_REQUEST)

            DBHandler.add_user_to_group_and_generate_hash(serializer, request.user)
            return Response(serializer.data, status=status.HTTP_201_CREATED)
        return Response(serializer.errors, status=status.HTTP_400_BAD_REQUEST)


# Usage
# /groups/{id}/
# GET - Get a group details
# PATCH - Change a group model, gets a JSON not a list
# DELETE - deletes a group
# {'name':'name', 'first_language':'first', 'second_language':'second'}


class GroupDetail(APIView):
    permission_classes = [IsAuthenticatedOrOptions]

    def get(self, request, pk, format=None):
        exists, group = group_exists(pk)
        if request.user not in group.users.all():
            return Response({'error': 'You are not allowed to view this site.'}, status=status.HTTP_403_FORBIDDEN)

        if not exists:
            return Response({
                'error': 'Group does not exist.'
            }, status=status.HTTP_404_NOT_FOUND)


        serializer = GroupSerializer(group, context={'request': request})
        return Response(serializer.data, status=status.HTTP_200_OK)

    def patch(self, request, pk):
        exists, group = group_exists(pk)

        if request.user != group.user:
            return Response({'error': 'You are not allowed to view this site.'}, status=status.HTTP_403_FORBIDDEN)
        if not exists:
            return Response({
                'error': 'Group does not exist.'
            }, status=status.HTTP_404_NOT_FOUND)

        serializer = GroupSerializer(group, data=request.data, context={'request': request})
        if not serializer.is_valid():
            return Response(serializer.data, status=status.HTTP_400_BAD_REQUEST)
        serializer.save()
        return Response(serializer.data, status=status.HTTP_202_ACCEPTED)

    def delete(self, request, pk):
        exists, group = group_exists(pk)

        if request.user != group.user:
            return Response({'error': 'You are not allowed to view this site.'}, status=status.HTTP_403_FORBIDDEN)
        if not exists:
            return Response({
                'error': 'Group does not exist.'
            }, status=status.HTTP_404_NOT_FOUND)

        group.delete()
        return Response(status=status.HTTP_204_NO_CONTENT)


# Usage
# /groups/{id}/add-card
# POST { 'first_word':'word', 'second_word':'word'} adds a word
# POST gets a list of JSONs, not a JSON


class AddCard(APIView):
    permission_classes = [IsAuthenticatedOrReadOnly]

    def get(self, request, pk, format=None):
        exists, group = group_exists(pk)

        if request.user != group.user:
            return Response({'error': 'You are not allowed to view this site.'}, status=status.HTTP_403_FORBIDDEN)
        if not exists:
            return Response({
                'error': 'Group does not exist.'
            }, status=status.HTTP_404_NOT_FOUND)

        serializer = WordSerializer(context={'request': request})
        return Response(serializer.data, status=status.HTTP_200_OK)

    def post(self, request, pk):
        exists, group = group_exists(pk)

        if request.user != group.user:
            return Response({'error': 'You are not allowed to view this site.'}, status=status.HTTP_403_FORBIDDEN)
        if not exists:
            return Response({
                'error': 'Group does not exist.'
            }, status=status.HTTP_404_NOT_FOUND)

        serializer = WordSerializer(data=request.data, many=True, context={'request': request})
        if not serializer.is_valid():
            return Response(serializer.data, status=status.HTTP_400_BAD_REQUEST)
        word = serializer.save()
        DBHandler.add_wordcards_to_group(word, group)

        return Response({}, status=status.HTTP_201_CREATED)


# Usage
# /groups/{group_id}/word/{word_id}
# PATCH : { 'first_word':'word', 'second_word':'word'} updates a word
# PATCH gets a JSON, not a list


class CardDetail(APIView):
    permission_classes = [IsAuthenticatedOrReadOnly]

    def get(self, request, pk, word_pk):

        exists, group = group_exists(pk)

        if request.user not in group.users.all():
            return Response({'error': 'You are not allowed to view this site.'}, status=status.HTTP_403_FORBIDDEN)
        if not exists:
            return Response({
                'error': 'Group does not exist.'
            }, status=status.HTTP_404_NOT_FOUND)

        exists, word = word_exists(pk, word_pk)

        if not exists:
            return Response({'error': 'Word does not exist in this group.'}, status=status.HTTP_404_NOT_FOUND)

        serializer = WordSerializer(word, context={'request': request})
        return Response(serializer.data, status=status.HTTP_200_OK)

    def patch(self, request, pk, word_pk):
        exists, group = group_exists(pk)

        if request.user != group.user:
            return Response({'error': 'You are not allowed to view this site.'}, status=status.HTTP_403_FORBIDDEN)
        if not exists:
            return Response({
                'error': 'Group does not exist.'
            }, status=status.HTTP_404_NOT_FOUND)

        exists, word = word_exists(pk, word_pk)

        if not exists:
            return Response({'error': 'Word does not exist in this group.'}, status=status.HTTP_404_NOT_FOUND)

        serializer = WordSerializer(word, data=request.data, context={'request': request})
        if not serializer.is_valid():
            return Response(serializer.data, status=status.HTTP_400_BAD_REQUEST)
        serializer.save()
        return Response(serializer.data, status=status.HTTP_202_ACCEPTED)

    def delete(self, request, pk, word_pk):

        exists, group = group_exists(pk)

        if request.user != group.user:
            return Response({'error': 'You are not allowed to view this site.'}, status=status.HTTP_403_FORBIDDEN)
        if not exists:
            return Response({
                'error': 'Group does not exist.'
            }, status=status.HTTP_404_NOT_FOUND)

        exists, word = word_exists(pk, word_pk)

        if not exists:
            return Response({'error': 'Word does not exist in this group.'}, status=status.HTTP_404_NOT_FOUND)

        word.delete()
        return Response({}, status=status.HTTP_204_NO_CONTENT)

#TODO wywalić te brzydkie rzeczy do funkcji


class Invitation(APIView):
    permission_classes = [IsAuthenticatedOrOptions]

    def get(self, request, hash):
        group = DBHandler.get_group_from_hash(hash)

        if group is None:
            return Response({'error': 'Wrong invitation link.'}, status=status.HTTP_404_NOT_FOUND)

        DBHandler.add_user_to_group(group, request.user)

        return Response({
            'pk': group.pk
        }, status=status.HTTP_200_OK)
