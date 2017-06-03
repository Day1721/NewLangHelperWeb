using System;
using System.Collections.Generic;
using System.Threading;

namespace NewLangGeneration
{
    class Trie
    {
        private readonly Node _parentNode;
        
        public Trie()
        {
            _parentNode = new Node('\0');
        }
        public void AddWord(string x,long current)
        {
            _parentNode.AddWord(x, 0,current);
        }
        public void AnalyzeString(string x)
        {
            _parentNode.AnalyzeString(x, 0);
        }
        public Dictionary<string,long> GetStatistics()
        {
            var x = _parentNode.GetReversedEndings();
            var result = new Dictionary<string, long>();
            foreach (var i in x)
            {
                //i.Key.RemoveAt((i.Key.Count - 1));
                i.Key.Reverse();
                
                string key = string.Join("", i.Key.ToArray());
                result[key] = i.Value;
            }
            return result;
        }
    }


    class Node
    {
        const int MaxSize = 15;
        private long _value;
        private readonly List<Node> _listPointers;
        private readonly Dictionary<char,Node> _arrayPointers;
        private int _size;
        private static Mutex _mut;
        private bool _isWord;
        public bool Word => _isWord;
        public char Letter { get; }
        public long Value => _value;

        public Node(char letter)
        {
            Letter = letter;
            _value = 0;
            _mut = new Mutex();
            _size = 0;
            _listPointers = new List<Node>();
            _arrayPointers = new Dictionary<char, Node>();
        }

        public void AddWord(string x,int pos,long current)
        {
            if (pos == x.Length)
            {
                _isWord = true;
                _value += current;
                return;
            }
            char now = x[pos];
            Node nxt = GetChild(now);
            if (nxt == null)
            {
                nxt = CreateChild(now);
            }
            nxt.AddWord(x, pos + 1,current);
        }

        public void AnalyzeString(string x, int pos)
        {
            if (_isWord && (pos == x.Length || !Char.IsLetter(x[pos])))
            {
                _mut.WaitOne();
                _value++;
                _mut.ReleaseMutex();
            }
            if (pos == x.Length)
            {
                return;
            }
            char now = x[pos];
            Node nxt = GetChild(now);
            if (nxt != null)
            {
                nxt.AnalyzeString(x, pos + 1);
            }
        }

        private Node CreateChild(char x)
        {
            Node toAdd = new Node(x);
            if (_size >= MaxSize)
            {
                foreach (var i in _listPointers)
                {
                    _arrayPointers[i.Letter] = i;
                }
                _listPointers.Clear();
                _arrayPointers[x] = toAdd;
            }
            else
            {
                _listPointers.Add(toAdd);
            }
            _size++;
            return toAdd;
        }

        private Node GetChild(char x)
        {
            if (_size >= MaxSize)
            {
                if (_arrayPointers.ContainsKey(x))
                {
                    return _arrayPointers[x];
                }
                else
                {
                    return null;
                }
            }
            else
            {
                foreach (var i in _listPointers)
                {
                    if (i.Letter == x)
                    {
                        return i;
                    }
                }
            }
            return null;
        }


        public List < KeyValuePair <List <char> , long> > GetReversedEndings()
        {
            var result = new List<KeyValuePair< List<char> , long>>();
            foreach(var i in _listPointers)
            {
                var toAdd = i.GetReversedEndings();
                foreach (var u in toAdd)
                {
                    u.Key.Add(i.Letter);
                    result.Add(u);
                }
            }
            foreach (var i in _arrayPointers)
            {
                var toAdd = i.Value.GetReversedEndings();
                foreach (var u in toAdd)
                {
                    u.Key.Add(i.Value.Letter);
                    result.Add(u);
                }
            }
            if (_isWord)
            {
                result.Add(new KeyValuePair<List<char>, long>(new List<char>(), _value));
            }
            return result;
        }



    }
}
