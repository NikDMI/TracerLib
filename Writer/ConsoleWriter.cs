using System;
using System.Text.Encodings;

namespace TracerLib.Writer
{
    internal class ConsoleWriter: IWriter
    {
        public void WriteStringUTF8(string data)
        {
            Console.WriteLine(data);
        }
    }
}
