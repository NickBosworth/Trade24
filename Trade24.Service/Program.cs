using Trade24.Service.Collection;
using Trade24.Service.Data;
using Trade24.Service.Jobs;


Console.WriteLine("Started the Trade24 Service...");

//Initialize the database
using (var db = new SqliteContext())
{
    Console.WriteLine("Database is ready.");
}


//Create a symbol collector for use.
var yahooMostActiveSymbolCollector = new YahooMostActiveSymbolCollector();

while (true)
{



    //UpdateSymbols.Update();

    UpdateHistory.Update();

    //UpdateCurrent.Update();

    //DetectorRunner.RunFor(DateTime.Now);

    DetectorRunner.RunBetween(new DateTime(2022, 1, 1), new DateTime(2023, 10, 29));

    //UpdateIndicatorStates.Update();

    //TrainModel.CreateTrainingSet();

    //Announce sleeping for 1 minute and speed.
    Console.WriteLine("Sleeping for 1 minute...");
    Console.ReadLine();
    Thread.Sleep(60000);
}