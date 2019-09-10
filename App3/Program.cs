using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

// Додаткова література, де можна найти аналітичну і апроксимовану формули 
// для розрахунку чисел Фібоначі
// 1. https://en.wikipedia.org/wiki/Fibonacci_number
// 2. http://www.maths.surrey.ac.uk/hosted-sites/R.Knott/Fibonacci/fibFormula.html
// 3. https://uk.wikipedia.org/wiki/Золотий_перетин
// 4. https://uk.wikipedia.org/wiki/Послідовність_Фібоначчі

namespace App3
{
    class Program
    {
        /// <summary>
        /// Делегат для передачі методу, який розраховує числа Фібоначі
        /// </summary>
        /// <param name="n">член фібоначі</param>
        private delegate int Fibonacci(int n);
        /// <summary>
        /// Список ключ-занчення, для розрахунку числа Фібоначі за рекурсивною формулою
        /// </summary>
        private static Dictionary<int, int?> FibData = new Dictionary<int, int?>();
        /// <summary>
        /// For lock Console
        /// </summary>
        private static object blockConsole = new object();
        /// <summary>
        /// For lock FibData
        /// </summary>
        private static object blockFib = new object();

        static void Main(string[] args)
        {
            // join unicode
            Console.OutputEncoding = Encoding.Unicode;

            // range
            int min = -8,
                max = 35;

            // time analysis 
            #region Usual recursion
            new Thread(() => PresentResult(FibRecursionUsual,
                "Fibonacci number - usual recursion", min, max)).Start();
            #endregion

            #region Lambda recursion
            new Thread(() => PresentResult(FibRecursionLambda,
                "Fibonacci number - lambda recursion", min, max)).Start();
            #endregion

            #region Analytic formula by Binet
            new Thread(() => PresentResult(FibAnalyticFormula,
                "Fibonacci number - analytic formula by Binet", min, max)).Start();
            #endregion

            #region Approximation formula
            new Thread(() => PresentResult(FibAnalyticFormula,
                "Fibonacci number - approximation formula", min, max)).Start();
            #endregion

            #region Iterative method
            new Thread(() => PresentResult(FibIterative,
                "Fibonacci number - iterative method", min, max)).Start();
            #endregion

            #region Matrix method
            new Thread(() => PresentResult(FibMatrix,
                "Fibonacci number - matrix method", min, max)).Start();
            #endregion

            // delay
            Console.ReadKey(true);
        }

