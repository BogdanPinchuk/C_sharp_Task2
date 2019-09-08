using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// 0 - 1-й варіант


namespace App1
{
    /// <summary>
    /// Analysis of sentence
    /// </summary>
    static class TextAnalysis
    {
        /// <summary>
        /// Group sentences/text on count of letter (Групування речень по кількості букв)
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

            return default;
        }
    }
}
