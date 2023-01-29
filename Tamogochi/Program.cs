using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Tamogochi;
using Tamogochi.Models;
using static Tamogochi.Display;
using static Tamogochi.Menu;
namespace Tamogochi;

class Program
{
    public static List<Cat> allCats = FileManager.ReadAll();


    // !! >>>>>> Эта часть нужна чтобы сделать окно во весь экран. Не знаю как работает, просто нашел в интернете и перепечатал  <<<<<<<<<<<
    /**/
    /**/
    [DllImport("kernel32.dll", ExactSpelling = true)]
    static extern IntPtr GetConsoleWindow();
    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
    const int MAXIMIZE = 3;
    /**/
    /**/
    // !! >>>>>> Эта часть нужна чтобы сделать окно во весь экран. Не знаю как работает, просто нашел в интернете и перепечатал  <<<<<<<<<<<

    public static void Main(string[] args)
    {
        // !! >>>>>> Эта часть нужна чтобы сделать окно во весь экран. Не знаю как работает, просто нашел в интернете и перепечатал  <<<<<<<<<<<
        /**/
        /**/
        Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
        ShowWindow(GetConsoleWindow(), Program.MAXIMIZE);
        /**/
        /**/
        // !! >>>>>> Эта часть нужна чтобы сделать окно во весь экран. Не знаю как работает, просто нашел в интернете и перепечатал  <<<<<<<<<<<


        Console.OutputEncoding = Encoding.UTF8;
        Console.CursorVisible = false;

        if (!File.Exists(FileManager.fileName))
            File.WriteAllText(FileManager.fileName, "");

        LogWindow();
        ShowAllCats();
        CreateMenu();
        ShowMenu();
        // !! >>>>>> Эта часть нужна чтобы сделать окно во весь экран. Не знаю как работает, просто нашел в интернете и перепечатал  <<<<<<<<<<<
        /**/
        /**/
        [DllImport("kernel32.dll", ExactSpelling = true)]
        static extern IntPtr GetConsoleWindow();
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        const int MAXIMIZE = 3;
        /**/
        /**/
        // !! >>>>>> Эта часть нужна чтобы сделать окно во весь экран. Не знаю как работает, просто нашел в интернете и перепечатал  <<<<<<<<<<<
    }


    public static Cat InputData()
    {
        Cat cat = new Cat();
        cat.Name = UserInput.InputString("Введи Имя кота");
        cat.Age = UserInput.InputInteger("Введи возраст кота");
        Random random = new Random();
        cat.Health = random.Next(20, 81);
        cat.Mood = random.Next(20, 81);
        cat.Satiety = random.Next(20, 81);
        cat.Level = (cat.Health + cat.Mood + cat.Satiety) / 3;
        LogWindow($"Вы завели нового кота {cat.Name} Его уровень: {cat.Level}");
        return cat;
    }


    public static void CatAction(Action action, string CatName = "")
    {
        string textAction = "";
        string textAnswer = "";
        int[] satiety = new int[3];
        int[] mood = new int[3];
        int[] health = new int[3];

        switch (action)
        {
            case Action.Feed:
                LogWindow($"Вы покормили кота {CatName}");

                satiety[0] = 7;
                satiety[1] = 5;
                satiety[2] = 4;
                mood[0] = 3;
                mood[1] = 5;
                mood[2] = 6;
                health[0] = 0;
                health[1] = 0;
                health[2] = 0;
                break;

            case Action.Play:
                LogWindow($"Вы поиграли с котом {CatName}");

                satiety[0] = -3;
                satiety[1] = -5;
                satiety[2] = -6;
                mood[0] = 7;
                mood[1] = 5;
                mood[2] = 4;
                health[0] = 7;
                health[1] = 5;
                health[2] = 4;
                break;

            case Action.Heal:
                LogWindow($"Вы полечили кота {CatName}");

                satiety[0] = -3;
                satiety[1] = -5;
                satiety[2] = -6;
                mood[0] = -3;
                mood[1] = -5;
                mood[2] = -6;
                health[0] = 7;
                health[1] = 5;
                health[2] = 4;
                break;

            case Action.NewDay:
                LogWindow("Наступил новый день");
                Random random = new Random();
                textAnswer = $" ";
                satiety[0] = random.Next(1, 5);
                satiety[1] = random.Next(1, 5);
                satiety[2] = random.Next(1, 5);
                mood[0] = random.Next(-3, 3);
                mood[1] = random.Next(-3, 3);
                mood[2] = random.Next(-3, 3);
                health[0] = random.Next(-3, 3);
                health[1] = random.Next(-3, 3);
                health[2] = random.Next(-3, 3);
                break;
            default: break;
        }
        bool findCat = false;

        foreach (Cat cat in allCats)
            if (cat.Name.Contains(CatName))
            {
                findCat = true;

                if (cat.Age <= 5)
                {
                    cat.Satiety += satiety[0];
                    cat.Mood += mood[0];
                    cat.Health += health[0];
                }
                else if (cat.Age >= 6 && cat.Age <= 10)
                {
                    cat.Satiety += satiety[1];
                    cat.Mood += mood[1];
                    cat.Health += health[1];
                }
                else
                {
                    cat.Satiety += satiety[2];
                    cat.Mood += mood[2];
                    cat.Health += health[2];
                }
                cat.Level = (cat.Health + cat.Mood + cat.Satiety) / 3;
                FileManager.SaveAll();
            }

    }


