using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Web;
using System.Web.Hosting;
using System.Web.Script.Serialization;

namespace TrollDetector
{
    public class TrollDetectorService : ITrollDetectorService
    {
        public string GetWords()
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            TrollResponse troll = new TrollResponse();
            Random rand = new Random();
            
			/** choose a text file at random to give to the client **/
			int textFilename = rand.Next(0, 5);
            string text = File.ReadAllText("../../Text/" + textFilename + ".txt");
            /** **/

			/** split text on different punctuations **/
			char[] separators = {'.', ' ', ',', '!', ';', '-'};
            string[] wordsInText = text.Split(separators, StringSplitOptions.RemoveEmptyEntries);
			/** **/
            
			/** create a set of unique words in the text **/
			HashSet<string> uniqueWords = new HashSet<string>(StringComparer.CurrentCultureIgnoreCase);
            foreach (string word in wordsInText)
            {
                uniqueWords.Add(word);
            }
			/** **/

            int len = uniqueWords.Count;

            troll.Text = text;

			/** if there is only one unique word in text exclude word list will be empty **/
            if (len == 1)
            {
                return serializer.Serialize(troll);            
            }
			/** **/

			/** select words to be excluded from count at random **/
            int excludeLen = rand.Next(0, len - 1);
			HashSet<string> excludeList = new HashSet<string>();
            for (int i = 0; i < excludeLen; i++)
            {
                excludeList.Add(uniqueWords.ElementAt(rand.Next(0 , len - 1)));
            }            
            troll.Exclude = excludeList.ToArray();
			/** **/

            return serializer.Serialize(troll);
        }

        public void TestWords(TrollRequest troll)
        {
            int i;
            string text;

            /** check if alien text is present in the files saved on the server **/
            for (i = 0; i < 6; i++)
            {
                text = File.ReadAllText("../../Text/" + i + ".txt");
                if (string.Equals(text, troll.Text, StringComparison.CurrentCultureIgnoreCase))
                    break;
            }

            if (i == 6)
            {
                throw new WebFaultException(System.Net.HttpStatusCode.BadRequest);
            }
            /** end check **/

            Dictionary<string, int> wordCount = new Dictionary<string, int>(StringComparer.CurrentCultureIgnoreCase);
            text = troll.Text;
            char[] separators = { '.', ' ', ',', '!', ';', '-'};
            string[] wordsInText = text.Split(separators, StringSplitOptions.RemoveEmptyEntries);

            foreach (string word in wordsInText)
            {
                if (!troll.Exclude.Contains(word, StringComparer.CurrentCultureIgnoreCase))
                {
                    if (wordCount.ContainsKey(word.ToLower()))
                    {
                        wordCount[word] = wordCount[word] + 1;
                    }
                    else
                    {
                        wordCount.Add(word, 1);
                    }
                }
            }

            /** check if number of words returned with a count is correct **/
            if (wordCount.Count != troll.WordCount.Count)
            {
                throw new WebFaultException(System.Net.HttpStatusCode.BadRequest);
            }
            /** end check **/

            /** check if exclude word list is empty when text has only one unique word **/
            if (wordCount.Count == 1 && troll.Exclude.Length > 0)
            {
                throw new WebFaultException(System.Net.HttpStatusCode.BadRequest);
            }
            /** end check **/

            /** check if words to be excluded are present in the words returned with a count **/
            foreach (string excludeWord in troll.Exclude)
            {

                if (troll.WordCount.ContainsKey(excludeWord))
                {
                    throw new WebFaultException(System.Net.HttpStatusCode.BadRequest);
                }
            }
            /** end check **/

            foreach (string word in wordsInText)
            {
                if(!troll.Exclude.Contains(word, StringComparer.CurrentCultureIgnoreCase))
                {
                    /** check if word (not in exclude list) is present **/
                    if (!troll.WordCount.ContainsKey(word))
                    {
                        throw new WebFaultException(System.Net.HttpStatusCode.BadRequest);
                    }
                    /** end check **/

                    /** check if word count is correct **/
                    if (troll.WordCount[word] != wordCount[word])
                    {
                        throw new WebFaultException(System.Net.HttpStatusCode.BadRequest);
                    }
                    /** end check **/
                }
            }
            
            return;
        }
    }
}
