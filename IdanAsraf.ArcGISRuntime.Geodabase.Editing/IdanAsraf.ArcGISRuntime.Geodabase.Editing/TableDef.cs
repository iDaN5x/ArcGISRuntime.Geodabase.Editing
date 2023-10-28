using System.Collections.Generic;
using Esri.ArcGISRuntime.Data;

namespace IdanAsraf.ArcGISRuntime.Geodabase.Editing
{
    public class TableDef
    {
        public string Name { get; }
        public IEnumerable<Field> AttributeFields { get; }

        public TableDef(string name, IEnumerable<Field> attributeFields)
        {
            Name = name;
            AttributeFields = attributeFields;
        }
    }
}