        /// <summary>
        /// Представлення результатів розрахунку
        /// </summary>
        /// <param name="fib">Метод який розраховує числа Фібоначі</param>
        /// <param name="info">Інформація про метод</param>
        /// <param name="min">мінімальний член</param>
        /// <param name="max">максимальний член</param>
        private static void PresentResult(Fibonacci fib, string info, int min, int max)
        {
            // timer
            Stopwatch timer = new Stopwatch();
            Dictionary<int, int> array = new Dictionary<int, int>();
            timer.Start();
            // calculate fib.number and save result
            for (int i = min; i <= max; i++)
            {
                array.Add(i, fib(i));
            }
            timer.Stop();
            double time = timer.Elapsed.TotalMilliseconds;

            lock (blockConsole)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n" + info);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Time: {time} ms");
                Console.ResetColor();
#if false
                foreach (var i in array)
                {
                    Console.WriteLine($"\tF({i.Key}) = {i.Value}");
                } 
#endif
            }
        }

        /// <summary>
        /// Рекурсивна реалізація знаходження числа Фібоначі
        /// </summary>
        /// <param name="n">номер члена</param>
        /// <returns></returns>
        private static int FibRecursionUsual(int n)
        {
            // includes negative members
            if (Math.Abs(n) <= 1)
            {
                return (int)Math.Pow(-1, n + 1) * Math.Abs(n);
            }

            return ((n < 0) ? (int)Math.Pow(-1, n + 1) : 1) *
                (FibRecursionUsual(Math.Abs(n) - 1) + FibRecursionUsual(Math.Abs(n) - 2));
        }

        /// <summary>
        /// Рекурсивна реалізація знаходження числа Фібоначі через лямда-вираз
        /// </summary>
        /// <param name="n">номер члена</param>
        /// <returns></returns>
        private static int FibRecursionLambda(int n)
        {
            // звичайно можна було б і через лямба вираз
            Func<int, int> fib = null;
            fib = (x) => (Math.Abs(x) > 1) ?
            ((x < 0) ? (int)Math.Pow(-1, x + 1) : 1) *
            (fib(Math.Abs(x) - 1) + fib(Math.Abs(x) - 2)) :
            (int)Math.Pow(-1, x + 1) * Math.Abs(x);

            return fib(n);
        }

        /// <summary>
        /// Реалізація знаходження числа Фібоначі ітераційно
        /// </summary>
        /// <param name="n">номер члена</param>
        /// <returns></returns>
        private static int FibIterative(int n)
        {
            // create array
            int[] array = new int[Math.Abs(n) + 1];

            // base value
            array[0] = 0;
            if (Math.Abs(n) > 0)
            {
                array[1] = 1;
            }

            for (int i = 2; i < array.Length; i++)
            {
                array[i] = array[i - 1] + array[i - 2];
            }

            return ((n < 0) ? (int)Math.Pow(-1, n + 1) : 1) * array.Last();
        }

        /// <summary>
        /// Реалізація знаходження числа Фібоначі за аналітичною формулою Біне
        /// </summary>
        /// <param name="n">номер члена</param>
        /// <returns></returns>
        private static int FibAnalyticFormula(int n)
        {
            // значення золотого перетину
            double phi = 0.5 * (Math.Sqrt(5) - 1);

            // результат
            double res = (Math.Pow(phi + 1, n) - Math.Pow(-phi, n)) / Math.Sqrt(5);

            // + округнення
            return (int)Math.Round(res, MidpointRounding.AwayFromZero);
        }

        /// <summary>
        /// Реалізація знаходження числа Фібоначі за апроксимованою формулою
        /// </summary>
        /// <param name="n">номер члена</param>
        /// <returns></returns>
        private static int FibApproximationFormula(int n)
        {
            // значення золотого перетину
            double phi = 0.5 * (Math.Sqrt(5) - 1);

            // результат
            double res = default(double);

            if (n >= 0)
            {
                res = Math.Pow(phi + 1, n) / Math.Sqrt(5);
            }
            else
            {
                res = -Math.Pow(-phi, n) / Math.Sqrt(5);
            }

            // + округнення
            return (int)Math.Round(res, MidpointRounding.AwayFromZero);
        }

        /// <summary>
        /// Реалізація знаходження числа Фібоначі на основі матриць
        /// </summary>
        /// <param name="n">номер члена</param>
        /// <returns></returns>
        private static int FibMatrix(int n)
        {
            // щоб не ускладнювати алгоритм і 
            // не реалізовувати знаходження оберненої матриці A^-1 
            if (n == 0)
            {
                return 0;
            }

            // Explain - пояснення
            // f_n   = f_n-1 + f_n-2    : n-й член числа Фібоначі
            // f_n-1 = f_n-1 + 0        : n-1-йчлен числа Фібоначі
            // переписувмо в матрицю
            // |f_n|   = |1 1| * |f_n-1| 
            // |f_n-1|   |1 0|   |f_n-2|
            // Якщо замінити  матриці на букви: X = A * M
            // то для того щоб дійти до членів 0 і 1, необхідно перемнодити m раз матрицю A
            // |f_n-n+1| = |1| = B
            // |f_n-n  |   |0|
            // отже X = A^(n-1) * B
            // |f_n  | = |1 1|n-1 * |1|
            // |f_n-1|   |1 0|      |0|
            // а так як ми не можемо реалізувати вектор, то згідно правил перемноження матриць
            // матрицю B можна замінити наступною
            // |1| => |1 0|
            // |0|    |0 0|
            // при цьому отримаємо X
            // |f_n   0| - звідки ми беремо лише необхідний нам член матриці
            // |f_n-1 0|

            // create matrix A
            Matrix a = new Matrix(1, 1, 1, 0, 0, 0);
            Matrix ac = a.Clone();
            // create matrix B
            Matrix b = new Matrix(1, 0, 0, 0, 0, 0);

            // Multiply A^n
            for (int i = 1; i < Math.Abs(n) - 1; i++)
            {
                a.Multiply(ac);
            }

            // Multiply A^(n-1) * B
            a.Multiply(b);

            return ((n < 0) ? (int)Math.Pow(-1, n + 1) : 1) * (int)a.Elements[0];
        }

    }
}
