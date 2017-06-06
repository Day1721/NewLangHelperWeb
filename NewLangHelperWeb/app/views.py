from rest_framework import status
from rest_framework.permissions import IsAuthenticatedOrReadOnly
from rest_framework.response import Response
from rest_framework.views import APIView

from app.permissions import IsAuthenticatedOrOptions, HasAccess
from .DBHandler import DBHandler
from .serializers import *


# Usage (need to be logged in)
# /groups/
# GET - returns groups
# POST - creates a group, gets a JSON not a list
# {"name":name, "first_language":default, "second_language":default}


class GroupList(APIView):
    permission_classes = [IsAuthenticatedOrOptions]

    def get(self, request):
        groups = DBHandler.get_groups_from_user(request.user)
        serializer = GroupSerializer(groups, many=True, context={'request': request})
        return Response(serializer.data, status=status.HTTP_200_OK)

    def post(self, request):
        serializer = GroupSerializer(data=request.data, context={'request': request})
        if serializer.is_valid():
            if DBHandler.group_with_name_exists(request.user, request.data['name']):  # TODO dbhandler
                return Response({
                    'detail': 'Group with the same name already exists.'
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
    permission_classes = [IsAuthenticatedOrOptions, HasAccess]

    def get(self, request, pk):
        group = DBHandler.get_group_from_id(pk)

        self.check_object_permissions(request, group)

        serializer = GroupSerializer(group, context={'request': request})
        return Response(serializer.data, status=status.HTTP_200_OK)

    def patch(self, request, pk):
        group = DBHandler.get_group_from_id(pk)
        self.check_object_permissions(request, group)

        serializer = GroupSerializer(group, data=request.data, context={'request': request})
        if not serializer.is_valid():
            return Response(serializer.data, status=status.HTTP_400_BAD_REQUEST)
        serializer.save()
        return Response(serializer.data, status=status.HTTP_202_ACCEPTED)

    def delete(self, request, pk):
        group = DBHandler.get_group_from_id(pk)
        self.check_object_permissions(request, group)

        group.delete()
        return Response(status=status.HTTP_204_NO_CONTENT)


# Usage
# /groups/{id}/add-card
# POST { 'first_word':'word', 'second_word':'word'} adds a word
# POST gets a list of JSONs, not a JSON


class AddCard(APIView):
    permission_classes = [IsAuthenticatedOrReadOnly, HasAccess]

    def get(self, request, pk):
        group = DBHandler.get_group_from_id(pk)
        self.check_object_permissions(request, group)

        serializer = WordSerializer(context={'request': request})
        return Response(serializer.data, status=status.HTTP_200_OK)

    def post(self, request, pk):
        group = DBHandler.get_group_from_id(pk)
        self.check_object_permissions(request, group)

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
    permission_classes = [IsAuthenticatedOrReadOnly, HasAccess]

    def get(self, request, pk, word_pk):

        group = DBHandler.get_group_from_id(pk)
        self.check_object_permissions(request, group)

        word = DBHandler.get_word_from_id_group(word_pk, pk)

        serializer = WordSerializer(word, context={'request': request})
        return Response(serializer.data, status=status.HTTP_200_OK)

    def patch(self, request, pk, word_pk):
        group = DBHandler.get_group_from_id(pk)
        self.check_object_permissions(request, group)

        word = DBHandler.get_word_from_id_group(word_pk, pk)

        serializer = WordSerializer(word, data=request.data, context={'request': request})
        if not serializer.is_valid():
            return Response(serializer.data, status=status.HTTP_400_BAD_REQUEST)
        serializer.save()
        return Response(serializer.data, status=status.HTTP_202_ACCEPTED)

    def delete(self, request, pk, word_pk):

        group = DBHandler.get_group_from_id(pk)
        self.check_object_permissions(request, group)

        word = DBHandler.get_word_from_id_group(word_pk, pk)

        word.delete()
        return Response({}, status=status.HTTP_204_NO_CONTENT)

#Usage
# /invite/{hash}
# GET : if hash corresponds to a group, user is added to this group

class Invitation(APIView):
    permission_classes = [IsAuthenticatedOrOptions]

    def get(self, request, hash):
        group = DBHandler.get_group_from_hash(hash)

        if group is None:
            return Response({'detail': 'Wrong invitation link.'}, status=status.HTTP_404_NOT_FOUND)

        DBHandler.add_user_to_group(group, request.user)

        return Response({
            'pk': group.pk
        }, status=status.HTTP_200_OK)
