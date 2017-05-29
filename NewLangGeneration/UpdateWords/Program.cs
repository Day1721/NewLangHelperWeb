using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpdateWords
{
    class Program
    {
        static bool isCapital(char c)
        {
            return (c >= 'A' && c <= 'Z');
        }

        static void Main(string[] args)
        {
            System.IO.StreamReader file =
                new System.IO.StreamReader("E:\\words_0.txt");
            System.IO.StreamWriter result = new System.IO.StreamWriter("E:\\words.txt");
            string line;
            while ((line = file.ReadLine()) != null)
            {
                
                if (line!="" && !isCapital(line[0]))
                {
                    result.WriteLine(line);
                    result.WriteLine("0");
                }
            }
            file.Close();
            result.Close();
        }
    }
}
