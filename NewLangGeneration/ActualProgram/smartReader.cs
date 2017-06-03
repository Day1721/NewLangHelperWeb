using System.Collections.Generic;

namespace NewLangGeneration
{
    class SmartReader
    {
        private readonly char[] _endOfString;

        private readonly System.IO.StreamReader _file;
        private int _pointerToRead;
        private int _pointerToWrite;
        private const int MaxLen = 100;
        private int _credit;
        private bool _start;

        public SmartReader(string path)
        {
            _file = new System.IO.StreamReader(path);
            _credit = 0;
            _pointerToRead = 0;
            _start = true;
            _pointerToWrite = 0;
            _endOfString = new char[MaxLen];
        }

        private void ReadForward()
        {
            while (_credit<=22)
            {
                char t;
                if (!_file.EndOfStream)
                {
                    t = (char)_file.Read();
                    if (char.IsLetter(t))
                    {
                        t = char.ToLower(t);
                    }
                }
                else
                {
                    t = (char)0;
                }
                _endOfString[_pointerToWrite] = t;
                _pointerToWrite++;
                _pointerToWrite %= MaxLen;
                _credit++;
            }
        }

        public string NextChar()
        {
            if (_credit == 0)
            {
                char t;
                if ((t = (char)_file.Read()) != -1)
                {
                    if (char.IsLetter(t))
                    {
                        t = char.ToLower(t);
                    }
                    _endOfString[_pointerToWrite] = t;
                    _pointerToWrite++;
                    _pointerToWrite %= MaxLen;
                }
                else
                {
                    return null;
                }
                
            }
            else
            {
                _credit--;
            }
           
            char currentStart = _endOfString[_pointerToRead];
            if (currentStart == (char)65535 || currentStart == (char)0)
            {
                return null;
            }
            _pointerToRead++;
            _pointerToRead %= MaxLen;
            if (!char.IsLetter(currentStart) || _start)
            {
                
                ReadForward();
                var wordToReturn = new List<char>();
                int i = _pointerToRead;
                if (_start)
                    i--;
                _start = false;
                while (wordToReturn.Count != 20)
                {
                    wordToReturn.Add(_endOfString[i]);
                    i++;
                    i %= MaxLen;
                }
                return string.Join("", wordToReturn.ToArray());
            }
            return "";
        }

        public void Close() => _file.Close();
    }
}
