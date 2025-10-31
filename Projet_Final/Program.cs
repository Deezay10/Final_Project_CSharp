using Projet_Final;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using Projet_Final.Interface;
using Projet_Final.Model;
using Projet_Final.Utils;
using System.Globalization;

//créer un lien vers appsetting.json
var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();
    
var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddDbContext<CarDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
        
        services.AddTransient<DbConnection>();
    })
    .Build();
    


String path = configuration.GetRequiredSection("CSVFileCar")["Projet_Final2"];

List<Car> cars = new List<Car>(); 

var lignes = File.ReadAllLines(path);

for (int i = 1; i < lignes.Length; i++)
{
    String line = lignes[i];
    Car car = new Car();

    car.Brand = line.Split('/')[0];
    car.Model = line.Split('/')[1];
    car.Year = Int32.Parse(line.Split('/')[2]);
    car.PriceExlTax = float.Parse(line.Split('/')[3], CultureInfo.InvariantCulture);
    car.PriceInclTax = car.PriceExlTax * 1.20f;
    car.Color = line.Split('/')[4];
    car.Status = Boolean.Parse(line.Split('/')[5]);


    cars.Add(car);
}

List<Client> clients = new List<Client>();

String path2 = configuration.GetRequiredSection("CSVFileClient")["Projet_Final1"];

var lignes_client = File.ReadAllLines(path2);

for (int f = 1; f < lignes.Length; f++)
{
    String client_line = lignes_client[f];
    Client client = new Client();

    client.Lastname = client_line.Split('%')[0];
    client.Firstname = client_line.Split('%')[1];
    client.Birthdate = DateTimeUtils.ConvertToDateTime(client_line.Split('%')[2]);
    client.PhoneNumber = client_line.Split('%')[3];
    client.Email = client_line.Split('%')[4];

    clients.Add(client);        
        
}

//Insert :
var db = host.Services.GetRequiredService<CarDbContext>();


if (!db.Cars.Any() && !db.Clients.Any())
{
    db.Clients.AddRange(clients);

    db.Cars.AddRange(cars);


    db.SaveChanges();
}




//obtenir la liste des voitures (en cours
using var scope = host.Services.CreateScope();
ICarRepository carRepository = scope.ServiceProvider.GetRequiredService<ICarRepository>();



//fonction qui pose la question à l'utilisateur
static string Question()
{
    Console.WriteLine("\t1) Voir liste voiture\n" +
                      "\t2) Historique d'achat (croissant)\n" +
                      "\t3) Ajouter un client\n" +
                      "\t4) Ajouter une voiture\n" +
                      "\t5) Faire un achat de voiture\n" +
                      "fin");

    string reply = Console.ReadLine();
    return reply;
}

//fonction main
void Main(string[] args)
{
    string reply = Question();
    
    if (reply == "1")
    {
        Console.WriteLine("Affichage de la liste des voitures");
        List<Car> carDb = carRepository.GetCar();
        foreach (var car in cars)
        {
            Console.WriteLine(car.Brand + " " + car.Model + " " + car.Year + " " + car.PriceExlTax);
        }
        
    }

    if (reply == "2")
    {
        Console.WriteLine("Affichage de l'historique d'achat en ordre croissant");
    }

    if (reply == "3")
    {
        Console.WriteLine("Ajout du client ...");
        Console.WriteLine("Quel est le nom du client ?");
        string lastname = Console.ReadLine();
        Console.WriteLine("Quel est le prenom du client ?");
        string firstname = Console.ReadLine();
        Console.WriteLine("Quel est la date de naissance du client ? (ex : 01/01/1900)");
        string birthdate = Console.ReadLine();
        Console.WriteLine("Quel est le numero de téléphone du client ?");
        string phonenumber = Console.ReadLine();
        Console.WriteLine("Quel est le mail du client ?");
        string email = Console.ReadLine();
        
        Client client = new Client();

        client.Lastname = lastname;
        client.Firstname = firstname;
        client.Birthdate = DateTimeUtils.ConvertToDateTime(birthdate);
        client.PhoneNumber = phonenumber;
        client.Email = email;

        clients.Add(client);
    }

    if (reply == "4")
    {
        Console.WriteLine("Ajout de la voiture ...");
    }

    if (reply == "5")
    {
        Console.WriteLine("Achat de la voiture ...");
    }

    if (reply == "6")
    {
        Console.WriteLine("Retours au menu");
    }
    else
    {
        Console.WriteLine("Erreur");
        reply =  Question();

    }
}