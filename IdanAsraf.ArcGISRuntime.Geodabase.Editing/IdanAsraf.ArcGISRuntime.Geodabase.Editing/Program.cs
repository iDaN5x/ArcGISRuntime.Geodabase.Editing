using System.Collections.Generic;
using System.Threading.Tasks;
using Esri.ArcGISRuntime.Data;
using Esri.ArcGISRuntime.Geometry;
using static IdanAsraf.ArcGISRuntime.Geodabase.Editing.GeodatabaseEditingExtensions;

namespace IdanAsraf.ArcGISRuntime.Geodabase.Editing
{
    internal class Program
    {
        private const string GDB_PATH = @"C:\temp\test.gdb";
        private const string MY_LAYER_NAME = "MyLayer";

        private static TableDef CreateMyLayerTableDef()
        {
            var attributeFields = new[]
            {
                Field.CreateString("FirstName", "First Name", 255),
                Field.CreateString("LastName", "Last Name", 255),
                Field.CreateShort("Age")
            };
            
            var shapeField = CreateGeometryField("SHAPE", "Shape");
            
            var geometryDef = new GeometryDef(
                SpatialReferences.Wgs84, GeometryType.Point);

            return new FeatureTableDef(
                MY_LAYER_NAME, attributeFields, shapeField, geometryDef);
        }

        private static Feature CreateMyFeature(FeatureTable myTable)
        {
            var attributes = new Dictionary<string, object>
            {
                ["FirstName"] = "Idan",
                ["LastName"] = "Asraf",
                ["Age"] = 28
            };

            var geometry = new MapPoint(5.0, 5.0);
            
            return myTable.CreateFeature(attributes, geometry);
        }
        
        public static async Task Main(string[] args)
        {
            var gdb = await FileGdbGenerator.Create(GDB_PATH)
                .AddTable(CreateMyLayerTableDef())
                .GenerateAsync();

            var myLayer = gdb.GeodatabaseFeatureTable(MY_LAYER_NAME);

            var feature = CreateMyFeature(myLayer);

            await myLayer.AddFeatureAsync(feature);
        }
    }
}