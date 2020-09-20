namespace FitReader
{
    internal struct DataType
    {
        public bool endianAbility;
        public byte baseFieldtype;
        public string typeName;
        public object invalidValue;
        public byte size;

        public DataType(bool endianAbility, byte baseFieldtype, string typeName, object invalidValue, byte size)
        {
            this.endianAbility = endianAbility;
            this.baseFieldtype = baseFieldtype;
            this.typeName = typeName;
            this.invalidValue = invalidValue;
            this.size = size;
        }
    }
}