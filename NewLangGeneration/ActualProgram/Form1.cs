using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using java.util;
using System.Timers;
using System.Diagnostics;
using System.Threading;
using edu.stanford.nlp.pipeline;
using Console = System.Console;
using Newtonsoft.Json;
using Google.Cloud.Translation.V2;
using System.Threading.Tasks;

namespace NewLangGeneration
{

    public partial class Form1 : Form
    {
        Trie words = new Trie();
        const string BookPath = "books\\texts";
        private string currentLanguage = "Polish";
        private System.Object lockThis = new System.Object();

        public Form1()
        {
            InitializeComponent();
            if (!File.Exists("words.txt"))
            {
                createWords();
            }
            wordsInit();
        }

        private void createWords()
        {
            string InputFileName = "words_0.txt";
            string OutputFileName = "words.txt";

            var input = File.ReadAllLines(InputFileName);

            var res = input.Aggregate(string.Empty, (acc, line) =>
            {
                if (line != string.Empty && !char.IsUpper(line[0]))
                {
                    acc += $"{line}\n";
                    acc += $"0\n";
                }
                return acc;
            });

            File.WriteAllText(OutputFileName, res);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {

            Environment.Exit(0);
        }



        private void btnClear_Click(object sender, EventArgs e)
        {
            lbx.Items.Clear();
        }
        private void wordsInit()
        {
            object buf = new object();
            EventArgs e = new EventArgs();
            wordsInit(buf, e);
        }
        private void wordsInit(object sender, EventArgs e)
        {
            var file =
                new System.IO.StreamReader("words.txt");
            string line;
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            long xx = 0;
            while ((line = file.ReadLine()) != null)
            {
                line = line.ToLower();
                xx++;
                string line2 = file.ReadLine();
                long num = Int64.Parse(line2);
                words.AddWord(line, num);
            }
            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);
            lbx.Items.Add("Read finished " + elapsedTime + " lines read: " + xx);
            file.Close();
        }


        private void btnRead_Click(object sender, EventArgs e)
        {
            DialogResult dialog = openFileDialog1.ShowDialog();

            if (dialog == DialogResult.OK)
            {
                string fileName = openFileDialog1.FileName;


                try
                {
                    if (Directory.Exists("data_to_parse"))
                    {
                        Directory.Delete("data_to_parse", true);
                    }
                    ZipFile.ExtractToDirectory(fileName, "data_to_parse");
                    string parsePath = "data_to_parse";
                    System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(parsePath);
                    System.IO.FileInfo[] files = di.GetFiles("*.txt");
                    var names = new List<Object>();
                    
                    foreach(var x in files)
                    {
                        names.Add(x.FullName);
                    }
                    foreach (var x in names)
                    {
                        thread_DoWork(x);
                    }

                    //Parallel.ForEach(names, thread_DoWork);
                    lbx.Items.Add("analyzing finished");
                    Directory.Delete("data_to_parse", true);
                    

                }
                catch
                {
                    lbx.Items.Add("Wrong file");
                }


            }

        }


        private void button3_Click(object sender, EventArgs e)
        {
            var file =
                new System.IO.StreamWriter("words.txt");
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            long xx = 0;
            var toSave = words.GetStatistics();

            foreach (var x in toSave)
            {
                file.WriteLine(x.Key);
                file.WriteLine(x.Value);
                xx++;
            }
            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);
            lbx.Items.Add("Read finished " + elapsedTime + " lines wrote: " + xx * 2);
            file.Close();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            string fileName = (string)e.Argument;
            SmartReader reader = new SmartReader(fileName);
            string nxt;
            while ((nxt = reader.nextChar()) != null)
            {
                words.AnalyzeString(nxt);
            }
            

        }

