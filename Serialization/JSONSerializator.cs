using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace TracerLib.Serialization
{
    internal sealed partial class JSONSerializator: ISerializator
    {
        string ISerializator.Serialize(ITraceResult traceResult_)
        {
            TraceResult traceResult = traceResult_ as TraceResult;
            if (traceResult == null) throw new ArgumentException("JSONSerializer: ITraceResult has incompatible type");
            SerializationData serializationData = new SerializationData(traceResult);
            var options = new JsonSerializerOptions { WriteIndented = true };
            return JsonSerializer.Serialize(serializationData, options);
        }
    }
}
