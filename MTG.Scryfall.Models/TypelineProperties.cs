namespace MTG.Scryfall.Models;

public record TypelineProperties(string TypelineSeparator, string SubtypeSeparator, char[] TypeSeparators, List<string> Subtypes, List<string> Supertypes, List<string> Types);
