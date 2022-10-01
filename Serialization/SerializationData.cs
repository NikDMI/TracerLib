using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using System.Reflection;

namespace TracerLib.Serialization
{

    internal class SerializationData
    {
        public List<ThreadSerializeData> threads { get; set; }

        //Create internal structure for serialize methods
        public void SetData(TraceResult data)
        {
            List<KeyValuePair<string, ThreadInfo.MethodInfo>> tracedThreads = data.tracedThreads;
            threads = new List<ThreadSerializeData>();
            foreach (var threadMethodPair in tracedThreads)
            {
                ThreadSerializeData threadData = new ThreadSerializeData(threadMethodPair);
                threads.Add(threadData);
            }
        }

        public void GetData(ref TraceResult data)
        {

        }

        public class ThreadSerializeData
        {
            public ThreadSerializeData(KeyValuePair<string, ThreadInfo.MethodInfo> threadInfo)
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

            public string id { set; get; }
            public string time { set; get; }
            public List<MethodSerializeData> methods { get; set; }
        }



        public class MethodSerializeData
        {
            public MethodSerializeData(ThreadInfo.MethodInfo methodInfo)
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
            public string name { get; set; }

            [JsonPropertyName("class")]
            public string className { get; set; }
            public string time { get; set; }

            public List<MethodSerializeData> methods { get; set; }
        }
    }

}
