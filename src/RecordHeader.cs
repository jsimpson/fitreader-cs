namespace FitReader
{
    internal class RecordHeader
    {
        internal byte headerByte;
        internal byte headerType;
        internal byte messageType;
        internal byte messageTypeSpecific;
        internal byte reserved;
        internal byte localMessageType;
        internal byte timeOffset;

        public RecordHeader(EndianBinaryReader binaryReader)
        {
            this.headerByte = binaryReader.ReadUInt8();

            this.headerType = Bits.ReadBit(this.headerByte, 7);
            if (this.headerType == 0)
            {
                this.messageType = Bits.ReadBit(this.headerByte, 6);
                this.messageTypeSpecific = Bits.ReadBit(this.headerByte, 5);
                this.reserved = Bits.ReadBit(this.headerByte, 4);
                this.localMessageType = Bits.ReadBits(this.headerByte, new int[2] { 3, 0 });
            }
            else
            {
                this.localMessageType = Bits.ReadBits(this.headerByte, new int[2] { 6, 5 });
                this.timeOffset = Bits.ReadBits(this.headerByte, new int[2] { 4, 0 });
            }
        }

        public bool isDefinition()
        {
            return this.headerType == 0 && this.messageType == 1;
        }

        public bool isData()
        {
            return this.headerType == 0 && this.messageType == 0;
        }

        public bool hasDevFields()
        {
            return this.messageTypeSpecific == 1;
        }

        public bool hasTimestamp()
        {
            return this.headerType == 1;
        }
    }
}