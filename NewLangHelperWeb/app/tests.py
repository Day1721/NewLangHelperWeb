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
            'secondLanguage': 'EN',
            'public': False
        }

        del (response.data['pk'])
        del (response.data['url'])
        del (response.data['hash'])


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
            'secondLanguage': 'TestLang2',
            'public': False
        }

        del (response.data['pk'])
        del (response.data['url'])
        del (response.data['hash'])

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
            'not': 'important',
            'public': True
        }

        response = self.client.post(self.url, data, format='json')

        self.assertEqual(response.status_code, status.HTTP_201_CREATED)

        data = {
            'firstLanguage': 'PL',
            'words': [],
            'name': 'Testgroup',
            'secondLanguage': 'EN',
            'public': True
        }

        del (response.data['pk'])
        del (response.data['url'])
        del (response.data['hash'])

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