using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO.Compression;
using System.IO;
namespace ZipUnzipBooks
{
    class Program
    {
        static void Main(string[] args)
        {
            var di = new DirectoryInfo(@"..\..\..\books");
            var files = di.GetFiles("*.zip");
            Parallel.ForEach(files, Unzip);

        }



        private static void Unzip(object fileName)
        {
            string name = (string)fileName;
            try
            {
                ZipFile.ExtractToDirectory(name, "..\\..\\..\\books\\texts");
            }
            catch
            {

            }
            string pureName = Path.GetFileNameWithoutExtension(name);
            if (Directory.Exists("..\\..\\..\\books\\texts\\" + pureName))
            {
                // This path is a directory
                try
                {
                    File.Copy("..\\..\\..\\books\\texts\\" + pureName + "\\" + pureName + ".txt", "..\\..\\..\\books\\texts\\" + pureName + ".txt");
                }
                catch
                {

                }
                try
                {
                    
                    Directory.Delete("..\\..\\..\\books\\texts\\" + pureName, true);

                }
                catch
                {

                }
            }
            return;
        }
    }
}
