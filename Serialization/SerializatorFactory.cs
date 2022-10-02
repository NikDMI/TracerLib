using System;
using System.Collections.Generic;
using System.Text;

namespace TracerLib.Serialization
{
    public static class SerializatorFactory
    {
        public enum SerializatorType { JSON, XML};
        public static ISerializator GetSerializator(SerializatorType type)
        {
            switch (type)
            {
                case SerializatorType.JSON:
                    return new JSONSerializator();
                case SerializatorType.XML:
                    return new XMLSerializator();
            }
            throw new ArgumentException("SerializatorFactory: Not valid serializator type");
        }
    }
}
