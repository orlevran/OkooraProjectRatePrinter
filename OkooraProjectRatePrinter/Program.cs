using MongoDB.Driver;
//using ExchangeRateReader.Models;
using OkooraProjectRatePrinter.Models;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var configuration = new ConfigurationBuilder()
        .SetBasePath(AppContext.BaseDirectory) // or Directory.GetCurrentDirectory()
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .Build();

        var connectionString = configuration["MongoDBSettings:ConnectionString"];
        var databaseName = configuration["MongoDBSettings:DatabaseName"];
        var collectionName = configuration["MongoDBSettings:CollectionName"];

        var client = new MongoClient(connectionString);
        var database = client.GetDatabase(databaseName);
        var collection = database.GetCollection<ExchangePackage>(collectionName);

        Console.WriteLine("Starting Exchange Rate Reader...");

        while (true)
        {
            try
            {
                var sort = Builders<ExchangePackage>.Sort.Descending(x => x.CreatedAt);
                var latestPackage = await collection.Find(_ => true)
                                                     .Sort(sort)
                                                     .Limit(1)
                                                     .FirstOrDefaultAsync();

                Console.Clear();
                if (latestPackage != null)
                {
                    Console.WriteLine($"Exchange Rates as of {latestPackage.CreatedAt} UTC:");
                    Console.WriteLine("----------------------------------------------");

                    foreach (var rate in latestPackage.Rates)
                    {
                        Console.WriteLine($"1 {rate.FromCurrency} = {rate.Value} {rate.ToCurrency}");
                    }
                }
                else
                {
                    Console.WriteLine("No exchange packages found in the database.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading from DB: {ex.Message}");
            }

            await Task.Delay(TimeSpan.FromSeconds(10));
        }
    }
}
