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

List<Car> cars = new List<Car>(); 

var lignes = File.ReadAllLines(path);

for (int i = 1; i < lignes.Length; i++)
{
    String line = lignes[i];
    Car car = new Car();

    car.Brand = line.Split('/')[0];
    car.Model = line.Split('/')[1];
    car.Year = Int32.Parse(line.Split('/')[2]);
    car.PriceExlTax = Int32.Parse(line.Split('/')[3]);
    car.PriceInclTax = car.PriceExlTax * 1.20f;
    car.Color = line.Split('/')[4];
    car.Status = Boolean.Parse(line.Split('/')[5]);


    cars.Add(car);
}

List<Client> clients = new List<Client>();

var lignes_client = File.ReadAllLines(path);

for (int f = 1; f < lignes.Length; f++)
{
    String client_line = lignes[f];
    Client client = new Client();

    client.Lastname = client_line.Split('%')[0];
    client.Firstname = client_line.Split('%')[1];
    client.Birthdate = DateTimeUtils.ConvertToDateTime(client_line.Split('%')[2]);
    client.PhoneNumber = client_line.Split('%')[3];
    client.Email = client_line.Split('%')[4];

    clients.Add(client);
        
}