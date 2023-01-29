using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tamogochi;
using Tamogochi.Interface;
using Tamogochi.Models;
using static Tamogochi.Program;
using static Tamogochi.Menu;


namespace Tamogochi;

public static class Display
{
    const int MENU_WIDTH = 32;          // Ширина меню
    const int MENU_X = 58;              // Положение левого верхнего угла меню по горизонтали
    const int MENU_Y = 2;               // Положение левого верхнего угла меню по вертикали
    public static int countLine = 0;    // Счетчик пунктов меню
    public static int selectedLine = 0; // номер выбранного пункта меню
    public static int selectedCat = 0;  // номер выбранного Кота
    public static bool Exit = false;    // Если нажмем Escape - поле станет true и метод ShowMenu завершится
    public static List<string> MenuLines { get; set; } = new List<string>();


    public static int count;
    public static List<string> Logger = new List<string>();
    public const int WINDOW_LOGGER_X = 3;
    public const int WINDOW_LOGGER_Y = 3;
    public const int WINDOW_ACTION_X = 59;
    public const int WINDOW_ACTION_Y = 10;
    public const int WINDOW_LIST_X = 3;
    public const int WINDOW_LIST_Y = 18;
    public const int WINDOW_MESSAGE_INPUT_X = WINDOW_ACTION_X - 1;
    public const int WINDOW_MESSAGE_INPUT_Y = WINDOW_ACTION_Y + 4;

    public static void ShowMenu()
    {
        while (!Exit)
        {
            foreach (var line in MenuLines)
                LineMenu(line);
            SelectMenuLine();
        }
    }
    // создает пункт меню на отдельной строчке. Название пункта меню передается в text
    public static void LineMenu(string text)
    {
        // Горизонтальные рамки меню
        string BorderTopBottom = new string(' ', MENU_WIDTH + 4);
        Console.SetCursorPosition(MENU_X, MENU_Y - 1);
        Console.BackgroundColor = ConsoleColor.DarkGray;
        Console.Write(BorderTopBottom);
        Console.SetCursorPosition(MENU_X, MENU_Y + MenuLines.Count);
        Console.Write(BorderTopBottom);


        // отступы слева и справа в пунктах меню, чтобы текст был посередине
        string spaceLeft = new string(' ', (MENU_WIDTH - text.Length) / 2);
        string spaceRight = new string(' ', MENU_WIDTH - text.Length - spaceLeft.Length);

        // Ставим курсор в начало строки, где будет пункт меню
        Console.SetCursorPosition(MENU_X, MENU_Y + countLine);

        // левая вертикальная рамка меню
        Console.BackgroundColor = ConsoleColor.DarkGray;
        Console.Write("  ");
        Console.BackgroundColor = ConsoleColor.Black;

        if (countLine == selectedLine)
        {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.White;
        }

        // текст пункта меню с отступами слева и справа для отцентровки текста
        Console.Write(spaceLeft + text + spaceRight);

        // правая вертикальная рамка меню
        Console.BackgroundColor = ConsoleColor.DarkGray;
        Console.WriteLine("  ");

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.BackgroundColor = ConsoleColor.Black;

        // прибавялем счетчик пунктов меню, чтобы слеующий создавался на новой строке. Если счетчик больше, чем всего пнктов меню - обнуляем его
        countLine++;
        if (countLine > MenuLines.Count - 1) countLine = 0;
    }


    public static void ShowAllCats(int selectedCat = -1)
    {
        List<Cat> allCats = FileManager.ReadAll(); // Все коты из файла
        allCats.Sort((x, y) => y.Level.CompareTo(x.Level));

        int countLine = 0;
        Console.ForegroundColor = ConsoleColor.Green;
        Console.SetCursorPosition(WINDOW_LIST_X, WINDOW_LIST_Y - 2);
        Console.WriteLine(" _______________________________________________________________________________________");
        Console.SetCursorPosition(WINDOW_LIST_X, WINDOW_LIST_Y - 1);
        Console.WriteLine("| {0,-2} | {1,-18} | {2,-10} | {3,-10} | {4,-10} | {5,-10} | {6,-7} |", "№", "        Имя", "  Возраст", " Здоровье", "Настроение", "  Сытость", "Уровень");
        Console.SetCursorPosition(WINDOW_LIST_X, WINDOW_LIST_Y);

        int id = 0;
        foreach (Cat cat in allCats)
        {
            Console.SetCursorPosition(WINDOW_LIST_X, WINDOW_LIST_Y + countLine++);
            Console.WriteLine("|----+--------------------+------------+------------+------------+------------+---------|");

            Console.SetCursorPosition(WINDOW_LIST_X, WINDOW_LIST_Y + countLine++);

            Console.Write("|");
            if (selectedCat == id)
            {
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.Yellow;
            }
            Console.Write(" {0,-2} | {1,-18} | {2,-10} | {3,-10} | {4,-10} | {5,-10} | {6,-7} ", id++ + 1, " " + cat.Name, "   " + cat.Age, "   " + cat.Health, "   " + cat.Mood, "   " + cat.Satiety, "   " + cat.Level);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine("|");


            Console.SetCursorPosition(WINDOW_LIST_X, WINDOW_LIST_Y + countLine);
            Console.WriteLine("'---------------------------------------------------------------------------------------'");
        }


    }


