using System;
using System.Reflection;

namespace TracerLib
{
    //Describes tree of called methods in corresponding thread
    internal sealed partial class ThreadInfo
    {
        private MethodInfo _rootMethod;
        private MethodInfo _lastTracedMethod;
        private long _totalMeasuringTicksDelay = 0;

        public ThreadInfo()
        {
            _rootMethod = new MethodInfo(this, null, 0);
            _lastTracedMethod = _rootMethod;
        }

        //Add new method to be traced
        public void AddMethod(MethodBase tracedMethodBase, long startTickCount)
        {
            MethodInfo tracedMethod = new MethodInfo(this, tracedMethodBase, startTickCount);
            _lastTracedMethod.AddNestedMethod(tracedMethod);
            _lastTracedMethod = tracedMethod;
        }

        public void StopCurrentMethod(MethodBase tracedMethodBase, long endTickCount)
        {
            if(_rootMethod != _lastTracedMethod)
                _lastTracedMethod = _lastTracedMethod.StopMethodMeasuring(tracedMethodBase, endTickCount, _totalMeasuringTicksDelay);
            else
                throw new Exception("There is no methods to be stopped");
        }

        public void AddDelayTicks(long delayTicks)
        {
            _totalMeasuringTicksDelay += delayTicks;
        }

        private long TotalMeasuringDelay
        {
            get { return _totalMeasuringTicksDelay; }
        }

    }
}
