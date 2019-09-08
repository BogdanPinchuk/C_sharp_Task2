using System;
using System.Collections.Generic;
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

            char[] array = new[]
            {
                'a', 'b', 'c', 'd', 'e', 'f', 'g'
            };

            string s = new string(array);

            string testStr = "The “C# Professional” course includes the topics " +
                "I discuss in my CLR via C# book and teaches how the CLR works " +
                "thereby showing you how to develop applications and reusable " +
                "components for the.NET Framework.";

            // delay
            Console.ReadKey(true);
        }
    }
}
