using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App2
{
    /// <summary>
    /// Видалення дублыкатыв через розширений метод
    /// </summary>
    static class DeleteDuplicate
    {
        /// <summary>
        /// видалення дублікатів
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static int[] MyDistinct(this int[] input)
        {
            if (input.Length == 0)
            {
                throw new Exception("Array is empty.");
            }

            // create new array
            int[] array = new int[input.Length];

            array[0] = input[0];

            // counter
            int count = 1;

            for (int i = 1; i < input.Length; i++)
            {
                if (input[i] != input[i - 1])
                {
                    array[count++] = input[i];
                }
            }

            int[] output = new int[count];

            // this is not Linq
            Array.Copy(array, 0, output, 0, count);

            return output;
        }

        /// <summary>
        /// показ результатів
        /// </summary>
        /// <param name="input"></param>
        /// <param name="s"></param>
        public static void Show(this int[] input, string s)
        {
            var res = new StringBuilder(s.ToUpper() + ": int[] { ");
            for (int i = 0; i < input.Length; i++)
            {
                res.Append(input[i] + ", ");
            }

            res = res.Remove(res.Length - 2, 2).Append(" }");

            Console.WriteLine(res.ToString());
        }

    }
}
