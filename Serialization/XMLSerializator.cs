using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Text.Encodings;

namespace TracerLib.Serialization
{
    internal sealed class XMLSerializator: ISerializator
    {
        string ISerializator.Serialize(ITraceResult traceResult_)
        {
            TraceResult traceResult = traceResult_ as TraceResult;
            if (traceResult == null) throw new ArgumentException("JSONSerializer: ITraceResult has incompatible type");
            SerializationData serializationData = new SerializationData(traceResult);
            XmlSerializer serializer = new XmlSerializer(typeof(SerializationData));
            Stream writeStream = new MemoryStream();
            serializer.Serialize(writeStream, serializationData);
            long streamLength = writeStream.Length;
            byte[] streamData = new byte[streamLength];
            writeStream.Seek(0, SeekOrigin.Begin);
            writeStream.Read(streamData, 0, (int)streamLength);
            var utf8Encoder = new UTF8Encoding();
            string xmlString = utf8Encoder.GetString(streamData);
            return xmlString;
        }
    }
}
