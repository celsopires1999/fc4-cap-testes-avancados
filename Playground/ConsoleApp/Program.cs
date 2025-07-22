StockRecord stock1 = new StockRecord();
Console.WriteLine($"Symbol: {stock1.Symbol} Average: {stock1.Average} High: {stock1.High} Low: {stock1.Low} Length: {stock1.Length}");

// for (int i = 0; i < stock1.Length; i++)
// {
//     decimal val = stock1[i];
//     Console.WriteLine($"Val: {val:C}");
// }

Console.WriteLine(stock1["Monday"]);
Console.WriteLine(stock1["Tuesday"]);
Console.WriteLine(stock1["Wednesday"]);
Console.WriteLine(stock1["Thursday"]);
Console.WriteLine(stock1["Friday"]);
Console.WriteLine(stock1["Saturday"]);
Console.WriteLine(stock1["Sunday"]);
