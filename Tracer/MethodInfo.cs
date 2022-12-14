using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;

namespace TracerLib
{
    //Describes information of traced methods (describes node of the tree)
    internal sealed partial class ThreadInfo
    {
        internal class MethodInfo
        {
            //private readonly ThreadInfo _callingThreadInfo;
            private readonly MethodBase _methodBase;
            private readonly MethodInfo _parentMethod;
            private List<MethodInfo> _nestedMethods;
            private long _startTickCount;
            private long _startThreadDelayTickCount;
            private long _totalWorkingTickTime = 0;
            private long _totalWorkingMsTime = 0;

            public MethodInfo(ThreadInfo workingThread, MethodBase tracedMethodBase, long startTickCount)
            {
                //_callingThreadInfo = workingThread;
                _parentMethod = workingThread._lastTracedMethod;
                _startTickCount = startTickCount;
                _startThreadDelayTickCount = workingThread.TotalMeasuringDelay;
                _methodBase = tracedMethodBase;
                _nestedMethods = new List<MethodInfo>();
            }

            public void AddNestedMethod(MethodInfo nestedMethod)
            {
                _nestedMethods.Add(nestedMethod);
            }

            public MethodInfo StopMethodMeasuring(MethodBase tracedMethodBase, long endTickCount, long endDelayTickCount)
            {
                if (tracedMethodBase != _methodBase) throw new Exception(String.Format("Last traced method {0} can't be stopped throught {1} method",
                     _methodBase.Name, tracedMethodBase.Name));
                long delayTickCount = endDelayTickCount - _startThreadDelayTickCount;
                _totalWorkingTickTime = (endTickCount - _startTickCount) - delayTickCount;
                return _parentMethod;
            }

            public static MethodInfo CreateDeepCopy(MethodInfo methodInfo, long ticksPerMillisecond)
            {
                MethodInfo copyInfo = (MethodInfo) methodInfo.MemberwiseClone();
                copyInfo._nestedMethods = new List<MethodInfo>();
                copyInfo._totalWorkingMsTime = copyInfo._totalWorkingTickTime / ticksPerMillisecond;
                for (int i = 0; i < methodInfo._nestedMethods.Count; i++)
                {
                    copyInfo._nestedMethods.Add(MethodInfo.CreateDeepCopy(methodInfo._nestedMethods[i], ticksPerMillisecond));
                }
                return copyInfo;
            }

            public List<MethodInfo> NestedMethods { get { return _nestedMethods; } }
            public MethodBase MethodBase { get { return _methodBase; } }
            public long MethodTimeMs { get { return _totalWorkingMsTime; } }

        }
    }
}
