// 1 - виконання завдання за допомогою StringBuilder (перевага в швидкості і економії пам'яті)
// 2 - реалізація яка дозволяє не лізти в код, а зразу кинути файл на exe файл програми і реалізувати дану функцію
// 3 - реалізація через String, Regex

//#define test

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1
{
    class Program
    {
        static void Main(string[] args)
        {
            // join unicode
            Console.OutputEncoding = Encoding.Unicode;

            string testStr = "The “C# Professional” course includes the topics " +
                "I discuss in my CLR via C# book and teaches how the CLR works " +
                "thereby showing you how to develop applications and reusable " +
                "components for the .NET Framework.";

            // testing with timer
#if test
            Stopwatch timer = new Stopwatch();
            double[] result = new double[2];
#endif

            #region Variant 1
#if false
#if test
            timer.Start();
#endif
            // analysis of string and show result
            var v1 = TextAnalysis.GetGroupOfWords0(testStr);
#if test
            timer.Stop();
            result[0] = timer.Elapsed.TotalSeconds;
#endif
            Console.WriteLine();
            v1.ToPresent();
#endif
            #endregion

            #region Variant 2
#if false
            // create file for testing
            // CreateTestFile();

            // when, the user want to fall the *.txt file
            if (args.Length > 0)
            {
                AnalysisFile(args[0]);
            } 
#endif
            #endregion

            #region Variant 3
#if true
#if test
            timer.Restart();
#endif
            // analysis of string and show result
            var v3 = TextAnalysis.GetGroupOfWords1(testStr);//.ToPresent();
#if test
            timer.Stop();
            result[1] = timer.Elapsed.TotalSeconds;
#endif
            Console.WriteLine();
            v3.ToPresent();
#if test
            Console.WriteLine();
            Console.WriteLine($"Variant 1: " + result[0]);
            Console.WriteLine($"Variant 3: " + result[1]);
#endif
#endif
            #endregion

            // delay
            Console.ReadKey(true);
        }

        /// <summary>
        /// Create test file
        /// </summary>
        private static void CreateTestFile()
        {
            // Probably user will change above string or delete, or user watn use other file
            string test = "The “C# Professional” course includes the topics " +
                "I discuss in my CLR via C# book and teaches how the CLR works " +
                "thereby showing you how to develop applications and reusable " +
                "components for the .NET Framework.";

            string filePath = "TestFile.txt";

            try
            {
                using (FileStream stream = new FileStream(filePath,
                    FileMode.OpenOrCreate, FileAccess.Write, FileShare.None))
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(test);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            // open file
            Process.Start(".");
        }

        /// <summary>
        /// Analysis *.txt file
        /// </summary>
        private static void AnalysisFile(string pathFile)
        {
            // guard operator
            if (Path.GetExtension(pathFile) != ".txt")
            {
                return;
            }

            // message
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nInput is *.txt file.\n");
            Console.ResetColor();
            try
            {
                using (FileStream stream = new FileStream(pathFile,
                    FileMode.Open, FileAccess.Read, FileShare.None))
                using (StreamReader reader = new StreamReader(stream))
                {
                    // read to end
                    string data = reader.ReadToEnd();
                    // analysis the data
                    //TextAnalysis.GetGroupOfWords0(data).ToPresent();
                    // or
                    TextAnalysis.GetGroupOfWords1(data).ToPresent();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }
}
