using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fixTranslation
{
    class Program
    {
        static void Main(string[] args)
        {
            var file =
                new StreamReader("..\\..\\..\\words.txt");
            var file2 =
                new StreamReader("..\\..\\..\\polish.txt");
            var result = new StreamWriter("..\\..\\..\\polish_fix.txt");
            var result2 = new StreamWriter("..\\..\\..\\words_fix.txt");
            string line;
            string line2;
            while ((line=file.ReadLine())!=null)
            {
                line2 = file2.ReadLine();
                string line3 = file.ReadLine();
                
                if (line2!="")
                {
                    result.WriteLine(line);
                    result.WriteLine(line2);
                    result2.WriteLine(line);
                    result2.WriteLine(line3);
                }
            }
            file.Close();
            file2.Close();
            result.Close();
            result2.Close();
            File.Delete("..\\..\\..\\words.txt");
            File.Delete("..\\..\\..\\polish.txt");
            File.Copy("..\\..\\..\\words_fix.txt", "..\\..\\..\\words.txt");
            File.Copy("..\\..\\..\\polish_fix.txt", "..\\..\\..\\polish.txt");

        }
    }
}
