using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Видалити дублікати із уже відсортованого масиву

namespace App2
{
    class Program
    {
        static void Main()
        {
            // join unicode
            Console.OutputEncoding = Encoding.Unicode;

            // goto - використовувати не бажано, але в даному випадку
            // для ефективного повторення допустимо, так як запуск нового вікна 
            // зміщує його, а рекурсія може призвести до переповнення стека
            // якщо не використовувати алгоритм Дейкстри
            Repeat:

            // clear
            Console.Clear();

            // testing data
            int[] input = new int[] { 1, 2, 3, 4, 4, 56 };

            // use extention method
            int[] output = input.MyDistinct();
            // or use usual method 
            int[] output0 = DeleteDuplicate.MyDistinct(input);

            // result
            input.Show("input");
            Console.WriteLine();
            output.Show("output");
            output0.Show("output1");

            #region Create data
            // more interesting with use linq
            // this is for create other array for testing
            Random rnd = new Random();

            input = new int[rnd.Next(2, 28)];
            for (int i = 0; i < input.Length; i++)
            {
                input[i] = rnd.Next(0, 10);
            }

            // linq sorting
            input = input.OrderBy(t => t).Select(t => t).ToArray();

            #endregion

            output = input.MyDistinct();

            // show
            Console.WriteLine();
            input.Show("input");
            output.Show("output");

            #region Delay
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nRepeat again? [y, n]");
            Console.ResetColor();
            ConsoleKey key;

            while (true)
            {
                key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.Y)
                {
                    goto Repeat;
                }
                else if (key == ConsoleKey.N ||
                    key == ConsoleKey.Escape)
                {
                    //Environment.Exit(0);
                    // or
                    break;
                }
            } 
            #endregion
        }


    }
}
