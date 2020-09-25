namespace FitReader
{
    internal class Field
    {
        public string name;
        public string baseType;
        public dynamic scale;
        public dynamic offset;

        public Field(string name, string baseType, object scale, object offset)
        {
            this.name = name;
            this.baseType = baseType;
            this.scale = scale;
            this.offset = offset;
        }
    }
}