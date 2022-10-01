using System;
using System.Collections.Generic;
using System.Text;

namespace TracerLib.Serialization
{
    //Common interface for users to work with different serializable notations
    public interface ISerializator
    {
        internal string Serialize(ITraceResult traceResult);
        internal ITraceResult Deserialize(string data);
    }

}
