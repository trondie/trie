using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace SuffixTrie
{
    class AdapterSequence
    {
        private string adapter;
        private double percentage;
        const int FRAGMENT_SIZE = 50;
        Hashtable distribution = new Hashtable();
        public AdapterSequence(string adapter, double percentage)
        {
            this.adapter = adapter;
            this.percentage = percentage;
            this.percentage /= 100.0;
        }
        public AdapterSequence(string filename, string adapter, double percentage, int j)
        {
            this.percentage = percentage;
            this.percentage /= 100.0;
            this.adapter = adapter;

            string[] lines = System.IO.File.ReadAllLines(filename);
            int matches = 0;
        
            
            for (int i = 0; i < lines.Length; ++i)
            {
                string line = lines[i];

                // Console.WriteLine(matchLength);
 
                matches++;
                int index = FRAGMENT_SIZE - j;
                string substr = line.Substring(FRAGMENT_SIZE - j);
                if (distribution.ContainsKey(substr))
                {
                    distribution[substr] = (int)distribution[substr] + 1;
                }
                else
                {
                    distribution[substr] = 1;
                }
            }
            printResults();
        }
        public void printResults()
        {
           // int totalMatches = -(int)distribution[FRAGMENT_SIZE];
            Console.WriteLine("Distribution : ");
            int max = 0;
            foreach (DictionaryEntry entry in distribution)
            {
                Console.WriteLine(entry.Key + " : " + entry.Value + " " );
                if (max < (int)entry.Value)
                {
                    max = (int)entry.Value;
                }
            }
         
            Console.WriteLine("\nBest match : " + max);
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
                    if (++mismatches == (k + 1))
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
