using Projet_Final;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

//créer un lien vers appsetting.json
var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile(@"Projet_Final/appsettings.json", optional: false, reloadOnChange: true)
    .Build();