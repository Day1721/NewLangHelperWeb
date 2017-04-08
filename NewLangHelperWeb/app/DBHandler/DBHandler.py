from ..models import CardGroup, WordCard


def get_groups_from_user(user):
    return list(CardGroup.objects.all().filter(user=user))


def get_words_from_group(group):
    return list(group.words.all())


def add_wordcard_to_group(wordcard, group):
    group.words.add(wordcard)
    return


def create_wordcard(data_dict):
    first_word = data_dict['Słowo']
    second_word = data_dict['Translacja']

    first_language = data_dict['Język pierwszy']
    second_language = data_dict['Język drugi']

    return WordCard(first_word=first_word, second_word=second_word,
                    first_language=first_language, second_language=second_language)


def add_word_to_group(data_dict, group):
    wordcard = create_wordcard(data_dict)
    wordcard.save()
    add_wordcard_to_group(wordcard, group)
    return


def add_wordcards_to_group(word_dict_list, group):
    for word_dict in word_dict_list:
        add_word_to_group(word_dict, group)
    return


def create_group(name, user):
    groups = get_groups_from_user(user)
    group_names = list(map(lambda g: group.name, groups))
    if name in group_names:
        return False
    group = CardGroup(user=user, name=name)
    group.save()
    return True
