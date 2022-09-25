using System;
using System.Collections.Generic;
using System.Threading;
using System.Diagnostics;

namespace TracerLib
{
    public sealed class Tracer : ITracer
    {
        private object _lockObject = new object();
        private Dictionary<Thread, ThreadInfo> _tracedMethods = new Dictionary<Thread, ThreadInfo>();
        private Stopwatch _stopwatch = new Stopwatch();

        public Tracer()
        {
            _stopwatch.Start();
        }
        public void StartTrace()
        {
            long startTickCount = _stopwatch.ElapsedTicks;
            lock (_lockObject)
            {
                Thread callingThread = Thread.CurrentThread;
                if (!_tracedMethods.ContainsKey(callingThread))
                {
                    _tracedMethods.Add(callingThread, new ThreadInfo());
                }
            }

        }

        public void StopTrace()
        {

        }

        /*
        public TraceResult GetTraceResult()
        {

        }
        */

    }
}
