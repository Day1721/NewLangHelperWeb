from django.conf.urls import url
from app import views

urlpatterns = [
    url(r'^groups/$', views.GroupList.as_view(), name='cardgroup-list'),
    url(r'^groups/(?P<pk>[0-9]+)/$', views.GroupDetail.as_view(), name='cardgroup-detail'),
    url(r'^groups/(?P<pk>[0-9]+)/add_card/$', views.AddCard.as_view(), name='wordcard-add'),
    url(r'^groups/(?P<pk>[0-9]+)/word/(?P<word_pk>[0-9]+)/$', views.CardDetail.as_view(), name='word-detail')
]