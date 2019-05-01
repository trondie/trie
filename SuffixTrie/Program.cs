using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuffixTrie
{
    class Program
    {
        private static void assertFragmentTest()
        {
            string adapter = "GTTCGTCTTCTGCCGTATGCTCTAGTGACACACTGACCTCAAGGAACCGTGGGCTCTTAAGGT$";

            SuffixTree suffixTree123 = new SuffixTree(adapter);
            string reversedLine123 = "TGCCCTCAAGGAACCGTGGGCTCTTAAGGTCGATGTGGAACTGGATTGCA";
            string longestMatch123 = suffixTree123.longestExactMatch("CGTATGCTCTAGTGACACACTGACCTCAAGGAACCGTGGGCTCTTAAGGT");
            Debug.Assert(longestMatch123 == "CGTATGCTCTAGTGACACACTGACCTCAAGGAACCGTGGGCTCTTAAGGT");

            SuffixTree suffixTree12 = new SuffixTree(adapter);
            string reversedLine12 = "TGCCCTCAAGGAACCGTGGGCTCTTAAGGTCGATGTGGAACTGGATTGCA";
            string longestMatch12 = suffixTree12.longestExactMatch("TGCCCTCAAGGAACCGTGGGCTCTTAAGGTCGATGTGGAACTGGATTGCA");
            Debug.Assert(longestMatch12 == "T");

            SuffixTree suffixTree4 = new SuffixTree(adapter);
             string reversedLine4 = "TTTCTATGATGAATCAAACTAGCTCACTATGACCGACAGTGAAAATACAT";
             string longestMatch4 = suffixTree4.longestExactMatch("GACCTCAAGGAACCGTGGGCTCTTAAGGTTATCGGGACATGTTACGACGA");
             Debug.Assert(longestMatch4 == "GACCTCAAGGAACCGTGGGCTCTTAAGGT");

            

            SuffixTree suffixTree5 = new SuffixTree(adapter);
            string reversedLine5 = "TTTCTATGATGAATCAAACTAGCTCACTATGACCGACAGTGAAAATACAT";
            string longestMatch5 = suffixTree5.longestExactMatch(reversedLine5);
            Debug.Assert(longestMatch5 == "T");

            SuffixTree suffixTree3 = new SuffixTree(adapter);
            string reversedLine2 = "TGCCCTCAAGGAACCGTGGGCTCTTAAGGTCGATGTGGAACTGGATTGCA";
            string longestMatch2 = suffixTree3.longestExactMatch("T");
            Debug.Assert(longestMatch2 == "T");


            string reversedLine = "TCTTAAGGTCGAATTTAAGTCGCCCAGCGGTGCAGACTAGACTCCAGCGC";
            SuffixTree suffixTree = new SuffixTree(adapter);
            string longestMatch = suffixTree.longestExactMatch(reversedLine);
            Debug.Assert(longestMatch == "TCTTAAGGT");

            SuffixTree suffixTree2 = new SuffixTree(adapter);
            reversedLine = "GCTCTTAAGGGGCGTGCTCGGCTCACTAGGTGGCGATTCTCAGCATGCTC";
            longestMatch = suffixTree2.longestExactMatch(reversedLine);
            Debug.Assert(longestMatch == "");
        }
        static void Main(string[] args)
        {
            string adapter = "GTTCGTCTTCTGCCGTATGCTCTAGTGACACACTGACCTCAAGGAACCGTGGGCTCTTAAGGT$";
            string approxAdapter = "GTTCGTCTTCTGCCGTATGCTCTAGTGACACACTGACCTCAAGGAACCGTGGGCTCTTAAGGT";
            String test = "mississippi$";
       //     SuffixTree suffixTree = new SuffixTree("mississippi");
        //    string match = suffixTree.longestExactMatch("ippi");
            //Console.WriteLine(test.Substring(0, 3));
          //  assertFragmentTest();
           // Matcher matcher = new Matcher("dataset.txt", adapter);
           //  ApproxMatcher matcher = new ApproxMatcher("dataset.txt", approxAdapter, 10.0);
            AdapterSequence adapterSequence = new AdapterSequence("dataset.txt", "", 0.0, 5);
        //    ApproxMatcher matcher = new ApproxMatcher("BDEFGHIJKLMNOPQRSTUV", 10.0);
         //   Console.WriteLine(matcher.longestMatch("VCCCCCCCCCC"));
            Console.ReadLine();
        }
    }
}
