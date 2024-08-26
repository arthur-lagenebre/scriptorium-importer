namespace MTG.Importer.Models;

public record Card(Guid OracleId, CardCost Cost, string Language, CardName Name, CardText Text, CardTypeline Typeline, CardColor Colors, CardColor ColorsIdentity, CardColor ColorsIndicator, string Layout, List<string> Keyword, List<string> ProducedMana, CardSet Set, List<CardFace> CardFaces, List<RelatedCard> RelatedCards, CardCreature? Creature, CardPlaneswalker? Planeswalker, CardVanguard? Vanguard);
