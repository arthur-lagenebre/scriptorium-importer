# MTG Importer

[![.NET](https://img.shields.io/badge/.NET-8.0-512BD4)](https://dotnet.microsoft.com/)
[![Tests](https://img.shields.io/badge/tests-xUnit-blue)](https://xunit.net/)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE)
[![Status](https://img.shields.io/badge/status-work%20in%20progress-orange)]()

> ETL pipeline that turns Scryfall bulk JSON exports into a multilingual Magic: The Gathering database.
> ChaÃ®ne ETL transformant les exports JSON de Scryfall en une base de cartes Magic multilingue.

**ðŸ‡¬ðŸ‡§ [English](#english) Â· ðŸ‡«ðŸ‡· [FranÃ§ais](#franÃ§ais)**

---

## English

### What this is

A .NET 8 console application that reads Scryfall's bulk data files (several gigabytes of JSON covering every Magic card ever printed, in every language it was printed in), normalises them into a relational model, and publishes them to [MTG.API](https://github.com/arthur-lagenebre/MTG.API).

It is the ingestion half of a project whose goal is to let a community translate Magic cards into languages Wizards of the Coast does not support.

### The hard part

Magic cards do not have one shape. They have **22 of them**. A normal card has one face; a transform card has two with different names; a meld card becomes part of a third card; an adventure card has two rules boxes on one face; a split card has two halves; a reversible card has two printings on one physical object. Scryfall exposes this through a `layout` discriminator, and every layout puts its data in a different place â€” sometimes at the top level, sometimes inside `card_faces`, sometimes both.

Handling that with a single mapper produces an unmaintainable pile of conditionals. This project uses a **Builder + Director** pattern instead:

```
                    â”Œâ”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”
  Scryfall JSON â”€â”€â”€â–¶â”‚  ScryfallReader     â”‚  streaming deserialisation
                    â””â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”¬â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”˜
                               â–¼
                    â”Œâ”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”
                    â”‚ ScryfallCardDirectorâ”‚  dispatch on layout
                    â””â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”¬â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”˜
                               â–¼
        â”Œâ”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”¼â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”
        â–¼                      â–¼                      â–¼
  NormalBuilder        TransformBuilder         MeldBuilder   ... (22 total)
        â””â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”¼â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”˜
                               â–¼
                    â”Œâ”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”
                    â”‚   DatabaseMapper    â”‚  domain â†’ DTO
                    â””â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”¬â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”˜
                               â–¼
                    â”Œâ”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”
                    â”‚   DatabaseSaver     â”‚  HTTP POST â†’ MTG.API
                    â””â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”˜
```

Every builder implements `IScryfallBuilder` and declares the layout it handles. They are all registered in DI and resolved as `IEnumerable<IScryfallBuilder>`, so the director picks the right one by name. Adding support for a new Magic layout means adding one class and one registration â€” no existing code is touched.

Supported layouts: `adventure`, `augment`, `case`, `class`, `double_faced_token`, `emblem`, `flip`, `host`, `leveler`, `meld`, `modal_dfc`, `mutate`, `normal`, `planar`, `prototype`, `reversible_card`, `saga`, `scheme`, `split`, `token`, `transform`, `vanguard`.

### Other design notes

- **Streaming, not buffering.** The bulk card file is read through `JsonTextReader` rather than loaded into a string, so memory stays flat regardless of file size.
- **Language grouping.** Cards are grouped by `OracleId`, the most recent English printing becomes the oracle row, and every other language contributes translation rows for the same card. This is what turns a flat list of printings into a properly localised card.
- **Catalogue pre-import.** Artists, types, supertypes and subtypes are imported first from Scryfall's catalogue endpoints so that cards can reference existing rows.

### Project layout

| Project | Role |
|---|---|
| `MTG.Importer` | Console entry point, DI composition root, import orchestration |
| `MTG.Scryfall.Models` | DTOs mirroring the Scryfall JSON schema |
| `MTG.Scryfall.Importer` | Readers, director, the 22 builders, and helpers |
| `MTG.Importer.Models` | Normalised domain model, independent of Scryfall's shape |
| `MTG.Importer.Save` | Domain â†’ API DTO mapping and HTTP publication |
| `MTG.Scryfall.Importer.Tests` | xUnit + NSubstitute unit tests |

### Getting started

**Prerequisites**

- .NET 8 SDK
- A running instance of [MTG.API](https://github.com/arthur-lagenebre/MTG.API)
- Scryfall bulk data files: `all_cards` and `rulings`, downloadable from [scryfall.com/docs/api/bulk-data](https://scryfall.com/docs/api/bulk-data)

**Run**

```bash
git clone https://github.com/arthur-lagenebre/MTG-Importer.git
cd MTG-Importer
dotnet restore
dotnet run --project MTG.Importer
```

> âš ï¸ **Known limitation:** the bulk file paths and the API base URL are currently hardcoded in `ScryfallGetter` and `DatabaseSaver`. Externalising them into `appsettings.json` is the first item on the roadmap. Until then, edit those two files to match your environment.

**Test**

```bash
dotnet test
```

### Scryfall API etiquette

This importer sets an explicit `User-Agent` and `Accept` header on every request, as Scryfall requires. If you fork it, keep a `User-Agent` accurate to your own usage rather than letting the HTTP library pick one. Prefer the bulk data files over hammering the card endpoints â€” that is what they exist for.

### Roadmap

- [ ] Externalise configuration (bulk file paths, API URL) into `appsettings.json`
- [ ] Migrate to .NET 10 (LTS) â€” .NET 8 support ends 10 November 2026
- [ ] Replace `Newtonsoft.Json` with `System.Text.Json` streaming (`DeserializeAsyncEnumerable`)
- [ ] Replace `System.Runtime.Caching` with `Microsoft.Extensions.Caching.Memory`
- [ ] Remove blocking `.Result` calls in favour of full async
- [ ] Complete unit tests for the remaining builders (currently only `AdventureBuilder` and the helpers are covered)
- [ ] Incremental import â€” only process cards changed since the last run
- [ ] GitHub Actions CI (build + test)
- [ ] Structured logging in place of `Console.WriteLine`

### Related repositories

| Repository | Role |
|---|---|
| **MTG-Importer** | This repository â€” Scryfall ETL |
| [MTG.API](https://github.com/arthur-lagenebre/MTG.API) | REST API and database access |
| [card-tutor](https://github.com/arthur-lagenebre/card-tutor) | Angular front end |

---

## FranÃ§ais

### De quoi s'agit-il

Une application console .NET 8 qui lit les fichiers bulk de Scryfall (plusieurs gigaoctets de JSON couvrant toutes les cartes Magic jamais imprimÃ©es, dans toutes les langues oÃ¹ elles l'ont Ã©tÃ©), les normalise dans un modÃ¨le relationnel, et les publie vers [MTG.API](https://github.com/arthur-lagenebre/MTG.API).

C'est la moitiÃ© Â« ingestion Â» d'un projet dont l'objectif est de permettre Ã  une communautÃ© de traduire les cartes Magic dans les langues que Wizards of the Coast ne prend pas en charge.

### La vraie difficultÃ©

Les cartes Magic n'ont pas une forme. Elles en ont **22**. Une carte normale a une face ; une carte transform en a deux avec des noms diffÃ©rents ; une carte meld devient une partie d'une troisiÃ¨me carte ; une carte adventure a deux blocs de rÃ¨gles sur une seule face ; une carte split a deux moitiÃ©s ; une carte reversible porte deux impressions sur un mÃªme objet physique. Scryfall expose cela via un discriminant `layout`, et chaque layout range ses donnÃ©es ailleurs â€” parfois au niveau racine, parfois dans `card_faces`, parfois les deux.

Traiter Ã§a avec un mapper unique produit un empilement de conditions ingÃ©rable. Ce projet utilise un pattern **Builder + Director** :

```
                    â”Œâ”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”
  JSON Scryfall â”€â”€â”€â–¶â”‚  ScryfallReader     â”‚  dÃ©sÃ©rialisation en streaming
                    â””â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”¬â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”˜
                               â–¼
                    â”Œâ”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”
                    â”‚ ScryfallCardDirectorâ”‚  aiguillage par layout
                    â””â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”¬â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”˜
                               â–¼
        â”Œâ”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”¼â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”
        â–¼                      â–¼                      â–¼
  NormalBuilder        TransformBuilder         MeldBuilder   ... (22 au total)
        â””â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”¼â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”˜
                               â–¼
                    â”Œâ”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”
                    â”‚   DatabaseMapper    â”‚  domaine â†’ DTO
                    â””â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”¬â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”˜
                               â–¼
                    â”Œâ”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”
                    â”‚   DatabaseSaver     â”‚  HTTP POST â†’ MTG.API
                    â””â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”˜
```

Chaque builder implÃ©mente `IScryfallBuilder` et dÃ©clare le layout qu'il traite. Tous sont enregistrÃ©s dans le conteneur d'injection et rÃ©solus en `IEnumerable<IScryfallBuilder>`, le director sÃ©lectionnant le bon par son nom. Prendre en charge un nouveau layout Magic revient Ã  ajouter une classe et un enregistrement â€” aucun code existant n'est modifiÃ©.

Layouts pris en charge : `adventure`, `augment`, `case`, `class`, `double_faced_token`, `emblem`, `flip`, `host`, `leveler`, `meld`, `modal_dfc`, `mutate`, `normal`, `planar`, `prototype`, `reversible_card`, `saga`, `scheme`, `split`, `token`, `transform`, `vanguard`.

### Autres choix de conception

- **Streaming, pas de mise en tampon.** Le fichier bulk est lu via `JsonTextReader` plutÃ´t que chargÃ© en chaÃ®ne, la mÃ©moire reste donc constante quelle que soit la taille du fichier.
- **Regroupement par langue.** Les cartes sont groupÃ©es par `OracleId` ; l'impression anglaise la plus rÃ©cente devient la ligne oracle, et chaque autre langue apporte ses lignes de traduction pour la mÃªme carte. C'est ce qui transforme une liste plate d'impressions en une carte correctement localisÃ©e.
- **PrÃ©-import des catalogues.** Artistes, types, supertypes et sous-types sont importÃ©s en premier depuis les endpoints catalogue de Scryfall, afin que les cartes puissent rÃ©fÃ©rencer des lignes existantes.

### Organisation des projets

| Projet | RÃ´le |
|---|---|
| `MTG.Importer` | Point d'entrÃ©e console, racine de composition DI, orchestration de l'import |
| `MTG.Scryfall.Models` | DTO calquÃ©s sur le schÃ©ma JSON de Scryfall |
| `MTG.Scryfall.Importer` | Lecteurs, director, les 22 builders et les helpers |
| `MTG.Importer.Models` | ModÃ¨le de domaine normalisÃ©, indÃ©pendant de la forme Scryfall |
| `MTG.Importer.Save` | Mapping domaine â†’ DTO d'API et publication HTTP |
| `MTG.Scryfall.Importer.Tests` | Tests unitaires xUnit + NSubstitute |

### DÃ©marrage

**PrÃ©requis**

- SDK .NET 8
- Une instance de [MTG.API](https://github.com/arthur-lagenebre/MTG.API) en cours d'exÃ©cution
- Les fichiers bulk Scryfall `all_cards` et `rulings`, tÃ©lÃ©chargeables sur [scryfall.com/docs/api/bulk-data](https://scryfall.com/docs/api/bulk-data)

**Lancer**

```bash
git clone https://github.com/arthur-lagenebre/MTG-Importer.git
cd MTG-Importer
dotnet restore
dotnet run --project MTG.Importer
```

> âš ï¸ **Limitation connue :** les chemins des fichiers bulk et l'URL de base de l'API sont actuellement codÃ©s en dur dans `ScryfallGetter` et `DatabaseSaver`. Leur externalisation dans `appsettings.json` est le premier point de la feuille de route. En attendant, adaptez ces deux fichiers Ã  votre environnement.

**Tester**

```bash
dotnet test
```

### Bon usage de l'API Scryfall

Cet importer positionne explicitement les en-tÃªtes `User-Agent` et `Accept` sur chaque requÃªte, comme Scryfall l'exige. Si vous forkez le projet, conservez un `User-Agent` correspondant Ã  votre propre usage plutÃ´t que de laisser la bibliothÃ¨que HTTP en choisir un. PrÃ©fÃ©rez les fichiers bulk au martÃ¨lement des endpoints de cartes â€” c'est leur raison d'Ãªtre.

### Feuille de route

- [ ] Externaliser la configuration (chemins des fichiers bulk, URL de l'API) dans `appsettings.json`
- [ ] Migrer vers .NET 10 (LTS) â€” le support de .NET 8 s'arrÃªte le 10 novembre 2026
- [ ] Remplacer `Newtonsoft.Json` par le streaming `System.Text.Json` (`DeserializeAsyncEnumerable`)
- [ ] Remplacer `System.Runtime.Caching` par `Microsoft.Extensions.Caching.Memory`
- [ ] Supprimer les appels bloquants `.Result` au profit d'un asynchrone complet
- [ ] ComplÃ©ter les tests unitaires des builders restants (seuls `AdventureBuilder` et les helpers sont couverts)
- [ ] Import incrÃ©mental â€” ne traiter que les cartes modifiÃ©es depuis la derniÃ¨re exÃ©cution
- [ ] CI GitHub Actions (build + tests)
- [ ] Journalisation structurÃ©e en remplacement de `Console.WriteLine`

### DÃ©pÃ´ts liÃ©s

| DÃ©pÃ´t | RÃ´le |
|---|---|
| **MTG-Importer** | Ce dÃ©pÃ´t â€” ETL Scryfall |
| [MTG.API](https://github.com/arthur-lagenebre/MTG.API) | API REST et accÃ¨s base de donnÃ©es |
| [card-tutor](https://github.com/arthur-lagenebre/card-tutor) | Front Angular |

---

## License / Licence

Code released under the [MIT License](LICENSE).
Code publiÃ© sous [licence MIT](LICENSE).

### Fan content disclaimer

This project is unofficial Fan Content permitted under the Wizards of the Coast Fan Content Policy. Not approved or endorsed by Wizards. Portions of the materials used are property of Wizards of the Coast. Â© Wizards of the Coast LLC.

Card data originates from [Scryfall](https://scryfall.com/). Any information obtained from the Scryfall API that is not Â© Wizards of the Coast LLC is Â© Scryfall LLC. The MIT licence above covers **this repository's source code only** â€” it does not extend to card data, card names, rules text, artwork or Magic: The Gathering trademarks.

*Ce projet est un contenu de fan non officiel, autorisÃ© au titre de la Fan Content Policy de Wizards of the Coast. Non approuvÃ© ni soutenu par Wizards. La licence MIT ci-dessus couvre uniquement le code source de ce dÃ©pÃ´t : elle ne s'Ã©tend ni aux donnÃ©es des cartes, ni aux noms, textes de rÃ¨gles, illustrations ou marques Magic: The Gathering.*
