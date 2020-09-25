using System.Collections.Generic;

namespace FitReader
{
    internal class DataField
    {
        internal bool valid = false;
        internal dynamic data;
        public DataField(EndianBinaryReader binaryReader, Dictionary<string, byte> opts)
        {
            var baseNum = opts["baseNum"];
            var size = opts["size"];
            var arch = opts["arch"];
            var littleEndian = arch == 1;

            var baseType = DataTypes.Types[baseNum];
            var multiples = size / baseType.size;

            // TODO: These types should be an enum.
            switch (baseType.typeName)
            {
                case "enum":
                case "byte":
                case "uint8":
                case "uint8z":
                    if (multiples > 1)
                    {
                        var data = new byte[multiples];
                        for (int i = 0; i < multiples; i++)
                        {
                            data[i] = binaryReader.ReadUInt8();
                        }
                        this.data = data;
                    }
                    else
                    {
                        this.data = binaryReader.ReadUInt8();
                    }
                    break;
                case "sint8":
                    if (multiples > 1)
                    {
                        var data = new sbyte[multiples];
                        for (int i = 0; i < multiples; i++)
                        {
                            data[i] = binaryReader.ReadInt8();
                        }
                        this.data = data;
                    }
                    else
                    {
                        this.data = binaryReader.ReadInt8();
                    }
                    break;
                case "uint16":
                case "uint16z":
                    if (multiples > 1)
                    {
                        var data = new ushort[multiples];
                        for (int i = 0; i < multiples; i++)
                        {
                            data[i] = binaryReader.ReadUInt16(littleEndian);
                        }
                        this.data = data;
                    }
                    else
                    {
                        this.data = binaryReader.ReadUInt16(littleEndian);
                    }
                    break;
                case "sint16":
                    if (multiples > 1)
                    {
                        var data = new short[multiples];
                        for (int i = 0; i < multiples; i++)
                        {
                            data[i] = binaryReader.ReadInt16(littleEndian);
                        }
                        this.data = data;
                    }
                    else
                    {
                        this.data = binaryReader.ReadInt16(littleEndian);
                    }
                    break;
                case "uint32":
                case "uint32z":
                    if (multiples > 1)
                    {
                        var data = new uint[multiples];
                        for (int i = 0; i < multiples; i++)
                        {
                            data[i] = binaryReader.ReadUInt32(littleEndian);
                        }
                        this.data = data;
                    }
                    else
                    {
                        this.data = binaryReader.ReadUInt32(littleEndian);
                    }
                    break;
                case "sint32":
                    if (multiples > 1)
                    {
                        var data = new int[multiples];
                        for (int i = 0; i < multiples; i++)
                        {
                            data[i] = binaryReader.ReadInt32(littleEndian);
                        }
                        this.data = data;
                    }
                    else
                    {
                        this.data = binaryReader.ReadInt32(littleEndian);
                    }
                    break;
                case "float32":
                    if (multiples > 1)
                    {
                        var data = new float[multiples];
                        for (int i = 0; i < multiples; i++)
                        {
                            data[i] = binaryReader.ReadFloat(littleEndian);
                        }
                        this.data = data;
                    }
                    else
                    {
                        this.data = binaryReader.ReadFloat(littleEndian);
                    }
                    break;
                case "float64":
                    if (multiples > 1)
                    {
                        var data = new double[multiples];
                        for (int i = 0; i < multiples; i++)
                        {
                            data[i] = binaryReader.ReadFloat64(littleEndian);
                        }
                        this.data = data;
                    }
                    else
                    {
                        this.data = binaryReader.ReadFloat64(littleEndian);
                    }
                    break;
                case "uint64":
                case "uint64z":
                    if (multiples > 1)
                    {
                        var data = new ulong[multiples];
                        for (int i = 0; i < multiples; i++)
                        {
                            data[i] = binaryReader.ReadUInt64(littleEndian);
                        }
                        this.data = data;
                    }
                    else
                    {
                        this.data = binaryReader.ReadUInt64(littleEndian);
                    }
                    break;
                case "sint64":
                    if (multiples > 1)
                    {
                        var data = new long[multiples];
                        for (int i = 0; i < multiples; i++)
                        {
                            data[i] = binaryReader.ReadInt64(littleEndian);
                        }
                        this.data = data;
                    }
                    else
                    {
                        this.data = binaryReader.ReadInt64(littleEndian);
                    }
                    break;
                case "string":
                    this.data = binaryReader.ReadChars((int)size);
                    break;
                default:
                    // TODO: This should never happen but we should also throw an exception..
                    break;
            }

            this.valid = check(baseType.invalidValue);
        }

        private bool check(dynamic invalidValue)
        {
            var name = this.data.GetType().Name;
            if (name == "Byte[]" ||
                name == "SByte[]" ||
                name == "UInt16[]" ||
                name == "Int16[]" ||
                name == "UInt[]" ||
                name == "Int[]" ||
                name == "Single[]" ||
                name == "Double[]" ||
                name == "UInt32[]" ||
                name == "Int32[]" ||
                name == "Char[]"
            )
            {
                List<dynamic> valid = new List<dynamic>();
                foreach (var d in this.data)
                {
                    if (d != invalidValue)
                    {
                        // TODO: This sucks, fix it.
                        valid.Add(d);
                    }
                }

                return valid.Count > 0;
            }

            return this.data != invalidValue;
        }
    }
}