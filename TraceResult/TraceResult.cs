using System;
using System.Collections.Generic;


namespace TracerLib
{
	//Implementation of tracer result
	internal class TraceResult: ITraceResult
	{
		private int _defaultThreadID = 1;
		private List<KeyValuePair<string, ThreadInfo.MethodInfo>> _tracedThreads;
		internal TraceResult()
        {
			_tracedThreads = new List<KeyValuePair<string, ThreadInfo.MethodInfo>>();
		}

		//Write information about methods in the corresponding thread
		internal void WriteNewThread(ThreadInfo threadInfo, long TicksPerMillisecond, string threadName = null)
        {
			if (threadName == null) threadName = (_defaultThreadID++).ToString();
			_tracedThreads.Add(new KeyValuePair<string, ThreadInfo.MethodInfo>(threadName, threadInfo.GetResult(TicksPerMillisecond)));
        }
	}
}
