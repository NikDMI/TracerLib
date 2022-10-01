using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace TracerLib.Serialization
{
    internal class SerializationData
    {
        public void SetData(TraceResult data)
        {
            List<KeyValuePair<string, ThreadInfo.MethodInfo>> tracedThreads = data.tracedThreads;
        }
        public void GetData(ref TraceResult data)
        {

        }

        public class ThreadSerializeData
        {
            public string id { set; get; }
            public long time { set; get; }

            private List<MethodSerializeData> _methods;
            public List<MethodSerializeData> methods
            {
                get { return _methods; }
                private set { _methods = value; }
            }
        }

        public class MethodSerializeData
        {
            public string name { get; set; }

            [JsonPropertyName("class")]
            public string className { get; set; }
            public long time { get; set; }

            public List<MethodSerializeData> methods { get; set; }
        }
    }

}
