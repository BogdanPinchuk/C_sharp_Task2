using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

// 0 - 1-й варіант


namespace App1
{
    /// <summary>
    /// Analysis of sentence + Extention methods
    /// </summary>
    static class TextAnalysis
    {
        /// <summary>
        /// Group sentences/text on count of letter (Групування речень по кількості букв) 
        /// using StringBuilder
        /// </summary>
        /// <param name="s">sentence/text</param>
        /// <returns></returns>
        public static SortedDictionary<int, List<string>> GetGroupOfWords0(params string[] strArray)
        {
            // guard operator (сторожовий оператор)
            if (strArray.Length == 0)
            {
                return null;
            }

            // create string builder
            StringBuilder sb = new StringBuilder();

            // convert into string builder
            foreach (string i in strArray)
            {
                sb.Append(i + ' ');
            }

            return GetGroupOfWords0(sb);
        }

        /// <summary>
        /// Group sentences/text on count of letter (Групування речень по кількості букв) 
        /// using StringBuilder
        /// </summary>
        /// <param name="sb">sentence/text</param>
        /// <returns></returns>
        public static SortedDictionary<int, List<string>> GetGroupOfWords0(StringBuilder sb)
        {
            // trim the space on first and end
            while (sb.Length > 0 && sb[0] == ' ')
            {
                sb = sb.Remove(0, 1);
            }
            while (sb.Length > 0 && sb[sb.Length - 1] == ' ')
            {
                sb = sb.Remove(sb.Length - 1, 1);
            }

            // delete "\n", "\t"
            sb = sb.Replace("\n", " ").Replace("\t", " ");

            // guard operator (сторожовий оператор)
            if (sb.Length == 0)
            {
                return null;
            }

            // Примітка. Зазвичай в кінці речення стоїть "." або інший знак пунктуації, який не враховується 
            // як частина самого слова, але раз в умові сказано, що: "Framework." - Words of length: 10, Count: 2
            // значить так звучить ТЗ і вимога замовника, і ми не впарві змінювати щось. Тобто щось додатково робити від себе

            // create SortedDictionary
            SortedDictionary<int, List<string>> sbOut 
                = new SortedDictionary<int, List<string>>();

            // cut on words (ріжем на "слова")
            while (sb.Length != 0)
            {
                // trim the space on first, this space creates on last iteration
                while (sb.Length > 0 && sb[0] == ' ')
                {
                    sb = sb.Remove(0, 1);
                }

                // counter - лічильник
                int count = 0;

                // search space - the end of word (шукаємо пробіл - кінець слова)
                while (count < sb.Length && sb[count] != ' ')
                {
                    count++;
                }

                // temp array
                char[] array = new char[count];

                // cut out a word (вирізаєм слово)
                sb.CopyTo(0, array, 0, count);

                // save word in sorted dictionary
                if (sbOut.ContainsKey(count))
                {
                    sbOut[count].Add(new string(array));
                }
                else  // if key is absent (якщо ключ відсутній)
                {
                    sbOut.Add(count, new List<string>() { new string(array) });
                }
                
                // delete word in "sb"
                sb = sb.Remove(0, count);
            }

            // delete duplicate words
            for (int i = 0; i < sbOut.Count; i++)
            {
                sbOut[sbOut.Keys.ElementAt(i)] = sbOut[sbOut.Keys.ElementAt(i)].Distinct().ToList();
            }

            return sbOut;
        }

        /// <summary>
        /// Returns the data in SortedDictionary in console (Виводить дані SD в консоль)
        /// </summary>
        /// <param name="data">data of sorted dictionary (дані SD)</param>
        public static void ToPresent(this SortedDictionary<int, List<string>> data)
        {
            if (data == null)
            {
                return;
            }

            foreach (var i in data)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Words of length: {i.Key}, Count: {i.Value.Count}");
                Console.ResetColor();
                foreach (var j in i.Value)
                {
                    Console.WriteLine($"\t{j}");
                }
            }
        }

        /// <summary>
        /// Group sentences/text on count of letter (Групування речень по кількості букв) 
        /// using String
        /// </summary>
        /// <param name="s">sentence/text</param>
        /// <returns></returns>
        public static SortedDictionary<int, List<string>> GetGroupOfWords1(params string[] strArray)
        {
            // guard operator (сторожовий оператор)
            if (strArray.Length == 0)
            {
                return null;
            }

            string s = string.Empty;

            {
                // create string builder
                StringBuilder sb = new StringBuilder();

                foreach (string i in strArray)
                {
                    sb.Append(i + ' ');
                }

                s = sb.ToString();
            }

            // create SortedDictionary
            SortedDictionary<int, List<string>> sbOut
                = new SortedDictionary<int, List<string>>();

            // use Regular Expresion
            string pattern = @"\S+";
            Regex regex = new Regex(pattern);

            MatchCollection collection = regex.Matches(s);

            foreach (Match i in collection)
            {
                // save word in sorted dictionary
                if (sbOut.ContainsKey(i.Length))
                {
                    sbOut[i.Length].Add(i.Value);
                }
                else  // if key is absent (якщо ключ відсутній)
                {
                    sbOut.Add(i.Length, new List<string>() { i.Value });
                }
            }

            // delete duplicate words
            for (int i = 0; i < sbOut.Count; i++)
            {
                sbOut[sbOut.Keys.ElementAt(i)] = sbOut[sbOut.Keys.ElementAt(i)].Distinct().ToList();
            }

            return sbOut;
        }

    }
}