    public static void LogWindow(string message = "")
    {

        TextXY("                                                     ", WINDOW_LOGGER_X - 1, WINDOW_LOGGER_Y - 2, bgColor: Color.Blue);
        TextXY("                                                     ", WINDOW_LOGGER_X - 1, WINDOW_LOGGER_Y + 11, bgColor: Color.Blue);
        TextXY("Последние действия:", WINDOW_LOGGER_X + 16, WINDOW_LOGGER_Y - 1, Color.Red);


        for (int i = 0; i < 12; i++)
        {

            TextXY("  ", WINDOW_LOGGER_X - 1, WINDOW_LOGGER_Y + i - 1, bgColor: Color.Blue);
            TextXY("  ", WINDOW_LOGGER_X + 50, WINDOW_LOGGER_Y + i - 1, bgColor: Color.Blue);
        }

        if (Logger.Count > 9)
        {
            Logger.RemoveRange(0, 1); // Если лог переполнен - удалить первый

        }

        if (message != "")
            Logger.Add(message);
        int countLine = Logger.Count - 1; //номер строки в окне Логгера
        foreach (string msg in Logger)
        {
            TextXY("                                                ", WINDOW_LOGGER_X + 2, WINDOW_LOGGER_Y + countLine, bgColor: Color.Black);
            if (countLine <= 7)
                TextXY(msg, WINDOW_LOGGER_X + 2, WINDOW_LOGGER_Y + countLine--, colorText: Color.Green);
            if (countLine >= 8)
            {
                TextXY(msg, WINDOW_LOGGER_X + 2, WINDOW_LOGGER_Y + countLine--, colorText: Color.DarkGreen);
            }

        }
        if (Logger.Count > 9)
            TextXY("....................", WINDOW_LOGGER_X + 2, WINDOW_LOGGER_Y + 10, colorText: Color.Gray);
        ActionWindow(message);
    }

    public static void ActionWindow(string message, int countLine = 0)
    {
        // очистка окна
        TextXY("                                ", WINDOW_ACTION_X + 1, WINDOW_ACTION_Y + 1, Color.Blue);
        TextXY("                                ", WINDOW_ACTION_X + 1, WINDOW_ACTION_Y + 2, Color.Blue);
        TextXY("                                ", WINDOW_ACTION_X + 1, WINDOW_ACTION_Y + 3, Color.Blue);
        TextXY("                                ", WINDOW_ACTION_X + 1, WINDOW_ACTION_Y + 4, Color.Blue);

        // рисуем верхнюю и нижнюю грани
        TextXY("                                    ", WINDOW_ACTION_X - 1, WINDOW_ACTION_Y, bgColor: Color.DarkBlue);
        TextXY("                                    ", WINDOW_MESSAGE_INPUT_X, WINDOW_MESSAGE_INPUT_Y + 1, bgColor: Color.DarkBlue);
        // пишем текст в окно
        if (message.Length > 30)
        {
            string[] longMessage = message.Split(' ');
            string firstLineMessage = "";
            string secondLineMessage = "";
            for (int i = 0; i <= longMessage.Length / 2; i++)
                firstLineMessage += longMessage[i]+" ";
            for (int i = (longMessage.Length / 2) + 1; i < longMessage.Length; i++)
                secondLineMessage += longMessage[i]+" ";
            TextXY(firstLineMessage, WINDOW_ACTION_X + 18 - firstLineMessage.Length / 2, WINDOW_ACTION_Y + 2 + countLine, Color.Blue);
            TextXY(secondLineMessage, WINDOW_ACTION_X + 18 - secondLineMessage.Length / 2, WINDOW_ACTION_Y + 3 + countLine, Color.Blue);
        }
        else
            TextXY(message, WINDOW_ACTION_X + 16 - message.Length / 2, WINDOW_ACTION_Y + 2 + countLine, Color.Blue);

        for (int i = 0; i < 4; i++)
        {
            TextXY("  ", WINDOW_ACTION_X - 1, WINDOW_ACTION_Y + i + 1, bgColor: Color.DarkBlue);
            TextXY("  ", WINDOW_ACTION_X + 33, WINDOW_ACTION_Y + i + 1, bgColor: Color.DarkBlue);
        }

    }
}
