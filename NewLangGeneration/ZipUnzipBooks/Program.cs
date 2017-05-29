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
            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(@"E:\\books");
            System.IO.FileInfo[] files = di.GetFiles("*.zip");
            List<Thread> threads = new List<Thread>();
            foreach (var x in files)
            {
                Thread t = new Thread(unzip);
                threads.Add(t);
                t.Start(x.FullName);

            }
            foreach (var x in threads)
            {
                x.Join();
            }
        }



        private static void unzip(object fileName)
        {
            string name = (string)fileName;
            try
            {
                ZipFile.ExtractToDirectory(name, "E:\\books\\texts");
            }
            catch
            {

            }
            string pureName = Path.GetFileNameWithoutExtension(name);
            if (Directory.Exists("E:\\books\\texts\\" + pureName))
            {
                // This path is a directory
                try
                {
                    File.Copy("E:\\books\\texts\\" + pureName + "\\" + pureName + ".txt", "E:\\books\\texts\\" + pureName + ".txt");
                }
                catch
                {

                }
                try
                {
                    
                    Directory.Delete("E:\\books\\texts\\" + pureName, true);

                }
                catch
                {

                }
            }
            return;
        }
    }
}
