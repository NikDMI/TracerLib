using System;
using System.Collections.Generic;
using System.Threading;
using System.Diagnostics;
using System.Reflection;

namespace TracerLib
{
    public sealed class Tracer : ITracer
    {
        //Object for synchronization tracer via different threads
        private object _lockObject = new object();
        //Information about called methods in different threads with the help of tracer object
        private Dictionary<Thread, ThreadInfo> _tracedMethods = new Dictionary<Thread, ThreadInfo>();
        private Stopwatch _stopwatch = new Stopwatch();

        public Tracer()
        {
            _stopwatch.Start();
        }

        public void StartTrace()
        {
            //*This property returns read-only member (we can confirm that reading is thread-safe)
            //Returns number of ticks at the begining of measuring (to normalize real method work time)
            long startTickCount = _stopwatch.ElapsedTicks;
            
            lock (_lockObject)
            {
                Thread callingThread = Thread.CurrentThread;
                ThreadInfo currentThreadInfo = null;
                if (!_tracedMethods.ContainsKey(callingThread))
                {
                    currentThreadInfo = new ThreadInfo();
                    _tracedMethods.Add(callingThread, currentThreadInfo);
                }
                else
                {
                    currentThreadInfo = _tracedMethods[callingThread];
                }
                //Find MethodBase of calling method
                StackTrace stackTracer = new StackTrace(1);
                currentThreadInfo.AddMethod(stackTracer.GetFrame(0).GetMethod(),startTickCount);
                //Adding extra time, that doesn't belongs to measuring method 
                currentThreadInfo.AddDelayTicks(_stopwatch.ElapsedTicks - startTickCount);
            }
        }

        public void StopTrace()
        {
            //*This property returns read-only member (we can confirm that reading is thread-safe)
            //Returns number of ticks at the begining of measuring (to normalize real method work time)
            long startTickCount = _stopwatch.ElapsedTicks;

            lock (_lockObject)
            {
                Thread callingThread = Thread.CurrentThread;
                ThreadInfo currentThreadInfo;
                if (!_tracedMethods.TryGetValue(callingThread, out currentThreadInfo))
                    throw new Exception(String.Format("Thread {0} doesn't contains measuring methods", callingThread.Name));
                //Find MethodBase of calling method
                StackTrace stackTracer = new StackTrace(1);
                currentThreadInfo.StopCurrentMethod(stackTracer.GetFrame(0).GetMethod(), startTickCount);
                //Adding extra time, that doesn't belongs to measuring method 
                currentThreadInfo.AddDelayTicks(_stopwatch.ElapsedTicks - startTickCount);
            }
        }


        /*
        public TraceResult GetTraceResult()
        {

        }
        */

    }
}
