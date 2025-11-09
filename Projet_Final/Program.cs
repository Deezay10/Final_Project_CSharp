using Projet_Final;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using Projet_Final.InterfaceRepository;
//using Projet_Final.Repository;
using Projet_Final.Model;
using Projet_Final.Utils;
using System.Globalization;
using Projet_Final.Interface;
using Projet_Final.Interface.InterfaceRepository;

Console.WriteLine("le programme se lance...");
Console.WriteLine("T'y es le goat vlad, t'as bien pull");
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
        
        // on enregistre le repository
        services.AddScoped<ICarRepository, CarRepository>();
        
        // on enregistre la classe de connexion
        services.AddTransient<DbConnection>();
    })
    .Build();
    

// chargement du CSV pour les cars
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

//je me suis peut-être trompé...
if (!db.Cars.Any() && !db.Clients.Any())
{
    db.Clients.AddRange(clients);

    db.Cars.AddRange(cars);
    
    db.SaveChanges();
}

Console.WriteLine("jusqu'ici, tout va bien !!!!!!");


//fonction qui pose la question à l'utilisateur
static string Question()
{
    Console.WriteLine("\t1) Voir liste voiture\n" +
                      "\t2) Historique d'achat (croissant)\n" +
                      "\t3) Ajouter un client\n" +
                      "\t4) Ajouter une voiture\n" +
                      "\t5) Faire un achat de voiture\n" +
                      "\t6) fin\n");

    string reply = Console.ReadLine();
    return reply;
}
//pose la question à l'utilisateur qui déterminera la suite du programme
string reply = Question();



if (reply == "1")
{
    // On affiche la liste des voitures
    Console.WriteLine("Affichage de la liste des voitures");
    using var scope = host.Services.CreateScope();
    ICarRepository carRepository = scope.ServiceProvider.GetRequiredService<ICarRepository>();
    List<Car> carDb = carRepository.GetCar();
    foreach (var car in cars)
    {
        Console.WriteLine(car.Id + " " + car.Brand + " " + car.Model + " " + car.Year + " " + car.PriceExlTax);
    }
        
}

if (reply == "2")
{
    Console.WriteLine("Affichage de l'historique d'achat en ordre croissant");
}

if (reply == "3")
{
    Console.WriteLine("Ajout du client ...");
    
    //Demande des caractéristiques du nouveau client
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
    
    //Création du nouveau client
    Client client = new Client();

    //Implémentation des variables 
    client.Lastname = lastname;
    client.Firstname = firstname;
    client.Birthdate = DateTimeUtils.ConvertToDateTime(birthdate);
    client.PhoneNumber = phonenumber;
    client.Email = email;
    
    //Ajouter le client à la liste des clients
    db.Clients.Add(client);
    db.SaveChanges();
}

if (reply == "4")
{
    Console.WriteLine("Ajout de la voiture ...");
    
    //Demande des caractéristiques de la nouvelle voiture
    Console.WriteLine("Quel est la marque de la voiture ?");
    string marque = Console.ReadLine();
    Console.WriteLine("Quel est le modèle de la voiture");
    string modele = Console.ReadLine();
    Console.WriteLine("Quel est l'année de construction de la voiture (ex : 01/01/1900)");
    string anne = Console.ReadLine();
    Console.WriteLine("Quel est le prix HT de la voiture ?");
    string prixht = Console.ReadLine();
    Console.WriteLine("Quel est son statut (est à vendre ou non) (répondre True ou False)");
    string statut = Console.ReadLine();
    Console.WriteLine("Quel est la couleur de la voiture ?");
    string couleur = Console.ReadLine();
    DateTime annercar = DateTimeUtils.ConvertToDateTime(anne);
        
    //création de la nouvelle voiture
    Car car = new Car();

    //Implémentation des variables 
    car.Brand = marque;
    car.Model = modele;
    car.Year = annercar.Year;
    car.PriceExlTax = Single.Parse(prixht);
    car.Status = bool.Parse(statut);
    car.Color = couleur;

    //Ajouter la voiture à la liste des voitures
    db.Cars.Add(car);
    db.SaveChanges();
}

if (reply == "5")
{
    //Demande l'identifiant de la voiture pour la trouver dans la liste des voitures
    Console.WriteLine("Achat de la voiture ...");
    Console.WriteLine("Entrez l'ID de la voiture que vous voulez acheter");
    string idcar = Console.ReadLine();
    Guid idcarkey = Guid.Parse(idcar);
    var car = db.Cars.FirstOrDefault(c => c.Id == idcarkey);
    if (car != null)
    {
        //Si le status de la voiture est déjà vendu
        if (car.Status == false)
        {
            Console.WriteLine($"Vous ne pouvez pas acheter cette {car.Model} car elle a déjà été vendue");
            reply = Question();
        }
        else
        {
            //Demander le mail pour trouver le client
            Console.WriteLine("Quel est votre adresse email ?");
            string mailpotentialclient = Console.ReadLine();
            
            //On récupère le client qui a la même adresse mail que celle entrée
            var client = db.Clients.FirstOrDefault(c => c.Email == mailpotentialclient);
            
            //On modifie les caractéristiques de la voiture que le client viens d'acheter
            car.Status = false;
            car.ClientId = client.Id;
            car.Client = client;
            db.SaveChanges();
            Console.WriteLine($"Merci {client.Firstname} d'avoir acheté cette {car.Model}.");
            
        }
            
    }
}

if (reply == "6")
{
    Console.WriteLine("Fin du Menu");
}
