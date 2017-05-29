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
using java.io;
using edu.stanford.nlp.pipeline;
using Console = System.Console;
using Newtonsoft.Json;

namespace NewLangGeneration
{

    public partial class Form1 : Form
    {
        Trie words = new Trie();

        private System.Object lockThis = new System.Object();

        public Form1()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {

            Environment.Exit(0);
        }



        private void btnClear_Click(object sender, EventArgs e)
        {
            lbx.Items.Clear();
        }



        private void btnRead_Click(object sender, EventArgs e)
        {
            DialogResult dialog = openFileDialog1.ShowDialog();

            if (dialog == DialogResult.OK)
            {
                string fileName = openFileDialog1.FileName;


                try
                {
                    if (Directory.Exists("..\\..\\..\\data_to_parse"))
                    {
                        Directory.Delete("..\\..\\..\\data_to_parse",true);
                    }
                    ZipFile.ExtractToDirectory(fileName, "..\\..\\..\\data_to_parse");
                    string parsePath = "..\\..\\..\\data_to_parse";
                    System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(parsePath);
                    System.IO.FileInfo[] files = di.GetFiles("*.txt");
                    foreach (var x in files)
                    {
                        Thread th = new Thread(thread_DoWork);
                        th.Start(x.FullName);
                        //backgroundWorker1.RunWorkerAsync(x.FullName);
                    }

                }
                catch
                {
                    lbx.Items.Add("Wrong file");
                }


            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dialog = saveFileDialog1.ShowDialog();

            if (dialog == DialogResult.OK)
            {
                string fileName = saveFileDialog1.FileName;
                var fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
                byte[] byteText = System.Text.Encoding.ASCII.GetBytes(txtFile.Text);
                fs.Write(byteText, 0, byteText.Length);
                fs.Close();
            }


        }



        private void button2_Click_1(object sender, EventArgs e)
        {
            var file =
                new System.IO.StreamReader("..\\..\\..\\words.txt");
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
                words.addWord(line, num);
            }
            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);
            lbx.Items.Add("Read finished " + elapsedTime + " lines read: " + xx);
            file.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var file =
                new System.IO.StreamWriter("..\\..\\..\\words.txt");
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            long xx = 0;
            var toSave = words.getStatistics();

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
            lbx.Items.Add("Read finished " + elapsedTime + " lines wrote: " + xx*2);
            file.Close();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            string fileName = (string)e.Argument;
            smartReader reader = new smartReader(fileName);
            string nxt;
            while ((nxt = reader.nextChar()) != null)
            {
                words.analyzeString(nxt);
            }

        }

        private void thread_DoWork(object fileNameO)
        {
            string fileName = (string)fileNameO;
            smartReader reader = new smartReader(fileName);
            string nxt;
            while ((nxt = reader.nextChar()) != null)
            {
                words.analyzeString(nxt);
            }
        }



        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            lbx.Items.Add("finished ");
        }
        class Translation
        {
            public string eng;
            public string pol;
            public Translation(string en,string pl)
            {
                eng = en;
                pol = pl;
            }

        }
        private void button4_Click(object sender, EventArgs e)
        {
            var file = new StreamWriter("..\\..\\..\\top100.json");
            var poland = new StreamReader("..\\..\\..\\polish.txt");
            var translation = new Dictionary<string, string>();
            string eng, pol;
            while ((eng=poland.ReadLine())!=null)
            {
                pol = poland.ReadLine();
                translation[eng] = pol;
            }
            var wordsFile = new StreamReader("..\\..\\..\\words.txt");
            //list.Sort((x, y) => y.Item1.CompareTo(x.Item1));
            var result = new List<Tuple<string, long>>();
            string lineWord, lineNum;
            
            while ((lineWord=wordsFile.ReadLine())!=null)
            {
                lineNum = wordsFile.ReadLine();
                long key = Int64.Parse(lineNum);
                result.Add(new Tuple<string, long>(lineWord, key));
            }
            result.Sort((x, y) => y.Item2.CompareTo(x.Item2));
            //result.Reverse();
            var resultToJSON = new List<Translation>();
            for (int i=0;i<100;i++)
            {
                resultToJSON.Add(new Translation(result[i].Item1, translation[result[i].Item1]));
            }
            file.WriteLine(JsonConvert.SerializeObject(resultToJSON));
            file.Close();
            lbx.Items.Add("top100 finished");
            file = new StreamWriter("..\\..\\..\\top1000.json");
            for (int i = 10; i < 1000; i++)
            {
                resultToJSON.Add(new Translation(result[i].Item1, translation[result[i].Item1]));

            }
            file.WriteLine(JsonConvert.SerializeObject(resultToJSON));
            file.Close();
            lbx.Items.Add("top1000 finished");
            file = new StreamWriter("..\\..\\..\\top10000.json");
            for (int i = 1000; i < 10000; i++)
            {
                resultToJSON.Add(new Translation(result[i].Item1, translation[result[i].Item1]));

            }
            file.WriteLine(JsonConvert.SerializeObject(resultToJSON));
            file.Close();
            lbx.Items.Add("top10000 finished");
        }
    }





}
