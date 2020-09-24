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
                            processValue(fields[dataRecord.globalMsgNum], dataField[1]);
                        }
                    }
                }
            }
        }

        private void processValue(Field field, DataField dataField)
        {
            if (field.type.Substring(0, 4).Equals("enum"))
            {
                var value = dataField.data;
            }
        }

        internal string Name { get => name; }
    }
}