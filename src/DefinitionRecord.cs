namespace FitReader
{
    internal class DefinitionRecord
    {
        private int localNum;
        private byte reserved;
        private byte architecture;
        private ushort globalMsgNum;
        private FieldDefinition[] fieldDefinitions;

        public DefinitionRecord(EndianBinaryReader binaryReader, int localNum)
        {
            this.localNum = localNum;
            this.reserved = binaryReader.ReadUInt8();
            this.architecture = binaryReader.ReadUInt8();
            bool littleEndian = this.architecture == 0;
            this.globalMsgNum = binaryReader.ReadUInt16(littleEndian);
            byte numFields = binaryReader.ReadUInt8();

            this.fieldDefinitions = new FieldDefinition[numFields];
            for (int i = 0; i < numFields; i++)
            {
                this.fieldDefinitions[i] = new FieldDefinition(binaryReader);
            }
        }
    }
}