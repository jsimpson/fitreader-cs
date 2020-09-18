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

        internal bool valid;
        internal object data;
        public DataField(EndianBinaryReader binaryReader, Dictionary<string, long> opts)
        {
            var baseNum = opts["baseNum"];
            var size = opts["size"];
            var arch = opts["arch"];
            var littleEndian = arch == 1;
        }
    }
}