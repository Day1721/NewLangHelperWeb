using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpdateWords
{
    class Program
    {
        private const string InputFileName = "words_0.txt";
        private const string OutputFileName = "words.txt";

        static void Main()
        {
            var input = File.ReadAllLines(InputFileName);

            var res = input.Aggregate(string.Empty, (acc, line) =>
            {
                if (line != string.Empty && !char.IsUpper(line[0]))
                {
                    acc += $"{line}\n";
                }
                return acc;
            });

            File.WriteAllText(OutputFileName, res);
            /*
            var file =
                new StreamReader("..\\..\\..\\words_0.txt");
            var result = new StreamWriter("..\\..\\..\\words.txt");
            string line;
            while ((line = file.ReadLine()) != null)
            {

                if (line != string.Empty && !char.IsUpper(line[0]))
                {
                    result.WriteLine(line);
                    result.WriteLine("0");
                }
            }

            file.Close();
            result.Close();
            */
        }
    }
}
