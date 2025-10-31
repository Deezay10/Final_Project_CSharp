

//créer un lien vers appsetting.json
var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile(@"C:\\Users\\julie\\RiderProjects\\CoursSupDeVinci\\CoursSupDeVinci\\appsettings.json", optional: false, reloadOnChange: true)
    .Build();