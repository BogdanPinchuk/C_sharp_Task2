using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Видалити дублікати із уже відсортованого масиву
// https://metanit.com/sharp/algoritm/1.1.php
// ефективність алгоритма EA = O(n + m), або EA <= O(2n)
// де n - кількість відних елементів
// m - кількість вихідних (унікальних) елементів без дублікатів
// так як перший раз необхідно перебрати всі числа і знести в новий масив
// а друга ітерація, виконує копіювання за допомогою Array.Copy(...) дані
// менший масив, розмір якого відповідає кількості чисел які відрізняються
// Примітка. З одного боку можна було реалізувати свою власну колекцію 
// із зімнним внутрішнім масивом і в кінці вивеодити результат, але виходить,
// що згідно реалізації алгоритмів колекції необхідно буде набагато більше 
// ніж 1 раз перестворювати новий масив і копіювати елементи, тоді складність 
// вираховуватиметься EA = O(n + S + 2 * m)
// - де 1 перший обхід базового масиву
// - копіювання в масив на 4 елемента, 
// - якщо іде перевищення унікальних чисел вище масиву на 4 елементи,
// необхідно створити новий з ємністю "array.length * 2" 
// і при цьому перекопіювати дані із одного масиву в інший
// для підрахунку суми треба скористатися геометричною прогресією
// S = a * (Math.Pow(r, k - 1) - 1) / (r - 1)
// де r - 2 (у скільки разів збільшується масив)
// a - початковий член
// k - Math.Floor(log(n)/log(2)) - також означає кількість членів прогресії, 
// або ж кількітсь перестворених раз масивів
// також m - необхідний для фінального підрізання масиву

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
                else if (key == ConsoleKey.N || key == ConsoleKey.Escape)
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
