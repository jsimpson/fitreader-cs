using System.Collections.Generic;

namespace FitReader
{
    internal class DevFieldDefinition
    {
        public byte fieldNum;
        public byte size;
        public byte devDataIndex;
        public object fieldDef;

        public DevFieldDefinition(EndianBinaryReader binaryReader)
        {
            this.fieldNum = binaryReader.ReadUInt8();
            this.size = binaryReader.ReadUInt8();
            this.devDataIndex = binaryReader.ReadUInt8();
        }
    }
}