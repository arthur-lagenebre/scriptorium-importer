using Scriptorium.Mtg.Scryfall.Importer.Interfaces;
using Scriptorium.Mtg.Scryfall.Models;

namespace Scriptorium.Mtg.Scryfall.Importer.Builders;

/// <summary>
/// Layout « prepare », apparu avec Secrets of Strixhaven : une créature et
/// le sort qu'elle permet de lancer une fois préparée, réunis sur une même
/// carte. Structurellement identique à split — deux faces portant chacune
/// leur nom, leur coût et leur texte, avec un nom combiné par « // » à la
/// racine — mais la face avant garde force et endurance au niveau racine.
/// </summary>
public class PrepareBuilder : BaseScryfallBuilder
{
    public override Layout Layout => new("prepare");

    public PrepareBuilder() : base() { }

    public override IScryfallBuilder AddVanguard(string? handModifier, string? lifeModifier)
    {
        throw new NotSupportedException();
    }
}
