using System.Collections.Generic;
using System.Linq;

namespace FitReader
{
    internal class DataRecord
    {
        internal ushort globalMsgNum;
        private List<Dictionary<byte, DataField>> fields;
        private List<Dictionary<byte, DataField>> devFields;
        public DataRecord(EndianBinaryReader binaryReader, DefinitionRecord definitionRecord)
        {
            this.globalMsgNum = definitionRecord.GlobalMsgNum;
            this.fields = new List<Dictionary<byte, DataField>>();

            foreach(FieldDefinition fieldDefinition in definitionRecord.FieldDefinitions)
            {
                var opts = new Dictionary<string, byte>()
                {
                    { "baseNum", fieldDefinition.baseNum },
                    { "size", fieldDefinition.size },
                    { "arch", fieldDefinition.endianness }
                };

                this.fields.Add(new Dictionary<byte, DataField>(){
                    { fieldDefinition.fieldDefNum, new DataField(binaryReader, opts) }
                });
            }
        }

        internal List<Dictionary<byte, DataField>> valid()
        {
            var valid = new List<Dictionary<byte, DataField>>();

            foreach(var field in fields)
            {
                if (field[1].valid)
                {
                   valid.Add(field) ;
                }
            }
            
            return valid;
        }
    }
}