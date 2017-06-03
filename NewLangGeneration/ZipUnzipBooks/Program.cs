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
        private const string MainPath = "books/texts";

        static void Main()
        {
            var files = Directory.GetFiles("books", "*.zip");
            Directory.Delete(MainPath, true);  // remove old trash (IMO needed)
            Directory.CreateDirectory(MainPath);
            Parallel.ForEach(files, Unzip);
        }

        private static void Unzip(string filename)
        {
            ZipFile.ExtractToDirectory(filename, MainPath);

            var pureName = Path.GetFileNameWithoutExtension(filename);
            var dirName = Path.Combine(MainPath, pureName);

            if (Directory.Exists(dirName))
            {
                File.Copy(Path.Combine(dirName, $"{pureName}.txt"), 
                    Path.Combine(MainPath, $"{pureName}.txt"));

                Directory.Delete(dirName, true);
            }
        }
    }
}
