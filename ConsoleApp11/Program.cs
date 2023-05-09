using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace UniqueWordsCounter
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter the URL of the website:");
            string url = Console.ReadLine();

            // Create a web request to get the HTML content of the page
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream stream = response.GetResponseStream();
            StreamReader reader = new StreamReader(stream);

            // Read the HTML content of the page
            List<string> htmlContentList = new List<string>();
            StreamReader sr = new StreamReader(stream);
            while (!sr.EndOfStream)
            {
                htmlContentList.Add(sr.ReadLine());
            }
            string htmlContent = string.Join("", htmlContentList);

            // Remove all HTML tags from the HTML content
            htmlContent = System.Text.RegularExpressions.Regex.Replace(htmlContent, "<.*?>", "");

            // Split the text into words using a list of delimiters
            List<string> delimiters = new List<string>() { " ", ",", ".", "\n", "\r", "\t", "/", "\\", "|", "[", "]", "{", "}", "(", ")", "_", "-", ":", ";", "'", "\"", "=", "+", "*", "&", "^", "%", "$", "#", "@", "!", "?", "~" };
            string[] words = htmlContent.Split(delimiters.ToArray(), StringSplitOptions.RemoveEmptyEntries);

            // Count the unique words
            Dictionary<string, int> wordCount = new Dictionary<string, int>();
            foreach (string word in words)
            {
                if (wordCount.ContainsKey(word))
                {
                    wordCount[word]++;
                }
                else
                {
                    wordCount.Add(word, 1);
                }
            }

            // Sort the words by count in descending order
            List<KeyValuePair<string, int>> sortedWords = new List<KeyValuePair<string, int>>(wordCount);
            sortedWords.Sort((x, y) => y.Value.CompareTo(x.Value));

            // Output the results
            foreach (KeyValuePair<string, int> pair in sortedWords)
            {
                Console.WriteLine(pair.Key + " - " + pair.Value);
            }

            Console.ReadLine();
        }
    }
}
