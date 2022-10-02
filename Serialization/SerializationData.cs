using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Reflection;
using System.Xml.Serialization;

namespace TracerLib.Serialization
{
    //This class is used throught serialized method of .Net FCL
    [XmlRoot("root")]
    public sealed class SerializationData
    {
        [XmlElement(ElementName = "thread")]
        public List<ThreadSerializeData> threads { get; set; }

        //Create internal structure for serialize methods
        internal SerializationData(TraceResult data)
        {
            List<KeyValuePair<string, ThreadInfo.MethodInfo>> tracedThreads = data.tracedThreads;
            threads = new List<ThreadSerializeData>();
            foreach (var threadMethodPair in tracedThreads)
            {
                ThreadSerializeData threadData = new ThreadSerializeData(threadMethodPair);
                threads.Add(threadData);
            }
        }
        private SerializationData()
        {

        }

        public class ThreadSerializeData
        {
            internal ThreadSerializeData(KeyValuePair<string, ThreadInfo.MethodInfo> threadInfo)
            {
                long totalMethodsTime = 0;
                id = threadInfo.Key;
                methods = new List<MethodSerializeData>();
                foreach (var methodInfo in threadInfo.Value.NestedMethods)
                {
                    var serializedMethodInfo = new MethodSerializeData(methodInfo);
                    methods.Add(serializedMethodInfo);
                    totalMethodsTime += methodInfo.MethodTimeMs;
                }
                time = totalMethodsTime+" ms";
            }
            public ThreadSerializeData()
            {

            }

            [XmlAttribute]
            public string id { set; get; }

            [XmlAttribute]
            public string time { set; get; }

            [XmlElement(ElementName = "method")]

            public List<MethodSerializeData> methods { get; set; }
        }

        public class MethodSerializeData
        {
            internal MethodSerializeData(ThreadInfo.MethodInfo methodInfo)
            {
                MethodBase methodBase = methodInfo.MethodBase;
                name = methodBase.Name;
                className = methodBase.DeclaringType.Name;
                time = methodInfo.MethodTimeMs+" ms";
                methods = new List<MethodSerializeData>();
                foreach(var innerMethod in methodInfo.NestedMethods)
                {
                    methods.Add(new MethodSerializeData(innerMethod));
                }
            }
            public MethodSerializeData()
            {

            }

            [XmlAttribute]
            public string name { get; set; }

            [XmlAttribute("class")]
            [JsonPropertyName("class")]
            public string className { get; set; }

            [XmlAttribute]
            public string time { get; set; }

            [XmlElement(ElementName = "method")]
            public List<MethodSerializeData> methods { get; set; }
        }
    }

}
