using System;
using System.Collections.Generic;
using System.Text;

namespace TracerLib.Serialization
{
    //Common interface for users to work with different serializable notations
    interface ISerializator
    {
        string Serialize(ITraceResult traceResult);
        ITraceResult Deserialize(string data);
    }

}
