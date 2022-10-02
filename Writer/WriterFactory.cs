using System;
using System.Collections.Generic;
using System.Text;

namespace TracerLib.Writer
{
    public static class WriterFactory
    {
        public static IWriter CreateConsoleWriter()
        {
            return new ConsoleWriter();
        }

        public static IWriter CreateFileWriter(string path = null)
        {
            if (path == null) path = "ResultFile.txt";
            return new FileWriter(path);
        }
    }
}
