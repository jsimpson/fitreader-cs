using System.Collections.Generic;
using System.Linq;

namespace FitReader
{
    internal class DataRecord
    {
        internal ushort globalMsgNum;
        private List<Dictionary<byte, DataField>> fields;
        public DataRecord(EndianBinaryReader binaryReader, DefinitionRecord definitionRecord)
        {
            this.globalMsgNum = definitionRecord.GlobalMsgNum;
            this.fields = new List<Dictionary<byte, DataField>>();

            foreach (FieldDefinition fieldDefinition in definitionRecord.FieldDefinitions)
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

        internal List<KeyValuePair<byte, DataField>> valid()
        {
            var valid = new List<KeyValuePair<byte, DataField>>();

            foreach (var field in fields)
            {
                foreach (KeyValuePair<byte, DataField> dataField in field)
                {
                    if (dataField.Value.valid)
                    {
                        valid.Add(new KeyValuePair<byte, DataField>(dataField.Key, dataField.Value));
                    }
                }
            }

            return valid;
        }
    }
}