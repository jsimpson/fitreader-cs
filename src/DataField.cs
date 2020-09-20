using System.Collections.Generic;

namespace FitReader
{
    internal struct DataType
    {
        public bool endianAbility;
        public byte baseFieldtype;
        public string typeName;
        public object invalidValue;
        public byte size;

        public DataType(bool endianAbility, byte baseFieldtype, string typeName, object invalidValue, byte size)
        {
            this.endianAbility = endianAbility;
            this.baseFieldtype = baseFieldtype;
            this.typeName = typeName;
            this.invalidValue = invalidValue;
            this.size = size;
        }
    }

    internal class DataField
    {
        internal DataType[] types = new DataType[]
        {
            new DataType(false, 0x00, "enum",    (byte)   0xFF,               1),
            new DataType(false, 0x01, "sint8",   (sbyte)  0x7F,               1),
            new DataType(false, 0x02, "uint8",   (byte)   0xFF,               1),
            new DataType(false, 0x83, "sint16",  (short)  0x7FFF,             2),
            new DataType(false, 0x84, "uint16",  (ushort) 0xFFFF,             2),
            new DataType(true,  0x85, "sint32",  (int)    0x7FFFFFFF,         4),
            new DataType(true,  0x86, "uint32",  (uint)   0xFFFFFFFF,         4),
            new DataType(false, 0x07, "string",  (byte)   0x00,               1),
            new DataType(true,  0x88, "float32", (float)  0xFFFFFFFF,         4),
            new DataType(true,  0x89, "float64", (double) 0xFFFFFFFFFFFFFFFF, 8),
            new DataType(false, 0x0A, "uint8z",  (byte)   0x00,               1),
            new DataType(true,  0x8B, "uint16z", (ushort) 0x0000,             2),
            new DataType(true,  0x8C, "uint32z", (uint)   0x00000000,         4),
            new DataType(false, 0x0D, "byte",    (byte)   0xFF,               1)
        };

        internal bool valid = false;
        internal object data;
        public DataField(EndianBinaryReader binaryReader, Dictionary<string, byte> opts)
        {
            var baseNum = opts["baseNum"];
            var size = opts["size"];
            var arch = opts["arch"];
            var littleEndian = arch == 1;

            var baseType = types[baseNum];
            var multiples = size / baseType.size;

            // TODO: These types should be an enum.
            switch (baseType.typeName)
            {
                case "enum":
                case "byte":
                case "uint8":
                case "uint8z":
                    if (multiples > 1)
                    {
                        var data = new byte[multiples];
                        for (int i = 0; i < multiples; i++)
                        {
                            data[i] = binaryReader.ReadUInt8();
                        }
                        this.data = (object)data;
                    }
                    else
                    {
                        this.data = binaryReader.ReadUInt8();
                    }
                    break;
                case "sint8":
                    if (multiples > 1)
                    {
                        var data = new sbyte[multiples];
                        for (int i = 0; i < multiples; i++)
                        {
                            data[i] = binaryReader.ReadInt8();
                        }
                        this.data = (object)data;
                    }
                    else
                    {
                        this.data = binaryReader.ReadInt8();
                    }
                    break;
                case "uint16":
                case "uint16z":
                    if (multiples > 1)
                    {
                        var data = new ushort[multiples];
                        for (int i = 0; i < multiples; i++)
                        {
                            data[i] = binaryReader.ReadUInt16();
                        }
                        this.data = (object)data;
                    }
                    else
                    {
                        this.data = binaryReader.ReadUInt16();
                    }
                    break;
                case "sint16":
                    if (multiples > 1)
                    {
                        var data = new short[multiples];
                        for (int i = 0; i < multiples; i++)
                        {
                            data[i] = binaryReader.ReadInt16();
                        }
                        this.data = (object)data;
                    }
                    else
                    {
                        this.data = binaryReader.ReadInt16();
                    }
                    break;
                case "uint32":
                case "uint32z":
                    if (multiples > 1)
                    {
                        var data = new uint[multiples];
                        for (int i = 0; i < multiples; i++)
                        {
                            data[i] = binaryReader.ReadUInt32();
                        }
                        this.data = (object)data;
                    }
                    else
                    {
                        this.data = binaryReader.ReadUInt32();
                    }
                    break;
                case "sint32":
                    if (multiples > 1)
                    {
                        var data = new int[multiples];
                        for (int i = 0; i < multiples; i++)
                        {
                            data[i] = binaryReader.ReadInt32();
                        }
                        this.data = (object)data;
                    }
                    else
                    {
                        this.data = binaryReader.ReadInt32();
                    }
                    break;
                case "float32":
                    if (multiples > 1)
                    {
                        var data = new float[multiples];
                        for (int i = 0; i < multiples; i++)
                        {
                            data[i] = binaryReader.ReadFloat();
                        }
                        this.data = (object)data;
                    }
                    else
                    {
                        this.data = binaryReader.ReadFloat();
                    }
                    break;
                case "float64":
                    if (multiples > 1)
                    {
                        var data = new double[multiples];
                        for (int i = 0; i < multiples; i++)
                        {
                            data[i] = binaryReader.ReadFloat64();
                        }
                        this.data = (object)data;
                    }
                    else
                    {
                        this.data = binaryReader.ReadFloat64();
                    }
                    break;
                case "uint64":
                case "uint64z":
                    if (multiples > 1)
                    {
                        var data = new ulong[multiples];
                        for (int i = 0; i < multiples; i++)
                        {
                            data[i] = binaryReader.ReadUInt64();
                        }
                        this.data = (object)data;
                    }
                    else
                    {
                        this.data = binaryReader.ReadUInt64();
                    }
                    break;
                case "sint64":
                    if (multiples > 1)
                    {
                        var data = new long[multiples];
                        for (int i = 0; i < multiples; i++)
                        {
                            data[i] = binaryReader.ReadInt64();
                        }
                        this.data = (object)data;
                    }
                    else
                    {
                        this.data = binaryReader.ReadInt64();
                    }
                    break;
                case "string":
                    this.data = binaryReader.ReadChars((int)size);
                    break;
                default:
                    // TODO: This should never happen but we should also throw an exception..
                    break;
            }
        }
    }
}