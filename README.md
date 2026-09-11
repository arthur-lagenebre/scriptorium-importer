# Scriptorium — Importer

[![.NET](https://img.shields.io/badge/.NET-10.0-512BD4)](https://dotnet.microsoft.com/)
[![Tests](https://img.shields.io/badge/tests-46%20passing-brightgreen)](https://xunit.net/)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE)
[![Status](https://img.shields.io/badge/status-work%20in%20progress-orange)]()
[![build](https://github.com/arthur-lagenebre/scriptorium-importer/actions/workflows/build.yml/badge.svg)](https://github.com/arthur-lagenebre/scriptorium-importer/actions/workflows/build.yml)

> ETL pipeline turning Scryfall bulk JSON exports into a multilingual Magic: The Gathering database.
> Chaîne ETL transformant les exports JSON de Scryfall en une base de cartes Magic multilingue.

**🇬🇧 [English](#english) · 🇫🇷 [Français](#français)**

---

## English

### What this is

**Scriptorium** is a platform that lets a community translate trading card games into languages their publishers do not support. This repository is its ingestion half.

A .NET 10 console application that reads Scryfall's bulk data files — several gigabytes of JSON covering every Magic card ever printed, in every language it was printed in — normalises them into a relational model, and publishes them to [scriptorium-api](https://github.com/arthur-lagenebre/scriptorium-api).

### The hard part

Magic cards do not have one shape. They have **22 of them**. A normal card has one face; a transform card has two with different names; a meld card becomes part of a third card; an adventure card has two rules boxes on one face; a split card has two halves; a reversible card has two printings on one physical object. Scryfall exposes this through a `layout` discriminator, and every layout puts its data in a different place — sometimes at the top level, sometimes inside `card_faces`, sometimes both.

Handling that with a single mapper produces an unmaintainable pile of conditionals. This project uses a **Builder + Director** pattern instead:

```
                    ┌─────────────────────┐
  Scryfall JSON ───▶│  ScryfallReader     │  streaming deserialisation
                    └──────────┬──────────┘
                               ▼
                    ┌─────────────────────┐
                    │ ScryfallCardDirector│  dispatch on layout
                    └──────────┬──────────┘
                               ▼
        ┌──────────────────────┼──────────────────────┐
        ▼                      ▼                      ▼
  NormalBuilder        TransformBuilder         MeldBuilder   ... (22 total)
        └──────────────────────┼──────────────────────┘
                               ▼
                    ┌─────────────────────┐
                    │   DatabaseMapper    │  domain → DTO
                    └──────────┬──────────┘
                               ▼
                    ┌─────────────────────┐
                    │   DatabaseSaver     │  HTTP POST → scriptorium-api
                    └─────────────────────┘
```

Every builder implements `IScryfallBuilder` and declares the layout it handles. They are all registered in DI and resolved as `IEnumerable<IScryfallBuilder>`, so the director picks the right one by name. Supporting a new Magic layout means adding one class and one registration — no existing code is touched.

Supported layouts: `adventure`, `augment`, `case`, `class`, `double_faced_token`, `emblem`, `flip`, `host`, `leveler`, `meld`, `modal_dfc`, `mutate`, `normal`, `planar`, `prototype`, `reversible_card`, `saga`, `scheme`, `split`, `token`, `transform`, `vanguard`.

This dispatch-on-discriminator structure is the part of the project most likely to survive a move to another card game: only the builders would be rewritten.

### Other design notes

- **Streaming, not buffering.** The bulk card file is read through `JsonTextReader` rather than loaded into a string, so memory stays flat regardless of file size.
- **Language grouping.** Cards are grouped by `OracleId`, the most recent English printing becomes the oracle row, and every other language contributes translation rows for the same card. This is what turns a flat list of printings into a properly localised card.
- **Catalogue pre-import.** Artists, types, supertypes and subtypes are imported first from Scryfall's catalogue endpoints so that cards can reference existing rows.
- **Typed HTTP clients.** Both the Scryfall client and the API client are registered through `AddHttpClient`, so handlers are pooled and the Scryfall headers are configured once.

### Project layout

| Project | Role |
|---|---|
| `Scriptorium.Mtg.Importer` | Console entry point, DI composition root, import orchestration |
| `Scriptorium.Mtg.Scryfall.Models` | DTOs mirroring the Scryfall JSON schema |
| `Scriptorium.Mtg.Scryfall.Importer` | Readers, director, the 22 builders, and helpers |
| `Scriptorium.Mtg.Importer.Models` | Normalised domain model, independent of Scryfall's shape |
| `Scriptorium.Mtg.Importer.Save` | Domain → API DTO mapping and HTTP publication |
| `Scriptorium.Mtg.Scryfall.Importer.Tests` | xUnit + NSubstitute unit tests |

### Getting started

**Prerequisites**

- .NET 10 SDK
- A running instance of [scriptorium-api](https://github.com/arthur-lagenebre/scriptorium-api)
- Scryfall bulk data files — `all_cards` and `rulings`, from [scryfall.com/docs/api/bulk-data](https://scryfall.com/docs/api/bulk-data)

```bash
git clone https://github.com/arthur-lagenebre/scriptorium-importer.git
cd scriptorium-importer
dotnet restore
```

**Configuration** lives in `Scriptorium.Mtg.Importer/appsettings.json`:

| Key | Purpose |
|---|---|
| `Scryfall:BaseUrl` | Scryfall API root |
| `Scryfall:CardsFilePath` | Path to the `all_cards` bulk file |
| `Scryfall:RulingsFilePath` | Path to the `rulings` bulk file |
| `Scryfall:UserAgentProduct` / `:UserAgentVersion` | Sent on every Scryfall request |
| `Api:BaseUrl` | Where scriptorium-api is listening, e.g. `http://localhost:5141/api/` |

The bulk file paths are machine-specific, so keep them out of source control with user secrets (loaded in the Development environment only):

```bash
cd Scriptorium.Mtg.Importer
dotnet user-secrets init
dotnet user-secrets set "Scryfall:CardsFilePath" "/path/to/all-cards.json"
dotnet user-secrets set "Scryfall:RulingsFilePath" "/path/to/rulings.json"
```

**Run**

```bash
dotnet run --project Scriptorium.Mtg.Importer
dotnet test
```

Configuration is read from the directory holding the executable, so the importer behaves the same whether launched through `dotnet run`, by double-click, or from a scheduled task.

### Scryfall API etiquette

This importer sets an explicit `User-Agent` and `Accept` header on every request, as Scryfall requires. If you fork it, set a `User-Agent` accurate to your own usage rather than letting the HTTP library pick one. Prefer the bulk data files over hammering the card endpoints — that is what they exist for.

### Roadmap

- [ ] Convert the pipeline to async — `IScryfallGetter`, `IScryfallImporter` and `IDatabaseSaver` still block on `.Result`, which also wraps every failure in an `AggregateException`
- [ ] Batch publication: the importer currently sends one `POST` per artist, per set and per card. A batch endpoint on the API side would cut import time dramatically.
- [ ] Replace `Newtonsoft.Json` with `System.Text.Json` streaming (`DeserializeAsyncEnumerable`)
- [ ] Complete unit tests for the remaining builders (currently only `AdventureBuilder` and the helpers are covered)
- [ ] Fix the nullability warnings in `ScryfallCardDirector`
- [ ] Incremental import — only process cards changed since the last run
- [ ] GitHub Actions CI (build + test)
- [ ] Structured logging in place of `Console.WriteLine`

### Related repositories

| Repository | Role |
|---|---|
| [scriptorium-api](https://github.com/arthur-lagenebre/scriptorium-api) | REST API and data model |
| **scriptorium-importer** | This repository — Scryfall ETL |
| [scriptorium-web](https://github.com/arthur-lagenebre/scriptorium-web) | Angular front end |

---

## Français

### De quoi s'agit-il

**Scriptorium** est une plateforme permettant à une communauté de traduire des jeux de cartes à collectionner dans les langues que leurs éditeurs ne prennent pas en charge. Ce dépôt en constitue la moitié « ingestion ».

Une application console .NET 10 qui lit les fichiers bulk de Scryfall — plusieurs gigaoctets de JSON couvrant toutes les cartes Magic jamais imprimées, dans toutes les langues où elles l'ont été —, les normalise dans un modèle relationnel, et les publie vers [scriptorium-api](https://github.com/arthur-lagenebre/scriptorium-api).

### La vraie difficulté

Les cartes Magic n'ont pas une forme. Elles en ont **22**. Une carte normale a une face ; une carte transform en a deux avec des noms différents ; une carte meld devient une partie d'une troisième carte ; une carte adventure a deux blocs de règles sur une seule face ; une carte split a deux moitiés ; une carte reversible porte deux impressions sur un même objet physique. Scryfall expose cela via un discriminant `layout`, et chaque layout range ses données ailleurs — parfois au niveau racine, parfois dans `card_faces`, parfois les deux.

Traiter ça avec un mapper unique produit un empilement de conditions ingérable. Ce projet utilise un pattern **Builder + Director** :

```
                    ┌─────────────────────┐
  JSON Scryfall ───▶│  ScryfallReader     │  désérialisation en streaming
                    └──────────┬──────────┘
                               ▼
                    ┌─────────────────────┐
                    │ ScryfallCardDirector│  aiguillage par layout
                    └──────────┬──────────┘
                               ▼
        ┌──────────────────────┼──────────────────────┐
        ▼                      ▼                      ▼
  NormalBuilder        TransformBuilder         MeldBuilder   ... (22 au total)
        └──────────────────────┼──────────────────────┘
                               ▼
                    ┌─────────────────────┐
                    │   DatabaseMapper    │  domaine → DTO
                    └──────────┬──────────┘
                               ▼
                    ┌─────────────────────┐
                    │   DatabaseSaver     │  HTTP POST → scriptorium-api
                    └─────────────────────┘
```

Chaque builder implémente `IScryfallBuilder` et déclare le layout qu'il traite. Tous sont enregistrés dans le conteneur d'injection et résolus en `IEnumerable<IScryfallBuilder>`, le director sélectionnant le bon par son nom. Prendre en charge un nouveau layout Magic revient à ajouter une classe et un enregistrement — aucun code existant n'est modifié.

Layouts pris en charge : `adventure`, `augment`, `case`, `class`, `double_faced_token`, `emblem`, `flip`, `host`, `leveler`, `meld`, `modal_dfc`, `mutate`, `normal`, `planar`, `prototype`, `reversible_card`, `saga`, `scheme`, `split`, `token`, `transform`, `vanguard`.

Cette structure d'aiguillage par discriminant est la partie du projet la plus susceptible de survivre à un portage vers un autre jeu de cartes : seuls les builders seraient à réécrire.

### Autres choix de conception

- **Streaming, pas de mise en tampon.** Le fichier bulk est lu via `JsonTextReader` plutôt que chargé en chaîne, la mémoire reste donc constante quelle que soit la taille du fichier.
- **Regroupement par langue.** Les cartes sont groupées par `OracleId` ; l'impression anglaise la plus récente devient la ligne oracle, et chaque autre langue apporte ses lignes de traduction pour la même carte. C'est ce qui transforme une liste plate d'impressions en une carte correctement localisée.
- **Pré-import des catalogues.** Artistes, types, supertypes et sous-types sont importés en premier depuis les endpoints catalogue de Scryfall, afin que les cartes puissent référencer des lignes existantes.
- **Clients HTTP typés.** Le client Scryfall et le client de l'API sont enregistrés via `AddHttpClient` : les handlers sont mutualisés et les en-têtes Scryfall configurés une seule fois.

### Organisation des projets

| Projet | Rôle |
|---|---|
| `Scriptorium.Mtg.Importer` | Point d'entrée console, racine de composition DI, orchestration de l'import |
| `Scriptorium.Mtg.Scryfall.Models` | DTO calqués sur le schéma JSON de Scryfall |
| `Scriptorium.Mtg.Scryfall.Importer` | Lecteurs, director, les 22 builders et les helpers |
| `Scriptorium.Mtg.Importer.Models` | Modèle de domaine normalisé, indépendant de la forme Scryfall |
| `Scriptorium.Mtg.Importer.Save` | Mapping domaine → DTO d'API et publication HTTP |
| `Scriptorium.Mtg.Scryfall.Importer.Tests` | Tests unitaires xUnit + NSubstitute |

### Démarrage

**Prérequis**

- SDK .NET 10
- Une instance de [scriptorium-api](https://github.com/arthur-lagenebre/scriptorium-api) en cours d'exécution
- Les fichiers bulk Scryfall `all_cards` et `rulings`, depuis [scryfall.com/docs/api/bulk-data](https://scryfall.com/docs/api/bulk-data)

```bash
git clone https://github.com/arthur-lagenebre/scriptorium-importer.git
cd scriptorium-importer
dotnet restore
```

**La configuration** se trouve dans `Scriptorium.Mtg.Importer/appsettings.json` :

| Clé | Rôle |
|---|---|
| `Scryfall:BaseUrl` | Racine de l'API Scryfall |
| `Scryfall:CardsFilePath` | Chemin du fichier bulk `all_cards` |
| `Scryfall:RulingsFilePath` | Chemin du fichier bulk `rulings` |
| `Scryfall:UserAgentProduct` / `:UserAgentVersion` | Envoyés à chaque requête Scryfall |
| `Api:BaseUrl` | Adresse d'écoute de scriptorium-api, par ex. `http://localhost:5141/api/` |

Les chemins des fichiers bulk sont propres à chaque machine : gardez-les hors du dépôt grâce aux user secrets, chargés uniquement en environnement Development.

```bash
cd Scriptorium.Mtg.Importer
dotnet user-secrets init
dotnet user-secrets set "Scryfall:CardsFilePath" "D:\Cards Import\all-cards.json"
dotnet user-secrets set "Scryfall:RulingsFilePath" "D:\Cards Import\rulings.json"
```

**Lancer**

```bash
dotnet run --project Scriptorium.Mtg.Importer
dotnet test
```

La configuration est lue depuis le répertoire de l'exécutable : l'importer se comporte donc de la même façon lancé par `dotnet run`, par double-clic, ou depuis une tâche planifiée.

### Bon usage de l'API Scryfall

Cet importer positionne explicitement les en-têtes `User-Agent` et `Accept` sur chaque requête, comme Scryfall l'exige. Si vous forkez le projet, renseignez un `User-Agent` correspondant à votre propre usage plutôt que de laisser la bibliothèque HTTP en choisir un. Préférez les fichiers bulk au martèlement des endpoints de cartes — c'est leur raison d'être.

### Feuille de route

- [ ] Passer la chaîne en asynchrone — `IScryfallGetter`, `IScryfallImporter` et `IDatabaseSaver` bloquent encore sur `.Result`, ce qui emballe au passage chaque échec dans une `AggregateException`
- [ ] Publication par lots : l'importer envoie aujourd'hui un `POST` par artiste, par édition et par carte. Un endpoint de traitement par lots côté API réduirait drastiquement la durée d'import.
- [ ] Remplacer `Newtonsoft.Json` par le streaming `System.Text.Json` (`DeserializeAsyncEnumerable`)
- [ ] Compléter les tests unitaires des builders restants (seuls `AdventureBuilder` et les helpers sont couverts)
- [ ] Corriger les avertissements de nullabilité de `ScryfallCardDirector`
- [ ] Import incrémental — ne traiter que les cartes modifiées depuis la dernière exécution
- [ ] CI GitHub Actions (build + tests)
- [ ] Journalisation structurée en remplacement de `Console.WriteLine`

### Dépôts liés

| Dépôt | Rôle |
|---|---|
| [scriptorium-api](https://github.com/arthur-lagenebre/scriptorium-api) | API REST et modèle de données |
| **scriptorium-importer** | Ce dépôt — ETL Scryfall |
| [scriptorium-web](https://github.com/arthur-lagenebre/scriptorium-web) | Front Angular |

---

## License / Licence

Code released under the [MIT License](LICENSE).
Code publié sous [licence MIT](LICENSE).

### Fan content disclaimer

This project is unofficial Fan Content permitted under the Wizards of the Coast Fan Content Policy. Not approved or endorsed by Wizards. Portions of the materials used are property of Wizards of the Coast. © Wizards of the Coast LLC.

Card data originates from [Scryfall](https://scryfall.com/). Any information obtained from the Scryfall API that is not © Wizards of the Coast LLC is © Scryfall LLC. The MIT licence above covers **this repository's source code only** — it does not extend to card data, card names, rules text, artwork or Magic: The Gathering trademarks.

*Ce projet est un contenu de fan non officiel, autorisé au titre de la Fan Content Policy de Wizards of the Coast. Non approuvé ni soutenu par Wizards. La licence MIT ci-dessus couvre uniquement le code source de ce dépôt.*
