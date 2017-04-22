from rest_framework import permissions


class IsOwner(permissions.BasePermission):

    def has_object_permission(self, request, view, obj):
        print (obj.user, request.user)
        # Write permissions are only allowed to the owner of the snippet.
        return obj.user == request.user