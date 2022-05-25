using System.Net.Http.Json;

namespace ChessLibrary.Services.Engines;



public class StockfishAPIClient
{
    HttpClient _client;
    public StockfishAPIClient(HttpClient client)
    {
        _client = client;
    }


    public async Task<MovesResult?> GetComputerMoves(String fen)
    {
        var url = $"http://atlantic1.nic-weber.com:5092/api/stockfish?fen={fen}";
        //var result = await _client.GetFromJsonAsync<MovesResult>(url);
        //var result = await _client.GetStringAsync(url);

        // return HttpResponseMessage
        var res= await _client.GetAsync(url);

        var result = new MovesResult(); 
        if (res.IsSuccessStatusCode)
            result = res.Content.ReadFromJsonAsync<MovesResult>().Result;
        else
            Console.WriteLine("failed");

        return result;
    }
}

