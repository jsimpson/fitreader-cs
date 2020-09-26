using System;
using System.Collections.Generic;

namespace FitReader
{
    internal class Message
    {
        internal ushort globalMsgNum;
        internal string name;
        internal Dictionary<string, dynamic> data = new Dictionary<string, dynamic>();

        internal Message(DefinitionRecord definitionRecord)
        {
            this.globalMsgNum = definitionRecord.GlobalMsgNum;

            if (Messages.Names.ContainsKey(this.globalMsgNum))
            {
                this.name = Messages.Names[this.globalMsgNum];
                if (Fields._Fields.ContainsKey(this.globalMsgNum))
                {
                    var fields = Fields._Fields[this.globalMsgNum];
                    foreach (var dataRecord in definitionRecord.valid())
                    {
                        foreach (var dataField in dataRecord.valid())
                        {
                            if (fields.ContainsKey(dataField.Key))
                            {
                                var field = fields[dataField.Key];
                                var data = processValue(field, dataField.Value.data);
                                this.data[field.name] = data;
                            }
                            else
                            {
                                var a = 1;
                            }
                        }
                    }
                }
            }
        }

        private dynamic processValue(Field field, dynamic value)
        {
            if ((field.baseType.Length >= 4) && (field.baseType.Substring(0, 4).Equals("enum")))
            {
                if (Enums._Enums.ContainsKey(field.baseType))
                {
                    if (Enums._Enums[field.baseType].ContainsKey(value))
                    {
                        value = Enums._Enums[field.baseType][value];
                    }
                }
            }
            else if (field.baseType.Equals("dateTime") || field.baseType.Equals("localDateTime"))
            {
                var a = 1;
            }
            else if (field.baseType.Equals("coordinates"))
            {
                value = (double)value * 180.0 / Math.Pow(2, 31);
            }

            var isArray = Utils.IsArray.Check(value.GetType().Name);
            if (field.scale != 0)
            {
                if (isArray)
                {
                }
                else
                {
                    value = (value * 1.0) / field.scale;
                }
            }

            if (field.offset != 0)
            {
                if (isArray)
                {
                }
                else
                {
                    value = value - field.offset;
                }
            }

            return value;
        }

        internal string Name { get => name; }
    }
}