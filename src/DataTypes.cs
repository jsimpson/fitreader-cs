namespace FitReader
{
    internal static class DataTypes
    {
        private static DataType[] types = new DataType[] {
            new DataType(false, 0x00, "enum",    (byte)0xFF,                 1),
            new DataType(false, 0x01, "sint8",   (sbyte)0x7F,                1),
            new DataType(false, 0x02, "uint8",   (byte)0xFF,                 1),
            new DataType(true,  0x83, "sint16",  (short)0x7FFF,              2),
            new DataType(true,  0x84, "uint16",  (ushort)0xFFFF,             2),
            new DataType(true,  0x85, "sint32",  (int)0x7FFFFFFF,            4),
            new DataType(true,  0x86, "uint32",  (uint)0xFFFFFFFF,           4),
            new DataType(false, 0x07, "string",  (byte)0x00,                 1),
            new DataType(true,  0x88, "float32", (float)0xFFFFFFFF,          4),
            new DataType(true,  0x89, "float64", (double)0xFFFFFFFFFFFFFFFF, 8),
            new DataType(false, 0x0A, "uint8z",  (byte)0x00,                 1),
            new DataType(true,  0x8B, "uint16z", (ushort)0x0000,             2),
            new DataType(true,  0x8C, "uint32z", (uint)0x00000000,           4),
            new DataType(false, 0x0D, "byte",    (byte)0xFF,                 1),
            new DataType(true,  0x8D, "sint64",  (long)0x7FFFFFFFFFFFFFFF,   8),
            new DataType(true,  0x8F, "uint64",  (ulong)0xFFFFFFFFFFFFFFFF,  8),
            new DataType(true,  0x90, "uint64z", (ulong)0x0000000000000000,  8)
      };

      internal static DataType[] Types { get => types; }
    }
}
