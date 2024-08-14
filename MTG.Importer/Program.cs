using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MTG.Scryfall.Importer;
using MTG.Scryfall.Interfaces;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

builder.Services.AddSingleton<IReader, Reader>();
builder.Services.AddSingleton<IImporter, Importer>();
builder.Services.AddSingleton<IMapper, Mapper>();

using IHost host = builder.Build();

LaunchImport(host.Services);

static void LaunchImport(IServiceProvider services)
{
    using IServiceScope serviceScope = services.CreateScope();
    IServiceProvider provider = serviceScope.ServiceProvider;
    IImporter importer = provider.GetRequiredService<IImporter>();
    var cards = importer.Import(@"D:\MTG\all-cards.json");

    foreach (var card in cards)
        Console.WriteLine($"Id : {card.CardId} & Oracle : {card.OracleId} & Name : {card.Name}");
}

await host.RunAsync();
