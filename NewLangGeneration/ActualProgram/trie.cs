using System;
using System.Collections.Generic;
using System.Threading;

namespace NewLangGeneration
{
    class Trie
    {
        private readonly Node parentNode;
        
        public Trie()
        {
            parentNode = new Node((char)(0));
        }
        public void addWord(string x,long current)
        {
            parentNode.addWord(x, 0,current);
        }
        public void analyzeString(string x)
        {
            parentNode.analyzeString(x, 0);
        }
        public Dictionary<string,long> getStatistics()
        {
            var x = parentNode.getReversedEndings();
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
        const int maxSize = 15;
        private readonly char letter;
        private long value;
        private List<Node> listPointers;
        private Dictionary<char,Node> arrayPointers;
        private int size;
        private static Mutex mut;
        private bool isWord;
        public bool Word {  get { return isWord; } }
        public char Letter { get { return letter; } }
        public long Value { get { return value; } }
        public Node(char letter)
        {
            this.letter = letter;
            value = 0;
            mut = new Mutex();
            size = 0;
            listPointers = new List<Node>();
            arrayPointers = new Dictionary<char, Node>();
        }

        public void addWord(string x,int pos,long current)
        {
            if (pos == x.Length)
            {
                isWord = true;
                value += current;
                return;
            }
            char now = x[pos];
            Node nxt = getChild(now);
            if (nxt == null)
            {
                nxt = createChild(now);
            }
            nxt.addWord(x, pos + 1,current);
        }

        public void analyzeString(string x, int pos)
        {
            if (isWord && (pos == x.Length || !Char.IsLetter(x[pos])))
            {
                mut.WaitOne();
                value++;
                mut.ReleaseMutex();
            }
            if (pos == x.Length)
            {
                return;
            }
            char now = x[pos];
            Node nxt = getChild(now);
            if (nxt != null)
            {
                nxt.analyzeString(x, pos + 1);
            }
        }

        private Node createChild(char x)
        {
            Node toAdd = new Node(x);
            if (size >= maxSize)
            {
                foreach (var i in listPointers)
                {
                    arrayPointers[i.Letter] = i;
                }
                listPointers.Clear();
                arrayPointers[x] = toAdd;
            }
            else
            {
                listPointers.Add(toAdd);
            }
            size++;
            return toAdd;
        }

        private Node getChild(char x)
        {
            if (size >= maxSize)
            {
                if (arrayPointers.ContainsKey(x))
                {
                    return arrayPointers[x];
                }
                else
                {
                    return null;
                }
            }
            else
            {
                foreach (var i in listPointers)
                {
                    if (i.Letter == x)
                    {
                        return i;
                    }
                }
            }
            return null;
        }


        public List < KeyValuePair <List <char> , long> > getReversedEndings()
        {
            var result = new List<KeyValuePair< List<char> , long>>();
            foreach(var i in listPointers)
            {
                var toAdd = i.getReversedEndings();
                foreach (var u in toAdd)
                {
                    u.Key.Add(i.Letter);
                    result.Add(u);
                }
            }
            foreach (var i in arrayPointers)
            {
                var toAdd = i.Value.getReversedEndings();
                foreach (var u in toAdd)
                {
                    u.Key.Add(i.Value.Letter);
                    result.Add(u);
                }
            }
            if (isWord)
            {
                result.Add(new KeyValuePair<List<char>, long>(new List<char>(), value));
            }
            return result;
        }



    }
}
