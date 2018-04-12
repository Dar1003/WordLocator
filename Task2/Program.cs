using System;
using System.IO;
//using System.Threading;
using System.Collections.Generic;
using System.Net;

namespace Task2
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] lines = File.ReadAllLines(@"C:\Users\Din\Videos\Captures\Task2 urls and words\Urls and words.txt");
            int i = 0;
            List<string> urls = new List<string>();
            List<string> words = new List<string>();
            string PageData;
            foreach (string line in lines)
            {
                CheckUrl(line);
                if(CheckUrl(line)==true)
                {
                    urls.Add(line);
                }
                else
                {
                    words.Add(line);
                }
                i++;
            }
            for (int g = 0; g < urls.Count; g++)
            {
                PageData = GetWebText(urls[g]);
                for (int h = 0; h < words.Count;)
                {
                    if(h==words.Count-1)
                    {
                        Console.Write("the words ");
                        for (int y = 0; y < words.Count; y++)
                        {
                            Console.Write(words[y]+",");
                        }
                        Console.Write(" have been found in this link: {0}",urls[g]);
                    }
                    if(CheckWord(words[h], PageData)==true)
                    {
                        h++;
                    }
                    else
                    {
                        h = words.Count;
                        Console.WriteLine("the words failed to be found at {0}",urls[g]);
                    }
                }
            }
        }
        public static bool CheckUrl(string url)
        {
            string word = "https";
            
            if (url.Length > 4)
            {
                for (int i = 0; i < word.Length; i++)
                {
                    if(url[i]!=word[i])
                    {
                        return false;
                    }
                }
            }
            return true;       
        }
        private static string GetWebText(string url)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.UserAgent = "A .NET Web Crawler";
            WebResponse response = request.GetResponse();
            Stream stream = response.GetResponseStream();
            StreamReader reader = new StreamReader(stream);
            string htmlText = reader.ReadToEnd();
            return htmlText;
        }
        public static bool CheckWord(string word, string PageData)
        {
            int count = 0;
            for (int i = 0; i < PageData.Length; i++)
            {
                if (PageData[i] == word[count])
                {
                    count++;
                }
                else
                {
                    count = 0;
                }
                if(count==word.Length-1)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
