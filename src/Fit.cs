namespace FitReader
{
    public class Fit
    {
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
                }
            }
        }
    }
}