using System.Data.SqlTypes;
using System.IO;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

/*var currentDirectory = Directory.GetCurrentDirectory();
var storesDirectory = Path.Combine(currentDirectory, "stores");
var sales = FindFiles(storesDirectory);
foreach (var sale in sales)
    Console.WriteLine(sale);


IEnumerable<string> FindFiles(string FilePath)
{
    var foundFiles = Directory.EnumerateFiles(FilePath, "*", SearchOption.AllDirectories);
    var salesFiles = new List<string>();

    foreach (var file in foundFiles)
    {
        if (file.EndsWith("sales.json"))
            salesFiles.Add(file);
    }

    return salesFiles;
}


var salesTotalDir = Path.Combine(storesDirectory, "salesTotalDir");
Directory.CreateDirectory(salesTotalDir);
File.WriteAllText(Path.Combine(salesTotalDir, "totals.txt"), "1234");


var salesJson = File.ReadAllText(Path.Combine(storesDirectory, "201//sales.json"));
var salesJson2 = File.ReadAllText(Path.Combine(storesDirectory, "202//sales.json"));


var salesData = JsonConvert.DeserializeObject<SalesTotal>(salesJson);

Console.WriteLine(salesData.Total);

File.WriteAllText(Path.Combine(salesTotalDir, "totals.txt"), salesData.Total.ToString());
salesData = JsonConvert.DeserializeObject<SalesTotal>(salesJson2);
File.AppendAllText(Path.Combine(salesTotalDir, "totals.txt"), salesData.Total.ToString());


class SalesTotal
{
    public double Total { get; set; }
}
*/


var currentDirectoryPath = Directory.GetCurrentDirectory();
var storesDirectyPath = Path.Combine(currentDirectoryPath, "stores");
string totalDirectory = Path.Combine(storesDirectyPath, "salesTotalDir");

double CollectAllSalesTotal(string directoryPath)
{
    
    var allDirectoryFiles = Directory.EnumerateFiles(directoryPath, "*",SearchOption.AllDirectories);
    double totalSalesTotal = 0;
    string salesJson;
    File.WriteAllText(Path.Combine(totalDirectory, "allTotals.txt"), "");
    foreach (var file in allDirectoryFiles)
    {
        if (file.EndsWith("sales.json"))
        {
            salesJson = File.ReadAllText(file);
            var salesData = JsonConvert.DeserializeObject<SalesTotal>(salesJson);
            totalSalesTotal += salesData.Total;
            File.AppendAllText(Path.Combine(totalDirectory, "allTotals.txt"), salesData.Total.ToString() + "\n");
        }
    }
    return totalSalesTotal;
}

var totalSales = CollectAllSalesTotal(storesDirectyPath);
File.WriteAllText(Path.Combine(totalDirectory, "totals.txt"), totalSales.ToString());


class SalesTotal
{
    public double Total { get; set; }
}
