using System.Collections.Generic;
using System.Linq;
using Esri.ArcGISRuntime.Data;

namespace IdanAsraf.ArcGISRuntime.Geodabase.Editing
{
    public static class GeodatabaseEditingExtensions
    {
        public static Field CreateGeometryField(string name, string alias = null)
        {
            return new Field(FieldType.Geometry, name, alias, -1, null, true, false);
        }
        
        internal static void CreateTable(this Esri.FileGDB.Geodatabase gdb, TableDef tableDef)
        {
            var path = $@"\{tableDef.Name}";

            var fieldDefs = tableDef.AttributeFields
                .Select(f => f.AsInternalFieldDef())
                .ToList();
            
            if (tableDef is FeatureTableDef featureTableDef)
            {
                var shapeField = new Esri.FileGDB.FieldDef
                {
                    Name = "SHAPE",
                    Alias = "Shape",
                    Type = Esri.FileGDB.FieldType.Geometry,
                    geometryDef = featureTableDef.GeometryDef.AsInternalGeometryDef()
                };

                fieldDefs.Add(shapeField);
            }

            using (var table = gdb.CreateTable(path, fieldDefs.ToArray(), string.Empty))
            {
                table.Close();
            }
        }
        
        public static FeatureTable CreateTable(this Geodatabase gdb, TableDef tableDef)
        {
            gdb.AsInternalGeodatabase().CreateTable(tableDef);
            return gdb.GeodatabaseFeatureTable(tableDef.Name);
        }
        
        public static FeatureTable CreateTable(this Geodatabase gdb, string name, IEnumerable<Field> attributeFields)
        {
            var tableDef = new TableDef(name, attributeFields);
            return gdb.CreateTable(tableDef);
        }
        
        public static FeatureTable CreateTable(this Geodatabase gdb, string name, IEnumerable<Field> attributeFields, Field shapeField, GeometryDef geometryDef)
        {
            var tableDef = new FeatureTableDef(name, attributeFields, shapeField, geometryDef);
            return gdb.CreateTable(tableDef);
        }
    }
}