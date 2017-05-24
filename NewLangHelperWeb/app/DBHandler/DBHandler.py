from ..models import CardGroup, WordCard


# READ

def get_groups_from_user(user):
    return CardGroup.objects.all().filter(user=user)


def get_words_from_group(group):
    return group.words.all()


def get_group_from_id(id):
    return CardGroup.objects.all().get(id=id)


def get_word_from_id(id):
    return WordCard.objects.all().get(id=id)

# UPDATE


def add_wordcard_to_group(wordcard, group):
    group.words.add(wordcard)
    return
