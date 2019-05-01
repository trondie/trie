using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
namespace SuffixTrie
{
    class Matcher
    {
        SuffixTree suffixTree;
        Hashtable distribution = new Hashtable();
        const int FRAGMENT_SIZE = 50;

        public Matcher(string filename, string adapter)
        {
            suffixTree = new SuffixTree(adapter);
            string[] lines = System.IO.File.ReadAllLines(filename);
            int matches = 0;
            for (int i = 0; i <= FRAGMENT_SIZE; ++i)
            {
                distribution[i] = 0;
            }
            for (int i = 0; i < lines.Length; ++i)
            {
                string line = ReverseString(lines[i]);
                string longestMatch = suffixTree.longestExactMatch(line);
                if (longestMatch.Length > 0)
                {
                    int index = FRAGMENT_SIZE - longestMatch.Length;
                    distribution[index] = (int)distribution[index] + 1;
                    matches++;
                }
            }
            distribution[FRAGMENT_SIZE] = lines.Length - matches;
            printResults();
        }

        public void printResults()
        {
            int totalMatches = -(int)distribution[FRAGMENT_SIZE];
            Console.WriteLine("Distribution : ");
            foreach (DictionaryEntry entry in distribution)
            {
                Console.WriteLine(entry.Key + " : " + entry.Value);
                totalMatches += (int)entry.Value;
            }
            Console.WriteLine("\nTotal matches : " + totalMatches);
            Console.WriteLine("End");
        }
        public static string ReverseString(string s)
        {
            char[] arr = s.ToCharArray();
            Array.Reverse(arr);
            return new string(arr);
        }
    }
}
