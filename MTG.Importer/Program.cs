using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MTG.Database.Models.DatabaseContext;
using MTG.Database.Models.Interfaces;
using MTG.Database.Models.Services;
using MTG.Scryfall.Importer;
using MTG.Scryfall.Importer.Builders;
using MTG.Scryfall.Importer.Interfaces;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

builder.Services.AddSingleton<IScryfallReader, ScryfallReader>();
builder.Services.AddSingleton<IScryfallImporter, ScryfallImporter>();
builder.Services.AddSingleton<IScryfallMapper, ScryfallMapper>();
builder.Services.AddSingleton<IScryfallDirector, ScryfallDirector>();
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

builder.Services.AddDbContext<MTGDbContext>();

builder.Services.AddTransient<IColorService, ColorService>();

using IHost host = builder.Build();

LaunchImport(host.Services);

static void LaunchImport(IServiceProvider services)
{
    var basePath = @"D:\Cards Import\_MTG_";

    using IServiceScope serviceScope = services.CreateScope();
    IServiceProvider provider = serviceScope.ServiceProvider;
    IScryfallImporter importer = provider.GetRequiredService<IScryfallImporter>();
    provider.GetServices<IScryfallBuilder>();
    var cards = importer.Import(Path.Combine(basePath, "42_cards.json"));
}

await host.RunAsync();
