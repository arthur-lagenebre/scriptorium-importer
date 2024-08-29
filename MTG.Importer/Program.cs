using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MTG.Scryfall.Importer;
using MTG.Scryfall.Importer.Builders;
using MTG.Scryfall.Importer.Interfaces;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

builder.Services.AddSingleton<IScryfallReader, ScryfallReader>();
builder.Services.AddSingleton<IScryfallImporter, ScryfallImporter>();
builder.Services.AddSingleton<IScryfallMapper, ScryfallMapper>();
builder.Services.AddSingleton<IScryfallBuilderSelector, ScryfallBuilderSelector>();

builder.Services.AddSingleton<IScryfallBuilder, NormalBuilder>();
builder.Services.AddSingleton<IScryfallBuilder, TransformBuilder>();
builder.Services.AddSingleton<IScryfallBuilder, SagaBuilder>();
builder.Services.AddSingleton<IScryfallBuilder, VanguardBuilder>();

using IHost host = builder.Build();

LaunchImport(host.Services);

static void LaunchImport(IServiceProvider services)
{
    using IServiceScope serviceScope = services.CreateScope();
    IServiceProvider provider = serviceScope.ServiceProvider;
    IScryfallImporter importer = provider.GetRequiredService<IScryfallImporter>();
    provider.GetServices<IScryfallBuilder>();
    var cards = importer.Import(@"D:\MTG\_scryfall\Scryfall_layout_saga.json");

    foreach (var card in cards)
        Console.WriteLine($"{card.OracleId} & {card.Name}");
}

await host.RunAsync();
