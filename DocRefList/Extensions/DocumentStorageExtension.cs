using System.Collections.Generic;
using DocRefList.Storage;
using Microsoft.AspNetCore.Http;

namespace DocRefList.Extensions
{
    public static class DocumentStorageExtension
    {
        public static void AddFormFile(this DocumentStorage storage, IFormFile file)
        {
            FileItem item = new FileItem
            {
                FileName = file.FileName,
                MimeType = file.ContentType
            };

            file.CopyTo(item.Stream);

            storage.AddFile(item);
        }

        public static void AddFormFiles(this DocumentStorage storage, List<IFormFile> files)
        {
            if (files == null) return;
            
            foreach (IFormFile file in files)
            {
                storage.AddFormFile(file);
            }
        }
    }
}
