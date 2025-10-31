using Projet_Final;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using Projet_Final.Interface;
using Projet_Final.Model;
using Projet_Final.Utils;

//créer un lien vers appsetting.json
var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile(@"Projet_Final/appsettings.json", optional: false, reloadOnChange: true)
    .Build();
    
var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddDbContext<CarDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
        
        services.AddTransient<DbConnection>();
    })
    .Build();
    


String path = configuration.GetRequiredSection("CSVFileCar")["CoursSupDeVinci"];

List<Car> car = new List<Car>(); 

var lignes = File.ReadAllLines(path);

for (int i = 1; i < lignes.Length; i++)
{
    String line = lignes[i];
    Car car = new Car();

    person.Lastname = line.Split(',')[1];
    person.Firstname = line.Split(',')[2];
    person.Birthdate = DateTimeUtils.ConvertToDateTime(line.Split(',')[3]);
    person.Size = Int32.Parse(line.Split(',')[5]);

    List<String> details = line.Split(',')[4].Split(';').ToList();


    car.Add(Car);

    List<Client> clients = new List<Client>();

    var lignes_client = File.ReadAllLines(path);

    for (int f = 1; f < lignes.Length; f++)
    {
        String client_line = lignes[f];
        Client client = new Client();

        client.Lastname = line.Split('%')[0];
        client.Firstname = line.Split('%')[1];
        client.Birthdate = DateTimeUtils.ConvertToDateTime(line.Split('%')[2]);
        client.PhoneNumber = line.Split('%')[3];
        client.Email = line.Split('%')[4];

        clients.Add(client);
        
    }
}