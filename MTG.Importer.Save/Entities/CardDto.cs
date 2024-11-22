namespace MTG.Importer.Save.Entities;

public record CardDto(Guid Id, string ManaCost, double ManaValue, int Colors, int ColorsIdentity, int ColorsIndicator, string Layout, List<string> Keyword, List<string> ProducedMana, string Power, string Toughness, string Loyalty, string HandModifier, string LifeModifier, List<CardNameDto> CardNames, List<CardTextDto> CardTexts, List<CardFaceDto> CardFaces, List<CardSetDto> CardSets, List<CardTypelineDto> CardTypelines, List<RelatedCardDto> RelatedCards, List<RulingDto> Rulings);
