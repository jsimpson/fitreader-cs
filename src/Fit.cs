using System.Collections.Generic;

namespace FitReader
{
    public class Fit
    {
        internal List<DefinitionRecord> finished = new List<DefinitionRecord>();
        internal Dictionary<long, DefinitionRecord> definitions = new Dictionary<long, DefinitionRecord>();
        internal FileHeader fileHeader { get; }

        public Fit(EndianBinaryReader binaryReader)
        {
            this.fileHeader = new FileHeader(binaryReader);

            while (binaryReader.Position < (this.fileHeader.dataSize + this.fileHeader.size))
            {
                RecordHeader recordHeader = new RecordHeader(binaryReader);
                if (recordHeader.isDefinition())
                {
                    DefinitionRecord definitionRecord = new DefinitionRecord(binaryReader, recordHeader.localMessageType);
                    if (definitions.ContainsKey(recordHeader.localMessageType))
                    {
                        finished.Add(definitionRecord);
                    }

                    definitions[recordHeader.localMessageType] = definitionRecord;
                }
                else if (recordHeader.isData())
                {
                    DefinitionRecord definitionRecord = definitions[recordHeader.localMessageType];
                    DataRecord dataRecord = new DataRecord(binaryReader, definitionRecord);

                    if (definitionRecord.GlobalMsgNum != 206)
                    {
                        definitionRecord.dataRecords.Add(dataRecord);
                    }
                    else
                    {
                        // dev field
                    }
                }
                else
                {
                    // timestamp or...?
                }
            }
        }
    }
}