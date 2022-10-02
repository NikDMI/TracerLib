using System.Text.Encodings;

namespace TracerLib.Writer
{
    public interface IWriter
    {
        public void WriteStringUTF8(string data);
    }
}
