# KLab - .NET Core Swiss Army Knife

1. [ASP.NET Core Web API](https://github.com/aspnet/Mvc)
2. [Hosting](https://github.com/aspnet/Hosting)
    - [Kestrel](https://github.com/aspnet/KestrelHttpServer)
    - [IIS](https://github.com/aspnet/AspNetCoreModule)
3. [Logging](https://github.com/aspnet/Logging)
    - [serilog](https://github.com/serilog/serilog)
    - [serilog-extensions-logging-file](https://github.com/serilog/serilog-extensions-logging-file) estensione per aggiungere il log su file con una linea di codice: (attenzione a fileSizeLimitBytes e retainedFileCountLimit per limitare ulteriormente i log file)
4. [Configuration](https://github.com/aspnet/Configuration)
    - [Options](https://github.com/aspnet/Options)
5. [Swagger](https://swagger.io/)
    - [Swashbuckle](https://github.com/domaindrivendev/Swashbuckle.AspNetCore)
    - [Autorest](https://github.com/Azure/autorest) to generate clients based on swagger file. Supports C#, Go, Java, Node.js, TypeScript, Python, Ruby and PHP.
6. Accesso ai dati
    - [Entity Framework Core](https://github.com/aspnet/EntityFrameworkCore)
7. Estensibilità netcore:
    - [serilog-extensions-logging-applicationinsights](https://github.com/micdenny/serilog-extensions-logging-applicationinsights) estensione per pushare i log su application insights
    - estensione per loggare il dettaglio delle richieste HTTP: RequestLoggerMiddleware (vedi sorgente progetto di esempio)
8. ASP.NET Core MVC
    - finalmente un template praticamente identico alle Web API grazie al middleware estensibile, semplicemente con alcune estensioni in più aggiunte in fase di startup e le folder contenenti le parti statiche
9. L'esperimento

## 1. ASP.NET Core Web API

1. main function e startup: finalmente un semplice e leggero IoC container integrato nel framework: [Microsoft.Extensions.DependencyInjection](https://github.com/aspnet/DependencyInjection) (gestito dal team asp.net)
2. main function v1 vs v2: CreateDefaultBuilder, anche no!!
3. novità sul `WebHostBuilder` v2: configuration e logging prima dello startup
4. publish -> v1 vs v2 e FDD vs SCD: runtime store, e finalmente un output di binari un po' più contenuta e views pre-compilate. Framework-dependent deployment (FDD) vs Self-contained deployment (SCD -> RuntimeIdentifiers).
5. esecuzione sul sotto sistema Linux in windows 10 (AKA Bash on Ubuntu on Windows)
6. chiamare l'api con postman

## 2. Hosting

1. un progetto asp.net (`AspNetCoreWebApi20`) può essere avviato sia con IIS Express che in self-host su console
2. un progetto asp.net (`AspNetCoreWebApi20`) avviato in self-host viene chiusa e rilanciata automaticamente quando si ricompila il progetto
3. 1. una console self-host (`AspNetCoreSelfHost`) è la stessa cosa di un asp.net web site (`AspNetCoreWebApi20`)

## 3. Logging

1. setup su asp.net core
2. setup di serilog
3. setup su console app: `ILoggingBuilder` to rule them all
4. scopes per correlare le chiamate anche su console app

## 4. Configuration

1. setup su asp.net core e su console app: `ConfigurationBuilder` to rule them all
2. configurazione e uso delle options
3. override config da command-line o variabile d'ambiente windows

## 5. Swagger

1. cos'è e il one-line setup o quasi :)
2. generare il client c# e usarlo da una console net core

## 6. Entity Framework Core

1. one-line setup sql server con `AddDbContext` o `AddDbContextPool`
2. DbContext injection, si ma solo nelle asp.net application! Altrimenti è meglio utilizzare un factory.
3. panoramica dell'applicazione
4. provare e fallire nell'aggiungere una migrazione, perchè? (vedi punto seguente)
5. l'importanza del metodo `public static IWebHost BuildWebHost(string[] args)`
6. Provare di nuovo ad aggiungere una migrazione, tutto ok, ma il più delle volte è necessario utilizzare un implementazione di `IDesignTimeDbContextFactory<TContext>`

## 7. Estensibilità netcore

1. estensione per loggare il dettaglio delle richieste HTTP `RequestLoggerMiddleware`
2. estensione per pushare i log su application insights

## 8. ASP.NET Core MVC

1. l'architettura è identica, viene configurata una rotta di default e la possibilità di avere risorse statiche che si trovano nella folder wwwroot.

## 9. L'esperimento

Console app come una ASP.NET Core, perchè no?

1. servizio compatibile per linux
2. servizio compatibile per windows con Topshelf