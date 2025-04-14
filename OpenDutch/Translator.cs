// this is the main class for the translator
// it also contains the word-level dictionary

namespace OpenDutch
{
    internal class Translator
    {
        private static Dictionary<string, string> _nouns = new Dictionary<string, string>
        {
            { "cat", "kat" },
            { "dog", "hond" },
            { "house", "huis" },
            { "car", "auto" },
            { "tree", "boom" },
            { "book", "boek" },
            { "question", "vraag" },
            { "answer", "antwoord" },
            { "computer", "computer" },
            { "phone", "telefoon" },
            { "apple", "appel" },
            { "table", "tafel" },
            { "chair", "stoel" },
            { "window", "raam" },
            { "door", "deur" },
            { "friend", "vriend" },
            { "family", "familie" },
            { "school", "school" },
            { "word", "woord" },
        };

        private static Dictionary<string, string> _verbs = new Dictionary<string, string>
        {
            { "run", "ren" },
            { "jump", "spring" },
            { "eat", "eet" },
            { "drink", "drink" },
            { "sleep", "slaap" },
            { "play", "speel" },
            { "are", "zijn" },
            { "make", "maak" },
            { "love", "houd van" },
            { "want", "wil" },
            { "have", "heb" },
            { "am", "ben" },
            { "do", "doe" },
            { "is", "is" },
        };

        private static Dictionary<string, string> _adjectives = new Dictionary<string, string>
        {
            { "big", "groot" },
            { "small", "klein" },
            { "fast", "snel" },
            { "slow", "traag" },
            { "happy", "blij" },
            { "quick", "snel" },
            { "good", "goed" },
            { "best", "best" },
            { "amazing", "geweldig" },
            { "beautiful", "mooi" },
            { "ugly", "lelijk" },
            { "funny", "grappig" },
            { "sad", "verdrietig" },
            { "angry", "boos" },
            { "tall", "lang" },
            { "short", "kort" },
            { "young", "jong" },
            { "old", "oud" }
        };

        private static Dictionary<string, string> _adverbs = new Dictionary<string, string>
        {
            { "quickly", "snel" },
            { "slowly", "langzaam" },
            { "happily", "blij" },
            { "sadly", "verdrietig" },
            { "angrily", "boos" },
            { "now", "nu" },
        };

        private static Dictionary<string, string> _prepositions = new Dictionary<string, string>
        {
            { "in", "in" },
            { "on", "op" },
            { "at", "bij" },
            { "with", "met" },
            { "to", "naar" },
        };

        private static Dictionary<string, string> _conjunctions = new Dictionary<string, string>
        {
            { "and", "en" },
            { "or", "of" },
            { "but", "maar" },
            { "because", "omdat" },
            { "if", "als" }
        };

        private static Dictionary<string, string> _pronouns = new Dictionary<string, string>
        {
            { "i", "ik" },
            { "me", "mij" }, // "me"?
            { "you", "jij" },
            { "he", "hij" },
            { "she", "zij" },
            { "we", "wij" },
            { "us", "ons" },
            { "they", "zij" },
            { "them", "hen" },
            { "it", "het" },
        };

        private static Dictionary<string, string> _articles = new Dictionary<string, string>
        {
            { "the", "de" },
            { "a", "een" },
            { "an", "een" },
        };

        private static Dictionary<string, Article> _articles_b = new Dictionary<string, Article>
        {
            { "appel", Article.De },
            { "auto", Article.De },
            { "huis", Article.Het },
            { "kat", Article.De },
            { "hond", Article.De },
            { "boom", Article.De },
            { "tafel", Article.De },
            { "stoel", Article.De },
            { "boek", Article.Het },
            { "computer", Article.De },
            { "telefoon", Article.De },
            { "raam", Article.Het },
            { "deur", Article.De },
            { "vraag", Article.De },
            { "antwoord", Article.Het },
            { "vriend", Article.De },
            { "familie", Article.De },
            { "school", Article.De },
            { "woord", Article.Het },
        };

        public static bool endsWithConsonant(string word)
        {
            string consonants = "bcdfghjklmnpqrstvwxyz";
            return consonants.Contains(word[word.Length - 1]);
        }

