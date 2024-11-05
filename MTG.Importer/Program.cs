using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MTG.Importer.Save;
using MTG.Importer.Save.Interfaces;
using MTG.Scryfall.Importer;
using MTG.Scryfall.Importer.Builders;
using MTG.Scryfall.Importer.Interfaces;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

builder.Services.AddSingleton<IScryfallCardDirector, ScryfallCardDirector>();
builder.Services.AddSingleton<IScryfallGetter, ScryfallGetter>();
builder.Services.AddSingleton<IScryfallImporter, ScryfallImporter>();
builder.Services.AddSingleton<IScryfallMapper, ScryfallMapper>();
builder.Services.AddSingleton<IScryfallReader, ScryfallReader>();
builder.Services.AddSingleton<IScryfallTypelineManager, ScryfallTypelineManager>();

builder.Services.AddSingleton<IScryfallBuilder, AdventureBuilder>();
builder.Services.AddSingleton<IScryfallBuilder, AugmentBuilder>();
builder.Services.AddSingleton<IScryfallBuilder, CaseBuilder>();
builder.Services.AddSingleton<IScryfallBuilder, ClassBuilder>();
builder.Services.AddSingleton<IScryfallBuilder, DoubleFacedTokenBuilder>();
builder.Services.AddSingleton<IScryfallBuilder, EmblemBuilder>();
builder.Services.AddSingleton<IScryfallBuilder, FlipBuilder>();
builder.Services.AddSingleton<IScryfallBuilder, HostCreatureBuilder>();
builder.Services.AddSingleton<IScryfallBuilder, LevelerBuilder>();
builder.Services.AddSingleton<IScryfallBuilder, MeldBuilder>();
builder.Services.AddSingleton<IScryfallBuilder, ModalDoublefaceBuilder>();
builder.Services.AddSingleton<IScryfallBuilder, MutateBuilder>();
builder.Services.AddSingleton<IScryfallBuilder, NormalBuilder>();
builder.Services.AddSingleton<IScryfallBuilder, PlanarBuilder>();
builder.Services.AddSingleton<IScryfallBuilder, PrototypeBuilder>();
builder.Services.AddSingleton<IScryfallBuilder, ReversibleCardBuilder>();
builder.Services.AddSingleton<IScryfallBuilder, SagaBuilder>();
builder.Services.AddSingleton<IScryfallBuilder, SchemeBuilder>();
builder.Services.AddSingleton<IScryfallBuilder, SplitBuilder>();
builder.Services.AddSingleton<IScryfallBuilder, TokenBuilder>();
builder.Services.AddSingleton<IScryfallBuilder, TransformBuilder>();
builder.Services.AddSingleton<IScryfallBuilder, VanguardBuilder>();

builder.Services.AddSingleton<ICardDatabaseSaver, CardDatabaseSaver>();
builder.Services.AddSingleton<ICardDatabaseReader, CardDatabaseReader>();
builder.Services.AddSingleton<IDatabaseMapper, DatabaseMapper>();

IHostEnvironment env = builder.Environment;

builder.Configuration
    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, true);

using IHost host = builder.Build();

LaunchImport(host.Services);

static void LaunchImport(IServiceProvider services)
{
    using IServiceScope serviceScope = services.CreateScope();
    var provider = serviceScope.ServiceProvider;

    provider.GetServices<IScryfallBuilder>();

    var importer = provider.GetRequiredService<IScryfallImporter>();
    var cardDatabaseSaver = provider.GetRequiredService<ICardDatabaseSaver>();
    var cardDatabaseReader = provider.GetRequiredService<ICardDatabaseReader>();

    var artistsDb = cardDatabaseReader.GetArtists();
    var artists = importer.ArtistsImport(artistsDb);
    if (artists != null && artists.Count > 0)
        cardDatabaseSaver.SaveArtists(artists);

    var supertypesDb = cardDatabaseReader.GetSupertypes();
    var supertypes = importer.SupertypesImport(supertypesDb);
    if (supertypes != null && supertypes.Count > 0)
        cardDatabaseSaver.SaveSupertypes(supertypes);

    var typesDb = cardDatabaseReader.GetTypes();
    var types = importer.TypesImport(typesDb);
    if (types != null && types.Count > 0)
        cardDatabaseSaver.SaveTypes(types);

    var subtypesDb = cardDatabaseReader.GetSubtypes();

    var cardTypes = new List<string> { "Artifact", "Battle", "Creature", "Enchantment", "Land", "Planeswalker", "Spell" };

    foreach (var cardType in cardTypes)
    {
        var subtypes = importer.SubtypesImport(subtypesDb, cardType);
        if (subtypes != null && subtypes.Count > 0)
            cardDatabaseSaver.SaveSubtypes(subtypes);
    }

    var setsDb = cardDatabaseReader.GetSets();
    var sets = importer.SetsImport(setsDb);
    if (sets != null && sets.Count > 0)
        cardDatabaseSaver.SaveSets(sets);

    var cards = importer.CardsImport();
    if (cards != null && cards.Count > 0)
        cardDatabaseSaver.SaveCards(cards);
}

await host.RunAsync();
