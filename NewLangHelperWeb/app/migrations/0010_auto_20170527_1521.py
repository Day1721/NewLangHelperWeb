# -*- coding: utf-8 -*-
# Generated by Django 1.10.6 on 2017-05-27 15:21
from __future__ import unicode_literals

from django.db import migrations


class Migration(migrations.Migration):

    dependencies = [
        ('app', '0009_auto_20170527_1512'),
    ]

    operations = [
        migrations.RenameField(
            model_name='cardgroup',
            old_name='link',
            new_name='hash',
        ),
    ]
