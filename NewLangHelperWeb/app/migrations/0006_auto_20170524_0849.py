# -*- coding: utf-8 -*-
# Generated by Django 1.10.6 on 2017-05-24 06:49
from __future__ import unicode_literals

from django.db import migrations


class Migration(migrations.Migration):

    dependencies = [
        ('app', '0005_auto_20170415_1628'),
    ]

    operations = [
        migrations.RenameField(
            model_name='cardgroup',
            old_name='first_language',
            new_name='firstLanguage',
        ),
        migrations.RenameField(
            model_name='cardgroup',
            old_name='second_language',
            new_name='secondLanguage',
        ),
        migrations.RenameField(
            model_name='wordcard',
            old_name='first_word',
            new_name='firstWord',
        ),
        migrations.RenameField(
            model_name='wordcard',
            old_name='second_word',
            new_name='secondWord',
        ),
    ]
