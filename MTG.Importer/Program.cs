using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MTG.Scryfall.Importer;
using MTG.Scryfall.Importer.Interfaces;
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
    var cards = importer.Import(@"D:\MTG\_scryfall\Scryfall_layout_vanguard.json");

    foreach (var card in cards)
        Console.WriteLine($"{card.OracleId} & {card.Name}");
}

await host.RunAsync();
