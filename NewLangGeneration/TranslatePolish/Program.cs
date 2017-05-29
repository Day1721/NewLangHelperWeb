using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Xml;
using System.IO;
using Google.Cloud.Translation.V2;

namespace TranslatePolish
{
    class Program
    {
        static void Main(string[] args)
        {
             //Console.OutputEncoding = System.Text.Encoding.Unicode;
             TranslationClient client = TranslationClient.Create();
             //var response = client.TranslateText("Hello World.", "ru");
             //Console.WriteLine(response.TranslatedText);*/
            System.IO.StreamReader file =
                new System.IO.StreamReader("E:\\words.txt");
            System.IO.StreamWriter result = new System.IO.StreamWriter("E:\\polish.txt");
            string line;
            while ((line = file.ReadLine()) != null)
            {
                var response = client.TranslateText(line,"pl", "en");
                result.WriteLine(response.TranslatedText);
                //Console.WriteLine(response.TranslatedText);*/
                line = file.ReadLine();

            }
            result.Close();

        }
    }
}
