using System;

namespace TracerLib
{
    internal sealed class ThreadInfo
    {
        private MethodInfo _rootMethod;
        private MethodInfo _lastTracedMethod;

        internal ThreadInfo()
        {
            _rootMethod = new MethodInfo();
            _lastTracedMethod = _rootMethod;
        }
    }
}
