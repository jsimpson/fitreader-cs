namespace FitReader
{
    internal class FileHeader
    {
        internal byte size { get; }
        internal byte protocolVersion { get; }
        internal ushort profileVersion { get; }
        internal uint dataSize { get; }
        internal char[] dataType { get; }
        internal ushort crc { get; }

        internal FileHeader(EndianBinaryReader binaryReader)
        {
            this.size = binaryReader.ReadUInt8();
            this.protocolVersion = binaryReader.ReadUInt8();

            this.profileVersion = binaryReader.ReadUInt16(true);
            this.dataSize = binaryReader.ReadUInt32(true);

            this.dataType = binaryReader.ReadChars(4);

            // 12-byte file headers will not have a header CRC.
            if (this.size == 14)
            {
                this.crc = binaryReader.ReadUInt16(true);
            }
        }
    }
}