        private void thread_DoWork(object fileNameO)
        {
            string fileName = (string)fileNameO;
            SmartReader reader = new SmartReader(fileName);
            string nxt;
            while ((nxt = reader.nextChar()) != null)
            {
                words.AnalyzeString(nxt);
            }
            reader.Close();
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            lbx.Items.Add("finished ");
        }
        class Translation
        {
            public string eng;
            public string pol;
            public Translation(string en, string pl)
            {
                eng = en;
                pol = pl;
            }

        }
        private void button4_Click(object sender, EventArgs e)
        {
            var file = new StreamWriter("top100.json");
            var poland = new StreamReader("polish.txt");
            var translation = new Dictionary<string, string>();
            string eng, pol;
            while ((eng = poland.ReadLine()) != null)
            {
                pol = poland.ReadLine();
                translation[eng] = pol;
            }
            var wordsFile = new StreamReader("words.txt");
            var result = new List<Tuple<string, long>>();
            string lineWord, lineNum;

            while ((lineWord = wordsFile.ReadLine()) != null)
            {
                lineNum = wordsFile.ReadLine();
                long key = Int64.Parse(lineNum);
                result.Add(new Tuple<string, long>(lineWord, key));
            }
            result.Sort((x, y) => y.Item2.CompareTo(x.Item2));
            var resultToJSON = new List<Translation>();
            for (int i = 0; i < 100; i++)
            {
                resultToJSON.Add(new Translation(result[i].Item1, translation[result[i].Item1]));
            }
            file.WriteLine(JsonConvert.SerializeObject(resultToJSON));
            file.Close();
            lbx.Items.Add("top100 finished");
            file = new StreamWriter("top1000.json");
            for (int i = 10; i < 1000; i++)
            {
                resultToJSON.Add(new Translation(result[i].Item1, translation[result[i].Item1]));

            }
            file.WriteLine(JsonConvert.SerializeObject(resultToJSON));
            file.Close();
            lbx.Items.Add("top1000 finished");
            file = new StreamWriter("top10000.json");
            for (int i = 1000; i < 10000; i++)
            {
                resultToJSON.Add(new Translation(result[i].Item1, translation[result[i].Item1]));

            }
            file.WriteLine(JsonConvert.SerializeObject(resultToJSON));
            file.Close();
            lbx.Items.Add("top10000 finished");
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentLanguage = (string)listBox1.SelectedItem;
        }

        private void buttonTranslate_Click(object sender, EventArgs e)
        {
            string lang = "pl";
            string fileName = currentLanguage.ToLower() + ".txt";
            switch (currentLanguage)
            {
                case "Polish":
                    lang = "pl";
                    break;
                case "Russian":
                    lang = "ru";
                    break;
                default:
                    lang = "pl";
                    break;
            }

            var client = TranslationClient.Create();
            var file = new StreamReader("words.txt");
            var revisedFile = new StreamWriter("revised_words.txt");
            var result = new StreamWriter(fileName);
            string line;
            while ((line = file.ReadLine()) != null)
            {
                var response = client.TranslateText(line, lang, "en");

                result.WriteLine(response.TranslatedText);
                //Console.WriteLine(response.TranslatedText);
                string count = file.ReadLine();
                var translation = response.TranslatedText;
                if (translation != string.Empty)
                {
                    revisedFile.WriteLine(line);
                    revisedFile.WriteLine(count);
                    result.WriteLine(line);
                    result.WriteLine(translation);
                }


            }
            result.Close();
            revisedFile.Close();
            file.Close();
            File.Delete("words.txt");
            File.Copy("revised_words.txt", "words.txt");
            File.Delete("revised_words");

        }

        private void button5_Click(object sender, EventArgs e)
        {

            var files = Directory.GetFiles("books", "*.zip");
            Directory.Delete(BookPath, true);
            Directory.CreateDirectory(BookPath);
            Parallel.ForEach(files, Unzip);
            if (File.Exists("archive.zip"))
            {
                File.Delete("archive.zip");
            }
            ZipFile.CreateFromDirectory(BookPath, "archive.zip");
            lbx.Items.Add("preparing archive finished");
        }

        private static void Unzip(string filename)
        {
            string x = BookPath + '\\' + Path.GetFileNameWithoutExtension(filename);
            if (Directory.Exists(x))
            {
                Directory.Delete(BookPath + "\\" + Path.GetFileNameWithoutExtension(filename),true);
            }
            try
            {
                ZipFile.ExtractToDirectory(filename, BookPath);
            }
            catch
            {
                //nie wiem co sie dzieje;
            }

            var pureName = Path.GetFileNameWithoutExtension(filename);
            var dirName = Path.Combine(BookPath, pureName);

            if (Directory.Exists(dirName))
            {
                File.Copy(Path.Combine(dirName, $"{pureName}.txt"),
                    Path.Combine(BookPath, $"{pureName}.txt"));

                Directory.Delete(dirName, true);
            }
        }
    }

    




}
