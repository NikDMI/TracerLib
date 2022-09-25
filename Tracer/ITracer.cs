using System;

namespace TracerLib
{
    //Interface for tracer objects
    public interface ITracer
    {
        void StartTrace();
        void StopTrace();
        ITraceResult GetTraceResult();
    }
}
