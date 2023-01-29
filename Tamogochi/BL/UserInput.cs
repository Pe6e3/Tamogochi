using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tamogochi.Models;
using static Tamogochi.Display;


namespace Tamogochi;

public static class UserInput
{
    public static string InputString(string title)
    {
        string result = "";
        ActionWindow(title, -1);
        while (result == "")
        {
            Console.SetCursorPosition(WINDOW_ACTION_X + 3, WINDOW_ACTION_Y + 2);
            string? temp = Console.ReadLine();

            if (temp == "" || temp is null)
                Console.WriteLine("Вы не ввели данные");
            if (temp.Length > 8)
            {
                Console.SetCursorPosition(WINDOW_ACTION_X + 3, WINDOW_ACTION_Y + 2);
                Console.WriteLine("                              ");
                Console.SetCursorPosition(WINDOW_ACTION_X + 3, WINDOW_ACTION_Y + 1);

                Console.WriteLine("Придумай имя по-короче");
                temp = "";
            }
            result = temp;
        }

        return result;
    }
    public static int InputInteger(string title)
    {
        int result = 0;
        ActionWindow(title, -1);

        while (result == 0)
        {
            Console.SetCursorPosition(WINDOW_ACTION_X + 3, WINDOW_ACTION_Y + 2);

            string? temp = Console.ReadLine();
            if (int.TryParse(temp, out int value))
                result = value;
            else
                Console.WriteLine("Вы не ввели число");
        }
        return result;
    }



}
