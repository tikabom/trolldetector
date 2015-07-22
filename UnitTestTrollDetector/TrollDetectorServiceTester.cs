using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Script.Serialization;
using TrollDetector;
using System.ServiceModel.Web;

namespace UnitTestTrollDetector
{
    [TestClass]
    public class TrollDetectorServiceTester
    {
        [TestMethod]
        public void TestTrollDetectorGet()
        {
            JavaScriptSerializer serlializer = new JavaScriptSerializer();
            TrollDetectorService service = new TrollDetectorService();
            string response = service.GetWords();
            StringAssert.Contains(response, "Text");
            StringAssert.Contains(response, "Exclude");
            try
            {
                TrollResponse troll = (TrollResponse)serlializer.Deserialize(response, typeof(TrollResponse));
            }
            catch (Exception e)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void TestTrollDetectorPost()
        {
            TrollRequest troll = new TrollRequest();
            troll.Text = "foo";
            troll.WordCount.Add("foo", 1);

            try
            {
                TrollDetectorService service = new TrollDetectorService();
                service.TestWords(troll);
            }
            catch (Exception e)
            {
                Assert.Fail();
            }

            troll = new TrollRequest();
            troll.Text = "Hodor, hodor hodor. Hodor! Hodor hodor hodor hodor hodor hodor.";
            troll.WordCount.Add("hodor", 10);

            try
            {
                TrollDetectorService service = new TrollDetectorService();
                service.TestWords(troll);
            }
            catch (Exception e)
            {
                Assert.Fail();
            }

            troll.Exclude = new string[1]{"hodor"};

            try
            {
                TrollDetectorService service = new TrollDetectorService();
                service.TestWords(troll);
            }
            catch (Exception e)
            {
                StringAssert.Equals(e.Message, "Bad Request");
            }

            troll = new TrollRequest();
            troll.Text = "The quick brown fox jumped over the lazy dog.";
            troll.Exclude = new string[2] { "the", "fox" };
            troll.WordCount.Add("the", 2);
            troll.WordCount.Add("quick", 1);
            troll.WordCount.Add("brown", 1);
            troll.WordCount.Add("jumped", 1);
            troll.WordCount.Add("over", 1);
            troll.WordCount.Add("lazy", 1);
            troll.WordCount.Add("dog", 1);

            try
            {
                TrollDetectorService service = new TrollDetectorService();
                service.TestWords(troll);
            }
            catch (Exception e)
            {
                StringAssert.Equals(e.Message, "Bad Request");
            }

            troll.WordCount.Remove("the");

            try
            {
                TrollDetectorService service = new TrollDetectorService();
                service.TestWords(troll);
            }
            catch (Exception e)
            {
                Assert.Fail();
            }

            troll = new TrollRequest();
            troll.Text = "Hello, my name is Troll alien";
            troll.Exclude = new string[3] { "my", "troll", "is" };
            troll.WordCount.Add("Hello", 1);
            troll.WordCount.Add("name", 1);
            troll.WordCount.Add("alien", 1);

            try
            {
                TrollDetectorService service = new TrollDetectorService();
                service.TestWords(troll);
                Assert.Fail();
            }
            catch (Exception e)
            {
                StringAssert.Equals(e.Message, "Bad Request");
            }

            troll = new TrollRequest();
            troll.Text = "It was the best of times, it was the worst of times, it was the age of wisdom, it was the age of foolishness, it was the epoch of belief, it was the epoch of incredulity, it was the season of Light, it was the season of Darkness, it was the spring of hope, it was the winter of despair, we had everything before us, we had nothing before us, we were all going direct to Heaven, we were all going direct the other way - in short, the period was so far like the present period, that some of its noisiest authorities insisted on its being received, for good or for evil, in the superlative degree of comparison only.";
            troll.Exclude = new string[8] { "best", "superlative", "insisted", "only", "present", "in", "good", "evil" };
            troll.WordCount.Add("It", 10);
            troll.WordCount.Add("was", 11);
            troll.WordCount.Add("the", 14);
            troll.WordCount.Add("of", 12);
            troll.WordCount.Add("times", 2);
            troll.WordCount.Add("worst", 1);
            troll.WordCount.Add("age", 2);
            troll.WordCount.Add("wisdom", 1);
            troll.WordCount.Add("foolishness", 1);
            troll.WordCount.Add("epoch", 2);
            troll.WordCount.Add("belief", 1);
            troll.WordCount.Add("incredulity", 1);
            troll.WordCount.Add("light", 1);
            troll.WordCount.Add("darkness", 1);
            troll.WordCount.Add("spring", 1);
            troll.WordCount.Add("hope", 1);
            troll.WordCount.Add("winter", 1);
            troll.WordCount.Add("despair", 1);
            troll.WordCount.Add("had", 2);
            troll.WordCount.Add("everything", 1);
            troll.WordCount.Add("before", 2);
            troll.WordCount.Add("us", 2);
            troll.WordCount.Add("were", 2);
            troll.WordCount.Add("all", 2);
            troll.WordCount.Add("going", 2);
            troll.WordCount.Add("direct", 2);
            troll.WordCount.Add("to", 1);
            troll.WordCount.Add("heaven", 1);
            troll.WordCount.Add("other", 1);
            troll.WordCount.Add("way", 1);
            troll.WordCount.Add("short", 1);
            troll.WordCount.Add("period", 2);
            troll.WordCount.Add("so", 1);
            troll.WordCount.Add("far", 1);
            troll.WordCount.Add("like", 1);
            troll.WordCount.Add("some", 1);
            troll.WordCount.Add("its", 2);
            troll.WordCount.Add("noisiest", 1);
            troll.WordCount.Add("authorities", 1);
            troll.WordCount.Add("on", 1);
            troll.WordCount.Add("being", 1);
            troll.WordCount.Add("received", 1);
            troll.WordCount.Add("degree", 1);
            troll.WordCount.Add("comparison", 1);
            troll.WordCount.Add("for", 2);
            troll.WordCount.Add("or", 1);
            troll.WordCount.Add("that", 1);
            troll.WordCount.Add("nothing", 1);
            troll.WordCount.Add("season", 2);
            troll.WordCount.Add("we", 4);

            try
            {
                TrollDetectorService service = new TrollDetectorService();
                service.TestWords(troll);
            }
            catch (Exception e)
            {
                Assert.Fail();
            }
        }
    }
}
