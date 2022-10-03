using System;
using TracerLib;
using TracerLib.Serialization;
using System.Threading;

namespace DemoConsoleApp
{
    class Program
    {
        private static Tracer tracer = new Tracer();
        static void TestMethod(ITracer tracer)
        {
            tracer.StartTrace();
            Thread.Sleep(new Random().Next(50, 200));
            TestMethod2(tracer);
            tracer.StopTrace();
        }
        static void TestMethod2(ITracer tracer)
        {
            tracer.StartTrace();
            Thread.Sleep(new Random().Next(50, 200));
            if (new Random().Next(0, 2) == 0)
            {
                if (new Random().Next(0, 2) == 0)
                {
                    TestMethod(tracer);
                }
                else
                {
                    TestMethod2(tracer);
                }
            }
            tracer.StopTrace();
        }

        static void StartThreadRouting(object obj)
        {
            TestMethod(tracer);
        }
        static void Main(string[] args)
        {
            tracer.StartTrace();
            Thread thread2 = new Thread(StartThreadRouting);
            thread2.Start();
            TestMethod(tracer);
            thread2.Join();
            tracer.StopTrace();
            ITraceResult res = tracer.GetTraceResult();
            
            string resStrXML = res.SerializeResult(SerializatorFactory.GetSerializator(SerializatorFactory.SerializatorType.XML));
            string resStrJSON = res.SerializeResult(SerializatorFactory.GetSerializator(SerializatorFactory.SerializatorType.JSON));
            var consoleWriter = TracerLib.Writer.WriterFactory.CreateConsoleWriter();
            var fileWriter = TracerLib.Writer.WriterFactory.CreateFileWriter("ResultFile.txt");
            consoleWriter.WriteStringUTF8(resStrXML);
            consoleWriter.WriteStringUTF8(resStrJSON);
            fileWriter.WriteStringUTF8(resStrXML);
            fileWriter.WriteStringUTF8(resStrJSON);
        }
    }
}
