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

builder.Services.AddSingleton<ICardDatabaseSave, CardDatabaseSave>();
builder.Services.AddSingleton<ICardMapper, CardMapper>();

IHostEnvironment env = builder.Environment;

builder.Configuration
    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, true);

using IHost host = builder.Build();

LaunchImport(host.Services);

static void LaunchImport(IServiceProvider services)
{
    using IServiceScope serviceScope = services.CreateScope();
    IServiceProvider provider = serviceScope.ServiceProvider;
    IScryfallImporter importer = provider.GetRequiredService<IScryfallImporter>();
    ICardDatabaseSave cardDatabaseSave = provider.GetRequiredService<ICardDatabaseSave>();
    provider.GetServices<IScryfallBuilder>();
    //var sets = importer.SetImport();
    //cardDatabaseSave.SaveSets(sets);
    var cards = importer.CardImport();
    cardDatabaseSave.SaveCards(cards);
}

await host.RunAsync();
