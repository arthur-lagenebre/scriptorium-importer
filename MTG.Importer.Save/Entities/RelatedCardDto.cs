namespace MTG.Importer.Save.Entities;

public record RelatedCardDto(Guid Id, Guid CardId, string Name, string Component);
