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
            System.IO.StreamReader file =
                new System.IO.StreamReader("E:\\words.txt");
            System.IO.StreamReader file2 =
                new System.IO.StreamReader("E:\\polish.txt");
            System.IO.StreamWriter result = new System.IO.StreamWriter("E:\\polish_fix.txt");
            System.IO.StreamWriter result2 = new System.IO.StreamWriter("E:\\words_fix.txt");
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
            File.Delete("E:\\words.txt");
            File.Delete("E:\\polish.txt");
            File.Copy("E:\\words_fix.txt", "E:\\words.txt");
            File.Copy("E:\\polish_fix.txt", "E:\\polish.txt");

        }
    }
}
