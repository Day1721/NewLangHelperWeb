using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;
using Newtonsoft.Json;
using Google.Cloud.Translation.V2;
using System.Threading.Tasks;

namespace NewLangGeneration
{

    public partial class Form1 : Form
    {

        readonly Trie _words = new Trie();
        private int wordNumber = 100;
        const string BookPath = @"books\texts";
        private string _currentLanguage = "Polish";
        public Form1()
        {
            InitializeComponent();
            if (!File.Exists("words.txt"))
            {
                CreateWords();
            }
            WordsInit();
        }

        private void CreateWords()
        {
            const string inputFileName = "words_0.txt";
            const string outputFileName = "words.txt";

            var input = File.ReadAllLines(inputFileName);

            var res = input.Aggregate(string.Empty, (acc, line) =>
            {
                if (line != string.Empty && !char.IsUpper(line[0]))
                {
                    acc += $"{line}\n0\n";
                }
                return acc;
            });

            File.WriteAllText(outputFileName, res);
        }

        private void btnExit_Click(object sender, EventArgs e) => Environment.Exit(0);


        private void btnClear_Click(object sender, EventArgs e) => lbx.Items.Clear();

        private void WordsInit() => WordsInit(this, null);

        private void WordsInit(object sender, EventArgs e)
        {
            var file = new StreamReader("words.txt");
            string line;
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            long xx = 0;
            while ((line = file.ReadLine()) != null)
            {
                line = line.ToLower();
                xx++;
                string line2 = file.ReadLine();
                long num = long.Parse(line2);
                _words.AddWord(line, num);
            }
            TimeSpan ts = stopWatch.Elapsed;
            var elapsedTime = string.Format("{0:00}:{1:00}:{2:00}.{3:00}",
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
                    const string parsePath = "data_to_parse";
                    var di = new DirectoryInfo(parsePath);
                    FileInfo[] files = di.GetFiles("*.txt");
                    var names = new List<string>();
                    
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
            var file = new StreamWriter("words.txt");
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            long xx = 0;
            var toSave = _words.GetStatistics();

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



        private void thread_DoWork(string fileName)
        {
            var reader = new SmartReader(fileName);
            string nxt;
            while ((nxt = reader.NextChar()) != null)
            {
                _words.AnalyzeString(nxt);
            }
            reader.Close();
        }
        class Translation
        {
            public string Eng { get; }
            public string Pol { get; }

            public Translation(string en, string pl)
            {
                Eng = en;
                Pol = pl;
            }

        }
        private void button4_Click(object sender, EventArgs e)
        {
            string fileName = "top" + wordNumber.ToString() + ".txt";

            var file = new StreamWriter(fileName);
            string resultName = _currentLanguage.ToLower() + ".txt";
            var dict = new StreamReader(resultName);
            var translation = new Dictionary<string, string>();
            string eng, resultLanguage;
            while ((eng = dict.ReadLine()) != null)
            {
                resultLanguage = dict.ReadLine();
                translation[eng] = resultLanguage;
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
            var resultToJson = new List<Translation>();
            for (int i = 0; i < wordNumber; i++)
            {
                resultToJson.Add(new Translation(result[i].Item1, translation[result[i].Item1]));
            }
            file.WriteLine(JsonConvert.SerializeObject(resultToJson));
            file.Close();
            lbx.Items.Add("top"  + wordNumber.ToString() + "finished ");
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            _currentLanguage = (string)listBox1.SelectedItem;
        }

        private void buttonTranslate_Click(object sender, EventArgs e)
        {
            string lang;
            string fileName = _currentLanguage.ToLower() + ".txt";
            switch (_currentLanguage)
            {
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

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            wordNumber = Int32.Parse((string)listBox2.SelectedItem);
        }
    }
}
