using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace SuffixTrie
{
    class ApproxMatcher
    {
        private string adapter;
        private double percentage;
        const int FRAGMENT_SIZE = 50;
        Hashtable distribution = new Hashtable();
        public ApproxMatcher(string adapter, double percentage)
        {
            this.adapter = adapter;
            this.percentage = percentage;
            this.percentage /= 100.0;
        }
        public ApproxMatcher(string filename, string adapter, double percentage)
        {
            this.percentage = percentage;
            this.percentage /= 100.0;
            this.adapter = adapter;

            string[] lines = System.IO.File.ReadAllLines(filename);
            int matches = 0;
            for (int i = 0; i <= FRAGMENT_SIZE; ++i)
            {
                distribution[i] = 0;
            }

            for (int i = 0; i < lines.Length; ++i)
            {
                string line = ReverseString(lines[i]);
                int matchLength = longestMatch(line);
               // Console.WriteLine(matchLength);

                if (matchLength > 0)
                {
                    matches++;
                    int index = FRAGMENT_SIZE - matchLength;
                    distribution[index] = (int)distribution[index] + 1;
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
        public int longestMatch(string pattern)
        {
            int result = 0;
            for (int i = pattern.Length - 1; i >= 0; --i)
            {
                if (match(i, ref pattern))
                {
                    if (i >= result)
                    {
                        result = (i + 1);
                        break;
                    }
                }
            }
            return result;

        }
        private bool match(int subPatternEndIndex, ref string pattern)
        {

            if (subPatternEndIndex == 0)
            {
                int dummy = 0;
            }
            int k = (int)((double)(subPatternEndIndex + 1) * percentage);
            int mismatches = 0;
            if (subPatternEndIndex == 62)
            {
                int dummy = 0;
            }
            for (int i = 0; i <= subPatternEndIndex; ++i)
            {
                if (pattern[i] != adapter[adapter.Length - 1 - subPatternEndIndex + i])
                {
                    if (++mismatches == (k+1))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public static string ReverseString(string s)
        {
            char[] arr = s.ToCharArray();
            Array.Reverse(arr);
            return new string(arr);
        }
    }
}
