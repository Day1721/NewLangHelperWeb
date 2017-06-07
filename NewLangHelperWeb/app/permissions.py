from rest_framework.permissions import BasePermission
from app.models import WordCard


class IsAuthenticatedOrOptions(BasePermission):

    def has_permission(self, request, view):
        return request.method == 'OPTIONS' or request.user and request.user.is_authenticated

    def has_object_permission(self, request, view, obj):
        return True


class HasAccess(BasePermission):
    def has_permission(self, request, view):
        return True

    def has_object_permission(self, request, view, obj):
        if request.method == 'GET':
            return request.user in obj.users.all() or obj.public

        return type(obj) is WordCard or request.user == obj.owner


