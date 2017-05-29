using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewLangGeneration
{
    class smartReader
    {
        private char[] endOfString;

        private System.IO.StreamReader file;
        private int pointerToRead;
        private int pointerToWrite;
        const int max_len = 100;
        private int credit;
        private bool start;

        public smartReader(string path)
        {
            file = new System.IO.StreamReader(path);
            credit = 0;
            pointerToRead = 0;
            start = true;
            pointerToWrite = 0;
            endOfString = new char[max_len];
        }
        private void readForward()
        {
            while (credit<=22)
            {
                char t;
                if (!file.EndOfStream)
                {
                    t = (char)(file.Read());
                    if (Char.IsLetter(t))
                    {
                        t = Char.ToLower(t);
                    }
                }
                else
                {
                    t = (char)(0);
                }
                endOfString[pointerToWrite] = t;
                pointerToWrite++;
                pointerToWrite %= max_len;
                credit++;
            }
        }
        public string nextChar()
        {
            if (credit == 0)
            {
                char t;
                if ((t = (char)(file.Read())) != -1)
                {
                    if (Char.IsLetter(t))
                    {
                        t = Char.ToLower(t);
                    }
                    endOfString[pointerToWrite] = t;
                    pointerToWrite++;
                    pointerToWrite %= max_len;
                }
                else
                {
                    return null;
                }
                
            }
            else
            {
                credit--;
            }
           
            char currentStart = endOfString[pointerToRead];
            if (currentStart == (char)65535)
            {
                return null;
            }
            pointerToRead++;
            pointerToRead %= max_len;
            if (!Char.IsLetter(currentStart) || start)
            {
                
                readForward();
                var wordToReturn = new List<char>();
                int i = pointerToRead;
                if (start)
                    i--;
                start = false;
                while (wordToReturn.Count != 20)
                {
                    wordToReturn.Add(endOfString[i]);
                    i++;
                    i %= max_len;
                }
                return string.Join("", wordToReturn.ToArray());
            }
            return "";
        }

    }
}
