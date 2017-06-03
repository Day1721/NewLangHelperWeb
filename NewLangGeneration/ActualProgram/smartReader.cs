using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewLangGeneration
{
    class SmartReader
    {
        private readonly char[] _endOfString;

        private readonly System.IO.StreamReader _file;
        private int pointerToRead;
        private int pointerToWrite;
        const int max_len = 100;
        private int credit;
        private bool start;

        public SmartReader(string path)
        {
            _file = new System.IO.StreamReader(path);
            credit = 0;
            pointerToRead = 0;
            start = true;
            pointerToWrite = 0;
            _endOfString = new char[max_len];
        }
        private void readForward()
        {
            while (credit<=22)
            {
                char t;
                if (!_file.EndOfStream)
                {
                    t = (char)(_file.Read());
                    if (Char.IsLetter(t))
                    {
                        t = Char.ToLower(t);
                    }
                }
                else
                {
                    t = (char)(0);
                }
                _endOfString[pointerToWrite] = t;
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
                if ((t = (char)(_file.Read())) != -1)
                {
                    if (Char.IsLetter(t))
                    {
                        t = Char.ToLower(t);
                    }
                    _endOfString[pointerToWrite] = t;
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
           
            char currentStart = _endOfString[pointerToRead];
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
                    wordToReturn.Add(_endOfString[i]);
                    i++;
                    i %= max_len;
                }
                return string.Join("", wordToReturn.ToArray());
            }
            return "";
        }

    }
}