    public static void TextXY<T>(T text, int x, int y, Color colorText = Color.White, Color bgColor = Color.Black, bool newLine = true)
    {
        Console.SetCursorPosition(x, y);
        switch (colorText)
        {
            case Color.Black: Console.ForegroundColor = ConsoleColor.Black; break;
            case Color.White: Console.ForegroundColor = ConsoleColor.White; break;
            case Color.Red: Console.ForegroundColor = ConsoleColor.Red; break;
            case Color.Green: Console.ForegroundColor = ConsoleColor.Green; break;
            case Color.Blue: Console.ForegroundColor = ConsoleColor.Blue; break;
            case Color.Yellow: Console.ForegroundColor = ConsoleColor.Yellow; break;
            case Color.Magenta: Console.ForegroundColor = ConsoleColor.Magenta; break;
            case Color.Cyan: Console.ForegroundColor = ConsoleColor.Cyan; break;
            case Color.DarkRed: Console.ForegroundColor = ConsoleColor.DarkRed; break;
            case Color.DarkMagenta: Console.ForegroundColor = ConsoleColor.DarkMagenta; break;
            case Color.DarkYellow: Console.ForegroundColor = ConsoleColor.DarkYellow; break;
            case Color.DarkBlue: Console.ForegroundColor = ConsoleColor.DarkBlue; break;
            case Color.DarkGreen: Console.ForegroundColor = ConsoleColor.DarkGreen; break;
            case Color.Gray: Console.ForegroundColor = ConsoleColor.DarkGray; break;
            default: Console.ForegroundColor = ConsoleColor.White; break;
        }

        switch (bgColor)
        {
            case Color.Black: Console.BackgroundColor = ConsoleColor.Black; break;
            case Color.White: Console.BackgroundColor = ConsoleColor.White; break;
            case Color.Red: Console.BackgroundColor = ConsoleColor.Red; break;
            case Color.Green: Console.BackgroundColor = ConsoleColor.Green; break;
            case Color.Blue: Console.BackgroundColor = ConsoleColor.Blue; break;
            case Color.Yellow: Console.BackgroundColor = ConsoleColor.Yellow; break;
            case Color.Magenta: Console.BackgroundColor = ConsoleColor.Magenta; break;
            case Color.Cyan: Console.BackgroundColor = ConsoleColor.Cyan; break;
            case Color.DarkRed: Console.BackgroundColor = ConsoleColor.DarkRed; break;
            case Color.DarkMagenta: Console.BackgroundColor = ConsoleColor.DarkMagenta; break;
            case Color.DarkYellow: Console.BackgroundColor = ConsoleColor.DarkYellow; break;
            case Color.DarkBlue: Console.BackgroundColor = ConsoleColor.DarkBlue; break;
            case Color.DarkGreen: Console.BackgroundColor = ConsoleColor.DarkGreen; break;
            case Color.Gray: Console.BackgroundColor = ConsoleColor.Gray; break;

            default: Console.BackgroundColor = ConsoleColor.Black; break;
        }
        if (newLine) Console.WriteLine(text);
        if (!newLine) Console.Write(text);
        Console.ResetColor();
    }
}

public enum Color
{
    Black,
    White,
    Red,
    Green,
    Blue,
    Yellow,
    Magenta,
    Cyan,
    DarkRed,
    DarkYellow,
    DarkGreen,
    DarkBlue,
    DarkMagenta,
    Gray
}

public enum Action
{
    Feed,
    Play,
    Heal,
    NewDay
}

// TODO: В случае неверноего ввода убрать вывод в окно сообщений