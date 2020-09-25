using System;
using System.Collections.Generic;

namespace FitReader
{
    internal class Message
    {
        internal ushort globalMsgNum;
        internal string name;
        internal object data;

        internal Message(ushort globalMsgNum, DefinitionRecord definition)
        {
            this.globalMsgNum = globalMsgNum;

            if (Messages.Names.ContainsKey(globalMsgNum))
            {
                this.name = Messages.Names[globalMsgNum];
                if (Fields._Fields.ContainsKey(this.globalMsgNum))
                {
                    var fields = Fields._Fields[this.globalMsgNum];
                    var data = new List<DataRecord>();
                    foreach (var dataRecord in definition.valid())
                    {
                        foreach (var dataField in dataRecord.valid())
                        {
                            if (fields.ContainsKey(dataRecord.globalMsgNum))
                            {
                                processValue(fields[dataRecord.globalMsgNum], dataField.Value.data);
                            }
                        }
                    }
                }
            }
        }

        private void processValue(Field field, object value)
        {
            if ((field.type.Length >= 4) && (field.type.Substring(0, 4).Equals("enum")))
            {
                if (Enums._Enums.ContainsKey(field.type))
                {
                    value = Enums._Enums[field.type][(ushort)value];
                }
            }
            else if (field.type.Equals("dateTime") || field.type.Equals("localDateTime"))
            {
                var a = 1;
            }
            else if (field.type.Equals("coordinates"))
            {
                value = (double)value * 180.0 / Math.Pow(2, 31);
            }
        }

        internal string Name { get => name; }
    }
}