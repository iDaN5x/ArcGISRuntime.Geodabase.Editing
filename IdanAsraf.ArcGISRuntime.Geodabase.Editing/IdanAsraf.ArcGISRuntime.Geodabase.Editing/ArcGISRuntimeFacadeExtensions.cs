using System;
using Esri.ArcGISRuntime.Data;
using Esri.ArcGISRuntime.Geometry;

namespace IdanAsraf.ArcGISRuntime.Geodabase.Editing
{
    public static class ArcGISRuntimeFacadeExtensions
    {
        internal static Esri.FileGDB.Geodatabase AsInternalGeodatabase(this Geodatabase gdb)
        {
            // Ensure file geodatabase.
            var isFileGdb = gdb.Source?.IsFile ?? true;
            
            if (!isFileGdb)
            {
                throw new NotSupportedException();
            }

            // Open and return.
            return Esri.FileGDB.Geodatabase.Open(gdb.Path);
        }

        internal static Esri.FileGDB.FieldType AsInternalFieldType(this FieldType fieldType)
        {
            switch (fieldType)
            {
                case FieldType.Int16:
                    return Esri.FileGDB.FieldType.SmallInteger;
                case FieldType.Int32:
                    return Esri.FileGDB.FieldType.Integer;
                case FieldType.Guid:
                    return Esri.FileGDB.FieldType.GUID;
                case FieldType.Float32:
                    return Esri.FileGDB.FieldType.Single;
                case FieldType.Float64:
                    return Esri.FileGDB.FieldType.Double;
                case FieldType.Date:
                    return Esri.FileGDB.FieldType.Date;
                case FieldType.Text:
                    return Esri.FileGDB.FieldType.String;
                case FieldType.OID:
                    return Esri.FileGDB.FieldType.OID;
                case FieldType.GlobalID:
                    return Esri.FileGDB.FieldType.GlobalID;
                case FieldType.Blob:
                    return Esri.FileGDB.FieldType.Blob;
                case FieldType.Geometry:
                    return Esri.FileGDB.FieldType.Geometry;
                case FieldType.Raster:
                    return Esri.FileGDB.FieldType.Raster;
                case FieldType.Xml:
                    return Esri.FileGDB.FieldType.XML;
                case FieldType.Unknown:
                default:
                    throw new ArgumentOutOfRangeException(nameof(fieldType), fieldType, null);
            }
        }
        
        internal static Esri.FileGDB.FieldDef AsInternalFieldDef(this Field field)
        {
            return new Esri.FileGDB.FieldDef
            {
                Name = field.Name,
                Alias = field.Alias,
                Type = field.FieldType.AsInternalFieldType(),
                Length = field.Length,
                IsNullable = field.IsNullable
            };
        }

        internal static Esri.FileGDB.SpatialReference AsInternalSpatialReference(this SpatialReference sr)
        {
            return new Esri.FileGDB.SpatialReference
            {
                spatialReferenceID = sr.Wkid,
                spatialReferenceText = sr.WkText
            };
        }
        
        internal static Esri.FileGDB.GeometryType AsInternalGeometryType(this GeometryType geometryType)
        {
            switch (geometryType)
            {
                case GeometryType.Point:
                    return Esri.FileGDB.GeometryType.Point;
                case GeometryType.Polyline:
                    return Esri.FileGDB.GeometryType.Polyline;
                case GeometryType.Polygon:
                    return Esri.FileGDB.GeometryType.Polygon;
                case GeometryType.Multipoint:
                    return Esri.FileGDB.GeometryType.Multipoint;
                case GeometryType.Envelope:
                case GeometryType.Unknown:
                default:
                    throw new ArgumentOutOfRangeException(nameof(geometryType), geometryType, null);
            };
        }
    }
}