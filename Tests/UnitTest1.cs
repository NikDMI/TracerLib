using NUnit.Framework;
using TracerLib;
using System.Threading;
//using Microsoft.VisualStudio.TestTools.UnitTesting;

using PrivateObject = Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject;


namespace TestTracerLib
{
    public class Tests
    {
        //private static PrivateObject _tracerObject;
        public static Tracer tracer;

        [SetUp]
        public void Setup()
        {
            tracer = new Tracer();
            //_tracerObject = new PrivateObject(tracer);
        }

        [Test]
        public void TestNormalFunctionCall()
        {
            Assert.DoesNotThrow(() => TemplateFunctions.TracedFunction1(tracer), "Simple tracing was failed");
            Assert.DoesNotThrow(() => tracer.GetTraceResult(), "Simple tracing was failed in getting trace results");

        }

        [Test]
        public void TestNormalRecursiveFunctionCall()
        {
            Assert.DoesNotThrow(() => TemplateFunctions.TracedFunction2(tracer), "Recursive tracing was failed");
            Assert.DoesNotThrow(() => tracer.GetTraceResult(), "Recursive tracing was failed in getting trace results");
        }

        [Test]
        public void TestSingleFunctionCallWithMissingStop()
        {
            Tracer tracer = new Tracer();
            Assert.Catch(() => TemplateFunctions.TracedFunction3Bad(tracer), "Single tracing give invalid trace results");
        }

        [Test]
        public void TestSingleFunctionCallWithMissingStart()
        {
            Tracer tracer = new Tracer();
            Assert.Catch(() => TemplateFunctions.TracedFunction4Bad(tracer), "Single tracing give invalid trace results");
        }

        [Test]
        public void TestNestedFunctionCallWithMissingStop()
        {
            Tracer tracer = new Tracer();
            Assert.DoesNotThrow(() => TemplateFunctions.TracedFunction5Bad(tracer), "Recursive tracing give invalid trace results");
            Assert.Catch(() => tracer.GetTraceResult(), "Recursive tracing was failed in getting trace results");
        }

        [Test]
        public void TestNestedFunctionCallWithMissingStart()
        {
            Tracer tracer = new Tracer();
            Assert.Catch(() => TemplateFunctions.TracedFunction6Bad(tracer), "Recursive tracing give invalid trace results");
        }

        [Test]
        public void TestRecursiveFunctionCall()
        {
            Tracer tracer = new Tracer();
            Assert.DoesNotThrow(() => TemplateFunctions.TracedFunction7(tracer), "Recursive tracing give invalid trace results");
            Assert.DoesNotThrow(() => tracer.GetTraceResult(), "Can't get traced result after normal recursian call");
        }

        [Test]
        public void TestMultithreadFunctionCall()
        {
            Tracer tracer = new Tracer();
            Thread thread2 = new Thread(TemplateFunctions.ThreadStart1);
            thread2.Start(tracer);
            TemplateFunctions.TracedFunction1(tracer);
            thread2.Join();
            Assert.DoesNotThrow(() => tracer.GetTraceResult(), "Can't get traced result after normal multithread call");
        }

        [Test]
        public void TestMultithreadFunctionCallWithInfinitTracing()
        {
            Tracer tracer = new Tracer();
            Thread thread2 = new Thread(TemplateFunctions.ThreadStart2);
            thread2.Start(tracer);
            TemplateFunctions.TracedFunction1(tracer);
            thread2.Join();
            Assert.Catch(() => tracer.GetTraceResult(), "Give invalid tracing results");
        }


    }

    public static class TemplateFunctions
    {
        public static void TracedFunction1(Tracer tracer)
        {
            tracer.StartTrace();
            tracer.StopTrace();
        }

        public static void TracedFunction2(Tracer tracer)
        {
            tracer.StartTrace();
            TracedFunction1(tracer);
            tracer.StopTrace();
        }

        public static void TracedFunction3Bad(Tracer tracer)
        {
            tracer.StartTrace();
            tracer.GetTraceResult();
        }

        public static void TracedFunction4Bad(Tracer tracer)
        {
            tracer.StopTrace();
        }

        public static void TracedFunction5Bad(Tracer tracer)
        {
            tracer.StartTrace();
            TracedFunction1(tracer);
        }

        public static void TracedFunction6Bad(Tracer tracer)
        {
            TracedFunction1(tracer);
            tracer.StopTrace();
        }

        public static int RecursiveNumberCalls = 5;
        public static void TracedFunction7(Tracer tracer)
        {
            tracer.StartTrace();
            if(RecursiveNumberCalls != 0)
            {
                RecursiveNumberCalls--;
                TracedFunction7(tracer);
            }
            tracer.StopTrace();
        }

        public static void ThreadStart1(object o)
        {
            TracedFunction1(o as Tracer);
        }

        public static void ThreadStart2(object o)
        {
            (o as Tracer).StartTrace();
        }
    }
}