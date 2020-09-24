using System;    
using System.Collections.Generic;    
using System.Linq;   

namespace FitReader
{
    public class Fit
    {
        internal List<DefinitionRecord> finished = new List<DefinitionRecord>();
        internal Dictionary<ushort, DefinitionRecord> definitions = new Dictionary<ushort, DefinitionRecord>();
        internal FileHeader fileHeader { get; }
        internal List<Message> messages { get; } = new List<Message>();

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

                    // 206 is developer field definitions, skip processing
                    if (definitionRecord.GlobalMsgNum != 206)
                    {
                        definitionRecord.dataRecords.Add(dataRecord);
                    }
                }
            }

            foreach (var definition in definitions)
            {
                finished.Add(definition.Value);
            }

            var grouped = new SortedDictionary<ushort, List<DefinitionRecord>>();
            foreach (DefinitionRecord definition in finished)
            {
                if (!grouped.ContainsKey(definition.GlobalMsgNum))
                {
                    var list = new List<DefinitionRecord>();
                    list.Add(definition);
                    grouped.Add(definition.GlobalMsgNum, list);
                }
                else
                {
                    grouped[definition.GlobalMsgNum].Add(definition);
                }
            }

            foreach (KeyValuePair<ushort, List<DefinitionRecord>> entry in grouped)
            {
                foreach (var definition in entry.Value)
                {
                    var message = new Message(entry.Key, definition);
                    if (message.Name != null)
                    {
                        this.messages.Add(message);
                    }
                }
            }
        }
    }
}