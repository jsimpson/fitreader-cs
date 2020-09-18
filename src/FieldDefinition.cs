namespace FitReader
{
    internal class FieldDefinition
    {
        public byte fieldDefNum;
        public byte size;
        public byte endianness;
        public byte baseNum;

        public FieldDefinition(EndianBinaryReader binaryReader)
        {
            this.fieldDefNum = binaryReader.ReadUInt8();
            this.size = binaryReader.ReadUInt8();
            byte fieldByte = binaryReader.ReadUInt8();
            this.endianness = Bits.ReadBit(fieldByte, 7);
            this.baseNum = Bits.ReadBits(fieldByte, new int[2] { 4, 0 });
        }
    }
}