using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Esri.ArcGISRuntime.Data;

namespace IdanAsraf.ArcGISRuntime.Geodabase.Editing
{
    public class FileGdbGenerator
    {
        private const string GdbDirSuffix = ".gdb";
        
        private readonly List<TableDef> _tableDefs;
        
        public string Path { get; }
        public IReadOnlyList<TableDef> TableDefs => _tableDefs;

        public static FileGdbGenerator Create(string path)
        {
            return new FileGdbGenerator(path);
        }
        
        public FileGdbGenerator(string path)
        {
            Path = path;
            _tableDefs = new List<TableDef>();
        }

        public FileGdbGenerator AddTable(TableDef tableDef)
        {
            _tableDefs.Add(tableDef);
            return this;
        }
        
        public async Task<Geodatabase> GenerateAsync()
        {
            if (Directory.Exists(Path))
            {
                // throw
            }

            if (!Path.EndsWith(GdbDirSuffix))
            {
                // throw
            }

            if (_tableDefs.Count == 0)
            {
                // throw
            }

            using (var editableGdb = Esri.FileGDB.Geodatabase.Create(Path))
            {
                foreach (var tableDef in TableDefs)
                {
                    editableGdb.CreateTable(tableDef);
                }
                
                editableGdb.Close();
            }
            
            return await Geodatabase.OpenAsync(Path);
        }
    }
}