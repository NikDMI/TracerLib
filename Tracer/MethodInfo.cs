using System;
using System.Collections.Generic;
using System.Reflection;

namespace TracerLib
{
    //Describes information of traced methods
    internal sealed class MethodInfo
    {
        private readonly MethodBase _methodInfo;
        private List<MethodInfo> _nestedMethods;
        private long _tickCountStart;
        private long _tickCountDelayTime;
    }
}
