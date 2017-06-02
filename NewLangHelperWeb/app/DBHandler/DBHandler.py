from ..models import CardGroup, WordCard
import random


# READ

def get_groups_from_user(user):
    return user.users_with_access.all()


def get_words_from_group(group):
    return group.words.all()


def get_group_from_id(id):
    group = CardGroup.objects.all().filter(id=id)
    if not group:
        return None
    return group.get()


def get_word_from_id(id):
    word = WordCard.objects.all().filter(id=id)
    if not word:
        return None
    return word.get()


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
    group = serializer.save(user=user)
    group.users.add(user)
    hash = random.getrandbits(128)
    group.hash = hash
    group.save()


def add_wordcard_to_group(wordcard, group):
    group.words.add(wordcard)

def add_wordcards_to_group(wordcards, group):
    for word in wordcards:
        add_wordcard_to_group(word, group)
