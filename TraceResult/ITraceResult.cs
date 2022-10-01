using System;

namespace TracerLib
{
    //Interface to trace informations for users
    public interface ITraceResult
    {
        public string SerializeResult(Serialization.ISerializator serializator);
    }
}
