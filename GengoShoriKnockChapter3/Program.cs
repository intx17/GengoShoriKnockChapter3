using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using LogExtension;
using BashExtension;

namespace Q20
{
    class Program
    {
        const string filePath = "../jawiki-country.json";

        static void Main(string[] args)
        {
            var task = GetArticleAboutUK(filePath);
            var article = task.Result;

            $"cat ../jawiki-country.json | jq 'if .title == \"イギリス\" then .text else empty end'".WriteBashLine();
        }

        static async Task<Article> GetArticleAboutUK(string path)
        {
            var allLines = await File.ReadAllLinesAsync(path);

            return allLines
                .Select(l => JsonConvert.DeserializeObject<Article>(l))
                .FirstOrDefault(a => a.Title.Contains("イギリス"));
        }
    }

    public class Article
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }
    }
}