        public static void Init()
        {
            foreach (var verb in _verbs.ToList())
            {
                if (verb.Key.EndsWith('e'))
                {
                    _verbs.Add(verb.Key.Substring(0, verb.Key.Length - 1), verb.Value);
                }
            }
        }

        public static string FixTranslation(string translatedWord, string originalWord, WordForm wordForm, string previousEnglishWord, WordType nextWordType, WordType wordType, ref List<(string, WordType, WordForm)> translatedWords)
        {
            // adj
            if (wordType == WordType.Adjective && nextWordType == WordType.Noun && previousEnglishWord.ToLower() == "the")
            {
                translatedWord = translatedWord + "e";
            }
            // noun
            else if (wordType == WordType.Noun && wordForm == WordForm.Plural)
            {
                if (translatedWord.EndsWith("a") || translatedWord.EndsWith("e") || translatedWord.EndsWith("i") || translatedWord.EndsWith("o") || translatedWord.EndsWith("u"))
                {
                    translatedWord += "s";
                }
                else
                {
                    translatedWord += "en";
                }
            }

            if (wordType == WordType.Adjective && wordForm == WordForm.Comparative)
            {
                if (endsWithConsonant(translatedWord) && translatedWord.Length > 1)
                {
                    translatedWord += translatedWord[translatedWord.Length - 1] + "er";
                }
                else
                {
                    translatedWord += "er";
                }
            }
            if (wordForm == WordForm.Present && wordType == WordType.Verb)
            {
                if (endsWithConsonant(translatedWord) && translatedWord.Length > 2 && translatedWord[translatedWord.Length - 3] == translatedWord[translatedWord.Length - 2])
                {
                    translatedWord = translatedWord.Substring(0, translatedWord.Length - 2) + translatedWord[translatedWord.Length - 1] + "en";
                }
                else
                {
                    if (translatedWord.Contains(' '))
                    {
                        string tmp = translatedWord.Split(' ')[0];
                        translatedWord = tmp + "en " + string.Join(" ", translatedWord.Split(' ').Skip(1));
                    }
                    else
                    {
                        translatedWord = translatedWord + "en";
                    }
                }
                translatedWord = "aan het " + translatedWord;
            }
            else if (wordType == WordType.Verb && previousEnglishWord != "i" && originalWord != "is")
            {
                if (translatedWord.Contains(' '))
                {
                    string tmp = translatedWord.Split(' ')[0];
                    translatedWord = tmp + "t " + string.Join(" ", translatedWord.Split(' ').Skip(1));
                }
                else
                {
                    translatedWord = translatedWord + "t";
                    if (originalWord == "are")
                    {
                        translatedWord = "bent";
                    }
                }
            }
            return translatedWord;
        }

        public static string rootify(string word)
        {
            if (_nouns.ContainsKey(word.ToLower()) || _verbs.ContainsKey(word.ToLower()))
            {
                return word.ToLower();
            }
            if (word.EndsWith("ing"))
            {
                return word.Substring(0, word.Length - 3);
            }
            else if (word.EndsWith("ed"))
            {
                return word.Substring(0, word.Length - 2);
            }
            else if (word.EndsWith("s"))
            {
                return word.Substring(0, word.Length - 1);
            }
            if (word.EndsWith("er"))
            {
                return word.Substring(0, word.Length - 2);
            }
            else if (word.EndsWith("est"))
            {
                return word.Substring(0, word.Length - 3);
            }
            return word;
        }

