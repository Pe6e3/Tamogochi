using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Tamogochi;
using Tamogochi.Models;
using static Tamogochi.Program;
using static Tamogochi.Display;

namespace Tamogochi;

public static class Menu
{
    public static void CreateMenu()
    {
        MenuLines.Add("Завести нового кота");
        MenuLines.Add("Покормить");
        MenuLines.Add("Поиграть");
        MenuLines.Add("К ветеринару");
        MenuLines.Add("Следующий день");
        MenuLines.Add("Выход");
    }



    public static void SelectMenuLine()
    {
        ConsoleKey button = Console.ReadKey().Key;
        switch (button)
        {
            case ConsoleKey.Enter: EnterMenuLine(selectedLine); break;
            case ConsoleKey.UpArrow: selectedLine--; break;
            case ConsoleKey.DownArrow: selectedLine++; break;
            case ConsoleKey.Escape: Exit = true; break;
        }
        if (selectedLine < 0) selectedLine = MenuLines.Count - 1;
        if (selectedLine > MenuLines.Count - 1) selectedLine = 0;
    }

    public static void EnterMenuLine(int selectedLine)
    {
        switch (selectedLine)
        {
            case 0: FileManager.SaveOne(InputData()); ShowAllCats(); break; // Завести нового кота
            case 1: SelectCat(Action.Feed); ShowAllCats(); break; // Покормить кота
            case 2: SelectCat(Action.Play); ShowAllCats(); break; // Поиграть с котом
            case 3: SelectCat(Action.Heal); ShowAllCats(); break; // К ветеринару
            case 4: CatAction(Action.NewDay); ShowAllCats(); break; // Следующий день
            case 5: Console.Clear(); Environment.Exit(0); break;  // Выход
            default: break;
        }
    }


    // Метод запускается для выбора кота из табицы всех котов
    public static void SelectCat(Action action)
    {
        allCats.Sort((x, y) => y.Level.CompareTo(x.Level));
        ShowAllCats(selectedCat);

        while (true)
        {
            ConsoleKey button = Console.ReadKey().Key;
            switch (button)
            {
                case ConsoleKey.Enter: CatAction(action, allCats[selectedCat].Name);         return;
                case ConsoleKey.UpArrow: selectedCat--; break;
                case ConsoleKey.DownArrow: selectedCat++; break;
                case ConsoleKey.Escape: return;
            }
            if (selectedCat < 0) selectedCat = allCats.Count - 1;
            if (selectedCat > allCats.Count - 1) selectedCat = 0;

            ShowAllCats(selectedCat);
        }
    }

}
