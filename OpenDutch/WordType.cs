using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenDutch
{
    internal enum WordType
    {
        Noun,
        Verb,
        Conjunction,
        Preposition,
        Adverb,
        Pronoun,
        Adjective,
        Determiner,
        Article,
        Unknown,
        None
    }

    internal enum WordForm
    {
        Comparative,
        Superlative,
        Plural,
        Present,
        None,
    }
}
