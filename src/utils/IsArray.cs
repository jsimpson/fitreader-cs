namespace FitReader
{
    namespace Utils
    {
        internal static class IsArray
        {
            internal static bool Check(string typeName)
            {
                if (typeName == "Byte[]" ||
                    typeName == "SByte[]" ||
                    typeName == "UInt16[]" ||
                    typeName == "Int16[]" ||
                    typeName == "UInt[]" ||
                    typeName == "Int[]" ||
                    typeName == "Single[]" ||
                    typeName == "Double[]" ||
                    typeName == "UInt32[]" ||
                    typeName == "Int32[]" ||
                    typeName == "Char[]"
                )
                {
                    return true;
                }

                return false;
            }
        }
    }
}