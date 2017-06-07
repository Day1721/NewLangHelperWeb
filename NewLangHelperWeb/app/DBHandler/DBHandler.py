from ..models import CardGroup, WordCard
import random
from django.shortcuts import get_object_or_404
from itertools import chain

# READ


def group_with_name_exists(user, name):
    return user.cardgroup_set.filter(name=name).count() == 1


def get_groups_from_user(user):
    groups = CardGroup.objects.all().filter(public=True)

    result_list = list(chain(groups, user.users_with_access.all()))
    return result_list


def get_group_from_id(id):
    return get_object_or_404(CardGroup, pk=id)


def get_word_from_id_group(id, group_id):
    group = get_group_from_id(group_id)
    return get_object_or_404(group.words, pk=id)


def get_group_from_hash(hash):
    group = CardGroup.objects.all().filter(hash=hash)
    if not group:
        return None
    return group.get()


# UPDATE


def add_user_to_group(group, user):
    group.users.add(user)
    group.save()


def add_user_to_group_and_generate_hash(serializer, user):
    group = serializer.save(owner=user)
    add_user_to_group(group, user)
    group.hash = random.getrandbits(128)
    group.save()

def add_wordcards_to_group(wordcards, group):
    for word in wordcards:
        group.words.add(word)
