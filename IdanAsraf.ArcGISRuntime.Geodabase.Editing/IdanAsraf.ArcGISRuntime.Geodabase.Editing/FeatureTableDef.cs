using System.Collections.Generic;
using Esri.ArcGISRuntime.Data;

namespace IdanAsraf.ArcGISRuntime.Geodabase.Editing
{
    sealed class FeatureTableDef : TableDef
    {
        public Field ShapeField { get; }
        public GeometryDef GeometryDef { get; }

        public FeatureTableDef(string name, IEnumerable<Field> attributeFields, Field shapeField, GeometryDef geometryDef) 
            : base(name, attributeFields)
        {
            ShapeField = shapeField;
            GeometryDef = geometryDef;
        }
    }
}