using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MTG.Saver;
using MTG.Saver.Interfaces;
using MTG.Scryfall.Importer;
using MTG.Scryfall.Importer.Builders;
using MTG.Scryfall.Importer.Interfaces;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

builder.Services.AddSingleton<IScryfallReader, ScryfallReader>();
builder.Services.AddSingleton<IScryfallImporter, ScryfallImporter>();
builder.Services.AddSingleton<IScryfallMapper, ScryfallMapper>();
builder.Services.AddSingleton<IScryfallDirector, ScryfallDirector>();
builder.Services.AddSingleton<IScryfallTypelineManager, ScryfallTypelineManager>();

builder.Services.AddSingleton<IWriter, JsonWriter>();

builder.Services.AddSingleton<IScryfallBuilder, NormalBuilder>();
builder.Services.AddSingleton<IScryfallBuilder, TransformBuilder>();
builder.Services.AddSingleton<IScryfallBuilder, SagaBuilder>();
builder.Services.AddSingleton<IScryfallBuilder, VanguardBuilder>();

using IHost host = builder.Build();

LaunchImport(host.Services);

static void LaunchImport(IServiceProvider services)
{
    var basePath = @"D:\MTG\_scryfall";

    using IServiceScope serviceScope = services.CreateScope();
    IServiceProvider provider = serviceScope.ServiceProvider;
    IScryfallImporter importer = provider.GetRequiredService<IScryfallImporter>();
    provider.GetServices<IScryfallBuilder>();
    var cards = importer.Import(Path.Combine(basePath, "42_cards.json"));

    IWriter writer = provider.GetRequiredService<IWriter>();

    writer.WriteCards(cards, Path.Combine(basePath, "_output.json"));
}

await host.RunAsync();
