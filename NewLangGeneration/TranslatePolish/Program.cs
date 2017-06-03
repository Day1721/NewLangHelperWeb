using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Google.Cloud.Translation.V2;

namespace TranslatePolish
{
    static class Program
    {
        private static string Translate(this TranslationClient client, string text) =>
            client.TranslateText(text, "pl", "en").TranslatedText;

        static void Main()
        {
            var client = TranslationClient.Create();

            var input = File.ReadAllLines("words.txt");
            var res = input.Select(client.Translate);
            File.WriteAllLines("polish.txt", res);

            /*
            var file =
                new StreamReader("..\\..\\..\\words.txt");
            var result = new StreamWriter("..\\..\\..\\polish.txt");
            string line;
            while ((line = file.ReadLine()) != null)
            {
                var response = client.TranslateText(line,"pl", "en");
                result.WriteLine(response.TranslatedText);
                //Console.WriteLine(response.TranslatedText);
                line = file.ReadLine();

            }
            result.Close();*/
        }
    }
}
