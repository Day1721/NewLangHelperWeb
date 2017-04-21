from django.conf.urls import url
from app import views

urlpatterns = [
    url(r'^groups/$', views.GroupList.as_view(), name='cardgroup-list'),
    url(r'^groups/(?P<pk>[0-9]+)/$', views.GroupDetail.as_view(), name='cardgroup-detail'),
]