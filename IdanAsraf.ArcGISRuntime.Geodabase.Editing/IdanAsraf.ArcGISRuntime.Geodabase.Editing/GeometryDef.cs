using Esri.ArcGISRuntime.Geometry;

namespace IdanAsraf.ArcGISRuntime.Geodabase.Editing
{
    public class GeometryDef
    {
        public SpatialReference SpatialReference { get; }
        public GeometryType GeometryType { get; }
        public bool HasM { get; set; } = false;
        public bool HasZ { get; set; } = false;

        public GeometryDef(SpatialReference spatialReference, GeometryType geometryType)
        {
            SpatialReference = spatialReference;
            GeometryType = geometryType;
        }

        internal Esri.FileGDB.GeometryDef AsInternalGeometryDef()
        {
            return new Esri.FileGDB.GeometryDef
            {
                geometryType = GeometryType.AsInternalGeometryType(),
                HasM = HasM,
                HasZ = HasZ,
                spatialReference = SpatialReference.AsInternalSpatialReference()
            };
        }
    }
}