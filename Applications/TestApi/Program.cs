using NOAAWeather;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

NOAAWeatherService weather = new();



string gridId = "FWD";
string gridX = "68";
string gridY = "95";

var forecast = await weather.GetForecastForGridXYAsync(gridId, gridX, gridY);
var ob = await weather.GetObservationForStationAsync("KFWS");

Console.WriteLine(forecast?.Properties?.Periods[0].Temperature);