        public static string Translate(string input)
        {
            string[] words = input.Split(' ');
            List<(string, WordType, WordForm)> translatedWords = new List<(string, WordType, WordForm)>();

            foreach (string word in words)
            {
                WordType previousWordType = translatedWords.Count > 0 ? translatedWords.Last().Item2 : WordType.None;
                WordType wordType = WordType.Unknown;
                WordForm wordForm = WordForm.None;
                string translatedWord = string.Empty;
                string wordC = rootify(word);
                if (_nouns.TryGetValue(wordC.ToLower(), out translatedWord) &&
                    (previousWordType != WordType.Noun || previousWordType == WordType.None))
                {
                    wordType = WordType.Noun;
                }
                else if (_verbs.TryGetValue(wordC.ToLower(), out translatedWord) &&
                         (previousWordType != WordType.Verb || previousWordType == WordType.None))
                {
                    wordType = WordType.Verb;
                }
                else if (_adjectives.TryGetValue(wordC.ToLower(), out translatedWord) &&
                         (previousWordType != WordType.Adjective && previousWordType != WordType.Verb || previousWordType == WordType.None))
                {
                    wordType = WordType.Adjective;
                }
                else if (_adverbs.TryGetValue(wordC.ToLower(), out translatedWord) &&
                         (previousWordType != WordType.Adverb || previousWordType == WordType.None))
                {
                    wordType = WordType.Adverb;
                }
                else if (_prepositions.TryGetValue(wordC.ToLower(), out translatedWord) &&
                         (previousWordType != WordType.Preposition || previousWordType == WordType.None))
                {
                    wordType = WordType.Preposition;
                }
                else if (_conjunctions.TryGetValue(wordC.ToLower(), out translatedWord) &&
                         (previousWordType != WordType.Conjunction || previousWordType == WordType.None))
                {
                    wordType = WordType.Conjunction;
                }
                else if (_pronouns.TryGetValue(wordC.ToLower(), out translatedWord) &&
                         (previousWordType != WordType.Pronoun || previousWordType == WordType.None))
                {
                    wordType = WordType.Pronoun;
                }
                // unconditional checks
                else if (_adjectives.TryGetValue(wordC.ToLower(), out translatedWord))
                {
                    wordType = WordType.Adjective;
                }
                else if (_articles.TryGetValue(wordC.ToLower(), out translatedWord))
                {
                    wordType = WordType.Article;
                }
                else if (_verbs.TryGetValue(wordC.ToLower(), out translatedWord))
                {
                    wordType = WordType.Verb;
                }
                else
                {
                    translatedWord = word; // No translation found, keep the original word
                    wordType = WordType.Unknown;
                }
                if (wordType == WordType.Adjective && word.EndsWith("er"))
                {
                    wordForm = WordForm.Comparative;
                }
                else if (wordType == WordType.Noun && word.EndsWith("s"))
                {
                    wordForm = WordForm.Plural;
                }
                else if (wordType == WordType.Adjective && word.EndsWith("est"))
                {
                    wordForm = WordForm.Superlative;
                }
                else if (wordType == WordType.Verb && word.EndsWith("ing"))
                {
                    wordForm = WordForm.Present;
                }
                translatedWords.Add((translatedWord, wordType, wordForm));
            }

            for (int i = 0; i < translatedWords.Count; i++)
            {
                var (translatedWord, wordType, wordForm) = translatedWords[i];
                WordType nextWordType = (i + 1 < translatedWords.Count) ? translatedWords[i + 1].Item2 : WordType.None;
                string previousEnglishWord = i > 0 ? words[i - 1] : string.Empty;
                string nextEnglishWord = i < translatedWords.Count - 1 ? words[i + 1] : string.Empty;
                string originalWord = words[i];
                translatedWords[i] = (translatedWord, wordType, wordForm);
                translatedWord = FixTranslation(translatedWord, originalWord.ToLower(), wordForm, previousEnglishWord.ToLower(), nextWordType, wordType, ref translatedWords);
                translatedWords[i] = (translatedWord, wordType, wordForm);
            }

            for (int i = 0; i < translatedWords.Count; i++)
            {
                var (translatedWord, wordType, wordForm) = translatedWords[i];
                WordType nextWordType = (i + 1 < translatedWords.Count) ? translatedWords[i + 1].Item2 : WordType.None;
                if (wordType == WordType.Adverb && nextWordType == WordType.Verb)
                {
                    translatedWords.RemoveAt(i);
                    translatedWords.Insert(i + 1, (translatedWord, wordType, wordForm));
                    i--;
                    continue;
                }
                if (wordType == WordType.Adverb && nextWordType == WordType.Pronoun)
                {
                    translatedWords.RemoveAt(i);
                    translatedWords.Insert(i + 1, (translatedWord, wordType, wordForm));
                    i--;
                    continue;
                }
                if (wordType == WordType.Article && nextWordType == WordType.Noun && translatedWord == "de")
                {
                    if (_articles_b.TryGetValue(translatedWords[i + 1].Item1.ToLower(), out Article article))
                    {
                        translatedWords.RemoveAt(i);
                        translatedWords.Insert(i, (article == Article.De ? "de" : "het", WordType.Article, WordForm.None));
                    }
                    else
                    {
                        translatedWords.RemoveAt(i);
                        translatedWords.Insert(i, ("een", WordType.Article, WordForm.None));
                    }
                    continue;
                }
                else if (wordType == WordType.Article && nextWordType == WordType.Adjective)
                {
                    bool isNextNextNoun = (i + 2 < translatedWords.Count) && translatedWords[i + 2].Item2 == WordType.Noun;
                    if (isNextNextNoun)
                    {
                        if (_articles_b.TryGetValue(translatedWords[i + 2].Item1.ToLower(), out Article article))
                        {
                            translatedWords.RemoveAt(i);
                            translatedWords.Insert(i, (article == Article.De ? "de" : "het", WordType.Article, WordForm.None));
                        }
                        else
                        {
                            translatedWords.RemoveAt(i);
                            translatedWords.Insert(i, ("een", WordType.Article, WordForm.None));

                        }
                    }
                }
                else if (wordType == WordType.Verb && wordForm == WordForm.Present)
                {
                    int j = i + 1;
                    while (j < translatedWords.Count && translatedWords[j].Item2 != WordType.Noun)
                    {
                        j++;
                    }
                    if (j < translatedWords.Count)
                    {
                        var temp = translatedWords[i];
                        translatedWords.RemoveAt(i);
                        translatedWords.Insert(j, temp);
                    }
                }
                string translatedWordNoT = translatedWord.ToLower();
                if (translatedWordNoT.EndsWith("t"))
                {
                    translatedWordNoT = translatedWordNoT.Substring(0, translatedWordNoT.Length - 1);
                }
                if (i == 0 && (translatedWordNoT == "doe" || translatedWordNoT == "is" || translatedWordNoT == "ben") && nextWordType == WordType.Pronoun)
                {
                    translatedWords.RemoveAt(i);
                    if (i + 1 < translatedWords.Count && translatedWords[i + 1].Item2 == WordType.Verb)
                    {
                        var temp = translatedWords[i + 1];
                        string afterSpace = temp.Item1;
                        string beforeSpace = temp.Item1;
                        if (afterSpace.Contains(' '))
                        {
                            afterSpace = string.Join(" ", afterSpace.Split(' ').Skip(1));
                            beforeSpace = beforeSpace.Split(' ')[0];
                        }
                        if (beforeSpace.EndsWith("t"))
                        {
                            if (temp.Item1.Contains(' '))
                            {
                                temp = (beforeSpace.Substring(0, beforeSpace.Length - 1) + " " + afterSpace, temp.Item2, temp.Item3);
                            }
                            else
                            {
                                temp = (beforeSpace.Substring(0, beforeSpace.Length - 1), temp.Item2, temp.Item3);
                            }
                        }
                        translatedWords.RemoveAt(i + 1);
                        if (temp.Item1.Contains(' '))
                        {
                            // Remove after space
                            string[] parts = temp.Item1.Split(' ');
                            string afterSpacePart = string.Join(" ", parts.Skip(1));
                            string beforeSpacePart = parts[0];
                            translatedWords.Insert(i, (beforeSpacePart, temp.Item2, temp.Item3));
                            translatedWords.Insert(i + 2, (afterSpacePart, temp.Item2, temp.Item3));
                        }
                        else
                        {
                            translatedWords.Insert(i, temp);
                        }
                    }
                    else
                    {
                        translatedWords.Insert(i, ("doe", WordType.Verb, WordForm.None));
                    }
                    i--;
                    continue;
                }
            }

            return string.Join(" ", translatedWords.Select(t => t.Item1));
        }
    }
}