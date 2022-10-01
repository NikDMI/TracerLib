using System;
using System.Collections.Generic;
using System.Text;

namespace TracerLib.Serialization
{
    public sealed partial class JSONSerializator: ISerializator
    {
        public string Serialize(ITraceResult traceResult_)
        {
            TraceResult traceResult = traceResult_ as TraceResult;
            if (traceResult == null) throw new ArgumentException("JSONSerializer: ITraceResult has incompatible type");
            return "";
        }

        public ITraceResult Deserialize(string data)
        {
            return new TraceResult();
        }
    }
}
