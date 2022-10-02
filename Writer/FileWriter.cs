using System.IO;
using System;
namespace TracerLib.Writer
{
    internal class FileWriter: IWriter
    {
        FileStream fileStream;

        public FileWriter(string path)
        {
            if (path == null) throw new ArgumentNullException("FileWriter null path");
            fileStream = new FileStream(path, FileMode.OpenOrCreate | FileMode.Truncate);
        }
        public void WriteStringUTF8(string data)
        {
            var utf8Encoding = System.Text.Encoding.UTF8;
            byte[] utf8Data = utf8Encoding.GetBytes(data);
            fileStream.Write(utf8Data, 0, utf8Data.Length);
            fileStream.Flush();
        }
    }
}
