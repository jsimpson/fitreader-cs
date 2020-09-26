using System.Collections.Generic;

namespace FitReader
{
    internal class DefinitionRecord
    {
        private int localNum;
        private byte reserved;
        private byte architecture;
        private ushort globalMsgNum;
        private FieldDefinition[] fieldDefinitions;
        public List<DataRecord> dataRecords = new List<DataRecord>();

        public DefinitionRecord(EndianBinaryReader binaryReader, int localNum)
        {
            this.localNum = localNum;
            this.reserved = binaryReader.ReadUInt8();
            this.architecture = binaryReader.ReadUInt8();
            bool littleEndian = this.architecture == 0;
            this.GlobalMsgNum = binaryReader.ReadUInt16(littleEndian);
            byte numFields = binaryReader.ReadUInt8();

            this.fieldDefinitions = new FieldDefinition[numFields];
            for (int i = 0; i < numFields; i++)
            {
                this.fieldDefinitions[i] = new FieldDefinition(binaryReader);
            }
        }

        internal bool isLittleEndian()
        {
            return this.architecture == 0;
        }

        internal List<DataRecord> valid()
        {
            var valid = new List<DataRecord>();

            if (Fields._Fields.ContainsKey(this.globalMsgNum))
            {
                var fields = Fields._Fields[this.globalMsgNum];
                foreach (var dataRecord in this.dataRecords)
                {
                    foreach (var dataField in dataRecord.valid())
                    {
                        if (fields.ContainsKey(dataField.Key))
                        {
                            valid.Add(dataRecord);
                        }
                        else
                        {
                            var a = 1;
                        }
                    }
                }
            }
            else
            {
                var a = 1;
            }

            return valid;
        }

        internal ushort GlobalMsgNum { get => globalMsgNum; set => globalMsgNum = value; }
        internal FieldDefinition[] FieldDefinitions { get => fieldDefinitions; }
    }
}