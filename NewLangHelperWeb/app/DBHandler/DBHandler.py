from ..models import CardGroup, WordCard


# READ

def get_groups_from_user(user):
    return list(CardGroup.objects.all().filter(user=user))


def get_words_from_group(group):
    return list(group.words.all())

# CREATE


def create_wordcard(data_dict):
    first_word = data_dict['Słowo']
    second_word = data_dict['Translacja']

    return WordCard(first_word=first_word, second_word=second_word)


def create_group(data_dict, user):
    name = data_dict['Nazwa']
    groups = get_groups_from_user(user)
    group_names = list(map(lambda g: group.name, groups))
    if name in group_names:
        return False
    first_language = data_dict['Język pierwszy']
    second_language = data_dict['Język drugi']
    group = CardGroup(user=user, name=name, first_language=first_language, second_language=second_language)
    group.save()
    return True

# UPDATE


def add_wordcard_to_group(wordcard, group):
    group.words.add(wordcard)
    return


def add_word_to_group(data_dict, group):
    wordcard = create_wordcard(data_dict)
    wordcard.save()
    add_wordcard_to_group(wordcard, group)
    return


def add_wordcards_to_group(word_dict_list, group):
    for word_dict in word_dict_list:
        add_word_to_group(word_dict, group)
    return


def update_wordcard(data_dict, wordcard):
    wordcard.first_word = data_dict['Słowo']
    wordcard.second_word = data_dict['Translacja']
    wordcard.save()


def update_group(data_dict, group):
    group.name = data_dict['name']
    group.first_language = data_dict['Język pierwszy']
    group.second_language = data_dict['Język drugi']
    group.save()

# DELETE


def remove_word_from_group(wordcard, group):
    group.words.all().filter(words=wordcard).delete()

