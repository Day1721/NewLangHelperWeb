<<<<<<< HEAD
from django.urls import reverse
from rest_framework import status
from rest_framework.test import APITestCase
from django.contrib.auth.models import User


class GroupAuthenticationTests(APITestCase):
    def setUp(self):
        self.user = User.objects.create_user(
            username='Testuser', email='test@test.com', password='Testpassword')
        self.username = 'Testuser'
        self.password = 'Testpassword'
        self.url = reverse('cardgroup-list')

    def test_get_group_without_login(self):
        response = self.client.get(self.url, {}, format='json')

        self.assertEqual(response.status_code, status.HTTP_403_FORBIDDEN)

    def test_get_groups_with_login(self):
        self.client.login(username=self.username, password=self.password)
        response = self.client.get(self.url)

        self.assertEqual(response.status_code, status.HTTP_200_OK)

    #TODO same with post?


class GroupCreateTests(APITestCase):
    def setUp(self):
        self.user = User.objects.create_user(
            username='Testuser', email='test@test.com', password='Testpassword')
        self.username = 'Testuser'
        self.password = 'Testpassword'
        self.url = reverse('cardgroup-list')
        self.client.login(username=self.username, password=self.password)

    def test_create_group_with_defaults(self):

        data = {
            "name": "Testgroup"
        }

        response = self.client.post(self.url, data, format='json')

        self.assertEqual(response.status_code, status.HTTP_201_CREATED)

        data = {
            'firstLanguage': 'PL',
            'words': [],
            'name': 'Testgroup',
            'secondLanguage': 'EN'
        }

        del (response.data['pk'])
        del (response.data['url'])

        self.assertEqual(response.data, data)

    def test_create_group_without_defaults(self):
        data = {
            "name": "Testgroup",
            "words": [],
            "firstLanguage": "TestLang",
            "secondLanguage": "TestLang2",
        }

        response = self.client.post(self.url, data, format='json')

        self.assertEqual(response.status_code, status.HTTP_201_CREATED)

        data = {
            'firstLanguage': 'TestLang',
            'words': [],
            'name': 'Testgroup',
            'secondLanguage': 'TestLang2'
        }

        del(response.data['pk'])
        del(response.data['url'])

        self.assertEqual(response.data, data)

    def test_create_group_without_name(self):

        data = {
            "firstLanguage": "TestLang",
            "secondLanguage": "TestLang2",
        }

        response = self.client.post(self.url, data, format='json')

        self.assertEqual(response.status_code, status.HTTP_400_BAD_REQUEST)

        data = {
            'name': ['This field is required.'],
        }

        self.assertEqual(response.data, data)

    def test_create_group_with_rubbish(self):

        data = {
            'name': 'Testgroup',
            'garbage': 'field',
            'not': 'important'
        }

        response = self.client.post(self.url, data, format='json')

        self.assertEqual(response.status_code, status.HTTP_201_CREATED)

        data = {
            'firstLanguage': 'PL',
            'words': [],
            'name': 'Testgroup',
            'secondLanguage': 'EN'
        }

        del (response.data['pk'])
        del (response.data['url'])

        self.assertEqual(response.data, data)

    def tearDown(self):
        self.client.logout()


class GroupGetTests(APITestCase):
    def setUp(self):
        self.user = User.objects.create_user(
            username='Testuser', email='test@test.com', password='Testpassword')
        self.username = 'Testuser'
        self.password = 'Testpassword'
        self.url = reverse('cardgroup-list')
        self.client.login(username=self.username, password=self.password)

    def test_get_empty_groups(self):
        response = self.client.get(self.url, format='json')

        self.assertEqual(response.data, [])

    #TODO get one group/more groups
=======
"""
This file demonstrates writing tests using the unittest module. These will pass
when you run "manage.py test".
"""

import django
from django.test import TestCase

# TODO: Configure your database in settings.py and sync before running tests.

class ViewTest(TestCase):
    """Tests for the application views."""

    if django.VERSION[:2] >= (1, 7):
        # Django 1.7 requires an explicit setup() when running tests in PTVS
        @classmethod
        def setUpClass(cls):
            super(ViewTest, cls).setUpClass()
            django.setup()
>>>>>>> bf07793d56886014e68f26ec8741f65f46ad8ce7
