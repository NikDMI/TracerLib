using System;
using System.Collections.Generic;


namespace TracerLib
{
	//Implementation of tracer result
	internal class TraceResult: ITraceResult
	{
		private int _defaultThreadID = 1;
		//Thread names and corresponding ROOT method elements
		private List<KeyValuePair<string, ThreadInfo.MethodInfo>> _tracedThreads;
		public TraceResult()
        {
			_tracedThreads = new List<KeyValuePair<string, ThreadInfo.MethodInfo>>();
		}

		//Write information about methods in the corresponding thread
		public void WriteNewThread(ThreadInfo threadInfo, long TicksPerMillisecond, string threadName = null)
        {
			if (threadName == null) threadName = (_defaultThreadID++).ToString();
			_tracedThreads.Add(new KeyValuePair<string, ThreadInfo.MethodInfo>(threadName, threadInfo.GetResult(TicksPerMillisecond)));
        }
		public List<KeyValuePair<string, ThreadInfo.MethodInfo>> tracedThreads { get { return _tracedThreads; } }

		public string SerializeResult(Serialization.ISerializator serializator)
        {
			return serializator.Serialize(this);
        }
	}
}
