using System;
using System.Collections.Generic;

namespace FitReader
{
    public class Fit
    {
        internal List<DefinitionRecord> finished = new List<DefinitionRecord>();
        internal Dictionary<ushort, DefinitionRecord> definitions = new Dictionary<ushort, DefinitionRecord>();
        internal FileHeader fileHeader { get; }
        internal Dictionary<string, List<Message>> messages { get; } = new Dictionary<string, List<Message>>();

        public Fit(EndianBinaryReader binaryReader)
        {
            this.fileHeader = new FileHeader(binaryReader);

            if (!validate(binaryReader))
            {
                throw new Exception();
            }

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
                    definitionRecord.dataRecords.Add(dataRecord);
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
                var messages = new List<Message>();
                string name = null;
                foreach (var definition in entry.Value)
                {
                    var message = new Message(definition);
                    if (message.Name != null)
                    {
                        if (name == null)
                        {
                            name = message.Name;
                        }

                        messages.Add(message);
                    }
                }

                if (name != null)
                {
                    this.messages[name] = messages;
                }
            }
        }

        private bool validate(EndianBinaryReader binaryReader)
        {
            if (this.fileHeader.size == 14)
            {
                binaryReader.Seek(0);

                var crc = Crc.Calculate(binaryReader, 0, 12);
                if (crc != this.fileHeader.crc)
                {
                    return false;
                }

                binaryReader.Seek(14);
            }

            var fileCrcPosition = this.fileHeader.size + (int)this.fileHeader.dataSize;
            binaryReader.Seek(fileCrcPosition);

            var fileCrc = binaryReader.ReadUInt8() + (binaryReader.ReadUInt8() << 8);

            var start = this.fileHeader.size == 12 ? 0 : this.fileHeader.size;
            binaryReader.Seek(start);

            if (fileCrc != Crc.Calculate(binaryReader, start, fileCrcPosition))
            {
                return false;
            }

            binaryReader.Seek(this.fileHeader.size);

            return true;
        }
    }
}