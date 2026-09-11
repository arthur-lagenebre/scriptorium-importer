using System.Net.Http.Headers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Scriptorium.Mtg.Importer.Save;
using Scriptorium.Mtg.Importer.Save.Interfaces;
using Scriptorium.Mtg.Scryfall.Importer;
using Scriptorium.Mtg.Scryfall.Importer.Builders;
using Scriptorium.Mtg.Scryfall.Importer.Interfaces;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

var scryfallOptions = builder.Configuration.GetSection("Scryfall").Get<ScryfallOptions>() ?? new ScryfallOptions();
var apiOptions = builder.Configuration.GetSection("Api").Get<ApiOptions>() ?? new ApiOptions();

builder.Services.AddSingleton(scryfallOptions);
builder.Services.AddSingleton(apiOptions);

builder.Services.AddHttpClient<IScryfallGetter, ScryfallGetter>(client =>
{
    client.BaseAddress = new Uri(scryfallOptions.BaseUrl);
    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    client.DefaultRequestHeaders.UserAgent.Add(
        new ProductInfoHeaderValue(scryfallOptions.UserAgentProduct, scryfallOptions.UserAgentVersion));
});

builder.Services.AddHttpClient<IDatabaseSaver, DatabaseSaver>(client =>
{
    client.BaseAddress = new Uri(apiOptions.BaseUrl);
});

builder.Services.AddSingleton<IScryfallCardDirector, ScryfallCardDirector>();
builder.Services.AddSingleton<IScryfallImporter, ScryfallImporter>();
builder.Services.AddSingleton<IScryfallMapper, ScryfallMapper>();
builder.Services.AddSingleton<IScryfallReader, ScryfallReader>();

builder.Services.AddSingleton<IDatabaseReader, DatabaseReader>();
builder.Services.AddSingleton<IDatabaseMapper, DatabaseMapper>();

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

using var host = builder.Build();

LaunchImport(host.Services);
return;

static void LaunchImport(IServiceProvider services)
{
    using var serviceScope = services.CreateScope();
    var provider = serviceScope.ServiceProvider;

    var importer = provider.GetRequiredService<IScryfallImporter>();
    var cardDatabaseSaver = provider.GetRequiredService<IDatabaseSaver>();

    Console.WriteLine("Import artists");
    var artists = importer.ArtistsImport();
    Console.WriteLine($"{artists?.Count} artists found");
    if (artists is { Count: > 0 })
        cardDatabaseSaver.SaveArtists(artists);

    Console.WriteLine("Import supertypes");
    var supertypes = importer.SupertypesImport();
    Console.WriteLine($"{supertypes?.Count} supertypes found");
    if (supertypes is { Count: > 0 })
        cardDatabaseSaver.SaveSupertypes(supertypes);

    Console.WriteLine("Import types");
    var types = importer.TypesImport();
    Console.WriteLine($"{types?.Count} types found");
    if (types is { Count: > 0 })
        cardDatabaseSaver.SaveTypes(types);

    Console.WriteLine("Import cardTypes");
    var cardTypes = new List<string> { "Artifact", "Battle", "Creature", "Enchantment", "Land", "Planeswalker", "Spell" };

    foreach (var cardType in cardTypes)
    {
        var subtypes = importer.SubtypesImport(cardType);
        Console.WriteLine($"{subtypes?.Count} subtypes [{cardType}] found");
        if (subtypes is { Count: > 0 })
            cardDatabaseSaver.SaveSubtypes(subtypes);
    }

    Console.WriteLine("Import sets");
    var sets = importer.SetsImport();
    Console.WriteLine($"{sets?.Count} sets found");
    if (sets is { Count: > 0 })
        cardDatabaseSaver.SaveSets(sets);

    Console.WriteLine("Import rulings");
    var rulings = importer.RulingsImport();

    Console.WriteLine("Import cards");
    var cards = importer.CardsImport(rulings?.ToList() ?? []);
    Console.WriteLine($"{cards?.Count} cards found");
    if (cards is { Count: > 0 })
        cardDatabaseSaver.SaveCards(cards);
}