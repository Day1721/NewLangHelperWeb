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
            var file =
                new StreamReader("..\\..\\..\\words.txt");
            var result = new StreamWriter("..\\..\\..\\polish.txt");
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
