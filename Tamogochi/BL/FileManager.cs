using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Tamogochi;
using Tamogochi.Models;

namespace Tamogochi;

public class FileManager
{

    public static string fileName = @"Cats.json";
    public static void SaveAll()
    {
        Sorting();
        File.WriteAllText(fileName, "");
        // TODO: Отсортировать и Записать весь файл
        foreach (Cat cat in Program.allCats)
        {
            SaveOne(cat);
        }
    }
    public static void SaveOne(Cat cat)
    {
        // TODO: Отсортировать и Записать в файл одну строку
        string serCat = JsonSerializer.Serialize<Cat>(cat);
        File.AppendAllText(fileName, serCat + "\n");
    }

    public static List<Cat>  ReadAll()
    {
        // TODO:  Прочитать весь файл
        string[] allCatsString = File.ReadAllLines(fileName);
        List<Cat> allCats = new List<Cat>();

        foreach (string cat in allCatsString)
        {
            Cat catJson = JsonSerializer.Deserialize<Cat>(cat);
            allCats.Add(catJson);
        }
        
        return allCats;
    }

    public static void ReadOne()
    {
        // TODO:  Прочитать одну строку файла

    }


    public static void Sorting()
    {
        // TODO: Отсортировать объекты 
    }
}
