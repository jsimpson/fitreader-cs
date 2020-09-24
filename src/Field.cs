namespace FitReader
{
    internal class Field
    {
        public string name;
        public string type;
        public object scale;
        public object offset;

        public Field(string name, string type, object scale, object offset)
        {
            this.name = name;
            this.type = type;
            this.scale = scale;
            this.offset = offset;
        }
    }
}