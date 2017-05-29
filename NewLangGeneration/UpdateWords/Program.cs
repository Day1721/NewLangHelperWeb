using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace UpdateWords
{
    class Program
    {

        static void Main(string[] args)
        {
            var file =
                new StreamReader("..\\..\\..\\words_0.txt");
            var result = new StreamWriter("..\\..\\..\\words.txt");
            string line;
            while ((line = file.ReadLine()) != null)
            {
                
                if (line!="" && !Char.IsUpper(line[0]))
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
