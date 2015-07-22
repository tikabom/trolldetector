using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace TrollDetector
{
    [ServiceContract]
    public interface ITrollDetectorService
    {

        [OperationContract]
        [WebGet(UriTemplate="/", ResponseFormat=WebMessageFormat.Json)]
        string GetWords();

        [OperationContract]
        [WebInvoke(Method="POST", UriTemplate="/", RequestFormat=WebMessageFormat.Json)]
        void TestWords(TrollRequest troll);

    }

    [DataContract]
    public class TrollResponse
    {
        string text;
        string[] exclude = {};

        [DataMember]
        public string Text
        {
            get { return text; }
            set { text = value; }
        }

        [DataMember]
        public string[] Exclude
        {
            get { return exclude; }
            set { exclude = value; }
        }
        
    }

    [DataContract]
    public class TrollRequest
    {
        string text;
        string[] exclude = { };
        Dictionary<string, int> wordCount = new Dictionary<string, int>(StringComparer.CurrentCultureIgnoreCase);

        [DataMember]
        public string Text
        {
            get { return text; }
            set { text = value; }
        }

        [DataMember]
        public string[] Exclude
        {
            get { return exclude; }
            set { exclude = value; }
        }

        [DataMember]
        public Dictionary<string, int> WordCount
        {
            get { return wordCount; }
            set { wordCount = value; }
        }
    }
}
