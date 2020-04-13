using System.IO;

namespace DocRefList.Storage
{
    public class FileStorage
    {
        private const string STORAGE = "Storage";
        private readonly string _storage;
  
        public FileStorage(string path)
        {
            _storage = Directory.CreateDirectory(Path.Combine(path, STORAGE)).FullName;
        }

        public DocumentStorage this[int docId]
        {
            get
            {
                string path = Directory.CreateDirectory(Path.Combine(_storage, $"DOC{docId:000000000000}")).FullName;
                
                return new DocumentStorage(path);
            }
        }
    }
}