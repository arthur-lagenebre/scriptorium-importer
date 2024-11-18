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

builder.Services.AddSingleton<IDatabaseSaver, DatabaseSaver>();
builder.Services.AddSingleton<IDatabaseReader, DatabaseReader>();
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
    var cardDatabaseSaver = provider.GetRequiredService<IDatabaseSaver>();
    var cardDatabaseReader = provider.GetRequiredService<IDatabaseReader>();

    Console.WriteLine("Import artists");
    var artists = importer.ArtistsImport();
    Console.WriteLine($"{artists?.Count} artists found");
    if (artists != null && artists.Count > 0)
        cardDatabaseSaver.SaveArtists(artists);

    Console.WriteLine("Import supertypes");
    var supertypes = importer.SupertypesImport();
    Console.WriteLine($"{supertypes?.Count} supertypes found");
    if (supertypes != null && supertypes.Count > 0)
        cardDatabaseSaver.SaveSupertypes(supertypes);

    Console.WriteLine("Import types");
    var types = importer.TypesImport();
    Console.WriteLine($"{types?.Count} types found");
    if (types != null && types.Count > 0)
        cardDatabaseSaver.SaveTypes(types);

    Console.WriteLine("Import cardTypes");
    var cardTypes = new List<string> { "Artifact", "Battle", "Creature", "Enchantment", "Land", "Planeswalker", "Spell" };

    foreach (var cardType in cardTypes)
    {
        var subtypes = importer.SubtypesImport(cardType);
        Console.WriteLine($"{subtypes?.Count} subtypes [{cardType}] found");
        if (subtypes != null && subtypes.Count > 0)
            cardDatabaseSaver.SaveSubtypes(subtypes);
    }

    Console.WriteLine("Import sets");
    var sets = importer.SetsImport();
    Console.WriteLine($"{sets?.Count} sets found");
    if (sets != null && sets.Count > 0)
        cardDatabaseSaver.SaveSets(sets);

    Console.WriteLine("Import cards");
    var cards = importer.CardsImport();
    Console.WriteLine($"{cards?.Count} cards found");
    if (cards != null && cards.Count > 0)
        cardDatabaseSaver.SaveCards(cards);

    Console.WriteLine("Import rulings");
    var rulings = importer.RulingsImport();
    Console.WriteLine($"{rulings?.Count} ruling found");
    if (rulings != null && rulings.Count > 0)
        cardDatabaseSaver.SaveRulings(rulings);
}

await host.RunAsync();
