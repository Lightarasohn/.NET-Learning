using System.Data.SqlTypes;
using System.IO;

var currentDirectory = Directory.GetCurrentDirectory();
var storesDirectory = Path.Combine(currentDirectory, "stores");
var sales = FindFiles(storesDirectory);
foreach (var sale in sales)
    Console.WriteLine(sale);


IEnumerable<string> FindFiles(string FilePath)
{
    var foundFiles = Directory.EnumerateFiles(FilePath, "*", SearchOption.AllDirectories);
    var salesFiles = new List<string>();

    foreach(var file in foundFiles)
    {
        if(file.EndsWith("sales.json"))
            salesFiles.Add(file);
    }

    return salesFiles;
}

